using EnglishPremierLeague.Common.Entities;
using EnglishPremierLeague.Data.Adapters.Parsers;
using EnglishPremierLeague.Data.Adapters.Validators;
using Microsoft.Extensions.Logging;

namespace EnglishPremierLeague.Data.Parsers.DATParser
{
	public class DATParser : IParser
	{
		private readonly IValidator _datValidator;
		private readonly ILogger<DATParser> _logger;

		public DATParser(IValidator datValidator, ILoggerFactory loggerFactory)
		{
			_datValidator = datValidator;
			_logger = loggerFactory.CreateLogger<DATParser>();
		}

		public Team Parse(string rowData, bool headerRow)
		{

			Team team;
			if (_datValidator.Validate(rowData, headerRow, out team))
				return team;

			return null;
		}
	}
}
