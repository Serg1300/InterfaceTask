using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceTask_1_2
{
    internal class Program
    {
        static ILogger Logger { get; set; }
        static void Main(string[] args)
        {
            Logger = new Logger();
            var calculator = new CalculatorSum(Logger);

            calculator.Sum();

        }
    }

    public interface ICalculatorSum
    {
        void Sum();
    }

    public class CalculatorSum : ICalculatorSum
    {
        ILogger Logger { get; }
        public CalculatorSum(ILogger logger)
        {
            Logger = logger;
        }
        public void Sum()
        {
            try
            {
                Console.WriteLine("  Сумма двух чисел");
                Console.WriteLine("Введите первое число:");
                string x = Console.ReadLine();
                double namber1;
                if ((double.TryParse(x, out namber1)) != true) throw new MyException(Logger);
                Console.WriteLine("Введите второе число:");
                string y = Console.ReadLine();
                double namber2;
                if ((double.TryParse(y, out namber2)) != true) throw new MyException(Logger);
                Logger.Event($"Ответ: {namber1 + namber2}");
            }
            catch (MyException ex)
            {
                ex.Error();
            }
        }
    }
    public interface ILogger
    {
        void Event(string messager);
        void Error(string messager);

    }
    public class Logger : ILogger
    {
        public void Error(string messager)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(messager);
        }
        public void Event(string messager)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(messager);
        }
    }
    public class MyException : Exception
    {
        ILogger Logg { get; }
        public MyException(ILogger logger)
        {
            Logg = logger;

        }
        public MyException(string messager)
            : base(messager)
        { }
        public void Error()
        {
            Logg.Error("Ошибка ввода данных, введите числа");
        }

    }
}
