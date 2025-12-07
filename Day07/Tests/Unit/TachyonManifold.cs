using System.Diagnostics;

namespace AdventOfCode2025.Tests.Unit
{
    public class TachyonManifold
    {
        [Fact]
        public void SampleData_Map_Parses()
        {
            var lines = FileHelper.ReadSampleData("Day_07");

            var manifold = Framework.TachyonManifold.CreateManifold(lines);

            int splitterCount = manifold.Nodes.Count(n => n is Framework.TachyonManifold.Splitter);
            Assert.True(splitterCount == 22, $"Expected 22 splitters, found {splitterCount}");
        }


        [Fact]
        public void SampleData_Part1_Passes()
        {
            var lines = FileHelper.ReadSampleData("Day_07");

            var manifold = Framework.TachyonManifold.CreateManifold(lines);

            while (manifold.Progress()) 
            {
                Debug.WriteLine($"Split Count: {manifold.SplitCount}");
                Debug.WriteLine("");
            }
            Debug.WriteLine(manifold.ToString());
            Assert.Equal(21, manifold.SplitCount);
        }

        [Fact]
        public void SampleData_Part2_Passes()
        {
            var lines = FileHelper.ReadSampleData("Day_07");

            var manifold = Framework.TachyonManifold.CreateManifold(lines);

            while (manifold.Progress())
            {
                Debug.WriteLine($"Split Count: {manifold.SplitCount}");
                Debug.WriteLine("");
            }
            Debug.WriteLine(manifold.ToString());
            Assert.Equal(21, manifold.SplitCount);
            Assert.Equal(40, manifold.CalculateAllTimelines());
        }

        [Fact]
        public void PuzzleData_Part1_Passes()
        {
            var lines = FileHelper.ReadSampleData("Day_07_Puzzle");

            var manifold = Framework.TachyonManifold.CreateManifold(lines);

            while (manifold.Progress())
            {
                Debug.WriteLine($"Split Count: {manifold.SplitCount}");
                Debug.WriteLine("");
            }
            Debug.WriteLine(manifold.ToString());
            Assert.Equal(1566, manifold.SplitCount);
        }

        [Fact]
        public void PuzzleData_Part2_Passes()
        {
            var lines = FileHelper.ReadSampleData("Day_07_Puzzle");

            var manifold = Framework.TachyonManifold.CreateManifold(lines);

            while (manifold.Progress())
            {
                Debug.WriteLine($"Split Count: {manifold.SplitCount}");
                Debug.WriteLine("");
            }

            Assert.Equal(5_921_061_943_075, manifold.CalculateAllTimelines());
        }
    }
}
