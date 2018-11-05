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
using EnglishPremierLeague.Data.Parsers.DATParser;
using EnglishPremierLeague.Data.Validators.DATValidator;

namespace EnglishPremierLeague
{
	public class Program
	{
		#region Main Function
		/// <summary>
		/// Main function for the console
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			//Displays the help if no arguments are given.
			if (args.Length == 0)
				DisplayHelp();

			//Gets the input and converts them into a ProgramPnput object.
			var programInput = GetProgramInput(args);

			//Get the service provider based on the input type - csv or dat
			var serviceProvider = GetServiceProvider(programInput);

			//Setting the logging
			serviceProvider.GetService<ILoggerFactory>().AddConsole(programInput.LogLevel);
			var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

			logger.LogDebug("Getting the business service");
			var teamStandingsService = serviceProvider.GetService<IBusinessService>();

			logger.LogDebug("Calling service function to get the team with lowest difference");
			var team = teamStandingsService.GetTeamWithLowDifferenceInGoals();
			Console.WriteLine("\n The team with the least difference between for and against goals is: {0}", team.TeamName);
			Environment.Exit(0);

		} 
		#endregion

		#region Private static functions
		/// <summary>
		/// Gets the service provider based on the input arguments.
		/// </summary>
		/// <param name="programInput"></param>
		/// <returns></returns>
		private static ServiceProvider GetServiceProvider(ProgramInput programInput)
		{
			if (programInput.Input == InputType.CSV)
				return new ServiceCollection()
				.AddSingleton<IDataAdapter, CSVAdapter>()
				.AddSingleton<IParser, CSVParser>()
				.AddSingleton<IValidator, CSVValidator>()
				.AddSingleton<ILoggerFactory, LoggerFactory>()
				.AddSingleton(typeof(ILogger<>), typeof(Logger<>))
				.AddSingleton<IBusinessService, BusinessService>()
				.AddSingleton<IBusinessValidator, BusinessValidator>()
				.AddSingleton<IFileDetails>(t => new FileDetails(programInput.FilePath, programInput.ContainsHeaderRow))
				.BuildServiceProvider();

			return new ServiceCollection()
				.AddSingleton<IDataAdapter, DATAdapter>()
				.AddSingleton<IParser, DATParser>()
				.AddSingleton<IValidator, DATValidator>()
				.AddSingleton<ILoggerFactory, LoggerFactory>()
				.AddSingleton(typeof(ILogger<>), typeof(Logger<>))
				.AddSingleton<IBusinessService, BusinessService>()
				.AddSingleton<IBusinessValidator, BusinessValidator>()
				.AddSingleton<IFileDetails>(t => new FileDetails(programInput.FilePath, programInput.ContainsHeaderRow))
				.BuildServiceProvider();
		}

		/// <summary>
		/// Get the program options from the user's input
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		private static ProgramInput GetProgramInput(string[] args)
		{
			ProgramInput programInput = new ProgramInput();

			//setting the default values
			programInput.Input = InputType.CSV;
			programInput.LogLevel = LogLevel.None;
			programInput.ContainsHeaderRow = true;

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
			if (string.IsNullOrEmpty(programInput.FilePath) || !File.Exists(programInput.FilePath))
			{
				Console.WriteLine("File path is empty or does not exist.");
				DisplayHelp();
			}

			return programInput;
		}

		/// <summary>
		/// Function to display the help
		/// </summary>
		private static void DisplayHelp()
		{

			XmlDocument displayDocument = new XmlDocument();
			displayDocument.Load(@".\DisplayText.xml");
			var displayString = displayDocument.SelectSingleNode("displaytext").InnerText;

			Console.WriteLine(displayString);
			Environment.Exit(0);

		} 
		#endregion

	}
}
