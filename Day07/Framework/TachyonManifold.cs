using System.Diagnostics;
using System.Text;

namespace AdventOfCode2025.Framework
{
    public class TachyonManifold
    {
        private List<Node> _startState = new List<Node>();
        private List<Node> _nodes = new List<Node>();
        public IEnumerable<Node> Nodes => _nodes;

        public int Width { get; private set; }
        public int Height { get; private set; }

        private int _scanLines = 0;
        public int SplitCount { get; private set; } = 0;

        private TachyonManifold(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static TachyonManifold CreateManifold(List<string> lines)
        {
            var result = new TachyonManifold(lines.Max(l => l.Length), lines.Count);
            result._startState.AddRange(ParseMap(lines));
            result._nodes.AddRange(result._startState.Select(n => Node.Create(n.X, n.Y, n.Symbol)));

            return result;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Height; i++)
            {
                var nodes = _nodes.Where(n => n.Y == i);
                foreach (var item in nodes.OrderBy(n => n.X))
                {
                    sb.Append(item.Symbol);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public bool Progress()
        {
            if (_scanLines + 1 >= Height)
            {
                return false;
            }

            var thisLine = _nodes.Where(n => n.Y == _scanLines).ToList();
            foreach (var node in thisLine)
            {
                if (node is Entrance || node is Beam)
                {
                    var neighbor = _nodes.First(n => n.X == node.X && n.Y == node.Y + 1);
                    ProcessPathing(neighbor, node);
                }
            }

            _scanLines++;
            return true;
        }

        public long CalculateTimeline()
        {
            var finalBeam = (Beam?)_nodes.FirstOrDefault(n => n.Y == Height - 1 && !n.Processed && n is Beam);

            if (finalBeam is null)
            {
                return 0;
            }

            long timeLines = finalBeam.GetEntrances();
            System.Diagnostics.Debug.WriteLine($"Beam X: {finalBeam.X}, Y: {finalBeam.Y}, Timelines: {timeLines}");

            finalBeam.Processed = true;

            return timeLines;
        }

        public long CalculateAllTimelines()
        {
            var finalBeams = _nodes.Where(n => n.Y == Height - 1 && n is Beam).Select(n => (Beam)n).ToList();

            long timeLines = 0;
            foreach (var beam in finalBeams)
            {
                var beamParents = beam.GetEntrances();
                System.Diagnostics.Debug.WriteLine($"Beam X: {beam.X}, Y: {beam.Y}, Timelines: {beamParents}");
                timeLines += beamParents;
            }

            Debug.WriteLine($"Biggest Splitter: {_nodes.Max(n => n.GetEntrances())}");

            return timeLines;
        }

        private void ProcessPathing(Node path, Node parent)
        {
            if (path is Empty e)
            {
                var beam = new Beam() { X = e.X, Y = e.Y };
                beam.Parents.Add(parent);
                _nodes.Remove(e);
                _nodes.Add(beam);

                return;
            }

            if (path is Beam b)
            {
                b.Parents.Add(parent);
            }

            if (path is Splitter s)
            {
                s.Parents.Add(parent);

                var leftNeighbor = _nodes.FirstOrDefault(n => n.X == s.X - 1 && n.Y == s.Y);
                var rightNeighbor = _nodes.FirstOrDefault(n => n.X == s.X + 1 && n.Y == s.Y);

                if (leftNeighbor is not null)
                {
                    ProcessPathing(leftNeighbor, path);
                }

                if (rightNeighbor is not null)
                {
                    ProcessPathing(rightNeighbor, path);
                }

                SplitCount++;
            }
        }


        public void Untouch()
        {
            _nodes.ForEach(n => n.Untouch());
        }

        private static List<Node> ParseMap(List<string> lines)
        {
            var result = new List<Node>();
            foreach (var line in lines.Select((l, index) => (row: l, y: index)))
            {
                foreach (var column in line.row.Select((c, index) => (column: c, x: index)))
                {
                    result.Add(Node.Create(column.x, line.y, column.column));
                }
            }

            return result;
        }

        public void Reset()
        {
            _nodes.Clear();
            _nodes.AddRange(_startState.Select(n => Node.Create(n.X, n.Y, n.Symbol)));
            _scanLines = 0;
            SplitCount = 0;
        }

        public abstract class Node
        {
            public int X { get; internal set; }
            public int Y { get; internal set; }
            public List<Node> Parents { get; set; } = new List<Node>();
            public abstract char Symbol { get; }
            public bool Touched { get; set; }
            public bool Processed { get; set; }

            private long _entrances = 0;

            public long GetEntrances()
            {
                Touched = true;
                if (_entrances > 0)
                {
                    return _entrances;
                }

                if (this is Entrance)
                {
                    return 1;
                }

                foreach(var parent in Parents)
                {
                    _entrances += parent.GetEntrances();
                }

                return _entrances;
            }

            public void Untouch()
            {
                Touched = false;
                _entrances = 0;
            }

            public static Node Create(int x, int y, char symbol)
            {
                return symbol switch
                {
                    'S' => new Entrance { X = x, Y = y },
                    '^' => new Splitter { X = x, Y = y },
                    '|' => new Beam { X = x, Y = y },
                    _ => new Empty { X = x, Y = y },
                };
            }
        }

        public class Entrance : Node
        {
            public override char Symbol => 'S';
        }

        public class Splitter : Node
        {
            public override char Symbol => '^';
        }

        public class Beam : Node
        {
            public override char Symbol => '|';
        }

        public class Empty : Node
        {
            public override char Symbol => '.';
        }
    }
}
