using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EnglishPremierLeague.DataAdapters
{
	/// <summary>
	/// Common interface to import data
	/// </summary>
	public interface IDataAdapter
	{
		DataTable GetTeamStandings();
	}
}
