using System;
using System.Linq;
using NUnit.Framework;
using Should;
using StructureMap;

namespace All.About.Structure.Map.FeatureToggle
{
    [TestFixture]
    public class FeatureToggleBasedOnOtherService
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

                _.For<ICondition>().Use<Condition>();

                _.For<IService>().Use(c =>
                    c.GetInstance<ICondition>().GetService() == "Service2"
                        ? c.GetInstance<IService>(typeof (Service2).Name)
                        : c.GetInstance<IService>(typeof (Service1).Name));
            });
        }

        [Test]
        public void List_all_registrations()
        {
            Console.WriteLine(_container.WhatDoIHave());
        }

        [Test]
        public void Service_should_resolve_to_service2()
        {
            var type = _container.GetInstance<IService>();
            
            type.ShouldBeType<Service2>();
        }

        [Test]
        public void Should_have_all_instances_registered()
        {
            // two named + one default. 
            _container.GetAllInstances<IService>().Count().ShouldEqual(3);
        }
    }
}
