using System;
using System.Windows;
using System.Windows.Input;
using shuntingYard;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string expression = "0";
        private Program calcualtor = new Program();
        
        public MainWindow()
        {
            InitializeComponent();
            acButton.Click += AcButton_Click;
           
            ResultButton.Click += ResultButton_Click;
            ResultLabel.Cursor = Cursors.IBeam;
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
                //tan
                case "tan(x)":
                    ResultLabel.Content = $"tan({ResultLabel.Content})";
                    expression = ResultLabel.Content.ToString();
                    break;
                case "tanh(x)":
                    ResultLabel.Content = $"tanh({ResultLabel.Content})";
                    expression = ResultLabel.Content.ToString();
                    break;
                case "atan(x)":
                    ResultLabel.Content = $"atan({ResultLabel.Content})";
                    expression = ResultLabel.Content.ToString();
                    break;
                case "cot(x)":
                    ResultLabel.Content = $"cot({ResultLabel.Content})";
                    expression = ResultLabel.Content.ToString();
                    break;
                case "√":
                    ResultLabel.Content = $"√({ResultLabel.Content})";
                    expression = $"({expression})^0.5";
                    break;
                case "³√":
                    ResultLabel.Content = $"({ResultLabel.Content})^(1/3)";
                    expression = $"({expression})^(1/3)";
                    break;
                case "max(x,y)":
                    insert_char("max(");
                    break;
                case "min(x,y)":
                    insert_char("min(");
                    break;
                case "logB[x](y)":
                    insert_char("logB");
                    break;
                case "log(y)":
                    insert_char("log(");
                    break;
                case "ln(x)":
                    insert_char("ln(");
                    break;
                default:
                    break;
            }
        }

        private void template_function(object sender, RoutedEventArgs e)
        {

        }

        private void insert_char(string item)
        {
            if (ResultLabel.Content.ToString() == "0")
            {
                ResultLabel.Content = item;
            }
            else
            {
                ResultLabel.Content += item;
            }
            expression = ResultLabel.Content.ToString();
        }
        private void constant_function(object sender, RoutedEventArgs e)
        {
            string constant = sender.ToString().Remove(0, 32);
            switch (constant)
            {
                case "π":
                    insert_char("π");
                    
                    break;
                case "eˣ":
                    insert_char("e^");
                    break;
            }
        }

        private void Fraction_Click(object sender, RoutedEventArgs e)
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
            string item = sender.ToString().Remove(0, 32);
            insert_char(item);
            
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
