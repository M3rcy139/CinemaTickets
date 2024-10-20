using CinemaTickets.Contracts.Request;
using CinemaTickets.Contracts.Response;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace CinemaTicketsWpf
{
    /// <summary>
    /// Логика взаимодействия для PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        private int _seatId;
        private int _seanceId;
        private decimal _price;

        public PaymentWindow(int seatId, int seanceId, decimal price)
        {
            InitializeComponent();
            _seatId = seatId;
            _seanceId = seanceId;
            _price = price;

            AmountTextBlock.Text = $"К оплате: {_price}₽";
        }

        private async void PayButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;
            string email = EmailTextBox.Text;
            string paymentType = (PaymentTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            decimal amountPaid = decimal.Parse(AmountPaidTextBox.Text); // Пользователь платит полную сумму

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(paymentType))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (paymentType == "Наличные")
            {
                MessageBox.Show("Оплата наличными производится на кассе");
                return;
            }

            var paymentRequest = new PaymentRequest
            (
                Name: name,
                Surname: surname,
                Email: email,
                PaymentType: paymentType,
                AmountPaid: amountPaid,
                SeatId: _seatId,
                SeanceId: _seanceId
            );

            try
            {
                var CardWindow = new CardWindow(paymentRequest, _price);
                CardWindow.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при оплате: {ex.Message}");
            }
        }

        private async void Support_Click(object sender, RoutedEventArgs e)
        {
            var supportWindow = new SupportWindow();
            supportWindow.ShowDialog();
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
