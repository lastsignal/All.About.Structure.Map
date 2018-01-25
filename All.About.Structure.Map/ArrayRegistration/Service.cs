namespace All.About.Structure.Map.ArrayRegistration
{
    public interface IService { }

    public class Service1 : IService
    { }

    public class Service2 : IService
    { }

    public class Service3 : IService
    { }

    public interface IChildService
    {
        IService[] GetServices();
    }

    public class ChildService : IChildService
    {
        private readonly IService[] _services;

        public ChildService(IService[] services)
        {
            _services = services;
        }

        public IService[] GetServices()
        {
            return _services;
        }
    }
}
