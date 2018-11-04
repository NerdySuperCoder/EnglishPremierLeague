using System;
using System.Collections.Generic;
using System.Text;
using EnglishPremierLeague.Common;
using EnglishPremierLeague.DataAdapters.Validators;
using EnglishPremierLeague.DataAdapters.Validators.CSVValidator;

namespace EnglishPremierLeague.DataAdapters.Parsers.CSVParser
{
	public class CSVParser : IParser
	{
		public IValidator CSVValidator;

		public CSVParser()
		{
			CSVValidator = new CSVValidator();
		}
		
		public Team Parse(string rowData, bool headerRow)
		{
			
			Team team;
			if (CSVValidator.Validate(rowData, headerRow, out team))
				return team;

			return null;
		}
	}
}
