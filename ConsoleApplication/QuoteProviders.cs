using System;
using System.Configuration;

namespace QuoteChainOfResposibility
{
    public class QuoteParameters
    {
        public long QuoteNumber { get; set; }
    }

    public interface IQuoteProvider
    {
        bool CanHandle(long quoteNumber);
        void GetQuote(QuoteParameters parameters);
    }

    public abstract class QuoteProviderBase : IQuoteProvider
    {
        protected IQuoteProvider NextProvider;

        public abstract bool CanHandle(long quoteNumber);

        protected abstract void Handle();

        public void GetQuote(QuoteParameters parameters)
        {
            if (CanHandle(parameters.QuoteNumber))
                Handle();
            else if (NextProvider != null)
                NextProvider.GetQuote(parameters);
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

        protected override void Handle()
        {
            Console.WriteLine("Handle with standard GetQuote() service call");
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

        protected override void Handle()
        {
            Console.WriteLine("Handle with GetQuote_Legacy_for_EMEA() service call");
        }
    }
}
