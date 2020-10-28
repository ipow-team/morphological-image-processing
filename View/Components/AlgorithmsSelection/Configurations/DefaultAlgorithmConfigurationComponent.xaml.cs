using MorphologicalImageProcessing.Core.Algorithms;
using System;
using System.CodeDom;
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
            MatrixSizeSelector.Minimum = (double)configuration.MinBoxSize;
            MatrixSizeSelector.Maximum = (double)configuration.MaxBoxSize;
            MatrixSizeSelector.Value = (double) configuration.BoxSize;
        }

        public IMorphologicalAlgorithmConfiguration GetConfiguration()
        {
            return configuration;
        }

        public Control GetControlComponent()
        {
            return this;
        }

        private void MatrixSizeSelector_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            configuration.BoxSize = (int)MatrixSizeSelector.Value;
        }
    }
}
