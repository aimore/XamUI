using System;
using System.Threading;
using Xamarin.Forms;

namespace XamHeroes
{
    public class LoadingControl : ContentView, IDisposable
    {
        Image _loadingIcon;
        Label _infoLabel;
        public LoadingControl()
        {
            _loadingIcon = new Image
            {
                Source = "marvel.png",
                HeightRequest = 90,
                WidthRequest = 90,
                HorizontalOptions = LayoutOptions.Center,
            };

            _infoLabel = new Label
            {
                FontSize = 14,
            };

            var layout = new StackLayout { Children = { _loadingIcon, _infoLabel } };
            Content = layout;
            this.HorizontalOptions = LayoutOptions.Center;
            this.VerticalOptions = LayoutOptions.Center;
        }



        #region IsLoadingProperty
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
        #endregion

        CancellationTokenSource _stopAnimation;

        static void HandleIsLoadingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (LoadingControl)bindable;
            if ((bool)newValue)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (control._stopAnimation == null)
                        control._stopAnimation = new CancellationTokenSource();
                    int index = 0;
                    string[] suffix = { ".", "..", "..." };
                    while (control._stopAnimation != null && !control._stopAnimation.IsCancellationRequested)
                    {
                        await control._loadingIcon.RelRotateTo(360, 800, Easing.Linear);
                        control._infoLabel.Text = control.InfoText + suffix[index % 3];
                        index++;
                    }
                    ViewExtensions.CancelAnimations(control._loadingIcon);
                });
            }
            else
            {
                control._stopAnimation.Cancel();
                control._stopAnimation = null;
            }

        }

        #region InfoTextProperty
        public static readonly BindableProperty InfoTextProperty =
            BindableProperty.Create("InfoText",
                                    typeof(string),
                                    typeof(LoadingControl),
                                    "Loading",
                                    propertyChanged: HandleInfoTextChanged
                                   );

        public string InfoText
        {
            get { return (string)GetValue(InfoTextProperty); }
            set { SetValue(InfoTextProperty, value); }
        }

        static void HandleInfoTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (LoadingControl)bindable;
            control._infoLabel.Text = newValue?.ToString();
        }


        #endregion

        public void Dispose()
        {
            if (_stopAnimation != null)
            {
                _stopAnimation.Cancel();
            }
        }
    }
}