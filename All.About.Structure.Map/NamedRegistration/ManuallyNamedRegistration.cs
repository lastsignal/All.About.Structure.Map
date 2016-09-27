using System;
using NUnit.Framework;
using Should;
using StructureMap;

namespace All.About.Structure.Map.NamedRegistration
{
    [TestFixture]
    public class ManuallyNamedRegistration
    {
        private IContainer _container;

        [OneTimeSetUp]
        public
        void RunsOncePriorRunningAnyTestMethodsWithinThisClass()
        {
            _container = new Container(_ =>
            {
                _.For<IService>().Add<Service1>().Named("First Service");
                _.For<IService>().Use<Service2>().Named("Second Service"); // Default
            });

            /*
             * Alternatively the following could be used which seems to read easier. 

            _container = new Container(_ =>
            {
                _.For<IService>().Add<Service1>().Named("First Service");
                _.For<IService>().Add<Service2>().Named("Second Service");

                _.For<IService>().Use<Service2>(); // Default
            });

             */

        }

        [Test]
        public void List_all_registrations()
        {
            Console.WriteLine(_container.WhatDoIHave());
        }

        [Test]
        public void Getting_instance_of_Service1_should_return_Service1_instance()
        {
            var instance = _container.GetInstance<IService>("First Service");
            instance.GetType().ShouldEqual(typeof(Service1));
        }

        [Test]
        public void Getting_instance_of_Service2_should_return_Service2_instance()
        {
            var instance = _container.GetInstance<IService>("Second Service");
            instance.GetType().ShouldEqual(typeof(Service2));
        }

        [Test]
        public void Getting_instance_of_IService__with_no_name_should_return_Service2_instance()
        {
            var instance = _container.GetInstance<IService>();
            instance.GetType().ShouldEqual(typeof(Service2));
        }
    }
}
