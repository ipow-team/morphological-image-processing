using morphological_image_processing_wpf.Core.Algorithms.Advanced.Configs;
using MorphologicalImageProcessing.Core.Algorithms;
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
    /// Interaction logic for CompassEdgeConfigurationComponent.xaml
    /// </summary>
    partial class CompassEdgeConfigurationComponent : UserControl, IConfigurationComponent
    {
        private readonly CompassEdgeConfiguration _compassConfiguration = new CompassEdgeConfiguration();

        public CompassEdgeConfigurationComponent()
        {
            InitializeComponent();
        }

        public IMorphologicalAlgorithmConfiguration GetConfiguration()
        {
            throw new NotImplementedException();
        }

        public Control GetControlComponent()
        {
            throw new NotImplementedException();
        }

        public void SetValuesFrom(IMorphologicalAlgorithmConfiguration other)
        {
            throw new NotImplementedException();
        }

        IMorphologicalAlgorithmConfiguration IConfigurationComponent.GetConfiguration()
        {
            throw new NotImplementedException();
        }
    }
}
