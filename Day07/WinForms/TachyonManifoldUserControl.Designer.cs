namespace AdventOfCode2025.WinForms
{
    partial class TachyonManifoldUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            doubleBufferedPanel1 = new DoubleBufferedPanel();
            dataPanel = new Panel();
            splitsLabel = new Label();
            timelinesLabel = new Label();
            label1 = new Label();
            label2 = new Label();
            dataPanel.SuspendLayout();
            SuspendLayout();
            // 
            // doubleBufferedPanel1
            // 
            doubleBufferedPanel1.Dock = DockStyle.Fill;
            doubleBufferedPanel1.Location = new Point(0, 0);
            doubleBufferedPanel1.Name = "doubleBufferedPanel1";
            doubleBufferedPanel1.Size = new Size(634, 361);
            doubleBufferedPanel1.TabIndex = 0;
            doubleBufferedPanel1.Paint += panel1_Paint;
            // 
            // dataPanel
            // 
            dataPanel.Controls.Add(splitsLabel);
            dataPanel.Controls.Add(timelinesLabel);
            dataPanel.Controls.Add(label1);
            dataPanel.Controls.Add(label2);
            dataPanel.Dock = DockStyle.Bottom;
            dataPanel.Location = new Point(0, 361);
            dataPanel.Name = "dataPanel";
            dataPanel.Size = new Size(634, 89);
            dataPanel.TabIndex = 7;
            // 
            // splitsLabel
            // 
            splitsLabel.AutoSize = true;
            splitsLabel.Font = new Font("Cascadia Code", 12F);
            splitsLabel.ForeColor = Color.LightGreen;
            splitsLabel.Location = new Point(144, 15);
            splitsLabel.Name = "splitsLabel";
            splitsLabel.Size = new Size(19, 21);
            splitsLabel.TabIndex = 4;
            splitsLabel.Text = "0";
            // 
            // timelinesLabel
            // 
            timelinesLabel.AutoSize = true;
            timelinesLabel.Font = new Font("Cascadia Code", 12F);
            timelinesLabel.ForeColor = Color.LightGreen;
            timelinesLabel.Location = new Point(143, 45);
            timelinesLabel.Name = "timelinesLabel";
            timelinesLabel.Size = new Size(19, 21);
            timelinesLabel.TabIndex = 5;
            timelinesLabel.Text = "0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cascadia Code", 12F);
            label1.ForeColor = Color.LightGreen;
            label1.Location = new Point(11, 15);
            label1.Name = "label1";
            label1.Size = new Size(127, 21);
            label1.TabIndex = 2;
            label1.Text = "Beam Splits: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Cascadia Code", 12F);
            label2.ForeColor = Color.LightGreen;
            label2.Location = new Point(11, 45);
            label2.Name = "label2";
            label2.Size = new Size(100, 21);
            label2.TabIndex = 3;
            label2.Text = "Timelines:";
            // 
            // TachyonManifoldUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGreen;
            Controls.Add(doubleBufferedPanel1);
            Controls.Add(dataPanel);
            Name = "TachyonManifoldUserControl";
            Size = new Size(634, 450);
            dataPanel.ResumeLayout(false);
            dataPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DoubleBufferedPanel doubleBufferedPanel1;
        private Panel dataPanel;
        private Label splitsLabel;
        private Label timelinesLabel;
        private Label label1;
        private Label label2;
    }
}
