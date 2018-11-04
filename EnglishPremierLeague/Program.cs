using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EnglishPremierLeague.Data.Adapters;
using EnglishPremierLeague.Data.Adapters.CSVAdapter;
using EnglishPremierLeague.Data.Adapters.DATAdapter;
using EnglishPremierLeague.BusinessServices.Services;

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

			var filePath = @"C:\Users\ElaRaji\OneDrive\Personal\GitHub\EnglishPremierLeague\EnglishPremierLeague\Resources\football.csv";

			
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

			var testCSV = csvDataProvider.GetService<IDataAdapter>().GetData(filePath);

			IBusinessService service = new BusinessServices.Services.BusinessService(testCSV);
			var team = service.GetTeamWithLowDifferenceInGoals();
		
			//var testDAT = datDataProvider.GetService<IDataAdapter>().GetData(filePath);



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
