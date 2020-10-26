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
            Control controlComponent = GetControlComponent();
            if(isSelected)
            {
                controlComponent.Visibility = Visibility.Visible;
                controlComponent.MaxHeight = double.PositiveInfinity;
                controlComponent.MaxWidth = double.PositiveInfinity;
            }
            else
            {
                controlComponent.Visibility = Visibility.Hidden;
                controlComponent.MaxHeight = 0.0001;
                controlComponent.MaxWidth = 0.0001;
            }
        }
    }
}
