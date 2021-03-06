﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shuntingYard
{
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public class Operator
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
    {
        private int precedence, associativity;
        private string name;
        private bool isFunction;

        public Operator(string symbol)
        {
            name = symbol;


            switch (symbol)
            {
                case "abs":
                    isFunction = true;
                    break;
                case "sin":
                    isFunction = true;
                    break;
                case "asin":
                    isFunction = true;
                    break;
                case "sinh":
                    isFunction = true;
                    break;
                case "max":
                    isFunction = true;
                    break;
                case "min":
                    isFunction = true;
                    break;
                case "log":
                    isFunction = true;
                    break;
                case "cos":
                    isFunction = true;
                    break;
                case "acos":
                    isFunction = true;
                    break;
                case "cosh":
                    isFunction = true;
                    break;
                case "tan":
                    isFunction = true;
                    break;
                case "atan":
                    isFunction = true;
                    break;
                case "tanh":
                    isFunction = true;
                    break;
                case "ln":
                    isFunction = true;
                    break;
                //log with base
                case "logB":
                    isFunction = true;
                    break;
                case "*":
                    associativity = 1;
                    precedence = 3;
                    break;
                case "/":
                    associativity = 1;
                    precedence = 3;
                    break;
                case "+":
                    associativity = 1;
                    precedence = 2;
                    break;
                case "-":
                    associativity = 1;
                    precedence = 2;
                    break;
                case "^":
                    associativity = -1;
                    precedence = 4;
                    break;
                case "(":
                    precedence = 14;
                    break;
                default:
                    if (symbol.Contains("logB"))
                    {
                        isFunction = true;
                        break;
                    }
                    throw new Exception("invalid operator");
            }
        }

        public bool IsFunction() => isFunction;

        public string GetName() => name;

        public int GetPrecedence()
        {
            return precedence;
        }

        public static bool operator >(Operator self, Operator other)
        {
            return self.GetPrecedence() > other.GetPrecedence();
        }

        public static bool operator <(Operator self, Operator other)
        {
            return self.precedence < other.GetPrecedence();
        }

        public static bool operator ==(Operator self, Operator other)
        {
            return self.precedence == other.GetPrecedence();
        }

        public static bool operator !=(Operator self, Operator other)
        {
            return self.precedence == other.GetPrecedence();
        }
        public int GetAssociativity()
        {
            return associativity;
        }
        public override string ToString()
        {
            return name;
        }
    }


    public class Program
    {

        public Queue<string> ShuntingYardAlgorithm(string var)
        {
            /*accepts an infix expression as a string; converts it into a posfix expression
             * @param : a non-null string of length 0 to max
             *             
             * @return: a Queue containing tokens of the postfix equivalent of the input expression
             */
            Queue<string> Output = new Queue<string>();
            Stack<Operator> Operators = new Stack<Operator>();

            List<string> tokens = Tokenize(var);

            foreach (string x in tokens)
            {


                if (float.TryParse(x.ToString(), out float number))
                {
                    Output.Enqueue(number.ToString());
                }
                else if (x == ",")
                {
                    //do nothing...maybe not
                    while (Operators.Peek().GetName() != "(")
                    {
                        Output.Enqueue(Operators.Pop().ToString());
                    }
                }
                else
                {
                    if (x != ")")
                    {
                        Operator token = new Operator(x);

                        if (token.IsFunction())
                        {
                            Operators.Push(token);
                        }
                        else if (x == "(")
                        {
                            Operators.Push(token);
                        }
                        else
                        {
                            bool breakout = false;
                            while (!breakout && Operators.Count != 0)
                            {
                                if ((Operators.Peek() > token) && Operators.Peek().GetName() != "(" || Operators.Peek().IsFunction() || (Operators.Peek() == token) && Operators.Peek().GetAssociativity() == 1)
                                {
                                    Operator current = Operators.Pop();
                                    Output.Enqueue(current.ToString());
                                }
                                else
                                {
                                    breakout = true;
                                }
                            }

                            Operators.Push(token);
                        }


                    }
                    else
                    {
                        while (Operators.Peek().GetName() != "(")
                        {
                            Output.Enqueue(Operators.Pop().ToString());
                        }
                        if (Operators.Peek().GetName() == "(")
                        {
                            Operators.Pop();
                        }


                    }



                }

            }

            while (Operators.Count != 0)
            {
                Operator @operator = Operators.Pop();
                if (@operator.GetName() != "(")
                {
                    Output.Enqueue(@operator.ToString());
                }

            }



            return Output;


        }

        public List<string> Tokenize(string expression)
        {
            //purpose: separates a string expression of numbers and operators into tokens; also simplifies an expression by
            //removing excess operators
            //params: a string expression
            //return: a list of tokens; throws an error if an invalid token is encountered in the expression


            Regex numberRegex = new Regex(@"\d+\.?\d*");
            Regex operatorRegex = new Regex(@"[\+\^\-\/\*\)\(,]|(logB\[\d+\.?\d*\])|(log)|(max)|(min)|(ln)|(cosh)|(cos)|(acos)|(sin)|(sinh)|(asin)|(tan)|(atan)|(tanh)");


            MatchCollection numberMatches = numberRegex.Matches(expression);
            MatchCollection operatorMatches = operatorRegex.Matches(expression);

            string[] operatorsArray = { "+", "-", "/", "*", "^" };

            int size = numberMatches.Count + operatorMatches.Count;

            List<string> tokens = new List<string>();

            Queue<Match> numbers = new Queue<Match>();
            Queue<Match> operators = new Queue<Match>();

            foreach (Match number in numberMatches)
            {
                numbers.Enqueue(number);
            }

            foreach (Match oper in operatorMatches)
            {
                operators.Enqueue(oper);
            }
            int last_added_index = 0;

            for (int i = 0; i < size; i++)
            {
                Match number;
                Match oper;


                _ = numbers.Count != 0 ? number = numbers.Peek() : number = null;
                _ = operators.Count != 0 ? oper = operators.Peek() : oper = null;


                if (numbers.Count != 0)
                {
                    if (oper != null && number != null)
                        if (number.Index < oper.Index && number.Index >= last_added_index)
                        {
                            if (tokens.Count != 0)
                            {
                                string beforelastitem = "";
                                string lastitem = tokens[tokens.Count - 1];

                                _ = tokens.Count >= 2 ? beforelastitem = tokens[tokens.Count - 2] : null;

                                if ((beforelastitem == "^" || beforelastitem == "/" || beforelastitem == ")" || beforelastitem == "(" || beforelastitem == "*") && lastitem == "-")
                                {
                                    tokens[tokens.Count - 1] += number.ToString();
                                }
                                else
                                {
                                    last_added_index = number.Index + number.Length;
                                    tokens.Add(number.ToString());
                                }
                            }
                            else
                            {
                                last_added_index = number.Index + number.Length;
                                tokens.Add(number.ToString());
                            }


                            numbers.Dequeue();
                        }
                        else if (number.Index < oper.Index && number.Index <= last_added_index)
                        {
                            numbers.Dequeue();
                        }

                }
                if (operators.Count != 0)
                {


                    if (oper != null && number != null)
                        if (number.Index > oper.Index && oper.Index >= last_added_index)
                        {
                            if (tokens.Count != 0)
                            {
                                //if last item is an operator, replace it with oper,else just add oper
                                string lastitem = tokens[tokens.Count - 1];
                                if (operatorsArray.Contains(lastitem))
                                {
                                    //if the last operator was a - and the incoming is a -, replace the first with a +
                                    //if the incoming was a +, do no addition
                                    if (oper.ToString() == "(" || oper.ToString() == ")")
                                    {
                                        last_added_index = oper.Index + oper.Length;
                                        tokens.Add(oper.ToString());
                                    }

                                    else if (lastitem == "-")
                                    {
                                        _ = oper.ToString() == "-" ? tokens[tokens.Count - 1] = "+" : tokens[tokens.Count - 1] = oper.ToString();
                                    }
                                    else if (lastitem == "+")
                                    {
                                        tokens[tokens.Count - 1] = oper.ToString();

                                    }
                                    else if (lastitem == "*" || lastitem == "/")
                                    {
                                        //if incoming is a +, leave * in place
                                        //if incoming is a -, add - to tokens
                                        //if incoming is a ^, repla
                                        if (oper.ToString() == "-")
                                        {
                                            last_added_index = oper.Index + oper.Length;
                                            tokens.Add(oper.ToString());
                                        }
                                        else if (oper.ToString() == "^")
                                        {
                                            tokens[tokens.Count - 1] = "^";
                                        }
                                        else if (oper.ToString() == "+")
                                        {

                                        }

                                        else
                                        {
                                            last_added_index = oper.Index + oper.Length;
                                            tokens.Add(oper.ToString());
                                        }
                                    }
                                    else
                                    {
                                        last_added_index = oper.Index + oper.Length;
                                        tokens.Add(oper.ToString());
                                    }


                                    operators.Dequeue();
                                }
                                else
                                {
                                    last_added_index = oper.Index + oper.Length;
                                    tokens.Add(oper.ToString());
                                    operators.Dequeue();
                                }

                            }
                            else
                            {
                                last_added_index = oper.Index + oper.Length;
                                tokens.Add(oper.ToString());
                                operators.Dequeue();
                            }

                        }
                        else if (oper.Index > number.Index && oper.Index >= last_added_index)
                        {
                            tokens.Add(oper.ToString());
                            last_added_index = oper.Index + oper.Length;
                            operators.Dequeue();
                        }
                }
                if (oper == null && number != null)
                {
                    if (tokens.Count != 0 && number.Index >= last_added_index)
                    {
                        string beforelastitem = "";
                        string lastitem = tokens[tokens.Count - 1];

                        _ = tokens.Count >= 3 ? beforelastitem = tokens[tokens.Count - 2] : null;

                        if (beforelastitem == "^" || beforelastitem == "/" || beforelastitem == "*" && lastitem == "-")
                        {
                            tokens[tokens.Count - 1] += number.ToString();
                        }
                        else
                        {
                            last_added_index = number.Index + number.Length;
                            tokens.Add(number.ToString());
                        }
                    }
                    else
                    {
                        last_added_index = number.Index + number.Length;
                        tokens.Add(number.ToString());
                    }

                    numbers.Dequeue();
                }
                else if (oper != null && number == null)
                {
                    if (tokens.Count != 0 && oper.Index >= last_added_index)
                    {
                        //if last item is an operator, replace it with oper,else just add oper
                        string lastitem = tokens[tokens.Count - 1];
                        if (operatorsArray.Contains(lastitem))
                        {
                            tokens[tokens.Count - 1] = oper.ToString();
                            operators.Dequeue();
                        }
                        else
                        {
                            last_added_index = oper.Index + oper.Length;
                            tokens.Add(oper.ToString());
                            operators.Dequeue();
                        }

                    }
                    else
                    {
                        last_added_index = oper.Index + oper.Length;
                        tokens.Add(oper.ToString());
                        operators.Dequeue();
                    }
                }


            }

            return tokens;
        }

        public float HandleMath(float n1, float n2, string Operator)
        {
            switch (Operator)
            {
                case ("+"):
                    return n1 + n2;

                case ("-"):
                    return n1 - n2;

                case ("*"):
                    return n1 * n2;
                case ("/"):
                    return n1 / n2;
                case ("^"):
                    double second = (double)n2;
                    double first = (double)n1;
                    double ans = Math.Pow(first, second);
                    return (float)ans;
                case "max":
                    return Math.Max(n1, n2);
                case "min":
                    return Math.Min(n1, n2);

                default:
                    return 0;

            }
        }

        public double HandleOneInputMath(float n1, string Operator)
        {
            //purpose: calculates the result of a one-input function
            //params: float and operator
            //return: the result, float

            switch (Operator)
            {
                case "abs":
                    return Math.Abs(n1);
                case "sin":

                    return Math.Round(Math.Sin(Math.PI * n1 / 180), 15);
                case "asin":
                    return Math.Round(Math.Asin(Math.PI * n1 / 180), 15);
                case "sinh":
                    return Math.Round(Math.Sinh(Math.PI * n1 / 180), 15);


                case "cos":
                    return Math.Round(Math.Cos(Math.PI * n1 / 180), 15);
                case "acos":
                    return Math.Round(Math.Acos(Math.PI * n1 / 180), 15);
                case "cosh":
                    return Math.Round(Math.Cosh(Math.PI * n1 / 180), 15);

                case "tan":
                    return Math.Round(Math.Tan(Math.PI * n1 / 180), 15);
                case "atan":
                    return Math.Atan(n1);
                case "tanh":

                    return Math.Round(Math.Tanh(Math.PI * n1 / 180), 15);

                case "log":
                    return Math.Round(Math.Log10(n1), 15);
                case "ln":
                    return Math.Round(Math.Log(n1), 15);
                default:
                    if (Operator.Contains("log"))
                    {
                        Regex log_base = new Regex(@"\d+");
                        MatchCollection number = log_base.Matches(Operator);
                        int base_int;
                        int.TryParse(number[0].ToString(), out base_int);
                        return Math.Round(Math.Log(n1, base_int), 15);

                    }
                    throw new Exception("failed");

            }

        }

        public float Evaluator(Queue<string> postfix)
        {
            /*purpose: accepts a postfix expression, computes the result of the expression 
             * @param: a non-null queue containing tokens of a postfix in the appropriate order: queue must contain
             * at least one number;
             * @return: an int representing the result of the expression
             */



            Stack<float> eval = new Stack<float>();
            foreach (string item in postfix)
            {
                if (float.TryParse(item, out float number))
                {
                    eval.Push(number);
                }
                else
                {
                    Operator @operator = new Operator(item);
                    float second = eval.Pop();
                    float result = 1;

                    if (@operator.IsFunction())
                    {
                        if (!(@operator.GetName() == "max" || @operator.GetName() == "min"))
                        {
                            result = (float)HandleOneInputMath(second, item);
                        }
                        else
                        {
                            float first = eval.Pop();
                            result = HandleMath(first, second, item);
                        }
                    }
                    else
                    {

                        try
                        {
                            float first = eval.Pop();
                            result = HandleMath(first, second, item);
                        }
                        catch (System.InvalidOperationException)
                        {
                            string intermediate = item + "1";
                            if (float.TryParse(intermediate, out float first))
                            {
                                result = first * second;
                            }

                        }
                    }


                    eval.Push(result);

                }
            }
            if (eval.Count != 0)
            {
                return eval.Pop();
            }
            return 0;

        }

        public float Solve(string expression)
        {
            Queue<string> posfix = ShuntingYardAlgorithm(expression);
            return Evaluator(posfix);
        }

       
    }

}
