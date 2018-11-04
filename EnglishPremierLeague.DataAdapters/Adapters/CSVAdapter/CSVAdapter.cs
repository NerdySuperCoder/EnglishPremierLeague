using EnglishPremierLeague.Common.Entities;
using EnglishPremierLeague.Data.Adapters.Parsers;
using EnglishPremierLeague.Data.Adapters.Parsers.CSVParser;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace EnglishPremierLeague.Data.Adapters.CSVAdapter
{
	public class CSVAdapter : DataAdapter
	{
		private readonly IParser _csvParser;
		private readonly ILogger<CSVAdapter> _logger;

		public CSVAdapter(IParser csvParser, ILoggerFactory loggerFactory)
		{
			_csvParser = csvParser;
			_logger = loggerFactory.CreateLogger<CSVAdapter>();
		}

		public override IEnumerable<Team> GetRepository(string FilePath, bool containsHeaderRow)
		{
			List<Team> teamStandings = new List<Team>();

			//Check for file exists or not
			_logger.LogDebug("Checking for File exists : {0}", FilePath);

			if (!File.Exists(FilePath))
			{
				_logger.LogDebug("File Found");
				throw new FileNotFoundException();
			}


			_logger.LogDebug("Reading the CSV file");
			using (TextReader csvReader = new StreamReader(FilePath))
			{
				string line;
				bool headerRow = true;

				_logger.LogDebug("Parsing data from CSV File");
				while ((line = csvReader.ReadLine()) != null)
				{					
					var team = _csvParser.Parse(line, headerRow);
					if (!headerRow && team!=null)
						teamStandings.Add(team);

					headerRow = false;
				}
				
			}
			return teamStandings;
		}
	}
}
