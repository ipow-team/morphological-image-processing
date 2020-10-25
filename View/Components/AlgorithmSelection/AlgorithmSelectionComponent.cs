using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MorphologicalImageProcessing.Core.Services;

namespace MorphologicalImageProcessing.View.Components.AlgorithmSelection
{
    public partial class AlgorithmSelectionComponent : UserControl
    {
        private MorphologicalAlgorithmsService morphologicalAlgorithmsService;

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
    }
}
