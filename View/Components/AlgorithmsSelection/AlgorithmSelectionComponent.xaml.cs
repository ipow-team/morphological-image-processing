using MorphologicalImageProcessing.Core.Algorithms;
using MorphologicalImageProcessing.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace morphological_image_processing_wpf.View.Components.AlgorithmsSelection.Configurations
{
    /// <summary>
    /// Interaction logic for AlgorithmSelectionComponent.xaml
    /// </summary>
    public partial class AlgorithmSelectionComponent : UserControl
    {
        private readonly AlgorithmSelectionViewModel _algorithmSelectionViewModel;

        public AlgorithmSelectionComponent()
        {
            InitializeComponent();
            _algorithmSelectionViewModel = new AlgorithmSelectionViewModel(
                new MorphologicalAlgorithmsService(),
                baseAlgorithmConfigurationComponent.SetAlgorithm);
            ConfigureAlgorithmSelectionComboBox();
        }

        private void ConfigureAlgorithmSelectionComboBox()
        {
            AlgorithmComboBox.ItemsSource = _algorithmSelectionViewModel.AlgorithmEntries;
            AlgorithmComboBox.DisplayMemberPath = "Name";
            if(_algorithmSelectionViewModel.AlgorithmEntries.Count > 0)
            {
                AlgorithmComboBox.SelectedIndex = 0;
            }
        }

        private void AlgorithmComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AlgorithmComboBox.SelectedIndex != -1)
            {
                _algorithmSelectionViewModel.SelectedAlgorithmEntry = (AlgorithmEntry)AlgorithmComboBox.SelectedItem;
            }
        }

        public IAlgorithm GetSelectedAlgorithm()
        {
            return _algorithmSelectionViewModel.SelectedAlgorithm;
        }

        public IMorphologicalAlgorithmConfiguration getCurrentConfiguration()
        {
            return baseAlgorithmConfigurationComponent.GetCurrentConfiguration();
        }

        public class AlgorithmSelectionViewModel
        {
            private readonly Action<IAlgorithm> _onAlgorithmChangedAction;
            private AlgorithmEntry _selectedAlgorithm;

            public AlgorithmSelectionViewModel(MorphologicalAlgorithmsService morphologicalAlgorithmsService, Action<IAlgorithm> onAlgorithmChangedAction)
            {
                IList<AlgorithmEntry> _algorithmModels = 
                    morphologicalAlgorithmsService.GetAllAlgorithms().Select(algorithm => new AlgorithmEntry(algorithm)).ToList();
                AlgorithmEntries = new ObservableCollection<AlgorithmEntry>(_algorithmModels);
                _onAlgorithmChangedAction = onAlgorithmChangedAction;
            }

            public ObservableCollection<AlgorithmEntry> AlgorithmEntries { get; }

            public AlgorithmEntry SelectedAlgorithmEntry
            {
                get { return _selectedAlgorithm; }
                set
                {
                    if (_selectedAlgorithm == value)
                        return;
                    _selectedAlgorithm = value;
                    _onAlgorithmChangedAction.Invoke(_selectedAlgorithm.Algorithm);
                }
            }

            public IAlgorithm SelectedAlgorithm
            {
                get
                {
                    if (SelectedAlgorithmEntry == null)
                        throw new Exception("You have to select an algorithm.");
                    return SelectedAlgorithmEntry.Algorithm;
                }
            }
        }

        public class AlgorithmEntry
        {
            public String Name { get; }
            public IAlgorithm Algorithm { get; set; }

            public AlgorithmEntry(IAlgorithm algorithm)
            {
                Name = algorithm.Name;
                Algorithm = algorithm;
            }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
