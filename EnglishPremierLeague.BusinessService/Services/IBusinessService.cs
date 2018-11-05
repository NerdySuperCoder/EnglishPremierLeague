using EnglishPremierLeague.Common.Entities;

namespace EnglishPremierLeague.BusinessServices.Services
{
	public interface IBusinessService
	{
		//void SetRepository(IEnumerable<Team> teams);
		Team GetTeamWithLowDifferenceInGoals();
		string GetTeamLeader();
	}
}
