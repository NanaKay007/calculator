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
           
            ResultButton.Click += ResultButton_Click;
        }

        private void function_click(object sender, RoutedEventArgs e)
        {
            string function = sender.ToString().Remove(0,32);
            switch (function)
            {
                case "abs(x)":
                    ResultLabel.Content = $"abs({ResultLabel.Content})";
                    expression = ResultLabel.Content.ToString();
                    break;
                //sines
                case "sin(x)":
                    ResultLabel.Content = $"sin({ResultLabel.Content})";
                    expression = ResultLabel.Content.ToString();
                    break;
                case "asin(x)":
                    ResultLabel.Content = $"asin({ResultLabel.Content})";
                    expression = ResultLabel.Content.ToString();
                    break;
                case "sinh(x)":
                    ResultLabel.Content = $"sinh({ResultLabel.Content})";
                    expression = ResultLabel.Content.ToString();
                    break;
                case "cosec(x)":
                    ResultLabel.Content = $"cosec({ResultLabel.Content})";
                    expression = ResultLabel.Content.ToString();
                    break;
                //cosines
                case "cos(x)":
                    ResultLabel.Content = $"cos({ResultLabel.Content})";
                    expression = ResultLabel.Content.ToString();
                    break;
                case "cosh(x)":
                    ResultLabel.Content = $"cosh({ResultLabel.Content})";
                    expression = ResultLabel.Content.ToString();
                    break;
                case "acos(x)":
                    ResultLabel.Content = $"acos({ResultLabel.Content})";
                    expression = ResultLabel.Content.ToString();
                    break;
                case "sec(x)":
                    ResultLabel.Content = $"sec({ResultLabel.Content})";
                    expression = ResultLabel.Content.ToString();
                    break;
                default:
                    break;
            }
        }

        private void template_function(object sender, RoutedEventArgs e)
        {

        }

        private void constant_function(object sender, RoutedEventArgs e)
        {

        }

        private void Fraction_Click(object sender, RoutedEventArgs e)
        {

        }

        private void navigation_click(object sender, RoutedEventArgs e)
        {

        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            
            ResultLabel.Content = calcualtor.Solve(expression);
            
        }

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            ResultLabel.Content += "/100";
            expression = ResultLabel.Content.ToString();
        }

        private void simple_operator_click(object sender, RoutedEventArgs e)
        {
            string value = sender.ToString();

            ResultLabel.Content += value[value.Length - 1].ToString();

            expression = ResultLabel.Content.ToString();
            
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            ResultLabel.Content = "0";
            expression = "";
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
                int len = ResultLabel.Content.ToString().Length;
                ResultLabel.Content = $"{ResultLabel.Content.ToString()}{value}";
            }
           
            expression += value.ToString();
        }

        private void BackSpaceButton_Click(object sender, RoutedEventArgs e)
        {
            if(ResultLabel.Content.ToString() != "0" && ResultLabel.Content.ToString().Length > 1)
            {
                ResultLabel.Content = ResultLabel.Content.ToString().Remove(ResultLabel.Content.ToString().Length - 1);
            } else if(ResultLabel.Content.ToString().Length == 1)
            {
                ResultLabel.Content = "0";
            }
            expression = ResultLabel.Content.ToString();
            
        }
        

        private void OpenBrace_Click(object sender, RoutedEventArgs e)
        {
            
            if (ResultLabel.Content.ToString() != "0")
            {
                ResultLabel.Content += "(";
            }
            else
            {
                ResultLabel.Content = "(";
            }
            expression = ResultLabel.Content.ToString();
        }

        private void CloseBrace_Click(object sender, RoutedEventArgs e)
        {
            if(ResultLabel.Content.ToString() != "0")
            {
                ResultLabel.Content += ")";
            } else
            {
                ResultLabel.Content = ")";
            }
            expression = ResultLabel.Content.ToString();
        }

        
    }
}
