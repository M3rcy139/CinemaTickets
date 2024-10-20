using CinemaTickets.Contracts.Response;
using CinemaTickets.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CinemaTicketsWpf
{
    public partial class SeatSelectionWindow : Window
    {
        private HttpClient _httpClient;
        private int _hallId;
        private int _seanceId;
        private int _rows;
        private int _columns;
        private Button _selectedSeatButton;

        public SeatSelectionWindow(int hallId, int seanceId, int row, int columns)
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _hallId = hallId;
            _seanceId = seanceId;
            _rows = row;
            _columns = columns;

            LoadSeats(); // Загружаем места при открытии окна
        }

        // Метод для загрузки мест
        private async void LoadSeats()
        {
            try
            {
                var seats = await GetSeats(_hallId);
                PopulateSeatsGrid(seats);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки мест: {ex.Message}");
            }
        }

        // Метод для заполнения сетки мест
        private async Task PopulateSeatsGrid(List<int> seats)
        {
            try
            {
                int rows = _rows;
                int columns = _columns;

                SeatsGrid.RowDefinitions.Clear();
                SeatsGrid.ColumnDefinitions.Clear();
                SeatsGrid.Children.Clear();

                // Добавляем строки
                for (int i = 0; i < rows; i++)
                {
                    SeatsGrid.RowDefinitions.Add(new RowDefinition());
                }

                // Добавляем столбцы
                for (int i = 0; i < columns; i++)
                {
                    SeatsGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                // Проходим через каждый ряд и место, запрашиваем информацию у контроллера
                for (int i = 0; i < seats.Count; i++)
                {
                    int seatId = seats[i];

                    // Выполняем запрос к контроллеру для каждого места
                    SeatInfoResponse seatInfo = await GetSeatInfo(seatId, _seanceId);

                    // Создаем кнопку в виде кружка
                    var seatButton = new Button
                    {
                        Width = 30,
                        Height = 30,
                        Tag = seatInfo, // Присваиваем seatInfo как тег
                        Margin = new Thickness(5),
                        Background = seatInfo.IsAvailable ? Brushes.White : Brushes.Gray, // Свободные места - белые, занятые - серые
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1),
                        Content = new Ellipse { Fill = seatInfo.IsAvailable ? Brushes.White : Brushes.Gray },
                    };

                    seatButton.Click += SeatButton_Click;

                    // Определяем строку и столбец
                    // Логика для переворота: сверху вниз - это от последнего ряда к первому.
                    int row = rows - 1 - (i / columns); // Определяем строку с конца (5 ряд сверху)
                    int column = columns - 1 - (i % columns); // Определяем столбец с конца (6 место слева)

                    // Устанавливаем позицию места в сетке
                    Grid.SetRow(seatButton, row);
                    Grid.SetColumn(seatButton, column);

                    // Добавляем кнопку в сетку
                    SeatsGrid.Children.Add(seatButton);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading seats: {ex.Message}");
            }
        }

        // Метод обработки клика по месту
        private async void SeatButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedSeatButton = (Button)sender;
            var seat = (SeatInfoResponse)_selectedSeatButton.Tag;

            // Сбрасываем цвет границы у всех кнопок
            foreach (var child in SeatsGrid.Children)
            {
                if (child is Button button)
                {
                    button.BorderBrush = Brushes.Black;
                }
            }

            // Устанавливаем синий цвет границы для выбранной кнопки
            _selectedSeatButton.BorderBrush = Brushes.Blue;

            await LoadSeatInfo(seat.Id, _seanceId);  // Загружаем информацию о месте
        }

        private void OpenPaymentWindowButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedSeatButton?.Tag is SeatInfoResponse seatInfo)
            {
                // Открываем окно оплаты и передаем информацию о выбранном месте и сеансе
                var paymentWindow = new PaymentWindow(seatInfo.Id, _seanceId, seatInfo.Price);
                paymentWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите место для оплаты.");
            }
        }

        private async void Support_Click(object sender, RoutedEventArgs e)
        {
            var supportWindow = new SupportWindow();
            supportWindow.ShowDialog();
        }

        // Метод для получения информации о выбранном месте
        private async Task LoadSeatInfo(int seatId, int seanceId)
        {
            try
            {
                var seatInfo = await GetSeatInfo(seatId, seanceId);
                RowInfo.Text = $"Ряд: {seatInfo.RowNumber}";
                SeatInfo.Text = $"Место {seatInfo.SeatNumber}";
                PriceInfo.Text = $"Стоимость: {seatInfo.Price} рублей";

                string IsAvailable = "";

                if (seatInfo.IsAvailable)
                {
                    IsAvailable = "Место доступно";
                }
                else
                {
                    IsAvailable = "Место недоступно";
                }
                AvailableInfo.Text = $"Доступность места: {IsAvailable}";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading seat info: {ex.Message}");
            }
        }

        // Метод для получения мест
        private async Task<List<int>> GetSeats(int hallId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7109/api/seat/allseats/hall/{hallId}");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<int>>(content);
        }

        // Метод для получения информации о конкретном месте
        private async Task<SeatInfoResponse> GetSeatInfo(int seatId, int seanceId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7109/api/seat/seat-info/{seatId}/{seanceId}");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SeatInfoResponse>(content);
        }

        // Кнопка для возврата к выбору сеансов
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
