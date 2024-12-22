using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
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

namespace KP
{
    public partial class MainWindow : Window
    {
        private SerialPort serialPort;
        private bool errorMessageShown = false;  // Флаг, показывающий было ли сообщение об ошибке показано.

        public MainWindow()
        {
            InitializeComponent();
            serialPort = new SerialPort("COM3", 9600); // Измените на ваш порт и настройки
            serialPort.DataReceived += SerialPort_DataReceived; // Подписка на событие получения данных
            serialPort.Open();
        }

        // Обработчик события получения данных из последовательного порта
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string message = serialPort.ReadLine(); // Чтение строки
            Dispatcher.Invoke(() => UpdateUI(message)); // Обновление UI в главном потоке
        }

        // Метод обновления пользовательского интерфейса
        private void UpdateUI(string message)
        {
if (message.Trim() == "ERROR")
            {
                // Проверяем, если сообщение об ошибке еще не было показано
                if (!errorMessageShown)
                {
                    MessageBox.Show("Активация тревоги!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    errorMessageShown = true; // Устанавливаем флаг, что сообщение показано
                }
            }
        }

        // Остальные методы остаются без изменений...

        private void ActivateGY_Checked(object sender, RoutedEventArgs e)
        {
            serialPort.WriteLine("ACTIVATE_GY"); // Отправляем команду активации
            errorMessageShown = false; // Сбрасываем флаг, когда активно включается модуль
        }

        private void DeactivateGY_Checked(object sender, RoutedEventArgs e)
        {
            serialPort.WriteLine("DEACTIVATE_GY"); // Отправляем команду деактивации
        }

        private void ActivateParkingSensors_Checked(object sender, RoutedEventArgs e)
        {
            serialPort.WriteLine("ACTIVATE_US"); // Отправляем команду активации ультразвукового датчика
            errorMessageShown = false; // Сбрасываем флаг, когда активно включается модуль
        }

        private void DeactivateParkingSensors_Checked(object sender, RoutedEventArgs e)
        {
            serialPort.WriteLine("DEACTIVATE_US"); // Отправляем команду деактивации ультразвукового датчика
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            serialPort.Close(); // Закрываем последовательный порт при закрытии окна
        }
    }
}