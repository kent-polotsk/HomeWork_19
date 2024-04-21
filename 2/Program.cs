//    Создать метод принимающий величину милисекунд time,
//    и запускающий задачу которая через определенное время(равное параметру time)
//    выполняет вывод в консоль
//    строки "Hello from callback" посредством продолжения.
//    Попробовать сделать это через awaiter и через ContinueWith

internal class Program
{


    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        int? time;
        var input = string.Empty;

        ConsoleKey key;

        WriteLineText("Задание2", ConsoleColor.DarkYellow);
        WriteLineText("Для запуска нажмите \"1\", для выхода нажмите \"ESC\" ", ConsoleColor.White);

        do
        {
            key = Console.ReadKey(intercept: true).Key;
            switch (key)
            {
                case ConsoleKey.D1 or ConsoleKey.NumPad1:
                    {
                        time = null;

                        do
                        {
                            WriteLineText("\bВведите величину задержки в миллисекундах (неотрицательное число не более 10000): ", ConsoleColor.White);
                            input = Console.ReadLine();

                            try
                            {
                                if (int.TryParse(input, out int result) && result >= 0 && result <= 10000)
                                    time = result;
                                else throw new FormatException();
                            }
                            catch (FormatException e)
                            {
                                WriteLineText("Некорректное значение, повторите ввод. " + e.Message, ConsoleColor.Red);
                            }

                        } while (time is null);

                        MethodCallback(time);

                        Task task = Task.Run(() => Task.Delay(0));

                        var awaiter = task.GetAwaiter();
                        awaiter.OnCompleted(() => MethodAwaiter(time));

                        break;
                    }

                case ConsoleKey.Escape:
                    {
                        WriteLineText("Работа приложения завершена", ConsoleColor.Green);
                        Environment.Exit(0);
                        break;
                    }
            }

        } while (true);

    }

    static async void MethodCallback(int? ms)
    {
        try
        {
            if (ms is int ms1)
                await Task.Delay(ms1).ContinueWith((stub) => WriteLineText($"Hello from callback", ConsoleColor.Cyan));
            else
                throw new NullReferenceException();
        }
        catch (NullReferenceException e)
        {
            WriteLineText("Недопустимое значение задержки: NULL " + e.Message, ConsoleColor.Red);
        }
    }


    static async Task MethodAwaiter(int? ms)
    {
        try
        {
            if (ms is int ms1)
            {
                await Task.Delay(ms1);
                WriteLineText($"Hello from awaiter", ConsoleColor.Blue);
            }
            else
                throw new NullReferenceException();
        }
        catch (NullReferenceException e)
        {
            WriteLineText("Недопустимое значение задержки: NULL " + e.Message, ConsoleColor.Red);
        }
    }


    static void WriteLineText(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}