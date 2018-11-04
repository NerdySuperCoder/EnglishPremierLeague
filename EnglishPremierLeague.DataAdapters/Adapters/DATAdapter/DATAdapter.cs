using EnglishPremierLeague.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EnglishPremierLeague.Data.Adapters.DATAdapter
{
	public class DATAdapter : DataAdapter
	{
		public override IEnumerable<Team> GetRepository(string FilePath, bool containsHeaderRow)
		{
			throw new NotImplementedException();
		}
	}
}
