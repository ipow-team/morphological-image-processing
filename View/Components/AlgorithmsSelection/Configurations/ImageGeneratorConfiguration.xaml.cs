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
        }

        public IGeneratorConfiguration GetConfiguration()
        {
            return configuration;
        }

        private void NumberOfShapesSelector_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            configuration.NumberOfShapes = (int) NumberOfShapesSelector.Value;
        }
    }
}
