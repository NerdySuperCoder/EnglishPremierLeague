using EnglishPremierLeague.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishPremierLeague.BusinessServices.Services
{
	public interface IBusinessService
	{
		//void SetRepository(IEnumerable<Team> teams);
		Team GetTeamWithLowDifferenceInGoals();
		string GetTeamLeader();
	}
}
