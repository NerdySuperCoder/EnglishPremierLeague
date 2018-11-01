using EnglishPremierLeague.DataAdapters.Parsers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EnglishPremierLeague.DataAdapters.CSVAdapter
{
	public class CSVAdapter : DataAdapter
	{
		public IParser CSVParser { get; set; }

		public CSVAdapter()
		{

		}

		public override DataTable GetData(string FilePath)
		{
			throw new NotImplementedException();
		}
	}
}
