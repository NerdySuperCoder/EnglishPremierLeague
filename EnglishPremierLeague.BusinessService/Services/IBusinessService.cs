using EnglishPremierLeague.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishPremierLeague.BusinessServices.Services
{
	public interface IBusinessService
	{
		Team GetTeamWithLowDifferenceInGoals();
		string GetTeamLeader();
	}
}
