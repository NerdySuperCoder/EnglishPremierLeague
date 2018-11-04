using System;
using System.Collections.Generic;
using System.Text;
using EnglishPremierLeague.Common.Entities;
using EnglishPremierLeague.Data.Adapters.Validators;
using EnglishPremierLeague.Data.Adapters.Validators.CSVValidator;

namespace EnglishPremierLeague.Data.Adapters.Parsers.CSVParser
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
