using StructureMap.Attributes;

namespace All.About.Structure.Map.PropertyInjection
{
    public interface IBaseService
    {}

    public class BaseService : IBaseService
    {}

    public interface IPolicyBaseService
    {}

    public class PolicyBaseService : IPolicyBaseService
    {}

    public interface ICompositeService
    {
        IBaseService GetBaseClass1();
        IBaseService GetBaseClass2();
        IPolicyBaseService GetPolicyBaseClass();
    }

    public class CompositeService : ICompositeService
    {
        [SetterProperty]
        public IBaseService BaseService1 { get; set; }
        public IBaseService BaseService2 { get; set; }
        public IPolicyBaseService PolicyBaseService { get; set; }

        public IBaseService GetBaseClass1()
        {
            return BaseService1;
        }

        public IBaseService GetBaseClass2()
        {
            return BaseService2;
        }
        public IPolicyBaseService GetPolicyBaseClass()
        {
            return PolicyBaseService;
        }
    }
}
