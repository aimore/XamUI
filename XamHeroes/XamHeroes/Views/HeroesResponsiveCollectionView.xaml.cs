using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;

namespace XamHeroes
{
    public partial class HeroesResponsiveCollectionView : ScrollView
    {
        //ViewModels.HeroesPageViewModel _vm;
        ObservableCollection<List<Models.Heroes>> b;
        //int i = 0;
        List<Models.Heroes> _heroesList;
        int listCount;
        public HeroesResponsiveCollectionView(List<Models.Heroes> list)
        {
            InitializeComponent();
            _heroesList = list;

            this.Scrolled -= OnScrolled;
            //BindingContext = _vm = new ViewModels.HeroesPageViewModel();
            b = new ObservableCollection<List<Models.Heroes>>(list.SplitList2());
            Device.BeginInvokeOnMainThread(() => {
                BindableLayout.SetItemsSource(stack, b[0]);
            });
            listCount = list.Count;
            this.Scrolled += OnScrolled;
        }

        void OnScrolled(object sender, ScrolledEventArgs e)
        {
            HeroesResponsiveCollectionView scrollView = sender as HeroesResponsiveCollectionView;
            double scrollingSpace = scrollView.ContentSize.Height - scrollView.Height;
            var source = BindableLayout.GetItemsSource(stack) as List<Models.Heroes>;
            //if(source?.Count > 50)
                //return;
            if (scrollingSpace <= e.ScrollY )
            {
                //// Touched bottom
                //// Do the things you want to do
                //if (source.Count >= listCount)
                //    return;
                //System.Diagnostics.Debug.WriteLine($"CHUNCK CURRENT VALUE: {i}");
                //var newList = b[i++];
                //System.Diagnostics.Debug.WriteLine($"CHUNCK NEW VALUE: {i}");
                //_heroesList = source.Concat(newList).ToList();
                //BindableLayout.SetItemsSource(stack, _heroesList);
            }
        }

        public static readonly BindableProperty IsLoadingProperty =
            BindableProperty.Create("IsLoading",
                                    typeof(bool),
                                    typeof(LoadingControl),
                                    false,
                                    propertyChanged: HandleIsLoadingChanged);

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        static void HandleIsLoadingChanged(BindableObject bindable, object oldVal, object newVal)
        {
            var control = bindable as HeroesResponsiveCollectionView;
            System.Diagnostics.Debug.WriteLine($"Is load completed? {newVal}");
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {

        }
    }

    public static class ListExtensions 
    {
        public static IEnumerable<List<T>> SplitList<T>(List<T> locations, int nSize = 30)
        {
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }

        public static List<List<T>> SplitList2<T>(this List<T> me, int size = 50)
        {
            var list = new List<List<T>>();
            for (int i = 0; i < me.Count; i += size)
                list.Add(me.GetRange(i, Math.Min(size, me.Count - i)));
            return list;
        }

        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }
}
