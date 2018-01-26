using NUnit.Framework;
using Should;
using StructureMap;

namespace All.About.Structure.Map.ConditionalDependency.SimpleConstructor
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
                _.For(typeof(IService)).Add(typeof(Service1));
                _.For(typeof(IService)).Add(typeof(Service2));

                _.ForConcreteType<Dependent1>()
                    .Configure.Ctor<IService>().Is<Service1>();

                _.ForConcreteType<Dependent2>()
                    .Configure.Ctor<IService>().Is<Service2>();
            });
        }

        [Test]
        public void TestMethod1()
        {
            var dependent1 = _container.GetInstance<Dependent1>();
            dependent1.GetService().ShouldBeType<Service1>();

            var dependent2 = _container.GetInstance<Dependent2>();
            dependent2.GetService().ShouldBeType<Service2>();
        }
    }
}
