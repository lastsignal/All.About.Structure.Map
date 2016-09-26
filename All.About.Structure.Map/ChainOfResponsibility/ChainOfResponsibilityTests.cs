using NUnit.Framework;
using Should;
using StructureMap;

namespace All.About.Structure.Map.ChainOfResponsibility
{
    [TestFixture]
    public class ChainOfResponsibilityTests
    {
        private IContainer _container;

        [OneTimeSetUp]
        public void RunsOncePriorRunningAnyTestMethodsWithin_TestFixture_Class()
        {
            _container = new Container(_ =>
            {
                _.For<IQuoteProvider>()
                    .Use<StandardQuoteProvider>()
                    .Ctor<IQuoteProvider>().Is(() => new LegacyQuoteProvider(null));
            });
        }

        [OneTimeTearDown]
        public void RunsOnceAfterRunningAnyTestMethodsWithin_TestFixture_Class()
        {
            _container.Dispose();
        }

        [Test]
        public void TestMethod()
        {
            var quoteProvider = _container.GetInstance<IQuoteProvider>();

            quoteProvider.GetQuote(new QuoteParameters { QuoteNumber = 9999 }).ShouldEqual("Standard");
            quoteProvider.GetQuote(new QuoteParameters { QuoteNumber = 10000 }).ShouldEqual("Legacy");
        }
    }
}
