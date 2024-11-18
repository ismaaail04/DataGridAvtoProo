using DataGridAvto.AvtoManager;
using DataGridAvto.StorageMemory;
using Serilog;
using Serilog.Extensions.Logging;
using System;
using System.Windows.Forms;

namespace DataGridAvto
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var serilogLogger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Seq("http://localhost:5341", apiKey: "JeoJo1P6sKdS4gSXESHB")
            .CreateLogger();

            var logger = new SerilogLoggerFactory(serilogLogger).CreateLogger("keysname");

            var storage = new MemoryCarStorage();
            var manager = new CarManager(storage, logger);

            Application.Run(new Form1(manager));
        }
    }
}
