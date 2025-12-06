namespace AdventOfCode2025.Framework
{
    public class WrappingPaperWarehouse
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private List<Container> _containers = new List<Container>();
        public IEnumerable<Container> Containers => _containers;

        public int MarkedContainers => _containers.Count(c => c.MarkedForRemoval && !c.Removed);
        public int RemovedContainerCount => _containers.Count(c=>c.Removed);

        public WrappingPaperWarehouse(List<string> lines)
        {
            Width = lines.Max(l => l.Length);
            Height = lines.Count;

            _containers.AddRange(InventoryCheck(lines));

            _containers.ForEach(c => c.CalculateNeighbors(_containers));
        }

        public void MarkForRemoval()
        {
            _containers.ForEach(c => c.MarkSelfForRemoval());
        }

        public bool RemoveContainers()
        {
            return _containers.Count(c => c.RemoveSelf()) > 0;
        }

        public void RefreshContainers()
        {
            _containers.ForEach(c => { c.MarkedForRemoval = false; c.Removed = false; });
        }

        public static IEnumerable<Container> InventoryCheck(List<string> lines) {}

        public static IEnumerable<Container> InventoryLineCheck(string line, int indexY) {}

        public static int RemoveContainers(List<string> lines) {}

        public class Container()
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool MarkedForRemoval { get; set; }
            public bool Removed { get; set; }
            public List<Container> Neighbors { get; set; } = new List<Container>();

            public void CalculateNeighbors(IEnumerable<Container> allContainers)
            {
                var filtered = allContainers.Where(c => Math.Abs(c.X - X) <= 1 && Math.Abs(c.Y - Y) <= 1);
                Neighbors.AddRange(filtered.Where(c => c != this));
            }

            public bool MarkSelfForRemoval()
            {
                if (Neighbors.Count(n => !n.Removed) < 4)
                {
                    MarkedForRemoval = true;
                    return true;
                }

                return false;
            }

            public bool RemoveSelf()
            {
                if (!MarkedForRemoval || Removed)
                {
                    return false;
                }

                Removed = true;
                return true;
            }
        }
    }
}
