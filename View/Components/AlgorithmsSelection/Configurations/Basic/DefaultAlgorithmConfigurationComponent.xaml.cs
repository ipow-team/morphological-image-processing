using MorphologicalImageProcessing.Core.Algorithms;
using System;
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

        private readonly DefaultMorphologicalAlgorithmConfiguration configuration = DefaultMorphologicalAlgorithmConfiguration.create();
        public DefaultAlgorithmConfigurationComponent()
        {
            InitializeComponent();
            SetValuesFrom(configuration);
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
            if (selectedCenter == null)
            {
                MessageBox.Show("Center cannot be empty", "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                configuration.Center = selectedCenter;
            }
        }

        private void OnShapeChangedAction(ISet<Tuple<int, int>> shape)
        {
            if (shape.Count < 1)
            {
                MessageBox.Show("Shape cannot be empty", "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                configuration.StructuralElementPoints = shape;
            }
        }

        public void SetValuesFrom(IMorphologicalAlgorithmConfiguration other)
        {
            if (typeof(DefaultMorphologicalAlgorithmConfiguration).IsAssignableFrom(other.GetType()))
            {
                SetValuesFrom((DefaultMorphologicalAlgorithmConfiguration)other);
            }

        }

        private void SetValuesFrom(DefaultMorphologicalAlgorithmConfiguration other)
        {
            if (configuration != other)
            {
                configuration.SetValuesFrom(other);
            }
            BrightnessThresholdSelector.Value = other.BrightnessThreshold;
            LineColorPicker.SelectedColor = ConvertColor(other.LineColor);
            KernelSelectorComponent.SetShape(other.StructuralElementPoints);
            KernelSelectorComponent.AddOnCenterChangedAction(OnSelectedCenterChangedAction);
            KernelSelectorComponent.SetCenter(other.Center);
            KernelSelectorComponent.AddOnKernelShapeChangedAction(OnShapeChangedAction);
        }
    }
}
