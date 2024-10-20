﻿using Newtonsoft.Json;
using System;
using System.Windows.Markup;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using CinemaTickets.Contracts.Response;
using CinemaTickets.Core.Models;
using CinemaTickets.Contracts.Request;
using System.Text;

namespace CinemaTicketsWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient _httpClient;
        private Hall selectedHall;
        private SeanceInfoResponse selectedSeance;
        private List<SeanceInfoResponse> seances;

        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            LoadHalls(); // Загружаем залы при открытии окна
        }

        // Метод для получения залов
        private async void LoadHalls()
        {
            try
            {
                var halls = await GetHalls();
                HallsComboBox.ItemsSource = halls;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading halls: {ex.Message}");
            }
        }

        // Метод для получения сеансов для выбранного зала
        private async void HallsComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (HallsComboBox.SelectedValue != null)
            {
                selectedHall = (Hall)HallsComboBox.SelectedItem;
                await LoadSeances(selectedHall.Id);
            }
        }

        private void SeancesComboBox_SeancesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SeancesComboBox.SelectedValue != null)
            {
                NameTextBlock.Text = "Название фильма:";
                StartTimeTextBlock.Text = "Начало сеанса:";
                DurationTextBlock.Text = "Продолжительность сеанса:";

                selectedSeance = (SeanceInfoResponse)SeancesComboBox.SelectedItem;
                NameTextBlock.Text += $" {selectedSeance.FilmName}";
                StartTimeTextBlock.Text += $" {selectedSeance.StartTime.ToString()}";
                DurationTextBlock.Text += $" {(selectedSeance.EndTime - selectedSeance.StartTime).ToString()}";
            }
        }

        // Метод для загрузки сеансов
        private async Task LoadSeances(int hallId)
        {
            try
            {
                seances = await GetSeances(hallId);
                SeancesComboBox.ItemsSource = seances;
                SeancesComboBox.IsEnabled = true;
                SelectSeatsButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading seances: {ex.Message}");
            }
        }

        // Переход на страницу выбора мест при нажатии кнопки
        private void SelectSeatsButton_Click(object sender, RoutedEventArgs e)
        {
            if (SeancesComboBox.SelectedValue != null)
            {
                int seanceId = (int)SeancesComboBox.SelectedValue;
                int hallId = selectedHall.Id;
                int rows = selectedHall.Rows;
                int columns = selectedHall.Columns;

                // Открываем новое окно для выбора мест
                var seatSelectionWindow = new SeatSelectionWindow(hallId, seanceId, rows, columns);
                seatSelectionWindow.Show();
                this.Close();
            }
            else MessageBox.Show("Выберите сеанс!");
        }

        private async void Support_Click(object sender, RoutedEventArgs e)
        {
            var supportWindow = new SupportWindow();
            supportWindow.ShowDialog();
        }

        // Метод для получения списка залов
        private async Task<List<Hall>> GetHalls()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7109/api/seance/get-halls");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Hall>>(content);
        }

        // Метод для получения списка сеансов по залу
        private async Task<List<SeanceInfoResponse>> GetSeances(int hallId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7109/api/seance/get-seances/hall/{hallId}");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<SeanceInfoResponse>>(content);
        }
    }
}
