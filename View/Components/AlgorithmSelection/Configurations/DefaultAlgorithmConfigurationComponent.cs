using System;
using System.Windows.Forms;
using MorphologicalImageProcessing.Core.Algorithms;

namespace MorphologicalImageProcessing.View.Components.AlgorithmSelection.Configurations
{
    public partial class DefaultAlgorithmConfigurationComponent : UserControl, IConfigurationComponent
    {
        private readonly DefaultMorphologicalAlgorithmConfiguration configuration = new DefaultMorphologicalAlgorithmConfiguration();
        public DefaultAlgorithmConfigurationComponent()
        {
            InitializeComponent();
            MatrixSizeSelector.Value = configuration.BoxSize;
        }

        public IMorphologicalAlgorithmConfiguration GetConfiguration()
        {
            return configuration;
        }

        public void SetSelected(bool isSelected)
        {
            IConfigurationComponent.SetSelected(this, isSelected);
        }

        private void MatrixSizeSelector_ValueChanged(object sender, EventArgs e)
        {
            configuration.BoxSize = (int) MatrixSizeSelector.Value;
        }
    }
}
