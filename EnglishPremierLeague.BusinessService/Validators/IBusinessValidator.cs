using EnglishPremierLeague.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishPremierLeague.BusinessServices.Validators
{
	public interface IBusinessValidator
	{
		bool Validate(IEnumerable<Team> teamsData, out List<Team> validTeams, bool ignoreInvalidData = true);
		bool ValidatePoints(Team team);
		bool ValidateMatches(Team team);
	}
}
