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

namespace morphological_image_processing_wpf.View.Components.DisplayImage
{
    /// <summary>
    /// Interaction logic for DisplayImageComponent.xaml
    /// </summary>
    public partial class DisplayImageComponent : UserControl
    {
        public DisplayImageComponent()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
