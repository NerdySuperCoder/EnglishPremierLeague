using EnglishPremierLeague.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EnglishPremierLeague.Data.Adapters
{
	/// <summary>
	/// Common interface to import data
	/// </summary>
	public interface IDataAdapter
	{
		IEnumerable<Team> GetRepository();
	}
}
