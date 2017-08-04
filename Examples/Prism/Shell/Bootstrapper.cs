using Microsoft.Practices.Unity;
using Infrastructure;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Footer;
using Header;
using Main;

namespace ShellProj
{
    /// <summary>
    /// In regular c#, objects are instansiated ad-hoc, with classes containing other classes instantiated in 
    /// constructors and other methods etc. With an IOC container, we try to pull out these interdependancies
    /// by passing dependent objects into class constructors or properties via an interface. Essentially
    /// an IOC container is a factory class where one registers interfaces with the class one wishes to 
    /// instantiate with it, and then calls on the container to instantiate an object. One can also 
    /// register an interface with a single instance.
    /// 
    /// The clever part comes when the classes you register have constructors containing inerfaces also registered,
    /// in these cases, the container will instansiate the object using itself to get the constructor 
    /// arguments. This is very useful for writing code which is unit testable.
    /// 
    /// The bootstrapper is essentially where all the classes and accosiated interfaces are registered. In 
    /// Prism, much of the work is already done, so here, we only register the model, and add the various modules.
    /// The specific code to register classes and objects used within modules is handled inside the modules.
    /// </summary>
    class Bootstrapper : UnityBootstrapper
    {
        protected override void InitializeModules()
        {
            //Here we register the instance of the model the front end will hook up to.
            //As we are using RegisterInstance, requests on the container with the IModel 
            //interface will return this instance.
            Container.RegisterInstance<IModel>(new Infrastructure.Model());
            base.InitializeModules();
        }
        protected override DependencyObject CreateShell()
        {
            return new Shell();
        }
        /// <summary>
        /// Viewmodellocator is a prism object which essentaily instansiates viewmodels and wires them up to 
        /// views behind the scenes. It's default rule for this is to look for views in a "Views" folder,
        /// and wire them to the viewmodel classes defined in a ViewModels folder with the same name + "ViewModel".
        /// I'm not particularly keen on this... it's too magic, and generally I favour flat project structures.
        /// but this behavoiur can be overwritten here. In Footer, there is an example of me setting up the viewmodel
        /// to the view explicity in the module class.
        /// </summary>
        protected override void ConfigureViewModelLocator()
        {
            
            base.ConfigureViewModelLocator();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Adding in the various modules here. Here I have done it with explicit references to the modules,
        /// but it is also possible to do this dynamically by telling the bootstrapper to load all modules 
        /// within a folder.
        /// 
        /// Note: If this were a real project, I would probably not have created a seperate project for each section,
        /// and would have done it all in a single project (a todo list is not very big), probably with a single viewmodel.
        /// This is just to illustrate modules in prism.
        /// </summary>
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(FooterModule));
            moduleCatalog.AddModule(typeof(HeaderModule));
            moduleCatalog.AddModule(typeof(MainModule));
        }
    }
}
