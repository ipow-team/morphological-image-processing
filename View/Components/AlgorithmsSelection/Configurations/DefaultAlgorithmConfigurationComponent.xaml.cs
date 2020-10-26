using MorphologicalImageProcessing.Core.Algorithms;
using System;
using System.Windows.Controls;

namespace morphological_image_processing_wpf.View.Components.AlgorithmsSelection.Configurations
{
    /// <summary>
    /// Interaction logic for DefaultAlgorithmConfigurationComponent.xaml
    /// </summary>
    public partial class DefaultAlgorithmConfigurationComponent : UserControl, IConfigurationComponent
    {
        private readonly DefaultMorphologicalAlgorithmConfiguration configuration = new DefaultMorphologicalAlgorithmConfiguration();
        public DefaultAlgorithmConfigurationComponent()
        {
            InitializeComponent();
            MatrixSizeSelector.Text = configuration.BoxSize.ToString();
        }

        public IMorphologicalAlgorithmConfiguration GetConfiguration()
        {
            return configuration;
        }

        public Control GetControlComponent()
        {
            return this;
        }

        private void MatrixSizeSelector_TextChanged(object sender, EventArgs e) {        
            configuration.BoxSize = int.Parse(MatrixSizeSelector.Text);
        }
    }
}
