using EnglishPremierLeague.Common.Entities;
using EnglishPremierLeague.Data.Adapters.Validators;
using Microsoft.Extensions.Logging;

namespace EnglishPremierLeague.Data.Adapters.Parsers.CSVParser
{
	public class CSVParser : IParser
	{
		#region Private fields
		private readonly IValidator _csvValidator;
		private readonly ILogger<CSVParser> _logger;
		#endregion

		#region Constructor
		public CSVParser(IValidator csvValidator, ILoggerFactory loggerFactory)
		{
			_csvValidator = csvValidator;
			_logger = loggerFactory.CreateLogger<CSVParser>();
		}
		#endregion

		#region IParser Methods
		public Team Parse(string rowData, bool headerRow)
		{
			try
			{

				Team team;
				if (_csvValidator.Validate(rowData, headerRow, out team))
					return team;

				return null;
			}
			catch (System.Exception)
			{

				throw;
			}
		} 
		#endregion
	}
}
