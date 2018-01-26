using System;
using NUnit.Framework;
using Should;
using StructureMap;

namespace All.About.Structure.Map.ConditionalDependency.GenericProperty
{
    [TestFixture]
    public class ConditionalDependencyTests
    {
        private IContainer _container;

        [OneTimeSetUp]
        public
        void RunsOncePriorRunningAnyTestMethodsWithinThisClass()
        {
            _container = new Container(_ =>
            {
                _.For(typeof(IService<>)).Add(typeof(Service1<>)).Named("Service1");
                _.For(typeof(IService<>)).Add(typeof(Service2<>)).Named("Service2");

                throw new Exception("cannot set property of open generic");

/*
                expecting to do something like this:
                _.ForConcreteType<Dependent1>()
                    .Configure.SetProperty(p => p.Service = (IService<>) _container.GetInstance(typeof(IService<>), "Service1"));
                _.ForConcreteType<Dependent2>()
                    .Configure.SetProperty(p => p.Service = (IService<>)_container.GetInstance(typeof(IService<>), "Service2"));

                of course this works:
                _.ForConcreteType<Dependent1>()
                    .Configure.SetProperty(p => p.Service = (IService<Model>) _container.GetInstance(typeof(IService<Model>), "Service1"));
                _.ForConcreteType<Dependent2>()
                    .Configure.SetProperty(p => p.Service = (IService<Model>)_container.GetInstance(typeof(IService<Model>), "Service2"));
*/
            });
        }

        [Test]
        public void TestMethod1()
        {
            var dependent1 = _container.GetInstance<Dependent1>();
            dependent1.Service.ShouldBeType<Service1<Model>>();

            var dependent2 = _container.GetInstance<Dependent2>();
            dependent2.Service.ShouldBeType<Service2<Model>>();
        }

    }
}
