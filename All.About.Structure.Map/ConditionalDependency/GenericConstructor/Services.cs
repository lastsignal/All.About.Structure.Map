namespace All.About.Structure.Map.ConditionalDependency.GenericConstructor
{
    public class Model { }

    public interface IService<T> { }

    public class Service1<T> : IService<T> { }
    public class Service2<T> : IService<T> { }

    public class Dependent1
    {
        private readonly IService<Model> _service;

        public Dependent1(IService<Model> service)
        {
            _service = service;
        }

        public IService<Model> GetService()
        {
            return _service;
        }
    }

    public class Dependent2
    {
        private readonly IService<Model> _service;

        public Dependent2(IService<Model> service)
        {
            _service = service;
        }

        public IService<Model> GetService()
        {
            return _service;
        }
    }
}
