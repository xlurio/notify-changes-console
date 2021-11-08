using System;
using System.IO;

namespace MyNamespace
{

    class MyClassCS
    {
        public static string USERNAME;
        static void Main()
        {
            Console.WriteLine("Digite seu nome:");
            USERNAME = Console.ReadLine();

            FileSystemWatcher watcher = new FileSystemWatcher(@"\\clbrfs\Operational\PRODUÇÃO\PCP\WATER JET");

            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.Error += OnError;

            watcher.Filter = "*.txt";
            watcher.IncludeSubdirectories = false;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Pressione enter para sair.");
            Console.ReadLine();
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{e.FullPath} modificado por " + USERNAME + " às " + DateTime.Now.ToString("hh:mm:ss"));
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"{e.FullPath} criado modificado por " + USERNAME + " às " + DateTime.Now.ToString("hh:mm:ss");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.FullPath + " deletado modificado por " + USERNAME + " às " + DateTime.Now.ToString("hh:mm:ss"));
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"{e.OldFullPath} renomeado para {e.FullPath} modificado por " + USERNAME + " às " + DateTime.Now.ToString("hh:mm:ss"));
        }

        private static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private static void PrintException(Exception ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                Console.WriteLine("StackTrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }
    }
}