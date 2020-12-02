using MorphologicalImageProcessing.Core.Algorithms;
using System;
using System.CodeDom;
using System.Collections.Generic;
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
            BrightnessThresholdSelector.Value = configuration.BrightnessThreshold;
            LineColorPicker.SelectedColor = ConvertColor(configuration.LineColor);
            KernelSelectorComponent.SetShape(configuration.StructuralElementPoints);
            KernelSelectorComponent.AddOnCenterChangedAction(OnSelectedCenterChangedAction);
            KernelSelectorComponent.SetCenter(configuration.Center);
            KernelSelectorComponent.AddOnKernelShapeChangedAction(OnShapeChangedAction);
        }

        public IMorphologicalAlgorithmConfiguration GetConfiguration()
        {
            return configuration;
        }

        public Control GetControlComponent()
        {
            return this;
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

        private void OnSelectedCenterChangedAction(Tuple<int, int> selectedCenter)
        {
            if(selectedCenter == null)
            {
                // add validations
                throw new Exception("Center cannot be empty");
            }
            configuration.Center = selectedCenter;
        }

        private void OnShapeChangedAction(ISet<Tuple<int, int>> shape)
        {
            if(shape.Count < 1)
            {
                // add validations
                throw new Exception("Shape cannot be empty");
            }
            configuration.StructuralElementPoints = shape;
        }
    }
}
