using System;
using NUnit.Framework;
using Should;
using StructureMap;

namespace All.About.Structure.Map.ConditionalDependency.GenericConstructor
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
                _.For(typeof(IService<>)).Add(typeof(Service1<>));
                _.For(typeof(IService<>)).Add(typeof(Service2<>));

                throw new Exception("cannot call Ctor with open generic");
/*
                something like this needed
                _.ForConcreteType<Dependent1>()
                    .Configure.Ctor<IService<>>().Is<Service1<Model>>();

                _.ForConcreteType<Dependent2>()
                    .Configure.Ctor<IService<>>().Is<Service2<Model>>();

                if you provide a single model, of course, it works
                _.ForConcreteType<Dependent1>()
                    .Configure.Ctor<IService<Model>>().Is<Service1<Model>>();

                _.ForConcreteType<Dependent2>()
                    .Configure.Ctor<IService<Model>>().Is<Service2<Model>>();
*/
            });
        }

        [Test]
        public void TestMethod1()
        {
            var dependent1 = _container.GetInstance<Dependent1>();
            dependent1.GetService().ShouldBeType<Service1<Model>>();

            var dependent2 = _container.GetInstance<Dependent2>();
            dependent2.GetService().ShouldBeType<Service2<Model>>();
        }

    }
}
