using EnglishPremierLeague.Common.Entities;
using System.Collections.Generic;

namespace EnglishPremierLeague.BusinessServices.Services
{
	public interface IBusinessService
	{
		#region IBusinessServiceMethods

		List<Team> GetTeamWithLowDifferenceInGoals();
		string GetTeamLeader(); 
	
		#endregion
	}
}
