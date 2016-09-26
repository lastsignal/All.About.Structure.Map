using System.Configuration;

namespace All.About.Structure.Map.ChainOfResponsibility
{
    public class QuoteParameters
    {
        public long QuoteNumber { get; set; }
    }

    public interface IQuoteProvider
    {
        bool CanHandle(long quoteNumber);
        string GetQuote(QuoteParameters parameters);
    }

    public abstract class QuoteProviderBase : IQuoteProvider
    {
        protected IQuoteProvider NextProvider;

        public abstract bool CanHandle(long quoteNumber);

        protected abstract string Handle();

        public string GetQuote(QuoteParameters parameters)
        {
            if (CanHandle(parameters.QuoteNumber))
                return Handle();
            else if (NextProvider != null)
                return NextProvider.GetQuote(parameters);
            else
                throw new ConfigurationErrorsException("There is no configured QuoteProvider with the provided parameters to handle the case");
        }
    }

    public class StandardQuoteProvider : QuoteProviderBase
    {

        public StandardQuoteProvider(IQuoteProvider nextProvider)
        {
            NextProvider = nextProvider;
        }

        public override bool CanHandle(long quoteNumber)
        {
            return quoteNumber < 10000;
        }

        protected override string Handle()
        {
            return "Standard";
        }
    }

    public class LegacyQuoteProvider : QuoteProviderBase
    {
        public LegacyQuoteProvider(IQuoteProvider nextProvider)
        {
            NextProvider = nextProvider;
        }

        public override bool CanHandle(long quoteNumber)
        {
            return quoteNumber >= 10000;
        }

        protected override string Handle()
        {
            return "Legacy";
        }
    }
}
