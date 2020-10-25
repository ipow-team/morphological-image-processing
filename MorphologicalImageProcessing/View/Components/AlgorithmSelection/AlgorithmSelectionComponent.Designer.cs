﻿namespace MorphologicalImageProcessing.View.Components.AlgorithmSelection
{
    partial class AlgorithmSelectionComponent
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
            this.components = new System.ComponentModel.Container();
            this.SelectAlgorithmLabel = new System.Windows.Forms.Label();
            this.AlgorithmComboBox = new System.Windows.Forms.ComboBox();
            this.algorithmConfigurationComponent = new MorphologicalImageProcessing.View.Components.AlgorithmSelection.AlgorithmConfigurationComponent();
            this.AlgorithmSelectionComboBoxBinding = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.AlgorithmSelectionComboBoxBinding)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectAlgorithmLabel
            // 
            this.SelectAlgorithmLabel.AutoSize = true;
            this.SelectAlgorithmLabel.Location = new System.Drawing.Point(14, 21);
            this.SelectAlgorithmLabel.Name = "SelectAlgorithmLabel";
            this.SelectAlgorithmLabel.Size = new System.Drawing.Size(95, 15);
            this.SelectAlgorithmLabel.TabIndex = 0;
            this.SelectAlgorithmLabel.Text = "Select Algorithm";
            // 
            // AlgorithmComboBox
            // 
            this.AlgorithmComboBox.FormattingEnabled = true;
            this.AlgorithmComboBox.Location = new System.Drawing.Point(14, 39);
            this.AlgorithmComboBox.Name = "AlgorithmComboBox";
            this.AlgorithmComboBox.Size = new System.Drawing.Size(181, 23);
            this.AlgorithmComboBox.TabIndex = 1;
            // 
            // algorithmConfigurationComponent
            // 
            this.algorithmConfigurationComponent.Location = new System.Drawing.Point(14, 68);
            this.algorithmConfigurationComponent.Name = "algorithmConfigurationComponent";
            this.algorithmConfigurationComponent.Size = new System.Drawing.Size(181, 226);
            this.algorithmConfigurationComponent.TabIndex = 2;
            // 
            // AlgorithmSelectionComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.algorithmConfigurationComponent);
            this.Controls.Add(this.AlgorithmComboBox);
            this.Controls.Add(this.SelectAlgorithmLabel);
            this.Name = "AlgorithmSelectionComponent";
            this.Size = new System.Drawing.Size(209, 309);
            ((System.ComponentModel.ISupportInitialize)(this.AlgorithmSelectionComboBoxBinding)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SelectAlgorithmLabel;
        private System.Windows.Forms.ComboBox AlgorithmComboBox;
        private AlgorithmSelection.AlgorithmConfigurationComponent algorithmConfigurationComponent;
        private System.Windows.Forms.BindingSource AlgorithmSelectionComboBoxBinding;
    }
}
