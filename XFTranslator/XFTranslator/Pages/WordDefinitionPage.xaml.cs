using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XFTranslator.Pages
{
    public partial class WordDefinitionPage : ContentPage
    {
        ViewModels.WordDefinitionPageViewModel _vm;
        public WordDefinitionPage(string word)
        {
            InitializeComponent();
            Word.Text = word;
            BindingContext = _vm = new ViewModels.WordDefinitionPageViewModel(word);
            NavigationPage.SetHasBackButton(this, false);
        }
    }
}
