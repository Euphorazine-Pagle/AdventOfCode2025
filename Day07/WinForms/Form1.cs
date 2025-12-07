using System.Reflection;

namespace AdventOfCode2025.WinForms
{
    public partial class Form1 : Form
    {
        private TachyonManifoldUserControl? _canvas;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var day7 = ReadData("Day_07_Puzzle");
            var manifold = Framework.TachyonManifold.CreateManifold(day7);
            _canvas = new TachyonManifoldUserControl(manifold);
            _canvas.Left = 10;
            _canvas.Top = 10;
            this.Controls.Add(_canvas);
        }

        private static List<string> ReadData(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream? stream = assembly.GetManifestResourceStream($"AdventOfCode2025.WinForms.Data.{fileName}.txt"))
            {
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string fileContents = reader.ReadToEnd();
                        return fileContents.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
                    }
                }
            }

            return new List<string>();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (_canvas == null)
            {
                return;
            }

            button1.Enabled = false;
            button2.Enabled = false;

            await _canvas.Start();

            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _canvas?.ResetManifold();
        }
    }
}
