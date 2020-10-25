namespace MorphologicalImageProcessing
{
    partial class MainView
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
            this.algorithmSelectionComponent1 = new MorphologicalImageProcessing.View.Components.AlgorithmSelection.AlgorithmSelectionComponent();
            this.SuspendLayout();
            // 
            // algorithmSelectionComponent1
            // 
            this.algorithmSelectionComponent1.Location = new System.Drawing.Point(12, 12);
            this.algorithmSelectionComponent1.Name = "algorithmSelectionComponent1";
            this.algorithmSelectionComponent1.Size = new System.Drawing.Size(209, 309);
            this.algorithmSelectionComponent1.TabIndex = 0;
            // 
            // MainView
            // 
            this.ClientSize = new System.Drawing.Size(1072, 587);
            this.Controls.Add(this.algorithmSelectionComponent1);
            this.Name = "MainView";
            this.ResumeLayout(false);

        }

        #endregion

        private View.Components.AlgorithmSelection.AlgorithmSelectionComponent algorithmSelectionComponent1;
    }
}

