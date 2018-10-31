using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EnglishPremierLeague.DataAdapters;
using EnglishPremierLeague.DataAdapters.CSVAdapter;
using EnglishPremierLeague.DataAdapters.DATAdapter;


namespace EnglishPremierLeague
{
    public class Program
    {
        static void Main(string[] args)
        {
   //         var serviceProvider = new ServiceCollection()
   //             .AddSingleton<ITestInterface, TestClass>()
   //             .AddSingleton<ILoggerFactory, LoggerFactory>()
   //             .AddSingleton(typeof(ILogger<>), typeof(Logger<>))
   //             .BuildServiceProvider();


			//serviceProvider.GetService<ILoggerFactory>().AddConsole(LogLevel.Debug);//.AddDebug();

   //         var logger = serviceProvider.GetService<ILoggerFactory>()
   //         .CreateLogger<Program>();
   //         logger.LogDebug("Starting application");


   //         var bar = serviceProvider.GetService<ITestInterface>();
   //         bar.TestMethod();

   //         logger.LogDebug("All done!");

   //         Console.ReadLine();
			
			var csvDataProvider = new ServiceCollection()
				.AddSingleton<IDataAdapter, CSVAdapter>()
				.AddSingleton<ILoggerFactory, LoggerFactory>()
				.AddSingleton(typeof(ILogger<>), typeof(Logger<>))
				.BuildServiceProvider();

			var datDataProvider = new ServiceCollection()
				.AddSingleton<IDataAdapter, DATAdapter>()
				.AddSingleton<ILoggerFactory, LoggerFactory>()
				.AddSingleton(typeof(ILogger<>), typeof(Logger<>))
				.BuildServiceProvider();

			var testCSV = csvDataProvider.GetService<IDataAdapter>().GetTeamStandings();
			var testDAT = datDataProvider.GetService<IDataAdapter>().GetTeamStandings();

		}



		//public int Add(int x, int y)
		//{
		//    return (x + y);
		//}

		//public int Subtract(int x, int y)
		//{
		//    return x - y;
		//}
	}


    //public interface ITestInterface
    //{
    //    void TestMethod();
    //}

    //public class TestClass : ITestInterface
    //{
    //    public void TestMethod()
    //    {
    //        Console.WriteLine("Called TestMethod");
    //    }
       
    //}
}
