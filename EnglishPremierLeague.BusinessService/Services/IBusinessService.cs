using EnglishPremierLeague.Common.Entities;

namespace EnglishPremierLeague.BusinessServices.Services
{
	public interface IBusinessService
	{
		#region IBusinessServiceMethods

		Team GetTeamWithLowDifferenceInGoals();
		string GetTeamLeader(); 
	
		#endregion
	}
}
