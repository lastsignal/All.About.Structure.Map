using System;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using Should;
using StructureMap;

namespace All.About.Structure.Map.FeatureToggle
{
    [TestFixture]
    public class FeatureToggleBasedOnAppConfigNamed
    {
        private IContainer _container;

        [OneTimeSetUp]
        public
        void RunsOncePriorRunningAnyTestMethodsWithinThisClass()
        {
            _container = new Container(_ =>
            {
                _.Scan(s =>
                {
                    s.TheCallingAssembly();
                    s.AddAllTypesOf<IService>()
                        .NameBy(type => type.Name);
                });

                _.For<IService>().Use(c =>
                    ConfigurationManager.AppSettings["Feature1"] == "Service2"
                        ? c.GetInstance<IService>(typeof (Service2).Name)
                        : c.GetInstance<IService>(typeof (Service1).Name)
                    );
            });
        }

        [Test]
        public void List_all_registrations()
        {
            Console.WriteLine(_container.WhatDoIHave());
        }

        [Test]
        public void IServiceShouldResolveToService2()
        {
            var type = _container.GetInstance<IService>();
            
            type.ShouldBeType<Service2>();
        }

        [Test]
        public void ShouldHaveAllInstancesRegistered()
        {
            // two named + one default. 
            _container.GetAllInstances<IService>().Count().ShouldEqual(3);
        }
    }
}
