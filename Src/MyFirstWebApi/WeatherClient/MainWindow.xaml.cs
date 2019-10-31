using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using WeatherClient.Services;

namespace WeatherClient
{
    public partial class MainWindow : Window
    {
        private readonly IWeatherService weatherService;

        public MainWindow(IWeatherService weatherService)
        {
            InitializeComponent();

            this.weatherService = weatherService;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ConditionCityTextBlock.Text = null;
            ConditionImage.Source = null;
            ConditionTextBlock.Text = null;
            TemperatureTextBlock.Text = null;

            var weather = await weatherService.GetCurrentWeatherAsync(CountryTextBox.Text, ZipCodeTextBox.Text);

            var weatherInfo = weather.WeatherInfo.FirstOrDefault();
            if (weatherInfo != null)
            {
                ConditionCityTextBlock.Text = weather.Name;
                ConditionImage.Source = new BitmapImage(new Uri(weatherInfo.ConditionIconUrl));
                ConditionTextBlock.Text = weatherInfo.Description;
                TemperatureTextBlock.Text = $"{weather.WeatherDetail.Temperature}°C";
            }
        }
    }
}
