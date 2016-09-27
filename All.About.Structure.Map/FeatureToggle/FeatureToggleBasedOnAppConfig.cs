using System;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using Should;
using StructureMap;

namespace All.About.Structure.Map.FeatureToggle
{
    [TestFixture]
    public class FeatureToggleBasedOnAppConfig
    {
        private IContainer _container;

        [OneTimeSetUp]
        public
        void RunsOncePriorRunningAnyTestMethodsWithinThisClass()
        {
            _container = new Container(_ =>
            {
                if(ConfigurationManager.AppSettings["Feature1"] == "Service2")
                    _.For<IService>().Use<Service2>();
                else
                    _.For<IService>().Use<Service1>();
                    
            });
        }

        [Test]
        public void ListRegistrations()
        {
            Console.WriteLine(_container.WhatDoIHave());
        }

        [Test]
        public void ServiceShouldResolveToService2()
        {
            var type = _container.GetInstance<IService>();
            
            type.ShouldBeType<Service2>();
        }

        [Test]
        public void OnlyOneInstanceShouldBeRegistered()
        {
            _container.GetAllInstances<IService>().Count().ShouldEqual(1);
        }

    }
}
