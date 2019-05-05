using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using FileExplorerTree.ViewModels;

namespace FileExplorerTree
{
    public class AppBootstrapper : BootstrapperBase
    {
        private SimpleContainer container;
        public AppBootstrapper() 
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();
            //container.Singleton<IEventAggregator, EventAggregator>();

            container.PerRequest<MainWindowViewModel>();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainWindowViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}
