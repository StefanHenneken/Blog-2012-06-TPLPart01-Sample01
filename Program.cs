using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Sample01
{
    class Program
    {
        private int iterations;
        private double[] arr = new double[1000000];
        Thread currentThread = null;
        Process process = null;
        double elapsedMilliseconds = 0;
        
        static void Main(string[] args)
        {
            new Program().Run();
        }
        public void Run()
        {
            try
            {
                Console.WriteLine("start programm");
                
                process = Process.GetCurrentProcess();
                process.PriorityClass = ProcessPriorityClass.RealTime;
                currentThread = Thread.CurrentThread;
                currentThread.Priority = ThreadPriority.Highest;

                //////////////////////////////////////////////////////////////////

                iterations = 100;
                Console.Write("start sequential {0} loops", iterations);
                elapsedMilliseconds = 0;
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(".");
                    Stopwatch stopWatch = Stopwatch.StartNew();
                    SequentialLoop();
                    stopWatch.Stop();
                    elapsedMilliseconds += stopWatch.ElapsedMilliseconds;
                }
                Console.WriteLine("{0}ms", elapsedMilliseconds / 10.0);

                Console.Write("start parallel {0} loops", iterations);
                elapsedMilliseconds = 0;
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(".");
                    Stopwatch stopWatch = Stopwatch.StartNew();
                    ParallelLoop();
                    stopWatch.Stop();
                    elapsedMilliseconds += stopWatch.ElapsedMilliseconds;
                }
                Console.WriteLine("{0}ms", elapsedMilliseconds / 10.0);

                //////////////////////////////////////////////////////////////////

                iterations = 10000;
                Console.Write("start sequential {0} loops", iterations);
                elapsedMilliseconds = 0;
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(".");
                    Stopwatch stopWatch = Stopwatch.StartNew();
                    SequentialLoop();
                    stopWatch.Stop();
                    elapsedMilliseconds += stopWatch.ElapsedMilliseconds;
                }
                Console.WriteLine("{0}ms", elapsedMilliseconds / 10.0);

                Console.Write("start parallel {0} loops", iterations);
                elapsedMilliseconds = 0;
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(".");
                    Stopwatch stopWatch = Stopwatch.StartNew();
                    ParallelLoop();
                    stopWatch.Stop();
                    elapsedMilliseconds += stopWatch.ElapsedMilliseconds;
                }
                Console.WriteLine("{0}ms", elapsedMilliseconds / 10.0);

                ////////////////////////////////////////////////////////////////////

                iterations = 1000000;
                Console.Write("start sequential {0} loops", iterations);
                elapsedMilliseconds = 0;
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(".");
                    Stopwatch stopWatch = Stopwatch.StartNew();
                    SequentialLoop();
                    stopWatch.Stop();
                    elapsedMilliseconds += stopWatch.ElapsedMilliseconds;
                }
                Console.WriteLine("{0}ms", elapsedMilliseconds / 10.0);

                Console.Write("start parallel {0} loops", iterations);
                elapsedMilliseconds = 0;
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(".");
                    Stopwatch stopWatch = Stopwatch.StartNew();
                    ParallelLoop();
                    stopWatch.Stop();
                    elapsedMilliseconds += stopWatch.ElapsedMilliseconds;
                }
                Console.WriteLine("{0}ms", elapsedMilliseconds / 10.0);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nerror: " + e.Message);
            }
            finally
            {
                currentThread.Priority = ThreadPriority.Normal;
                process.PriorityClass = ProcessPriorityClass.Normal;

                Console.WriteLine("end program");
                Console.ReadLine();
            }
        }
        private void SequentialLoop()
        {
            for (int i = 0; i < iterations; i++)
            {
                for (int n = 0; n < 50; n++)
                    arr[i] = Math.Sin(i) + Math.Sqrt(i) * Math.Pow(i, 3.1415);
            }
        }
        private void ParallelLoop()
        {
            Parallel.For(0, iterations, i =>
            {
                for (int n = 0; n < 50; n++)
                    arr[i] = Math.Sin(i) + Math.Sqrt(i) * Math.Pow(i, 3.1415);
            });
        }
    }
}
