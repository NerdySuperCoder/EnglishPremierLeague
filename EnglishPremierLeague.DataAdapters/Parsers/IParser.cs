using EnglishPremierLeague.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishPremierLeague.Data.Adapters.Parsers
{
	public interface IParser
	{
		Team Parse(string rowData, bool headerRow);
	}
}
