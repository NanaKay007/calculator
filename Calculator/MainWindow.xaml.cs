using System;
using System.Collections.Generic;
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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber,result;
        SelectedOperator SelectedOperator;

        public MainWindow()
        {
            InitializeComponent();
            acButton.Click += AcButton_Click;
            percentButton.Click += PercentButton_Click;
            ResultButton.Click += ResultButton_Click;
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;

            if (double.TryParse(ResultLabel.Content.ToString(), out newNumber))
            {
                
                switch (SelectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNumber,newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        result = SimpleMath.Subtract(lastNumber,newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNumber,newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Divide(lastNumber,newNumber);
                        break;
                    default:
                        break;
                }
                ResultLabel.Content = result.ToString();
                Console.WriteLine("in here");
                Console.WriteLine(result);
            }
            
        }

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber /100;
                ResultLabel.Content = lastNumber.ToString();
            }
        }

        private void AddSubtractButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                ResultLabel.Content = lastNumber.ToString();
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLabel.Content.ToString(), out lastNumber))
            {
                
                ResultLabel.Content = "0";
            }

            if (sender == multiplyButton)
                SelectedOperator = SelectedOperator.Multiplication;
            if (sender == divideButton)
                SelectedOperator = SelectedOperator.Division;
            if (sender == addButton)
                SelectedOperator = SelectedOperator.Addition;
            if (sender == subtractButton)
                SelectedOperator = SelectedOperator.Subtraction;
            
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            ResultLabel.Content = "0";
            lastNumber = 0;
            result = 0;
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ResultLabel.Content.ToString().Contains("."))
            {
                ResultLabel.Content = $"{ResultLabel.Content}.";
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
        }
    }

    public class SimpleMath
    {
       public static double Add(double n1,double n2)
        {
            return (n1 + n2);
        }

       public static double Subtract(double n1,double n2)
        {
            return n1 - n2;
        }

       public static double Multiply(double n1, double n2)
        {
            return n1 * n2;
        }

       public static double Divide(double n1,double n2)
        {
            return n1 / n2;
        }
    }


    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }
}
