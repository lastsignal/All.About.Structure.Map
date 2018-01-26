namespace All.About.Structure.Map.ConditionalDependency.SimpleConstructor
{
    public interface IService { }

    public class Service1 : IService { }
    public class Service2 : IService { }

    public class Dependent1
    {
        private readonly IService _service;

        public Dependent1(IService service)
        {
            _service = service;
        }

        public IService GetService()
        {
            return _service;
        }
    }

    public class Dependent2
    {
        private readonly IService _service;

        public Dependent2(IService service)
        {
            _service = service;
        }

        public IService GetService()
        {
            return _service;
        }
    }
}
