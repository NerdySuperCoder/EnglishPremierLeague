using System.Collections.Generic;
using EnglishPremierLeague.Common.Entities;
using Microsoft.Extensions.Logging;

namespace EnglishPremierLeague.BusinessServices.Validators
{
	public class BusinessValidator : IBusinessValidator
	{
		private readonly ILogger<BusinessValidator> _logger;

		public BusinessValidator(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger<BusinessValidator>();
		}

		public bool Validate(IEnumerable<Team> teamData, out List<Team> validTeams, bool ignoreInvalidData = true)
		{
			bool isValid = true;
			validTeams = null;
			var validatedTeams = new List<Team>();
			foreach (var team in teamData)
			{
				if (!ValidatePoints(team) || !ValidateMatches(team))
				{
					if (!ignoreInvalidData)
						return false;
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
					(team.NumberOfWins * 3) +
					(team.NumberOfDraws * 1)
					);

		}

		public bool ValidateMatches(Team team)
		{
			return team.NumberOfPlayed == (
				team.NumberOfWins +
				team.NumberOfLosses +
				team.NumberOfDraws
				);
		}
	}
}
