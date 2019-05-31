using System;
using System.Windows;
using shuntingYard;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string expression = "";
        private Program calcualtor = new Program();
        public MainWindow()
        {
            InitializeComponent();
            acButton.Click += AcButton_Click;
            percentButton.Click += PercentButton_Click;
            ResultButton.Click += ResultButton_Click;
            
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            
            ResultLabel.Content = calcualtor.Solve(expression);
            
        }

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void AddSubtractButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            string value = sender.ToString();
           
            expression += value[value.Length - 1];

            ResultLabel.Content = value[value.Length-1];
            
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            ResultLabel.Content = "0";
            expression = "0";
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ResultLabel.Content.ToString().Contains("."))
            {
                ResultLabel.Content = $"{ResultLabel.Content}.";
                expression += ".";
            }
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            string buttonInfo = sender.ToString();
            char value = sender.ToString()[buttonInfo.Length-1];
            if (ResultLabel.Content.ToString() == "0")
            {
                ResultLabel.Content = $"{value}";
            }
            else
            {
                ResultLabel.Content = $"{ResultLabel.Content}{value}";
            }
           
            expression += value.ToString();
        }
    }
}
