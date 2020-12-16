using morphological_image_processing_wpf.Core.Generator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace morphological_image_processing_wpf.View.Components.AlgorithmsSelection.Configurations
{
    /// <summary>
    /// Logika interakcji dla klasy ImageGeneratorConfiguration.xaml
    /// </summary>
    public partial class ImageGeneratorConfiguration : UserControl
    {
        private readonly GeneratorConfiguration configuration = new GeneratorConfiguration();
        public ImageGeneratorConfiguration()
        {
            InitializeComponent();
            NumberOfShapesSelector.Minimum = 1;
            NumberOfShapesSelector.Maximum = 10;
            NumberOfShapesSelector.Value = configuration.NumberOfShapes;

            NumberOfEdgesSelector.Minimum = 3;
            NumberOfEdgesSelector.Maximum = 10;
            NumberOfEdgesSelector.Value = configuration.MaxNumberOfEdges;

            StrokeThicknessSelector.Minimum = 1;
            StrokeThicknessSelector.Maximum = 15;
            StrokeThicknessSelector.Value = configuration.MaxStrokeThickness;
        }

        public IGeneratorConfiguration GetConfiguration()
        {
            return configuration;
        }

        private void NumberOfShapesSelector_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            configuration.NumberOfShapes = (int) NumberOfShapesSelector.Value;
        }

        private void NumberOfEdgesSelector_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            configuration.MaxNumberOfEdges = (int)NumberOfEdgesSelector.Value;
        }

        private void StrokeThicknessSelector_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            configuration.MaxStrokeThickness = (int)StrokeThicknessSelector.Value;
        }

        public void SetFromExternalConfiguration(GeneratorConfiguration otherConfiguration)
        {
            configuration.SetValuesFrom(otherConfiguration);
        }
    }
}
