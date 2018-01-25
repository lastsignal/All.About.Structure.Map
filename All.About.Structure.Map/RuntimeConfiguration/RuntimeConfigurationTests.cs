using System.Linq;
using NUnit.Framework;
using Should;
using StructureMap;

namespace All.About.Structure.Map.RuntimeConfiguration
{
    [TestFixture]
    public class RuntimeConfigurationTests
    {
        [OneTimeSetUp]
        public
        void RunsOncePriorRunningAnyTestMethodsWithinThisClass()
        {
        }

        [Test]
        public void TestMethod1()
        {
            var container = new Container(_ =>
            {
                _.For<IService>().Use<Service1>();
            });

            var allInstances = container.GetAllInstances<IService>().ToList();
            allInstances.ShouldNotBeNull();
            allInstances.Count.ShouldEqual(1);
            allInstances.First().ShouldBeType(typeof(Service1));

            container.GetInstance<IService>().ShouldBeType(typeof(Service1));

            container.Configure(_ =>
                _.For<IService>().Use<Service2>()   // THIS WILL ADD A NEW INSTANCE
            );

            allInstances = container.GetAllInstances<IService>().ToList();
            allInstances.ShouldNotBeNull();
            allInstances.Count.ShouldEqual(2);

            container.GetInstance<IService>().ShouldBeType(typeof(Service2));

        }

        [Test]

        public void TestMethod2()
        {
            var container = new Container(_ =>
            {
                _.For<IService>().Use<Service1>();
            });

            var allInstances = container.GetAllInstances<IService>().ToList();
            allInstances.ShouldNotBeNull();
            allInstances.Count.ShouldEqual(1);
            allInstances.First().ShouldBeType(typeof(Service1));

            container.GetInstance<IService>().ShouldBeType(typeof(Service1));

            container.Inject<IService>(new Service2());  // THIS WILL STILL ADD A NEW INSTANCE AND DOESN'T REPLACE

            allInstances = container.GetAllInstances<IService>().ToList();
            allInstances.ShouldNotBeNull();
            allInstances.Count.ShouldEqual(2);

            container.GetInstance<IService>().ShouldBeType(typeof(Service2));
        }

        [Test]
        public void TestMethod3()
        {
            var container = new Container(_ =>
            {
                _.For<IService>().Use<Service1>();
            });

            var allInstances = container.GetAllInstances<IService>().ToList();
            allInstances.ShouldNotBeNull();
            allInstances.Count.ShouldEqual(1);
            allInstances.First().ShouldBeType(typeof(Service1));

            container.GetInstance<IService>().ShouldBeType(typeof(Service1));

            container.EjectAllInstancesOf<IService>();
            container.Configure(_ =>
                _.For<IService>().Use<Service2>()   // THIS WILL ADD A BRAND NEW INSTANCE TO THE EMPTY LIST
            );

            allInstances = container.GetAllInstances<IService>().ToList();
            allInstances.ShouldNotBeNull();
            allInstances.Count.ShouldEqual(1);

            container.GetInstance<IService>().ShouldBeType(typeof(Service2));

        }

        [Test]
        public void When_eject_a_service_no_other_services_should_be_affected()
        {
            var container = new Container(_ =>
            {
                _.For<IService>().Use<Service1>();
                _.For<IAdditionalService>().Use<AdditionalService>();
            });

            container.GetInstance<IService>().ShouldBeType(typeof(Service1));
            container.GetInstance<IAdditionalService>().ShouldBeType(typeof(AdditionalService));

            container.EjectAllInstancesOf<IService>();

            Assert.Throws<StructureMapConfigurationException>(() =>
            {
                container.GetInstance<IService>();
            });
            container.GetInstance<IAdditionalService>().ShouldBeType(typeof(AdditionalService));
        }
    }
}
