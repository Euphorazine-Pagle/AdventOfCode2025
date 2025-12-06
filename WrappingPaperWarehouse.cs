namespace AdventOfCode2025.Framework
{
    public static class WrappingPaperWarehouse
    {
        public static int CountMovableContainers(List<string> lines)
        {
            var containers = InventoryCheck(lines).ToList();
            containers.ForEach(c => c.CalculateNeighbors(containers));

            return containers.Count(c => c.Neighbors.Count <= 3);
        }

        public static IEnumerable<Container> InventoryCheck(List<string> lines)
        {
            List<Container> result = lines.Select((line, y) => new { line, y })
                .Aggregate(new List<Container>(), (acc, line) =>
                {
                    acc.AddRange(InventoryLineCheck(line.line, line.y));
                    return acc;
                });

            return result;
        }

        public static IEnumerable<Container> InventoryLineCheck(string line, int indexY)
        {
            return line.Select((character, x) => new { character, x })
                    .Aggregate(new List<Container>(), (acc, character) =>
                    {
                        if (character.character == '@')
                        {
                            acc.Add(new Container() { X = character.x, Y = indexY });
                        }
                        return acc;
                    });
        }

        public static int RemoveContainers(List<string> lines)
        {
            var containers = InventoryCheck(lines).ToList();
            int startingInvCount = containers.Count();

            containers.ForEach(c => c.CalculateNeighbors(containers));

            int removed = -1;
            while (removed != 0)
            {
                var contCopy = containers.ToList();
                removed = contCopy.Sum(c => c.RemoveSelf(containers));
            }

            return startingInvCount - containers.Count();
        }

        public class Container()
        {
            public int X { get; set; }
            public int Y { get; set; }
            public List<Container> Neighbors { get; set; } = new List<Container>();

            public void CalculateNeighbors(IEnumerable<Container> allContainers)
            {
                var filtered = allContainers.Where(c => Math.Abs(c.X - X) <= 1 && Math.Abs(c.Y - Y) <= 1);
                Neighbors.AddRange(filtered.Where(c => c != this));
            }

            public void RemoveNeighbor(Container container)
            {
                if (Neighbors.Contains(container))
                {
                    Neighbors.Remove(container);
                }
            }

            public int RemoveSelf(List<Container> containers)
            {
                if (Neighbors.Count >= 4)
                {
                    return 0;
                }

                foreach (var container in Neighbors)
                {
                    container.RemoveNeighbor(this);
                }

                containers.Remove(this);

                return 1;
            }
        }
    }
}
