
namespace _3
{
    partial class Program
    {


        static async Task Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ConsoleKey key;
            int? workers = null;

            WriteLineText("Задание 3. Пиццерия", ConsoleColor.DarkYellow);
            WriteLineText("\nВведите количество поваров в пиццерии (1-10): ", ConsoleColor.Gray);

            do
            {
                if (int.TryParse(Console.ReadLine(), out int result) && result > 0 && result <= 10)
                {
                    workers = result;
                }
                else
                {
                    WriteLineText("Некорректный ввод данных, повторите!", ConsoleColor.Red);
                }
            } while (workers is null);

            Pizzeria pizzeria = new((int)workers);

            WriteLineText("Для приготовления пиццы нажмите \"1\", для выхода из приложения нажмите \"ESC\"\n", ConsoleColor.Blue);

            while (true)
            {
                key = Console.ReadKey(intercept: true).Key;
                switch (key)
                {
                    case ConsoleKey.D1 or ConsoleKey.NumPad1:
                        {
                            Task.Run(pizzeria.CookPizza);
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            await Task.Delay(100);
                            WriteLineText("\nРабота приложения завершена", ConsoleColor.Green);
                            Environment.Exit(0);
                            break;
                        }
                }
            }
        }

        public static void WriteLineText(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
