using MorphologicalImageProcessing.Core.Algorithms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace MorphologicalImageProcessing.View.Components.AlgorithmSelection.Configurations
{
    interface IConfigurationComponent
    {

        IMorphologicalAlgorithmConfiguration GetConfiguration();

        void SetSelected(bool isSelected);

        public static void AdjustComponent(Control component)
        {
            component.Dock = DockStyle.Fill;
        }

        public static void SetSelected(Control component, bool isSelected)
        {
            component.Visible = isSelected;
        }
    }
}
