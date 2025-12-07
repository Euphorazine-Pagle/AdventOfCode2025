using System.Data;

namespace AdventOfCode2025.WinForms
{
    public partial class TachyonManifoldUserControl : UserControl
    {
        private readonly Framework.TachyonManifold _manifold;

        private const int itemSizeInPixels = 5;

        public TachyonManifoldUserControl(Framework.TachyonManifold manifold)
        {
            InitializeComponent();

            _manifold = manifold;
            this.Size = new System.Drawing.Size(manifold.Width * itemSizeInPixels, (manifold.Height * itemSizeInPixels) + dataPanel.Height);
            this.BackColor = System.Drawing.Color.DarkGreen;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (_manifold == null)
            {
                return;
            }

            Graphics g = e.Graphics;

            g.Clear(Color.DarkGreen);

            using (Brush redBrush = new SolidBrush(Color.Coral))
            {
                foreach (Framework.TachyonManifold.Node node in _manifold.Nodes.Where(c => c is Framework.TachyonManifold.Splitter && !c.Touched))
                {
                    int left = node.X * itemSizeInPixels;
                    int right = (node.X + 1) * itemSizeInPixels;

                    g.FillPolygon(redBrush,
                        new PointF(left, (node.Y + 1) * itemSizeInPixels),
                        new PointF(right, (node.Y + 1) * itemSizeInPixels),
                        new PointF((left + right) / 2.0f, node.Y * itemSizeInPixels));
                }
            }

            using (Brush redBrush = new SolidBrush(Color.LightBlue))
            {
                foreach (Framework.TachyonManifold.Node node in _manifold.Nodes.Where(c => c is Framework.TachyonManifold.Splitter && c.Touched))
                {
                    int left = node.X * itemSizeInPixels;
                    int right = (node.X + 1) * itemSizeInPixels;

                    g.FillPolygon(redBrush,
                        new PointF(left, (node.Y + 1) * itemSizeInPixels),
                        new PointF(right, (node.Y + 1) * itemSizeInPixels),
                        new PointF((left + right) / 2.0f, node.Y * itemSizeInPixels));
                }
            }

            using (Brush greenBrush = new SolidBrush(Color.LightGreen))
            {
                foreach (Framework.TachyonManifold.Node node in _manifold.Nodes.Where(c => c is Framework.TachyonManifold.Beam && !c.Touched))
                {
                    g.FillRectangle(greenBrush, new RectangleF((node.X * itemSizeInPixels) + (itemSizeInPixels / 4.0f), node.Y * itemSizeInPixels, itemSizeInPixels / 2.0f, itemSizeInPixels));
                }
            }
            using (Brush greenBrush = new SolidBrush(Color.DarkRed))
            {
                foreach (Framework.TachyonManifold.Node node in _manifold.Nodes.Where(c => c is Framework.TachyonManifold.Beam && c.Touched))
                {
                    g.FillRectangle(greenBrush, new RectangleF((node.X * itemSizeInPixels) + (itemSizeInPixels / 4.0f), node.Y * itemSizeInPixels, itemSizeInPixels / 2.0f, itemSizeInPixels));
                }
            }
        }

        public async Task Start()
        {
            while (true)
            {
                bool prog = _manifold.Progress();
                doubleBufferedPanel1.Invalidate();

                splitsLabel.Text = $"{_manifold.SplitCount}";

                await Task.Delay(60).ConfigureAwait(true);

                if (!prog)
                {
                    break;
                }
            }

            long totalTimelines = 0;
            while (true)
            {
                _manifold.Untouch();
                long timelines = _manifold.CalculateTimeline();
                doubleBufferedPanel1.Invalidate();
                totalTimelines += timelines;

                timelinesLabel.Text = $"{totalTimelines}";

                await Task.Delay(150).ConfigureAwait(true);

                if (timelines == 0)
                {
                    break;
                }
            }
        }

        public void ResetManifold()
        {
            _manifold.Reset();
            doubleBufferedPanel1.Invalidate();
            splitsLabel.Text = "0";
            timelinesLabel.Text = "0";
        }
    }
}
