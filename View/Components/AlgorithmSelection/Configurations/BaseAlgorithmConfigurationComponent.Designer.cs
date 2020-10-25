namespace MorphologicalImageProcessing.View.Components.AlgorithmSelection.Configurations
{
    partial class BaseAlgorithmConfigurationComponent
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
            this.defaultAlgorithmConfigurationComponent = new MorphologicalImageProcessing.View.Components.AlgorithmSelection.Configurations.DefaultAlgorithmConfigurationComponent();
            this.SuspendLayout();
            // 
            // defaultAlgorithmConfigurationComponent
            // 
            this.defaultAlgorithmConfigurationComponent.BackColor = System.Drawing.Color.Red;
            this.defaultAlgorithmConfigurationComponent.Location = new System.Drawing.Point(0, 0);
            this.defaultAlgorithmConfigurationComponent.Name = "defaultAlgorithmConfigurationComponent";
            this.defaultAlgorithmConfigurationComponent.Size = new System.Drawing.Size(172, 13);
            this.defaultAlgorithmConfigurationComponent.TabIndex = 0;
            this.defaultAlgorithmConfigurationComponent.Visible = false;
            // 
            // BaseAlgorithmConfigurationComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.Controls.Add(this.defaultAlgorithmConfigurationComponent);
            this.Name = "BaseAlgorithmConfigurationComponent";
            this.Size = new System.Drawing.Size(172, 220);
            this.ResumeLayout(false);

        }

        #endregion

        private DefaultAlgorithmConfigurationComponent defaultAlgorithmConfigurationComponent;
    }
}
