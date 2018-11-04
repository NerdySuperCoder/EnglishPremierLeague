using System;
using System.Collections.Generic;
using System.Text;
using EnglishPremierLeague.Common.Entities;
using EnglishPremierLeague.Data.Adapters.Validators;
using EnglishPremierLeague.Data.Adapters.Validators.CSVValidator;
using Microsoft.Extensions.Logging;

namespace EnglishPremierLeague.Data.Adapters.Parsers.CSVParser
{
	public class CSVParser : IParser
	{
		private readonly IValidator _csvValidator;
		private readonly ILogger<CSVParser> _logger;

		public CSVParser(IValidator csvValidator, ILoggerFactory loggerFactory)
		{
			_csvValidator = csvValidator;
			_logger = loggerFactory.CreateLogger<CSVParser>();
		}
		
		public Team Parse(string rowData, bool headerRow)
		{
			
			Team team;
			if (_csvValidator.Validate(rowData, headerRow, out team))
				return team;

			return null;
		}
	}
}
