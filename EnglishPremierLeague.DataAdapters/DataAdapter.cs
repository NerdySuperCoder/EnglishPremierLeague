using System;
using System.Data;

namespace EnglishPremierLeague.DataAdapters
{
	public abstract class DataAdapter : IDataAdapter
	{
		public abstract DataTable GetTeamStandings();		
	}
}
