using EnglishPremierLeague.Common.Entities;
using EnglishPremierLeague.Data.Adapters.Parsers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace EnglishPremierLeague.Data.Adapters.DATAdapter
{
	public class DATAdapter : DataAdapter
	{
		#region Private variables
		private readonly IParser _datParser;
		private readonly ILogger<DATAdapter> _logger;
		private readonly IFileDetails _fileDetails;
		#endregion

		#region Constructor
		public DATAdapter(IFileDetails fileDetails, IParser datParser, ILoggerFactory loggerFactory)
		{
			_fileDetails = fileDetails;
			_datParser = datParser;
			_logger = loggerFactory.CreateLogger<DATAdapter>();
		}
		#endregion

		#region Overidden Methods
		public override IEnumerable<Team> GetRepository()
		{
			List<Team> teamStandings = new List<Team>();

			//Check for file exists or not
			_logger.LogDebug("Checking for File exists : {0}", _fileDetails.FilePath);

			if (!File.Exists(_fileDetails.FilePath))
			{
				_logger.LogDebug("File Found");
				throw new FileNotFoundException();
			}


			_logger.LogDebug("Reading the DAT file");
			using (TextReader datReader = new StreamReader(_fileDetails.FilePath))
			{
				string line;
				bool headerRow = _fileDetails.ContainsHeader;

				_logger.LogDebug("Parsing data from DAT File");
				while ((line = datReader.ReadLine()) != null)
				{
					var team = _datParser.Parse(line, headerRow);
					if (!headerRow && team != null)
						teamStandings.Add(team);

					headerRow = false;
				}

			}
			return teamStandings;
		} 
		#endregion
	}
}
