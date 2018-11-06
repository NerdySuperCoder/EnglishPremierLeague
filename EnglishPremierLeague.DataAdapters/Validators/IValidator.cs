using EnglishPremierLeague.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishPremierLeague.Data.Adapters.Validators
{
	public interface IValidator
	{
		#region IValidator Methods
		bool Validate(string rowData, bool isHeaderRow, out Team team); 
		#endregion
	}
}
