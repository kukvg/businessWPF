using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace businessWPF
{
    /// <summary>
    /// Logika interakcji dla klasy CalcWindow.xaml
    /// </summary>
    public partial class CalcWindow : Window
    {
        public CalcWindow()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void mySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            if (slider != null)
            {
                // zaokragalanie do wartosci calkowitej
                slider.Value = Convert.ToInt32(e.NewValue);
            }
        }

        private void Button_Calc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double plnValue = Convert.ToDouble(valuePln.Text); // pobranie wartosci a textboxa i przekonwertowanie go na int, analogicznie dla kolejnych
                //int financeTime = Convert.ToInt32(timeList.SelectedValue);
                int financeTime = 0;
                double labelStartValue = Convert.ToDouble(LabelStart.Content);
                double labelEndValue = Convert.ToDouble(LabelEnd.Content);

                double emp, emp2;
                double leasingValue = 0;
                double rate = 0.048;

                if (timeList.SelectedItem != null)
                {
                    ListBoxItem selectedItem = (ListBoxItem)timeList.SelectedItem;
                    if (selectedItem != null)
                    {
                        if (int.TryParse(selectedItem.Content.ToString(), out int selectedValue))
                        {
                            financeTime = selectedValue;
                        }
                        else
                        {
                            MessageBox.Show("Nie można przekonwetować.");
                        }
                    }
                    else
                    { 
                        MessageBox.Show("Element jest null.");
                    }
                }
                else
                {
                    MessageBox.Show("Nie wybrano elementu.");
                }
                if (plnValue > 0)
                {
                    leasingValue = plnValue - (plnValue * labelStartValue / 100) - (plnValue * labelEndValue / 100); ; // obliczenie kwoty leasingu - kapital poczatkowy + wplata koncowas
                    emp = (leasingValue * rate) / (1 - Math.Pow(1 + rate, -financeTime)); // obliczenie meisiecznej raty wedlug wzoru miesiecznej raty leasingu (EMP)
                    monthlyPayment.Text = emp.ToString("C"); // przekonwertowanie jako watosc walutowa
                   

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd podczas obliczeń: " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
