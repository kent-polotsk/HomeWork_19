
internal class Program
{
    public static ConsoleKeyInfo PressKey()
    {
        int cursorLeft = Console.CursorLeft;
        ConsoleKeyInfo key = Console.ReadKey();
        Console.SetCursorPosition(cursorLeft, Console.CursorTop);
        Console.Write(" ");
        Console.SetCursorPosition(cursorLeft, Console.CursorTop);
        return key;
    }

    public static void PrintGuide()
    {
        const string Guide =
            "Задание 1:\n" +
            "1 - Добавить задачу вывода символов (char)\t2 - Добавить задачу вывода чисел (int)\n" +
            "3 - Снять задачи вывода символов (char)\t\t4 - Снять задачи вывода чисел (int)\n" +
            "0 - Инструкция\nESC - выход";

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Guide);
        Console.ResetColor();
    }

    static async Task Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Action operation;//= null;

        List<Task> listTaskChars = [];
        List<Task> listTaskInts = [];

        CancellationTokenSource cancelTokenSource = new();
        CancellationTokenSource cancelTokenSource2 = new();

        ConsoleKeyInfo k;

        PrintGuide();

        while (true)
        {

            k = PressKey();
            switch (k.Key)
            {
                case ConsoleKey.D1 or ConsoleKey.NumPad1:
                    {
                        foreach (var t in listTaskChars.ToList())
                            if (t.IsCanceled || t.IsCompleted || t.IsFaulted)
                                listTaskChars.Remove(t);

                        operation = InfinityChars;
                        cancelTokenSource = new CancellationTokenSource();

                        if (listTaskChars.Count <= 10)
                            listTaskChars.Add(Task.Factory.StartNew(() => MyMethod(operation), cancelTokenSource.Token));
                        else
                        {
                            WriteText("\n_Запущено максимальное количество задач InfinityChars!_\n", ConsoleColor.Red);
                        }
                        break;
                    }

                case ConsoleKey.D2 or ConsoleKey.NumPad2:
                    {
                        foreach (var t in listTaskInts.ToList())
                            if (t.IsCanceled || t.IsCompleted || t.IsFaulted)
                            {
                                t.Dispose();
                                listTaskInts.Remove(t);
                            }

                        operation = InfinityInts;
                        cancelTokenSource2 = new CancellationTokenSource();

                        if (listTaskInts.Count <= 10)
                            listTaskInts.Add(Task.Factory.StartNew(() => MyMethod(operation), cancelTokenSource2.Token));
                        else
                        {
                            WriteText("\n_Запущено максимальное количество задач InfinityInts!_\n", ConsoleColor.Red);
                        }

                        break;
                    }

                case ConsoleKey.D3 or ConsoleKey.NumPad3:
                    {
                        if (cancelTokenSource is not null && !cancelTokenSource.IsCancellationRequested)
                        {
                            cancelTokenSource.Cancel();

                            WriteText("\n_Запрос на остановку задачи _InfinityChars_ отправлен_\n", ConsoleColor.Green);

                            cancelTokenSource.Dispose();
                        }
                        else
                        {
                            WriteText("\n_Запрос на остановку _InfinityChars_ УЖЕ отправлен или задача не была запущена_\n", ConsoleColor.Red);
                        }

                        break;
                    }

                case ConsoleKey.D4 or ConsoleKey.NumPad4:
                    {
                        if (cancelTokenSource2 is not null && !cancelTokenSource2.IsCancellationRequested)
                        {
                            cancelTokenSource2.Cancel();

                            WriteText("\n_Запрос на остановку задачи _InfinityInts_ отправлен_\n", ConsoleColor.Green);

                            cancelTokenSource2.Dispose();
                        }
                        else
                        {
                            WriteText("\n_Запрос на остановку _InfinityInts_ УЖЕ отправлен или задача не была запущена_\n", ConsoleColor.Red);
                        }
                        break;
                    }

                case ConsoleKey.D0 or ConsoleKey.NumPad0:
                    {
                        PrintGuide();
                        break;
                    }

                case ConsoleKey.Escape:
                    {
                        try
                        {
                            if (cancelTokenSource is not null && !cancelTokenSource.IsCancellationRequested)
                                cancelTokenSource.Cancel();
                            if (cancelTokenSource2 is not null && !cancelTokenSource2.IsCancellationRequested)
                                cancelTokenSource2.Cancel();
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n_Токены были удалены_");
                            Console.ResetColor();
                        }
                        finally
                        {

                            await Task.WhenAll(listTaskChars);
                            await Task.WhenAll(listTaskInts);
                            await Task.Delay(200);

                            Console.Write("\n1");
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            string bye = "\nРабота приложения завершена...";
                            for (int i = 0; i < bye.Length; i++)
                            {
                                Console.Write(bye[i]);
                                Thread.Sleep(15);
                            }
                            Console.ResetColor();
                            Thread.Sleep(200);
                            Console.WriteLine();

                            Environment.Exit(0);
                        }

                        break;
                    }

                default:
                    break;
            }
        }

        void MyMethod(Action act) => act();

        async void InfinityChars()
        {
            WriteText("\n_InfinityChars (33=>126=>33)_", ConsoleColor.DarkYellow);

            if (cancelTokenSource is not null)
            while (!cancelTokenSource.IsCancellationRequested)
            {
                for (int i = 33; i <= 126; i++)
                {
                    if (!cancelTokenSource.IsCancellationRequested)
                    {
                        Console.Write((char)i + " ");
                        Task.Delay(60).GetAwaiter().GetResult();
                    }
                }

                for (int i = 126; i >= 33; i--)
                {
                    if (!cancelTokenSource.IsCancellationRequested)
                    {
                        Console.Write((char)i + " ");
                        Task.Delay(60).GetAwaiter().GetResult();
                    }
                }
            }

            await Task.Delay(30).ContinueWith((stub) => WriteText("\n_InfinityChars_stopped_\n", ConsoleColor.DarkRed));
            Task.CompletedTask.Dispose();
        }

        async void InfinityInts()
        {
            WriteText("\n_InfinityInts (0=>100=>0)_ ", ConsoleColor.DarkYellow);

            if (cancelTokenSource2 is not null)
            while (!cancelTokenSource2.IsCancellationRequested)
            {

                for (int i = 0; i <= 100; i++)
                {
                    if (!cancelTokenSource2.IsCancellationRequested)
                    {
                        Console.Write(i + " ");
                        Task.Delay(60).GetAwaiter().GetResult();
                    }
                }

                for (int i = 100; i >= 0; i--)
                {
                    if (!cancelTokenSource2.IsCancellationRequested)
                    {
                        Console.Write(i + " ");
                        Task.Delay(60).GetAwaiter().GetResult();
                    }
                }
            }

            await Task.Delay(30).ContinueWith((stub) => WriteText("\n_InfinityInts_stopped_\n", ConsoleColor.DarkRed));
            Task.CompletedTask.Dispose();
        }

        void WriteText(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}