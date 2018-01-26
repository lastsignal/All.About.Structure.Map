namespace All.About.Structure.Map.ConditionalDependency.GenericProperty
{
    public class Model { }

    public interface IService<T> { }

    public class Service1<T> : IService<T> { }
    public class Service2<T> : IService<T> { }

    public class Dependent1
    {
        public IService<Model> Service { get; set; }
    }

    public class Dependent2
    {
        public IService<Model> Service { get; set; }
    }
}
