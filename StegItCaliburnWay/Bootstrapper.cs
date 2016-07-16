using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Caliburn.Micro;
using Ninject;
using Ninject.Syntax;
using StegIt.Text;
using StegItCaliburnWay.ViewModels;

namespace StegItCaliburnWay
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly IKernel _kernel = new StandardKernel();

        public Bootstrapper()
        {
            DefineKernelBindings(_kernel);
            Initialize();
        }
        
        protected override void BuildUp(object instance)
        {
            _kernel.Inject(instance);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            if (serviceType != null)
            {
                return _kernel.Get(serviceType);
            }

            throw new ArgumentNullException("serviceType");
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] {Assembly.GetExecutingAssembly()};
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
     
        private static void DefineKernelBindings(IBindingRoot kernel)
        {
            kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
        }
    }
}
