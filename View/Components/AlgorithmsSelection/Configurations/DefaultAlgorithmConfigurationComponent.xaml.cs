using MorphologicalImageProcessing.Core.Algorithms;
using System;
using System.CodeDom;
using System.Drawing;
using System.Windows;
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
            MatrixSizeSelector.Minimum = configuration.MinBoxSize;
            MatrixSizeSelector.Maximum = configuration.MaxBoxSize;
            MatrixSizeSelector.Value = configuration.BoxSize;
            BrightnessThresholdSelector.Value = configuration.BrightnessThreshold;
            LineColorPicker.SelectedColor = ConvertColor(configuration.LineColor);
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

        private void LineColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        { 
            if(LineColorPicker.SelectedColor != null)
            {

                configuration.LineColor = ConvertColor(LineColorPicker.SelectedColor.Value);
            }
        }

        private Color ConvertColor(System.Windows.Media.Color sourceColor)
        {
            return Color.FromArgb(sourceColor.A, sourceColor.R, sourceColor.G, sourceColor.B);
        }

        private System.Windows.Media.Color ConvertColor(Color sourceColor)
        {
            return System.Windows.Media.Color.FromArgb(sourceColor.A, sourceColor.R, sourceColor.G, sourceColor.B);
        }

        private void BrightnessThresholdSelector_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            configuration.BrightnessThreshold = Math.Max(0.01, Math.Min(0.99, BrightnessThresholdSelector.Value));
        }
    }
}
