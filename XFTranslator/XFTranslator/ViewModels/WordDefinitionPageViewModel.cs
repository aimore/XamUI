using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XFTranslator.ViewModels
{
    public class WordDefinitionPageViewModel : BaseViewModel
    {
        public WordDefinitionPageViewModel(string word)
        {
            Word = word;
            Services.ApiService.GetWordDefinitions("awesome")
           .ContinueWith(task =>
           {
               if (task.IsCompleted && task.Status == TaskStatus.RanToCompletion)
               {
                   // Get result and update any UI here.
                   Model = task.Result;
                   if(Model != null) 
                   {

                    }
                   // For property serialized/deserialized using Newtonsoft.Json
               }
               else if (task.IsFaulted)
               {
                   // If any error occurred exception throws.
               }
               else if (task.IsCanceled)
               {
                   // Task cancelled
               }
           }, TaskScheduler.FromCurrentSynchronizationContext())// execute in main/UI thread.
           .ConfigureAwait(false);// Execute API call on background or worker thread. 
        }

        public string _word;
        public string Word
        {
            get => Get(_word);
            set => Set(value);
        }

        public Models.WordDefinitionModel _model;
        public Models.WordDefinitionModel Model
        {
            get => Get(_model);
            set => Set(value);
        }

        public ICommand NavigateToSearchCommand => Cmd() ?? RegCmd(async () =>
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new MainPage());
        }, TimeSpan.FromMilliseconds(300), true, ex => {
            //handle exception if you want
            System.Diagnostics.Debug.WriteLine($"COMMAND ERROR: {ex}");
        });
    }
}
