using CinemaTickets.Contracts.Request;
using CinemaTickets.Contracts.Response;
using CinemaTickets.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CinemaTicketsWpf
{
    /// <summary>
    /// Логика взаимодействия для TicketPage.xaml
    /// </summary>
    public partial class TicketPage : Window
    {
        private HttpClient _httpClient;
        private Guid _paymentId;

        public TicketPage(Guid paymentId)
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _paymentId = paymentId;

            LoadTicket();
        }

        private async void LoadTicket()
        {
            try
            {
                HallTextBlock.Text = "Зал: ";
                FilmTextBlock.Text = "Название фильма: ";
                StartTimeTextBlock.Text = "Начало сеанса: ";
                RowTextBlock.Text = "Ряд: ";
                SeatTextBlock.Text = "Место: ";
                PriceTextBlock.Text = "Стоимость: ";
                UserNameTextBlock.Text = "Владелец билета: ";
                IssueTimeTextBlock.Text = "Время получения: ";

                var response = await GetTicket(_paymentId);
                HallTextBlock.Text += response.HallName;
                FilmTextBlock.Text += response.FilmName;
                StartTimeTextBlock.Text += response.FilmStartTime.ToString();
                RowTextBlock.Text += response.RowNumber.ToString();
                SeatTextBlock.Text += response.SeatNumber.ToString();
                PriceTextBlock.Text += response.Price.ToString();
                UserNameTextBlock.Text += $"{response.UserName} {response.UserSurname}";
                IssueTimeTextBlock.Text += response.IssueTime.AddHours(3).ToString();

            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Ошибка загрузки билета: {ex.Message}");
            }
        }

        private async Task<TicketResponse> GetTicket(Guid paymentId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7109/api/Payment/get-ticket/ticket/{paymentId}");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TicketResponse>(content);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрываем окно чека
        }
    }
}
