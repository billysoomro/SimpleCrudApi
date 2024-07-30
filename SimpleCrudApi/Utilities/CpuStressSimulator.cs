using System.Diagnostics;

namespace SimpleCrudApi.Utilities
{
    public class CpuStressSimulator
    {
        public static void SimulateCpuUsage(int durationInMinutes)
        {
            // Calculate the duration in milliseconds
            int durationInMilliseconds = durationInMinutes * 60 * 1000;

            // Get the start time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Run CPU intensive tasks until the specified duration elapses
            while (stopwatch.ElapsedMilliseconds < durationInMilliseconds)
            {
                // Perform a CPU intensive task
                PerformCpuIntensiveTask();
            }

            stopwatch.Stop();            
        }
        
        public static void SimulateCpuUsageByCores(int durationInMinutes)
        {
            int durationInMilliseconds = durationInMinutes * 60 * 1000;
            int numberOfCores = Environment.ProcessorCount;
            CancellationTokenSource cts = new CancellationTokenSource();

            // Start a task for each CPU core to maximize CPU usage
            Task[] tasks = new Task[numberOfCores];
            for (int i = 0; i < numberOfCores; i++)
            {
                tasks[i] = Task.Run(() => PerformCpuIntensiveTask(cts.Token), cts.Token);
            }

            // Wait for the specified duration
            Task.Delay(durationInMilliseconds).Wait();

            // Cancel the tasks
            cts.Cancel();

            // Wait for all tasks to complete
            Task.WaitAll(tasks);            
        }

        private static void PerformCpuIntensiveTask()
        {
            // This method performs some intensive computations to keep the CPU busy
            double result = 0;

            for (int i = 0; i < 1000000; i++)
            {
                result += Math.Sqrt(i);
            }
        }

        private static void PerformCpuIntensiveTask(CancellationToken token)
        {
            // Perform CPU intensive task until the cancellation is requested
            while (!token.IsCancellationRequested)
            {
                double result = 0;
                for (int i = 0; i < 1000000; i++)
                {
                    result += Math.Sqrt(i);
                }
            }
        }
    }
}
