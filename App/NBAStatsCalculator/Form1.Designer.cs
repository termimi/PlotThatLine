namespace NBAStatsCalculator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            mainGraph = new ScottPlot.WinForms.FormsPlot();
            mainLayoutPanel = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // mainGraph
            // 
            mainGraph.DisplayScale = 1F;
            mainGraph.Location = new Point(22, 26);
            mainGraph.Margin = new Padding(6, 6, 6, 6);
            mainGraph.Name = "mainGraph";
            mainGraph.Size = new Size(2238, 943);
            mainGraph.TabIndex = 0;
            // 
            // mainLayoutPanel
            // 
            mainLayoutPanel.Location = new Point(2302, 36);
            mainLayoutPanel.Name = "mainLayoutPanel";
            mainLayoutPanel.Size = new Size(400, 933);
            mainLayoutPanel.TabIndex = 1;
            mainLayoutPanel.Paint += flowLayoutPanel1_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2756, 1837);
            Controls.Add(mainLayoutPanel);
            Controls.Add(mainGraph);
            Margin = new Padding(6, 6, 6, 6);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private ScottPlot.WinForms.FormsPlot mainGraph;
        private FlowLayoutPanel mainLayoutPanel;
    }
}
