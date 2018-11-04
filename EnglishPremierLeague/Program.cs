using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EnglishPremierLeague.Data.Adapters;
using EnglishPremierLeague.Data.Adapters.CSVAdapter;
using EnglishPremierLeague.Data.Adapters.DATAdapter;
using EnglishPremierLeague.BusinessServices.Services;
using EnglishPremierLeague.Data.Adapters.Parsers;
using EnglishPremierLeague.Data.Adapters.Parsers.CSVParser;
using EnglishPremierLeague.Data.Adapters.Validators;
using EnglishPremierLeague.Data.Adapters.Validators.CSVValidator;
using EnglishPremierLeague.BusinessServices.Validators;

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

			//Setting Up Dependency Injection
			var csvServiceProvider = new ServiceCollection()
				.AddSingleton<IDataAdapter, CSVAdapter>()
				.AddSingleton<IParser, CSVParser>()
				.AddSingleton<IValidator, CSVValidator>()
				.AddSingleton<ILoggerFactory, LoggerFactory>()
				.AddSingleton(typeof(ILogger<>), typeof(Logger<>))
				.AddSingleton<IBusinessService, BusinessService>()
				.AddSingleton<IBusinessValidator, BusinessValidator>()
				
				.BuildServiceProvider();

			//Setting the logging
			csvServiceProvider.GetService<ILoggerFactory>().AddConsole(LogLevel.Debug);
			var logger = csvServiceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

			logger.LogDebug("Starting the console application");

			var datDataProvider = new ServiceCollection()
				.AddSingleton<IDataAdapter, DATAdapter>()
				.AddSingleton<ILoggerFactory, LoggerFactory>()
				.AddSingleton(typeof(ILogger<>), typeof(Logger<>))
				.BuildServiceProvider();

			var csvRepository = csvServiceProvider.GetService<IDataAdapter>().GetRepository(filePath);

			var businessService = csvServiceProvider.GetService<IBusinessService>();
			businessService.SetRepository(csvRepository);
			
			var team = businessService.GetTeamWithLowDifferenceInGoals();


			Console.WriteLine(team.TeamName);
			Console.ReadLine();
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
