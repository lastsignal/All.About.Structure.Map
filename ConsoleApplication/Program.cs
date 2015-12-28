using System;
using StructureMap;

namespace QuoteChainOfResposibility
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(_ =>
            {
                _.For<IQuoteProvider>()
                    .Use<StandardQuoteProvider>()
                    .Ctor<IQuoteProvider>().Is(() => new LegacyQuoteProvider(null));
            });

            var quoteProvider = container.GetInstance<IQuoteProvider>();

            quoteProvider.GetQuote(new QuoteParameters { QuoteNumber = 9999 });
            quoteProvider.GetQuote(new QuoteParameters { QuoteNumber = 10000 });

            Console.ReadKey();
        }
    }
}
