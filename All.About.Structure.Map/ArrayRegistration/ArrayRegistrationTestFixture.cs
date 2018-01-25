using System;
using System.Linq;
using NUnit.Framework;
using Should;
using StructureMap;

namespace All.About.Structure.Map.ArrayRegistration
{
    [TestFixture]
    public class ArrayRegistrationTestFixture
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
                    s.AddAllTypesOf<IService>();
                });
                _.For<IChildService>().Use<ChildService>();
            });
        }

        [Test]
        public void ListRegistrations()
        {
            Console.WriteLine(_container.WhatDoIHave());
        }

        [Test]
        public void All_instances_should_be_registered()
        {
            _container.GetAllInstances<IService>().Count().ShouldEqual(3);
        }

        [Test]
        public void Regular_array_dependency_should_have_all_instances()
        {
            var childService = _container.GetInstance<IChildService>();

            childService.GetServices().Length.ShouldEqual(3);
        }

        [Test]
        public void Special_array_dependency_should_have_specific_instances()
        {
            _container.Configure(_ =>
            {
                _.For<IChildService>()
                    .Use<ChildService>()
                    .Ctor<IService[]>()
                    .Is(i => new IService[]
                    {
                        new Service1(),
                        new Service2()
                    });
            });

            var childService = _container.GetInstance<IChildService>();

            childService.GetServices().Length.ShouldEqual(2);
        }
    }
}
