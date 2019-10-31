using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamHeroes
{
    public partial class HeroCard : Grid
    {
        private bool _canExecute = true;
        private Animation _animation;
        private double _initialHeight;

        public HeroCard()
        {
            InitializeComponent();
            _initialHeight = 250;
        }

        private void AnimateRow()
        {
            if (!_canExecute)
                return;
            _canExecute = false;
            try
            {
                if (rowDefinition.Height.Value < _initialHeight)
                {
                    // Move back to original height
                    _animation = new Animation(
                        (d) => rowDefinition.Height = new GridLength(Clamp(d, 0, double.MaxValue)),
                        rowDefinition.Height.Value, _initialHeight, Easing.Linear, () => _animation = null);
                }
                else
                {
                    // Hide the row
                    _animation = new Animation(
                        (d) => rowDefinition.Height = new GridLength(Clamp(d, 0, double.MaxValue)),
                        _initialHeight, 0, Easing.Linear, () => _animation = null);
                }
                _animation.Commit(this, "the animation");

                _canExecute = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return;
            }
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            AnimateRow();
        }

        // Make sure we don't go below zero
        private double Clamp(double value, double minValue, double maxValue)
        {
            if (value < minValue)
            {
                return minValue;
            }

            if (value > maxValue)
            {
                return maxValue;
            }

            return value;
        }
    }
}