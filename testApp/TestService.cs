using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static testApp.TestApp;

namespace testApp
{
    class TestService
    {
        public delegate void TestCompletedEventHandler(bool completed, string message);
        public event TestCompletedEventHandler TestCompleted;

        public async Task<TestResult> RunTestAsync(int testIndex, CancellationToken token)
        {
            var random = new Random();
            var delay = random.Next(10, 31) * 1000;
            try
            {
                return await Task.Run(async () =>
                {
                    try
                    {
                        await Task.Delay(delay, token);
                        var result = new TestResult
                        {
                            Success = random.Next(2) == 0,
                            ErrorMessage = null,
                            Data = new StringBuilder()
                        };

                        if (!result.Success)
                        {
                            result.ErrorMessage = "Ошибка #" + random.Next(100, 999);
                        }

                        switch (testIndex)
                        {
                            case 0:
                                result.Data.AppendLine($"Температура: {random.Next(-50, 100)}°C");
                                break;
                            case 1:
                                result.Data.AppendLine($"Напряжение: {random.NextDouble() * 220:F2}V");
                                result.Data.AppendLine($"Ток: {random.NextDouble() * 10:F2}A");
                                break;
                            case 2:
                                result.Data.AppendLine($"Частота: {random.Next(1000, 5000)} Гц");
                                result.Data.AppendLine($"Коэффициент мощности: {random.NextDouble():F2}");
                                result.Data.AppendLine($"Сопротивление: {random.Next(1, 1000)} Ом");
                                break;
                        }
                        
                        TestCompleted?.Invoke(result.Success, result.ErrorMessage ?? "Тест выполнен успешно");
                        
                        return result;
                    }
                    catch (OperationCanceledException)
                    {
                        var result = new TestResult
                        {
                            Success = false,
                            ErrorMessage = "Тест был отменен",
                            Data = new StringBuilder("Операция отменена пользователем")
                        };
                        TestCompleted?.Invoke(false, result.ErrorMessage);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        var result = new TestResult
                        {
                            Success = false,
                            ErrorMessage = $"Произошла ошибка: {ex.Message}",
                            Data = new StringBuilder($"Стек вызовов: {ex.StackTrace}")
                        };
                        TestCompleted?.Invoke(false, result.ErrorMessage);
                        return result;
                    }
                });
            }
            catch (Exception ex)
            {
                var result = new TestResult
                {
                    Success = false,
                    ErrorMessage = $"Критическая ошибка: {ex.Message}",
                    Data = new StringBuilder($"Стек вызовов: {ex.StackTrace}")
                };
                TestCompleted?.Invoke(false, result.ErrorMessage);
                return result;
            }
        }

        public bool SaveResults(string productId, TestResult result)
        {
            if (string.IsNullOrWhiteSpace(productId))
            {
                throw new ArgumentException("ID продукта не может быть пустым", nameof(productId));
            }
            
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result), "Результат теста не может быть null");
            }
            
            try
            {
                string directoryPath = "C:\\TestTaskResults";
                Directory.CreateDirectory(directoryPath);

                var filePath = Path.Combine(directoryPath, $"{productId}.txt");
                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    writer.WriteLine($"Результат теста: {(result.Success ? "Успешно" : "Ошибка")}");
                    if (!string.IsNullOrEmpty(result.ErrorMessage))
                        writer.WriteLine($"Ошибка: {result.ErrorMessage}");
                    writer.WriteLine("Данные:");
                    writer.WriteLine(result.Data.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении результатов: {ex.Message}");
                return false;
            }
        }

        public class TestResult
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }
            public StringBuilder Data { get; set; }
        }
    }
}
