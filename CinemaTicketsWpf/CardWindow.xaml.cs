using CinemaTickets.Contracts.Request;
using CinemaTickets.Contracts.Response;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CinemaTicketsWpf
{
    /// <summary>
    /// Логика взаимодействия для CardWindow.xaml
    /// </summary>
    public partial class CardWindow : Window
    {
        private PaymentRequest _paymentRequest;
        private decimal _price;
        public CardWindow(PaymentRequest paymentRequest, decimal price)
        {
            InitializeComponent();
            _paymentRequest = paymentRequest;
            _price = price;

            AmountCardTextBlock.Text = $"К оплате: {_price}₽";
        }

        private async void PayButton_Click(object sender, RoutedEventArgs e)
        {
            var cardNumber = CardNumberTextBox.Text;
            var cardDate = CardDateTextBox.Text;
            var cardCVC = CardCVCTextBox.Text;

            if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(cardDate) || string.IsNullOrEmpty(cardCVC))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }
            
            try
            {
                var result = await ProcessPayment(_paymentRequest);
                if (result != null)
                {
                    var receiptWindow = new ReceiptWindow(result);
                    receiptWindow.Show();
                    this.Close();
                }
            }
            
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при оплате: {ex.Message}");
            }

            
        }

        private async Task<PaymentResponse> ProcessPayment(PaymentRequest paymentRequest)
        {
            using (var client = new HttpClient())
            {
                // Укажите правильный URL контроллера оплаты
                var url = "https://localhost:7109/api/Payment/pay";

                var content = new StringContent(JsonConvert.SerializeObject(paymentRequest), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<PaymentResponse>(result);
                }

                throw new Exception("Ошибка при выполнении запроса.");
            }
        }
    }
}
