using EnglishPremierLeague.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishPremierLeague.DataAdapters.Parsers
{
	public interface IParser
	{
		Team Parse(string rowData, bool headerRow);
	}
}
