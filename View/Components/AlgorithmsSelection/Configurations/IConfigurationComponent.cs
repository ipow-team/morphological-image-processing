using MorphologicalImageProcessing.Core.Algorithms;
using System.Windows;
using System.Windows.Controls;

namespace morphological_image_processing_wpf.View.Components.AlgorithmsSelection.Configurations
{
    interface IConfigurationComponent
    {

        IMorphologicalAlgorithmConfiguration GetConfiguration();

        public Control GetControlComponent();

        public void FillParent()
        {
            Control controlComponent = GetControlComponent();
            controlComponent.HorizontalAlignment = HorizontalAlignment.Stretch;
            controlComponent.VerticalAlignment = VerticalAlignment.Stretch;
            controlComponent.Margin = new Thickness(10, 10, 10, 10);
        }

        public void SetSelected(bool isSelected)
        {
           GetControlComponent().Visibility = isSelected ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
