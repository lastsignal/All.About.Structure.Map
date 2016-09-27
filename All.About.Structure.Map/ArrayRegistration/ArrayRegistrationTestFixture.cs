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
            _container.GetAllInstances<IService>().Count().ShouldEqual(2);
        }
    }
}
