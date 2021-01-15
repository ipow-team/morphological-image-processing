using morphological_image_processing_wpf.Core.Algorithms.Advanced.Configs;
using MorphologicalImageProcessing.Core.Algorithms;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace morphological_image_processing_wpf.View.Components.AlgorithmsSelection.Configurations
{
    /// <summary>
    /// Interaction logic for BaseAlgorithmConfigurationComponent.xaml
    /// </summary>
    public partial class BaseAlgorithmConfigurationComponent : UserControl
    {
        private readonly AlgorithmConfigurationsViewModel _algorithmConfigurationsViewModel;

        private static readonly AlgoritmConfigurationDictionary _algorithmConfigurationsDictionary = new AlgoritmConfigurationDictionary()
            {
                { typeof(DefaultMorphologicalAlgorithmConfiguration), new DefaultAlgorithmConfigurationComponent() },
                { typeof(CompassEdgeConfiguration), new CompassEdgeConfigurationComponent() }
            };

        public BaseAlgorithmConfigurationComponent()
        {
            InitializeBaseComponent();
            _algorithmConfigurationsViewModel = new AlgorithmConfigurationsViewModel(_algorithmConfigurationsDictionary);
        }

        public void InitializeBaseComponent()
        {
            InitializeComponent();
            foreach(var component in _algorithmConfigurationsDictionary.Values)
            {
                AlgorithmConfigurationGrid.Children.Add(component.GetControlComponent());
            }
        }

        public void SetAlgorithm(IAlgorithm algorithm)
        {
            _algorithmConfigurationsViewModel.CurrentAlgorithm = algorithm;
        }

        public IMorphologicalAlgorithmConfiguration GetCurrentConfiguration()
        {
            return _algorithmConfigurationsViewModel.GetCurrentAlgorithmConfiguration();
        }

        public void SetCurrentConfiguration(IAlgorithm algorithm, IMorphologicalAlgorithmConfiguration config)
        {
            _algorithmConfigurationsViewModel.SetCurrentConfiguration(algorithm, config);
        }
    }

    class AlgoritmConfigurationDictionary : Dictionary<Type, IConfigurationComponent>
    {
        public new virtual void Add(Type configurationType, IConfigurationComponent configurationComponent)
        {
            if (typeof(UserControl).IsAssignableFrom(configurationComponent.GetType()))
            {
                configurationComponent.FillParent();
                base.Add(configurationType, configurationComponent);
            }
        }
    }

    class AlgorithmConfigurationsViewModel
    {
        private readonly AlgoritmConfigurationDictionary _configurations = new AlgoritmConfigurationDictionary();
        private IAlgorithm _CurrentAlgorithm;
        private IConfigurationComponent _CurrentConfigurationComponent;

        public IAlgorithm CurrentAlgorithm
        {
            set
            {
                Type configurationType = value.GetConfigurationClass();
                if(_CurrentAlgorithm != null && value != null && _CurrentAlgorithm.GetName() == value.GetName())
                {
                    return;
                }
                if (typeof(EmptyMorphologicalAlgorithmConfiguration).IsAssignableFrom(configurationType))
                {
                    CurrentConfigurationComponent = null;
                } 
                else
                {
                    CurrentConfigurationComponent = _configurations[configurationType];
                }
                _CurrentAlgorithm = value;
            }
        }

        private IConfigurationComponent CurrentConfigurationComponent
        {
            set
            {
                if (_CurrentConfigurationComponent != null)
                {
                    _CurrentConfigurationComponent.SetSelected(false);
                }
                if (value != null)
                {
                    value.SetSelected(true);
                }
                _CurrentConfigurationComponent = value;
            }
        }

        public AlgorithmConfigurationsViewModel(AlgoritmConfigurationDictionary configurationsDictionary)
        {
            _configurations = configurationsDictionary;
            InitializeConfigurationFields(_configurations);
        }

        public void SetCurrentConfiguration(IAlgorithm newAlgorithm, IMorphologicalAlgorithmConfiguration config)
        {
            CurrentAlgorithm = newAlgorithm;
            _CurrentConfigurationComponent = _configurations[newAlgorithm.GetConfigurationClass()];
            if(_CurrentConfigurationComponent.GetConfiguration() != config)
            {
                _CurrentConfigurationComponent.GetConfiguration().SetValuesFrom(config);
            }
            _CurrentConfigurationComponent.SetValuesFrom(config);
        }

        public IMorphologicalAlgorithmConfiguration GetCurrentAlgorithmConfiguration()
        {
            if (_CurrentConfigurationComponent == null)
            {
                return new EmptyMorphologicalAlgorithmConfiguration();
            }
            return _CurrentConfigurationComponent.GetConfiguration();
        }

        private static void InitializeConfigurationFields(AlgoritmConfigurationDictionary algorithConfigurationDictionary)
        {
            if (algorithConfigurationDictionary == null)
                return;
            foreach(var configuration in algorithConfigurationDictionary.Values)
            {
                configuration.SetSelected(false);
                configuration.FillParent();
            }
        }
    }
}
