using System;
using Xamarin.Forms;

namespace XamBank.Cards
{
    public class CardsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate InvoiceTemplate { get; set; }
        public DataTemplate TransactionTemplate { get; set; }
        public DataTemplate BonusTemplate { get; set; }


        public CardsTemplateSelector()
        {
            InvoiceTemplate = new DataTemplate(typeof(InvoiceCard));
            TransactionTemplate = new DataTemplate(typeof(TransactionsCard));
            BonusTemplate = new DataTemplate(typeof(BonusCard));

        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            CardType selected = (CardType)item;
            switch (selected)
            {
                case CardType.Invoice:
                    return InvoiceTemplate;
                case CardType.Transaction:
                    return TransactionTemplate;
                case CardType.Bonus:
                    return BonusTemplate;
                default:
                    Console.WriteLine("Default case");
                    return InvoiceTemplate;
            }
        }
    }
}
