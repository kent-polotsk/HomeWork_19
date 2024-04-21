using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3
{
    partial class Program
    {
        internal class Pizzeria
        {
            private readonly int numberOfWorkers;
            private readonly SemaphoreSlim semaphoreSlim;

            public Pizzeria()
            {
                numberOfWorkers = 1;
                semaphoreSlim = new SemaphoreSlim(numberOfWorkers);
            }
            public Pizzeria(int numberOfWorkers)
            {
                this.numberOfWorkers = numberOfWorkers;
                semaphoreSlim = new SemaphoreSlim(numberOfWorkers);
            }

            public async Task CookPizza()
            {
                if (semaphoreSlim.CurrentCount > 0)
                {
                    await semaphoreSlim.WaitAsync();
                    WriteLineText("Пицца готовится...", ConsoleColor.DarkYellow);
                    await Task.Delay(2500);
                    WriteLineText("Пицца готова!", ConsoleColor.Green);
                    semaphoreSlim.Release();
                    if (semaphoreSlim.CurrentCount == numberOfWorkers)
                        WriteLineText("Все повора свободны!", ConsoleColor.Green);
                }
                else
                {
                    WriteLineText("В данный момент все сотрудники заняты, попробуйте позже!", ConsoleColor.Red);
                }
            }
        }
    }
}
