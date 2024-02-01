using System.Diagnostics;

namespace BasicAsync {
    internal class Program {
        internal static async Task Main() {
            int count = 2000;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            PrintMessagesUsingThreads(count);
            sw.Stop();
            Console.WriteLine($"{count} threads finished in {sw.ElapsedMilliseconds} ms\n");

            sw.Reset();
            sw.Start();
            await PrintMessagesUsingTasks(count);
            sw.Stop();
            Console.WriteLine($"{count} tasks finished in {sw.ElapsedMilliseconds} ms");

            Console.ReadKey();
        }


        private static void PrintMessage(string message) {
            var rnd = new Random();
            var time = rnd.Next(0, 100);
            Thread.Sleep(time);
            Thread.CurrentThread.Name = message;
            Console.WriteLine($"Message: {message}. " +
                $"Thread id = {Thread.CurrentThread.ManagedThreadId}; " +
                $"Thread name = {Thread.CurrentThread.Name}; " +
                $"Processor id = {Thread.GetCurrentProcessorId()}; " +
                $"I've been waiting for {time} ms");
        }

        private static async Task PrintMessagesUsingTasks(int messagesCount) {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < messagesCount; i++) {
                Console.WriteLine($"{i} task iteration is adding");
                string message = $"{i} task iteration";
                tasks.Add(Task.Run(() => PrintMessage(message)));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine($"{messagesCount} tasks ended successfully");
        }

        private static void PrintMessagesUsingThreads(int messagesCount) {
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < messagesCount; i++) {
                Console.WriteLine($"{i} thread iteration is adding");
                string message = $"{i} thread iteration";
                threads.Add(new Thread(() => PrintMessage(message)));
            }
            threads.ForEach(thread => thread.Start());
            threads.ForEach(thread => thread.Join());
            Console.WriteLine($"{messagesCount} threads ended successfully");
        }
    }
}
