using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XSalary
{
    public partial class MainPage : ContentPage
    {

        Dictionary<string, RadioButton> examenBonuses;  
        public MainPage()
        {
            InitializeComponent();
            DeviceDisplay.KeepScreenOn = true;

            examenBonuses = new Dictionary<string, RadioButton>()
            {
                { nameof(examFine), examFine },
                { nameof(examZero), examZero },
                { nameof(examHalf), examHalf },
                { nameof(examFull), examFull },
            };
             
            calculateBtn.Clicked += CalculateBtn_Clicked;
        }
        private double GetDayCoast()
        {

            return Double.Parse(dayCoast.Text);
        }

        private double GetWorkDays()
        {
            if (days.Text == null | days.Text == String.Empty) return 0.0;
            else return Double.Parse(days.Text);
        }

        private double GetOtherBonuses()
        {
            if (otherBonuses.Text == null | otherBonuses.Text == String.Empty) return 0.0;
            else return Double.Parse(otherBonuses.Text);
        }

        private double GetFines()
        {
            if (fines.Text == null | fines.Text == String.Empty) return 0.0;
            else return Double.Parse(fines.Text) * (-1);
        }
        private double GetDelays()
        {
            return delays.IsToggled ? 3000.0 : 0.0;
        }

        private double GetExamBonuses()
        {
            if (examenBonuses["examFine"].IsChecked) return -3000.0;
            else if (examenBonuses["examZero"].IsChecked) return 0.0;
            else if (examenBonuses["examHalf"].IsChecked) return 3000.0;
            else return 6000.0;
        }

        private void CalculateBtn_Clicked(object sender, EventArgs e)
        {
            double oneRubleInLei = 144.0 / 500.0;

            double days = (GetWorkDays() * GetDayCoast());
            double examBonuses = (GetExamBonuses() * oneRubleInLei);
            double delaysBonuses = (GetDelays() * oneRubleInLei);
            double otherBonuses = (GetOtherBonuses() * oneRubleInLei);
            double fines = (GetFines() * oneRubleInLei);

            double result = days + examBonuses + delaysBonuses + otherBonuses + fines;

            DisplayAlert("Ваша зарплата:", result.ToString() + " лей", "Ok");
        }
    }
}
