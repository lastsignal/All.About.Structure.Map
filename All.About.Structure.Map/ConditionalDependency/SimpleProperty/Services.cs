namespace All.About.Structure.Map.ConditionalDependency.SimpleProperty
{
    public interface IService { }

    public class Service1 : IService
    { }

    public class Service2 : IService
    { }

    public class Dependent1
    {
        public IService Service { get; set; }
    }

    public class Dependent2
    {
        public IService Service { get; set; }
    }
}
