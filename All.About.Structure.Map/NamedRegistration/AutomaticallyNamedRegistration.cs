using System;
using System.Linq;
using NUnit.Framework;
using Should;
using StructureMap;

namespace All.About.Structure.Map.NamedRegistration
{
    [TestFixture]
    public class AutomaticallyNamedRegistration
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
                    s.AddAllTypesOf<IService>().NameBy(type => type.Name);
                });
            });
        }

        [Test]
        public void List_all_registrations()
        {
            Console.WriteLine(_container.WhatDoIHave());
        }

        [Test]
        public void Getting_instance_of_Service1_should_return_Service1_instance()
        {
            var instance = _container.GetInstance<IService>("Service1");
            instance.GetType().ShouldEqual(typeof (Service1));
        }

        [Test]
        public void Getting_instance_of_Service2_should_return_Service2_instance()
        {
            var instance = _container.GetInstance<IService>("Service2");
            instance.GetType().ShouldEqual(typeof (Service2));
        }
    }
}
