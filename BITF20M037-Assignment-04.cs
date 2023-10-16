//1.

using System;
using System.Collections.Generic;
using System.Linq;

class EquationSolver
{
    public static string Solve(string equation)
    {
        equation = equation.Replace(" ", "");

        if (!IsValidEquation(equation))
        {
            return "Invalid equation";
        }

        while (equation.Contains("("))
        {
            int openingBracketIndex = equation.LastIndexOf('(');
            int closingBracketIndex = equation.IndexOf(')', openingBracketIndex);

            if (closingBracketIndex == -1)
            {
                return "Invalid equation";
            }

            string bracketedExpression = equation.Substring(openingBracketIndex + 1, closingBracketIndex - openingBracketIndex - 1);
            string result = Solve(bracketedExpression);

            equation = equation.Remove(openingBracketIndex, closingBracketIndex - openingBracketIndex + 1).Insert(openingBracketIndex, result);
        }

        char[] operators = { '/', 'x', '+', '-' };

        foreach (char op in operators)
        {
            int opIndex;
            while ((opIndex = equation.IndexOf(op)) != -1)
            {
                string leftOperand = GetOperand(equation, opIndex, out int leftEnd);
                string rightOperand = GetOperand(equation, opIndex + 1, out int rightEnd);

                if (decimal.TryParse(leftOperand, out decimal operand1) && decimal.TryParse(rightOperand, out decimal operand2))
                {
                    decimal result = 0;

                    switch (op)
                    {
                        case '/':
                            if (operand2 == 0)
                            {
                                return "Invalid equation";
                            }
                            result = operand1 / operand2;
                            break;
                        case 'x':
                            result = operand1 * operand2;
                            break;
                        case '+':
                            result = operand1 + operand2;
                            break;
                        case '-':
                            result = operand1 - operand2;
                            break;
                        default:
                            return "Invalid equation";
                    }

                    equation = equation.Remove(leftEnd, rightEnd - leftEnd).Insert(leftEnd, result.ToString("0.######"));
                }
                else
                {
                    return "Invalid equation";
                }
            }
        }

        return equation;
    }

    private static bool IsValidEquation(string equation)
    {
        char[] operators = { '/', 'x', '+', '-' };
        int bracketCount = 0;

        for (int i = 0; i < equation.Length; i++)
        {
            if (equation[i] == '(')
            {
                bracketCount++;
            }
            else if (equation[i] == ')')
            {
                bracketCount--;
                if (bracketCount < 0)
                {
                    return false;
                }
            }
            else if (i < equation.Length - 1 && operators.Contains(equation[i]) && operators.Contains(equation[i + 1]))
            {
                return false;
            }
        }

        if (bracketCount != 0)
        {
            return false;
        }

        return true;
    }

    private static string GetOperand(string equation, int startIndex, out int endIndex)
    {
        int start = startIndex;
        int end = startIndex;

        while (start >= 0 && char.IsDigit(equation[start]) || equation[start] == '.' || equation[start] == '-')
        {
            start--;
        }

        while (end < equation.Length && char.IsDigit(equation[end]) || equation[end] == '.' || equation[end] == '-')
        {
            end++;
        }

        endIndex = end;
        return equation.Substring(start + 1, end - start - 1);
    }

    static void Main(string[] args)
    {
        try
        {
            Console.Write("Enter an equation: ");
            string equation = Console.ReadLine();
            string result = Solve(equation);
            Console.WriteLine("Result: " + result);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
