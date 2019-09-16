﻿using System;
using System.Collections;

namespace Yanyixiao
{
    class Program
    {
        public class Formula
        {
            public string Form { get; internal set; }
            public Stack Formstack { get; internal set; }
        }

        static string[] op = { "+", "-", "*", "/" };// Operation set
        static void Main(string[] args)
        {
            Formula question = MakeFormula();
            System.Console.WriteLine(question.Form);
            string ret = Solve(question);
            System.Console.WriteLine(ret);

        }


        public static  Formula MakeFormula()
        {
            Random ran = new Random();
            Formula Form = new Formula();
            string build = null;
            int count = (int)ran.Next(1, 3); // generate random count
            int start = 0;
            int number1 = ran.Next(1, 99);
            build = build + number1;
            Form.Formstack.Push(number1);
            while (start <= count)
            {
                int operation = (int)ran.Next(0, 3); ; // generate operator
                int number2 = (int)ran.Next(1, 99);
                build = build + op[operation] + number2;
                Form.Formstack.Push(op[operation]);
                Form.Formstack.Push(number2);
                start++;
            }
            Form.Form = build;
            return Form;
        }//Make a Formula(like 1*5+62-5)


        public static string Solve(Formula formula)
        {
            Stack numberStack = new Stack(60);//Store number
            Stack operatorStack = new Stack(60);//Store operator
            while (!(formula.Formstack.Count == 0))
            {
                if (!((string)formula.Formstack.Peek() == "+" || (string)formula.Formstack.Peek() == "-" 
                    || (string)formula.Formstack.Peek() == "*" || (string)formula.Formstack.Peek() == "/"))//判断是否数字或者operator
                {
                    numberStack.Push(formula.Formstack.Pop());
                }//如果是数字，压入numberStack
                else
                {
                    if((string)formula.Formstack.Peek() == "*")
                    {
                        formula.Formstack.Pop(); 
                        int temp = (int)formula.Formstack.Pop() * (int)numberStack.Pop();
                        numberStack.Push(temp);
                    }//如果栈顶为“*”。弹出numberStack栈顶和Formstack自上而下第一个数字，用于计算。
                    else if((string)formula.Formstack.Peek() == "/")
                    {
                        formula.Formstack.Pop();
                        int temp = (int)formula.Formstack.Pop() / (int)numberStack.Pop();
                        numberStack.Push(temp);
                    }//如果栈顶为“/”。弹出numberStack栈顶和Formstack自上而下第一个数字，用于计算。
                    else if((string)formula.Formstack.Peek() == "+"|| (string)formula.Formstack.Peek() == "-")
                    {
                        operatorStack.Push(formula.Formstack.Pop());
                    }
                }//非数字的处理方法
            }//计算算式中的乘除法，转化成加减法存入numberStack和opStack中
            while (!(numberStack.Count == 1))
            {
                if ((string)operatorStack.Peek() == "+")
                {
                    operatorStack.Pop();
                    int temp = (int)numberStack.Pop() + (int)numberStack.Pop();
                    numberStack.Push(temp);
                
                }else if ((string)operatorStack.Peek() == "-")
                {
                    operatorStack.Pop();
                    int temp = (int)numberStack.Pop() - (int)numberStack.Pop();
                    numberStack.Push(temp);
                }

            }//计算加减法
            return (string)numberStack.Pop();
  
        }
    }
}


class Stack
{
    public int Count = 0;
    public object[] s;
    object y;


    public Stack(int len)
    {
        s = new object[len];
    }
    public object Peek()
    {
        return s[Count];
    }
    public void Push(object x)
    {
        s[Count] = x;
        Count++;
    }
    public object Pop()
    {
        y = s[Count];
        Count--;
        return y;
    }
}
//废弃代码 XD



//Stack tempStack = new Stack();//Store number or operator
//Stack operatorStack = new Stack();//Store operator
//int len = formula.Length;
//int k = 0;
//for (int j = -1; j < len - 1; j++)
//{
//    string formulaChar = formula.Substring(j + 1??, 2???);
//    if (j == len - 2 || formulaChar.Equals('+') || formulaChar.Equals('-') || formulaChar.Equals('/') || formulaChar.Equals('*'))
//    {
//        if (j == len - 2)
//        {
//            tempStack.Push(formula.Substring(k));
//        }
//        else
//        {
//            if (k < j)
//            {
//                tempStack.Push(formula.Substring(k, j + 1));
//            }
//            if (operatorStack.Count == 0)
//            {
//                operatorStack.Push(formulaChar); //if operatorStack is empty, store it)
//            }
//            else
//            {
//                char stackChar = (char)operatorStack.Peek();//Get the top char of opSrack
//                if ((stackChar == '+' || stackChar == '-') && (formulaChar.Equals('*') || formulaChar.Equals('/')))
//                {
//                    operatorStack.Push(formulaChar);
//                }
//                else
//                {
//                    tempStack.Push(operatorStack.Pop());
//                    operatorStack.Push(formulaChar);
//                }
//            }
//        }
//        k = j + 2;
//    }
//}
//while (!(operatorStack.Count == 0))
//{ // Append remaining operators
//    tempStack.Push(operatorStack.Pop());
//}
//Stack calcStack = new Stack();
//foreach (string peekChar in tempStack)
//{ // Reverse traversing of stack
//    if (!(peekChar.CompareTo("+") == 0) && !(peekChar.CompareTo("-") == 0) && !(peekChar.CompareTo("/") == 0) && (peekChar.CompareTo("*") == 0))
//    {
//        calcStack.Push(peekChar); // Push number to stack
//    }
//    else
//    {
//        int a1 = 0;
//        int b1 = 0;
//        if (!(calcStack.Count == 0))
//        {
//            b1 = (int)(calcStack.Pop());
//        }
//        if (!(calcStack.Count == 0))
//        {
//            a1 = (int)(calcStack.Pop());
//        }
//        switch (peekChar)
//        {
//            case "+":
//                calcStack.Push((char)(a1 + b1));
//                break;
//            case "-":
//                calcStack.Push((char)(a1 - b1));
//                break;
//            case "*":
//                calcStack.Push((char)(a1 * b1));
//                break;
//            default:
//                calcStack.Push((char)(a1 / b1));
//                break;
//        }
//    }
//}
//return formula + "=" + calcStack.Pop();