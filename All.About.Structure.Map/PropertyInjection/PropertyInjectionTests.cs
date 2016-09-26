using NUnit.Framework;
using Should;
using StructureMap;

namespace All.About.Structure.Map.PropertyInjection
{
    [TestFixture]
    public class PropertyInjectionTests
    {
        private IContainer _container;

        [OneTimeSetUp]
        public void RunsOncePriorRunningAnyTestMethodsWithin_TestFixture_Class()
        {
            _container = new Container(_ =>
            {
                _.For<ICompositeService>().Use<CompositeService>();
                _.For<IBaseService>().Use<BaseService>();
                _.For<IPolicyBaseService>().Use<PolicyBaseService>();

                _.Policies.SetAllProperties(set => set.OfType<IPolicyBaseService>());
            });

        }

        [Test]
        public void Public_property_with_explicit_setter_should_be_set_by_ioc()
        {
            _container.GetInstance<ICompositeService>().GetBaseClass1().ShouldNotBeNull();
        }

        [Test]
        public void Public_property_with_no_decoration_should_not_be_set_by_ioc()
        {
            _container.GetInstance<ICompositeService>().GetBaseClass2().ShouldBeNull();
        }

        [Test]
        public void Public_property_with_setter_policy_should_be_set_by_ioc()
        {
            _container.GetInstance<ICompositeService>().GetPolicyBaseClass().ShouldNotBeNull();
        }
    }
}
