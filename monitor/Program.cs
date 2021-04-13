using System;
using System.Diagnostics;
using System.Timers;

namespace monitor
{
    class Program
    {
        static string procName;
        static int ttlWork;
        private static Timer aTimer;

        static void Main(string[] args)
        {          
            if (args.Length != 3)
            {
                Console.WriteLine("Enter process name, run time and update time in minutes !");
                return;
            }
            procName = args[0];
            ttlWork = Convert.ToInt32(args[1]);
            int ttCheck = Convert.ToInt32(args[2]) * 60 * 1000;

            Console.WriteLine($"Tracking the process : {args[0]}");
            Console.WriteLine($"Time limit: {args[1]} мин.");
            Console.WriteLine($"Update time: {args[2]} мин.");

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
                    if (p.ProcessName == procName)
                    {
                        if ((DateTime.Now - p.StartTime).TotalMinutes > ttlWork)
                        {
                            p.Kill();
                            Console.WriteLine($"{DateTime.Now} Killing the process {p.ProcessName}. Working hours {DateTime.Now - p.StartTime}.");
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
