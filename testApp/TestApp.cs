using System;
using System.Threading;
using System.Windows.Forms;

namespace testApp
{
    public partial class TestApp : Form
    {
        private CancellationTokenSource _cts;
        private readonly TestService _testService;

        public TestApp()
        {
            InitializeComponent();
            _testService = new TestService();
            _testService.TestCompleted += OnTestCompleted;
        }
        private void TestApp_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "Ожидается ввод...";

            cmbTestSelection.Items.Add("Тест 1 (Температура)");
            cmbTestSelection.Items.Add("Тест 2 (Напряжение и ток)");
            cmbTestSelection.Items.Add("Тест 3 (Частота, КПД, сопротивление)");
            cmbTestSelection.SelectedIndex = 0;

            btnStopTest.Enabled = false;
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
                var result = await _testService.RunTestAsync(cmbTestSelection.SelectedIndex, _cts.Token);
                _testService.SaveResults(txtIdentifier.Text, result);
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
                btnStartTest.Enabled = true;
                btnStopTest.Enabled = false;
            }
        }

        private void btnStopTest_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }

        private void OnTestCompleted(bool completed, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnTestCompleted(completed, message)));
                return;
            }

            if (!completed)
            {
                MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

