using System;
using System.Collections.ObjectModel;

namespace XamUI.QuizUp
{
    public class QuizUpPageViewModel : BaseViewModel
    {
        public QuizUpPageViewModel()
        {
            _carouselCards = new ObservableCollection<string>
            {
                "Card1",
                "Card2",
                "Card3",
             };
            _progress1 = 50.2f;
        }

        public ObservableCollection<string> _carouselCards;
        public ObservableCollection<string> CarouselCards
        {
            get => Get(_carouselCards);
            set => Set(value);
        }

        public float _progress1;
        public float Progress1
        {
            get => Get(_progress1);
            set => Set(value);
        }

    }
}
