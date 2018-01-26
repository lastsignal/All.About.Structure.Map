using NUnit.Framework;
using Should;
using StructureMap;

namespace All.About.Structure.Map.ConditionalDependency.SimpleProperty
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
                _.For<IService>().Add<Service1>().Named("Service1");
                _.For<IService>().Add<Service2>().Named("Service2");

                _.ForConcreteType<Dependent1>()
                    .Configure.SetProperty(p => p.Service = _container.GetInstance<IService>("Service1"));
                _.ForConcreteType<Dependent2>()
                    .Configure.SetProperty(p => p.Service = _container.GetInstance<IService>("Service2"));
            });
        }

        [Test]
        public void TestMethod1()
        {
            var dependent1 = _container.GetInstance<Dependent1>();
            dependent1.Service.ShouldBeType<Service1>();

            var dependent2 = _container.GetInstance<Dependent2>();
            dependent2.Service.ShouldBeType<Service2>();
        }

    }
}
