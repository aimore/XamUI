using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamBank
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        List<CardType> source;
        List<Models.MenuItem> list;

        public MainPage()
        {
            InitializeComponent();
            source = new List<CardType>
            {
                CardType.Invoice,                CardType.Transaction,
                CardType.Bonus
            };
            list = new List<Models.MenuItem> {
                new Models.MenuItem
                {
                    Icon = "ic_invite.png",
                    Text = "Indicar" + System.Environment.NewLine + "amigos"
                },
                 new Models.MenuItem
                {
                    Icon = "ic_deposit.png",
                    Text = "Depositar"
                },
                  new Models.MenuItem
                {
                    Icon = "ic_transfer_sent.png",
                    Text = "Transferir"
                },
                   new Models.MenuItem
                {
                    Icon = "ic_payment.png",
                    Text = "Pagar"
                },
            };
            cv.ItemsSource = source;
            BindableLayout.SetItemsSource(flex, list);
        }
    }
}
