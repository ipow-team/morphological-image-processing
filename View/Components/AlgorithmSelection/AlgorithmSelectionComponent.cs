using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MorphologicalImageProcessing.Core.Services;
using MorphologicalImageProcessing.Core.Algorithms;

namespace MorphologicalImageProcessing.View.Components.AlgorithmSelection
{
    public partial class AlgorithmSelectionComponent : UserControl
    {
        private readonly MorphologicalAlgorithmsService morphologicalAlgorithmsService;

        private IAlgorithm selectedAlgorithm;

        public AlgorithmSelectionComponent()
        {
            morphologicalAlgorithmsService = new MorphologicalAlgorithmsService();
            InitializeComponent();
            ConfigureAlgorithmSelectionComboBox();
        }

        private void ConfigureAlgorithmSelectionComboBox()
        {
            AlgorithmComboBox.DataSource = morphologicalAlgorithmsService.GetAllAlgorithms();
            AlgorithmComboBox.DisplayMember = "Name";
        }

        private void AlgorithmComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if(AlgorithmComboBox.SelectedIndex != -1)
            {
                selectedAlgorithm = (IAlgorithm) AlgorithmComboBox.SelectedItem;
                baseAlgorithmConfigurationComponent1.ShowAppropriateAlgorithmConfigurationComponent(selectedAlgorithm);
            }
        }

        public Image RunSelectedAlgorithm(Image image)
        {
            return selectedAlgorithm.Apply(image, baseAlgorithmConfigurationComponent1.GetCurrentConfiguration());
        }
    }
}
