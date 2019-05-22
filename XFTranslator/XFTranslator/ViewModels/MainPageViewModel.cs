using System;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using Newtonsoft.Json;
using XFTranslator.Services;

namespace XFTranslator.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
        }

        public string _searchEntryText;
        public string SearchEntryText
        {
            get => Get(_searchEntryText);
            set => Set(value);
        }

        public ICommand TranslateCommand => Cmd() ?? RegCmd(async () =>
        {
            if (!string.IsNullOrEmpty(SearchEntryText) || !string.IsNullOrWhiteSpace(SearchEntryText)) 
            {
                await ApiService.GetWordTranslation(SearchEntryText.ToLower())
                .ContinueWith(async(task) => {
                    if(task.IsCompleted && task.Status == TaskStatus.RanToCompletion) 
                    {
                        // Get result and update any UI here.
                        var responseObj = task.Result;
                        if (responseObj != null)
                        {
                            string json = JsonConvert.SerializeObject(responseObj, Formatting.Indented);
                            System.Diagnostics.Debug.WriteLine(json);
                        }
                        if (responseObj is Models.TranslationModel)
                        {
                            string word = string.Empty;
                            var getWord = ((Models.TranslationModel)responseObj).Outputs.FirstOrDefault(predicate: (Models.Output arg) => {
                                word = arg?.OutputOutput;
                                return true;
                            });
                            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Pages.WordDefinitionPage(word));
                        }
                    }
                    else if (task.IsFaulted)
                    {
                        // If any error occurred exception throws.
                        System.Diagnostics.Debug.WriteLine($"FAULTED : {task.Exception}");

                    }
                    else if (task.IsCanceled)
                    {
                        // Task cancelled
                        System.Diagnostics.Debug.WriteLine($"CANCELED : {task}");
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext())// execute in main/UI thread.
                    .ConfigureAwait(false);// Execute API call on background or worker thread.  
                //var responseObj = await ApiService.GetWordTranslation(SearchEntryText.ToLower());
                //if(responseObj != null)
                //{
                //    string json = JsonConvert.SerializeObject(responseObj, Formatting.Indented);
                //    System.Diagnostics.Debug.WriteLine(json);
                //}
                //if (responseObj is Models.TranslationModel)
                //{
                //    string word = string.Empty;
                //    var getWord = ((Models.TranslationModel)responseObj).Outputs.FirstOrDefault(predicate: (Models.Output arg) => {
                //        word = arg?.OutputOutput;
                //        return true;
                //    });
                //    await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Pages.WordDefinitionPage(word));
                //}
            }

        }, TimeSpan.FromMilliseconds(300), true, ex => {
            //handle exception if you want
        });
    }
}
