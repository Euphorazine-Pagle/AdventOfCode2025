using System.Data;

namespace AdventOfCode2025.WinForms
{
    public partial class WrappingPaperWarehouseUserControl : UserControl
    {
        private readonly Framework.WrappingPaperWarehouse _wrappingPaperWarehouse;

        private const int itemSizeInPixels = 6;

        public WrappingPaperWarehouseUserControl(Framework.WrappingPaperWarehouse wrappingPaperWarehouse)
        {
            InitializeComponent();

            _wrappingPaperWarehouse = wrappingPaperWarehouse;
            this.Size = new System.Drawing.Size(_wrappingPaperWarehouse.Width * itemSizeInPixels, _wrappingPaperWarehouse.Height * itemSizeInPixels);
            this.BackColor = System.Drawing.Color.DarkGreen;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (_wrappingPaperWarehouse == null)
            {
                return;
            }            

            Graphics g = e.Graphics;

            g.Clear(Color.DarkGreen);

            using (Brush redBrush = new SolidBrush(Color.Coral))
            {
                foreach (Framework.WrappingPaperWarehouse.Container container in _wrappingPaperWarehouse.Containers.Where(c => c.MarkedForRemoval && !c.Removed))
                {
                    g.FillRectangle(redBrush, new Rectangle(container.X * itemSizeInPixels, container.Y * itemSizeInPixels, itemSizeInPixels, itemSizeInPixels));
                }
            }

            using (Brush greenBrush = new SolidBrush(Color.LightGreen))
            {
                foreach (Framework.WrappingPaperWarehouse.Container container in _wrappingPaperWarehouse.Containers.Where(c => !c.MarkedForRemoval && !c.Removed))
                {
                    g.FillRectangle(greenBrush, new Rectangle(container.X * itemSizeInPixels, container.Y * itemSizeInPixels, itemSizeInPixels, itemSizeInPixels));
                }
            }
        }

        public async Task Start()
        {
            while (true)
            {
                _wrappingPaperWarehouse.MarkForRemoval();
                panel1.Invalidate();

                await Task.Delay(100).ConfigureAwait(true);

                bool removedMoreContainers = _wrappingPaperWarehouse.RemoveContainers();
                panel1.Invalidate();

                //await Task.Delay(1000).ConfigureAwait(true);

                if (!removedMoreContainers)
                {
                    break;
                }
            }
        }

        public void RefreshContainers()
        {
            _wrappingPaperWarehouse.RefreshContainers();
            panel1.Invalidate();
        }
    }
}
