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
            label1 = new Label();
            label2 = new Label();
            daysFlowLayoutPanel = new FlowLayoutPanel();
            openFileControlButton = new Button();
            SuspendLayout();
            // 
            // mainGraph
            // 
            mainGraph.DisplayScale = 1F;
            mainGraph.Location = new Point(22, 26);
            mainGraph.Margin = new Padding(6);
            mainGraph.Name = "mainGraph";
            mainGraph.Size = new Size(2144, 943);
            mainGraph.TabIndex = 0;
            mainGraph.Load += mainGraph_Load;
            // 
            // mainLayoutPanel
            // 
            mainLayoutPanel.AllowDrop = true;
            mainLayoutPanel.AutoScroll = true;
            mainLayoutPanel.FlowDirection = FlowDirection.TopDown;
            mainLayoutPanel.Location = new Point(2201, 149);
            mainLayoutPanel.Name = "mainLayoutPanel";
            mainLayoutPanel.Size = new Size(509, 654);
            mainLayoutPanel.TabIndex = 1;
            mainLayoutPanel.Paint += flowLayoutPanel1_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2201, 88);
            label1.Name = "label1";
            label1.Size = new Size(173, 32);
            label1.TabIndex = 2;
            label1.Text = "Filtrage Equipe";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(2201, 817);
            label2.Name = "label2";
            label2.Size = new Size(145, 32);
            label2.TabIndex = 3;
            label2.Text = "Filtrage Jour";
            // 
            // daysFlowLayoutPanel
            // 
            daysFlowLayoutPanel.Location = new Point(2201, 852);
            daysFlowLayoutPanel.Name = "daysFlowLayoutPanel";
            daysFlowLayoutPanel.Size = new Size(509, 117);
            daysFlowLayoutPanel.TabIndex = 4;
            // 
            // openFileControlButton
            // 
            openFileControlButton.Location = new Point(58, 1014);
            openFileControlButton.Name = "openFileControlButton";
            openFileControlButton.Size = new Size(296, 46);
            openFileControlButton.TabIndex = 5;
            openFileControlButton.Text = "Choisir un fichier";
            openFileControlButton.UseVisualStyleBackColor = true;
            openFileControlButton.Click += openFileControlButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2756, 1837);
            Controls.Add(openFileControlButton);
            Controls.Add(daysFlowLayoutPanel);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(mainLayoutPanel);
            Controls.Add(mainGraph);
            Margin = new Padding(6);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private ScottPlot.WinForms.FormsPlot mainGraph;
        private FlowLayoutPanel mainLayoutPanel;
        private Label label1;
        private Label label2;
        private FlowLayoutPanel daysFlowLayoutPanel;
        private Button openFileControlButton;
    }
}
