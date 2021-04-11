using System;
using System.Diagnostics;
using System.Timers;

namespace monitor
{
    class Program
    {
        static string procName;
        static int ttWork;
        private static Timer aTimer;

        static void Main(string[] args)
        {          

            if (args.Length != 3)
            {
                Console.WriteLine("Введите название процесса, время работы и время обновления в минутах!");
                return;
            }

            procName = args[0];
            ttWork = Convert.ToInt32(args[1]);
            int ttCheck = Convert.ToInt32(args[2]) * 60 * 1000;

            Console.WriteLine($"Отслеживаем процесс: {args[0]}");
            Console.WriteLine($"Ограничение времени: {args[1]} мин.");
            Console.WriteLine($"Время обновления: {args[2]} мин.");

            aTimer = new System.Timers.Timer();

            aTimer.Interval = ttCheck;
            aTimer.Elapsed += CheckProcess;
            aTimer.Enabled = true;
            aTimer.Start();

            Console.ReadLine();

        }
        private static void CheckProcess(Object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {

                Process[] processes = Process.GetProcesses();

                foreach (Process p in processes)
                {
                    if (p.ProcessName==procName)
                    {
                        if ((DateTime.Now - p.StartTime).TotalMinutes > ttWork)
                        {
                            p.Kill();
                            Console.WriteLine($"{DateTime.Now} Убиваем процесс {p.ProcessName}. Время работы {DateTime.Now - p.StartTime}.");
                        }
                    }
                }
            }
            catch 
            { 
            }
        }
    }
}
