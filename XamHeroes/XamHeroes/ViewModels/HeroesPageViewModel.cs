using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace XamHeroes.ViewModels
{
    public class HeroesPageViewModel : BaseViewModel
    {
        public HeroesPageViewModel()
        {
            GetHeroes();
        }

        async void GetHeroes() 
        {
            if (Heroes?.Count < 1)
                return;
            IsBusy = true;
            var service = new Services.APIService();
            var results = await service.GetAllHeroes();
            //var trimResults = (SplitList<Models.Heroes>(results, 20)).ToList();
            Heroes = new ObservableCollection<Models.Heroes>(results);
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> locations, int nSize = 30)
        {
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }

        public ObservableCollection<Models.Heroes> _heroes;
        public ObservableCollection<Models.Heroes> Heroes
        {
            get => Get(_heroes);
            set => Set(value);
        }

        public bool _isBusy;
        public bool IsBusy
        {
            get => Get(_isBusy);
            set => Set(value);
        }

        public bool _isSearching;
        public bool IsSearching
        {
            get => Get(_isSearching);
            set => Set(value);
        }
    }
}
