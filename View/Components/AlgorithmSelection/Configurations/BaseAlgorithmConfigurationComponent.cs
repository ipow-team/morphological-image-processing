using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MorphologicalImageProcessing.Core.Algorithms;

namespace MorphologicalImageProcessing.View.Components.AlgorithmSelection.Configurations
{
    public partial class BaseAlgorithmConfigurationComponent : UserControl
    {
        private readonly Dictionary<Type, IConfigurationComponent> userControlsByType = new Dictionary<Type, IConfigurationComponent>();
        private IConfigurationComponent currentVisibleConfiguration;

        public BaseAlgorithmConfigurationComponent()
        {
            InitializeComponent();
            AddContainerComponent(typeof(DefaultMorphologicalAlgorithmConfiguration), defaultAlgorithmConfigurationComponent);
        }

        private void AddContainerComponent(Type configurationType, IConfigurationComponent configurationComponent)
        {
            if(typeof(UserControl).IsAssignableFrom(configurationComponent.GetType()))
            {
                IConfigurationComponent.AdjustComponent((UserControl) configurationComponent);
                userControlsByType.Add(configurationType, configurationComponent);
            }
        }

        public void SetDefaultAlgorithmConfigurationVisibility(bool isVisible)
        {
            defaultAlgorithmConfigurationComponent.Visible = isVisible;
        }

        public void ShowAppropriateAlgorithmConfigurationComponent(IAlgorithm algorithm)
        {
            IConfigurationComponent componentToShow = GetAppropriateAlgorithmConfigurationComponent(algorithm);
            SetCurrentlyVisibleConfiguration(componentToShow);
        }

        private IConfigurationComponent GetAppropriateAlgorithmConfigurationComponent(IAlgorithm algorithm)
        {
            Type configurationType = algorithm.GetConfigurationClass();
            if(typeof(EmptyMorphologicalAlgorithmConfiguration).IsAssignableFrom(configurationType))
            {
                return null;
            }
            return userControlsByType[configurationType];
        }

        private void SetCurrentlyVisibleConfiguration(IConfigurationComponent configurationToShow)
        {
            if(currentVisibleConfiguration != null)
            {
                currentVisibleConfiguration.SetSelected(false);
            }
            if(configurationToShow != null)
            {
                configurationToShow.SetSelected(true);
            }
            currentVisibleConfiguration = configurationToShow;
        }

        public IMorphologicalAlgorithmConfiguration GetCurrentConfiguration()
        {
            if(currentVisibleConfiguration == null) {
                return new EmptyMorphologicalAlgorithmConfiguration();
            }
            return currentVisibleConfiguration.GetConfiguration();
        }
    }
}
