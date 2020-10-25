namespace MorphologicalImageProcessing.View.Components.AlgorithmSelection.Configurations
{
    partial class DefaultAlgorithmConfigurationComponent
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
            this.MatrixSizeLabel = new System.Windows.Forms.Label();
            this.MatrixSizeSelector = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.MatrixSizeSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // MatrixSizeLabel
            // 
            this.MatrixSizeLabel.AutoSize = true;
            this.MatrixSizeLabel.Location = new System.Drawing.Point(13, 14);
            this.MatrixSizeLabel.Name = "MatrixSizeLabel";
            this.MatrixSizeLabel.Size = new System.Drawing.Size(108, 15);
            this.MatrixSizeLabel.TabIndex = 0;
            this.MatrixSizeLabel.Text = "Matrix Size (min. 2)";
            // 
            // MatrixSizeSelector
            // 
            this.MatrixSizeSelector.Location = new System.Drawing.Point(127, 12);
            this.MatrixSizeSelector.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.MatrixSizeSelector.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.MatrixSizeSelector.Name = "MatrixSizeSelector";
            this.MatrixSizeSelector.Size = new System.Drawing.Size(70, 23);
            this.MatrixSizeSelector.TabIndex = 1;
            this.MatrixSizeSelector.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.MatrixSizeSelector.ValueChanged += new System.EventHandler(this.MatrixSizeSelector_ValueChanged);
            // 
            // DefaultAlgorithmConfigurationComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.MatrixSizeSelector);
            this.Controls.Add(this.MatrixSizeLabel);
            this.Name = "DefaultAlgorithmConfigurationComponent";
            this.Size = new System.Drawing.Size(213, 44);
            ((System.ComponentModel.ISupportInitialize)(this.MatrixSizeSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MatrixSizeLabel;
        private System.Windows.Forms.NumericUpDown MatrixSizeSelector;
    }
}
