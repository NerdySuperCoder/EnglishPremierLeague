using System;
using System.IO;
using System.Reflection;
using System.Xml;
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
			//commandline call
			//dotnet EnglishPremierLeague.dll [-csv|-dat] -filepath <filepath.csv| filepath.dat> -containsheader [true|false] -csvtemplatepath <filename.xml> -loglevel [debug|trace]

			var programInput = GetProgramInput(args);

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

			logger.LogInformation("Starting app");

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

		static ProgramInput GetProgramInput(string[] args)
		{
			ProgramInput programInput = new ProgramInput();

			if (args.Length == 0)
				return null;


			PropertyInfo propertyInfo = null;
			foreach (var argument in args)
			{


				if (string.IsNullOrEmpty(argument))
					break;


				switch (argument.ToUpper())
				{
					case "-CSV":
						programInput.Input = InputType.CSV;
						break;
					case "-DAT":
						programInput.Input = InputType.DAT;
						break;
					case "-CONTAINSHEADER":
						propertyInfo = (programInput.GetType()).GetProperty("ContainsHeaderRows");
						break;
					case "-FILEPATH":
						propertyInfo = (programInput.GetType()).GetProperty("FilePath");
						break;
					case "-LOGLEVEL":
						propertyInfo = (programInput.GetType()).GetProperty("LogLevel");
						break;
					case "-CSVTEMPLATEPATH":
						propertyInfo = (programInput.GetType()).GetProperty("CSVTemplatePathilePath");
						break;
					case "-DATTEMPLATEPATH":
						propertyInfo = (programInput.GetType()).GetProperty("DATTemplatePath");
						break;
					case "--HELP":
					case "-H":
					case "/?":
						DisplayHelp();
						break;

					default:
						if (propertyInfo != null)
						{
							if (propertyInfo.Name == "ContainsHeaderRows")
							{
								if (bool.TryParse(argument, out var containsHeaderRows))
								{
									propertyInfo.SetValue(programInput, containsHeaderRows);
									propertyInfo = null;
								}

							}
							else if (propertyInfo.Name == "FilePath")
							{
								propertyInfo.SetValue(programInput, argument);
								propertyInfo = null;
							}
							else if (propertyInfo.Name == "LogLevel")
							{

								if (Enum.TryParse(typeof(LogLevel), argument, true, out var logLevel))
								{
									propertyInfo.SetValue(programInput, logLevel);
									propertyInfo = null;
								}

							}
							else if (propertyInfo.Name == "CSVTemplatePathilePath")
							{
								propertyInfo.SetValue(programInput, argument);
								propertyInfo = null;
							}
							else if (propertyInfo.Name == "DATTemplatePath")
							{
								propertyInfo.SetValue(programInput, argument);
								propertyInfo = null;
							}
						}

						break;
				}

			}

			return programInput;
		}

		private static void DisplayHelp()
		{
			
			XmlDocument displayDocument = new XmlDocument();
			displayDocument.Load(@".\DisplayText.xml");
			var displayString = displayDocument.SelectSingleNode("displaytext").InnerText;


			Console.WriteLine(displayString);
			Environment.Exit(0);
			
		}

	}



}
