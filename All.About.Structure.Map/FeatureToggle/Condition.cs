namespace All.About.Structure.Map.FeatureToggle
{
    public interface ICondition
    {
        string GetService();
    }

    public class Condition : ICondition
    {
        public string GetService()
        {
            return "Service2";
        }
    }
}
