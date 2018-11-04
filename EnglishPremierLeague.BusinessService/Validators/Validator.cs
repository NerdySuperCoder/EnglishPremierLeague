using System;
using System.Collections.Generic;
using System.Text;
using EnglishPremierLeague.Common.Entities;

namespace EnglishPremierLeague.BusinessServices.Validators
{
	class BusinessValidator : IBusinessValidator
	{
		public bool Validate(IEnumerable<Team> teamData, out List<Team> validTeams, bool ignoreInvalidData = true)
		{
			bool isValid = false;
			validTeams = null;
			var validatedTeams = new List<Team>();
			foreach (var team in teamData)
			{
				if (!ValidatePoints(team) || !ValidateMatches(team))
				{
					if (!ignoreInvalidData)
						return isValid;
				}
				else
				{
					validatedTeams.Add(team);
				}

				
			}
			validTeams = validatedTeams;
			return isValid;
		}

		public bool ValidatePoints(Team team)
		{
			return team.Points == (
					(team.NumberOfGoalsScored * 3) +
					(team.NumberOfDraws * 1)
					);

		}

		public bool ValidateMatches(Team team)
		{
			return team.NumberOfPlayed == (
				team.NumberOfPlayed +
				team.NumberOfLosses +
				team.NumberOfDraws
				);
		}
	}
}
