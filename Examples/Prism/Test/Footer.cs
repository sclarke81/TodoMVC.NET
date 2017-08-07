using System;
using System.ComponentModel;
using Footer;
using Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Unity;
using Xunit;

namespace Test
{
    public class Footer
    { 
        IUnityContainer _container;
        public Footer()
        {
            _container = new UnityContainer();
            _container.RegisterInstance<IModel>(new Model());
            _container.RegisterInstance<IEventAggregator>(new EventAggregator());
            _container.RegisterType<FooterViewModel>();
        }

        [Fact]
        public void TestClearAllFiresEvent()
        {
            var model = _container.Resolve<IModel>();
            var eventAggregator = _container.Resolve<IEventAggregator>();
            bool eventHit = false;
            eventAggregator.GetEvent<Infrastructure.ToDoListChanged>()
                .Subscribe(() => eventHit = true);
            var viewModel = _container.Resolve<FooterViewModel>();

            viewModel.ClearAllCommand.Execute(null);
            Assert.True(eventHit);
        }

        [Fact]
        public void TestClearAllClearsTodos()
        {
            var model = _container.Resolve<IModel>();
            model.ToDos.Add(new ToDo() { State = false });
            model.ToDos.Add(new ToDo() { State = true });
      
            var viewModel = _container.Resolve<FooterViewModel>();

            viewModel.ClearAllCommand.Execute(null);
            Assert.Single(model.ToDos);
        }
    }
}
