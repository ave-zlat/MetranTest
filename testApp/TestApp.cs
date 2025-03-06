using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testApp
{
    public partial class TestApp : Form
    {
        private CancellationTokenSource _cts;

        public TestApp()
        {
            InitializeComponent();
        }

        private async void btnStartTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdentifier.Text))
            {
                MessageBox.Show("Введите идентификатор изделия!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbTestSelection.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите тест!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _cts = new CancellationTokenSource();
            btnStartTest.Enabled = false;
            btnStopTest.Enabled = true;
            lblStatus.Text = "Идет тестирование...";

            try
            {
                var result = await RunTestAsync(cmbTestSelection.SelectedIndex, _cts.Token);
                SaveResults(txtIdentifier.Text, result);
                lblStatus.Text = "Тест завершен.";
                MessageBox.Show("Тест завершен. Результаты сохранены.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                lblStatus.Text = "Тест отменен.";
                MessageBox.Show("Тест был остановлен пользователем.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {

                btnStopTest.Enabled = false;
            }
        }


        private void btnStopTest_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }

        private async Task<TestResult> RunTestAsync(int testIndex, CancellationToken token)
        {
            Random rand = new Random();
            int delay = rand.Next(10, 31) * 1000;
            await Task.Delay(delay, token);

            var result = new TestResult
            {
                Success = rand.Next(2) == 0,
                ErrorMessage = null,
                Data = new StringBuilder()
            };

            if (!result.Success)
            {
                result.ErrorMessage = "Ошибка #" + rand.Next(100, 999);
            }

            switch (testIndex)
            {
                case 0:
                    result.Data.AppendLine($"Температура: {rand.Next(-50, 100)}°C");
                    break;
                case 1:
                    result.Data.AppendLine($"Напряжение: {rand.NextDouble() * 220:F2}V");
                    result.Data.AppendLine($"Ток: {rand.NextDouble() * 10:F2}A");
                    break;
                case 2:
                    result.Data.AppendLine($"Частота: {rand.Next(1000, 5000)} Гц");
                    result.Data.AppendLine($"Коэффициент мощности: {rand.NextDouble():F2}");
                    result.Data.AppendLine($"Сопротивление: {rand.Next(1, 1000)} Ом");
                    break;
            }

            return result;
        }

        private void SaveResults(string productId, TestResult result)
        {
            string directoryPath = "D:\\Worrk\\results";
            Directory.CreateDirectory(directoryPath);

            string filePath = Path.Combine(directoryPath, $"{productId}.txt");
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine($"Результат теста: {(result.Success ? "Успешно" : "Ошибка")}");
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                    writer.WriteLine($"Ошибка: {result.ErrorMessage}");

                writer.WriteLine("Данные:");
                writer.WriteLine(result.Data.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "Ожидается ввод...";

            cmbTestSelection.Items.Add("Тест 1 (Температура)");
            cmbTestSelection.Items.Add("Тест 2 (Напряжение и ток)");
            cmbTestSelection.Items.Add("Тест 3 (Частота, КПД, сопротивление)");
            cmbTestSelection.SelectedIndex = 0;

            btnStopTest.Enabled = false;
        }

        public class TestResult
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }
            public StringBuilder Data { get; set; }
        }
    }
}

