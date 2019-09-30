using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Autoverhuur_1._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Public Variables
        public Double Price_Kilometers = 0.20;
        public int Price_DailyRate = 50;
        public Double Price_LitersOfFuel = 1.53;
        public Double Price_Total = 0;
        public bool DatePickerEvent;
        public DateTime Begin;
        public DateTime End;
        public Double Days;

        public double Change_Kilometers = 0;
        public double Change_LitersOfFuel = 0;
        public double Change_Days = 0;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            
            Input_Car.Background = Brushes.LightGreen;
            Input_Van.Background = Brushes.DarkRed;
            Input_Van.Foreground = Brushes.GhostWhite;
            Input_DateStart.SelectedDate = DateTime.Today;
            Input_DateEnd.SelectedDate = DateTime.Today;
        }
        #endregion

        #region Car or Van
        private void Button_Click_Input_Car(object sender, RoutedEventArgs e)
        {
            Input_Car.Background = Brushes.LightGreen;
            Input_Car.Foreground = Brushes.Black;
            Input_Van.Background = Brushes.DarkRed;
            Input_Van.Foreground = Brushes.GhostWhite;
            Price_Kilometers = 0.20;           
            Price_DailyRate = 50;

            Reset();            
        }

        private void Button_Click_Input_Van(object sender, RoutedEventArgs e)
        {
            Input_Car.Background = Brushes.DarkRed;
            Input_Car.Foreground = Brushes.GhostWhite;
            Input_Van.Background = Brushes.LightGreen;
            Input_Van.Foreground = Brushes.Black;
            Price_Kilometers = 0.30;
            Price_DailyRate = 95;

            Reset();
        }
        #endregion

        #region UI Input
        private void TextBox_Input_Kilometers(object sender, TextChangedEventArgs e)
        {
            Kilometers();
        }

        private void TextBox_Input_LitersOfFuel(object sender, TextChangedEventArgs e)
        {
            LitersOfFuel();
        }       

        private void DatePicker_Input_DateStart(Object sender, EventArgs e)
        {
            if (Input_DateStart.SelectedDate.Value != Begin)
            {
                Begin = Input_DateStart.SelectedDate.Value;
                DaysPriceCalc();
            }
        }

        private void DatePicker_Input_DateEnd(Object sender, EventArgs e)
        {
            if (Input_DateEnd.SelectedDate.Value != End)
            {
                End = Input_DateEnd.SelectedDate.Value;
                DaysPriceCalc();
            }           
        }
        #endregion

        #region Functions
        private void Reset()
        {
            Change_Kilometers = 0;
            Change_LitersOfFuel = 0;
            Change_Days = 0;
            Price_Total = 0;
            Kilometers();
            LitersOfFuel();
            DaysPriceCalc();
        }

        private void Kilometers()
        {
            double Test;
            if (Input_Kilometers.Text == "" || Input_Kilometers.Text == "0")
            {
                Test = 0;
            }
            else
            {
                Test = (Double.Parse(Input_Kilometers.Text) - (Days * 100));
            }

            Price_Total = Price_Total - Change_Kilometers;
            Price_Total += Test * Price_Kilometers;

            if (Input_TotalCosts != null)
            { Input_TotalCosts.Content = Price_Total.ToString("C2"); }

            Change_Kilometers = Test * Price_Kilometers;
        }

        private void LitersOfFuel()
        {
            double Test;
            if (Input_LitersOfFuel.Text == "" || Input_LitersOfFuel.Text == "0")
            {
                Test = 0;
            }
            else
            {
                Test = Double.Parse(Input_LitersOfFuel.Text);
            }

            Price_Total = Price_Total - Change_LitersOfFuel;
            Price_Total += Test * Price_LitersOfFuel;

            if (Input_TotalCosts != null)
            { Input_TotalCosts.Content = Price_Total.ToString("C2"); }

            Change_LitersOfFuel = Test * Price_LitersOfFuel;
        }

        private void DaysPriceCalc()
        {
            DaysTotal();

            Price_Total = Price_Total - Change_Days;
            Price_Total += Days * Price_DailyRate;
            Input_TotalCosts.Content = Price_Total.ToString("C2");

            Change_Days = Days * Price_DailyRate;
        }

        private void DaysTotal()
        {
            Days = ((End - Begin).TotalDays) + 1;
            Days = Days > 0 ? Days : 0;
        }
        #endregion
    }
}
