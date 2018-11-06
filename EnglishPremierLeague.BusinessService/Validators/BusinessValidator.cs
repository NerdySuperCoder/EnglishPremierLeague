using System.Collections.Generic;
using EnglishPremierLeague.Common.Entities;
using Microsoft.Extensions.Logging;

namespace EnglishPremierLeague.BusinessServices.Validators
{
	public class BusinessValidator : IBusinessValidator
	{

		#region Private variables
		private readonly ILogger<BusinessValidator> _logger;
		#endregion

		#region Constructor
		public BusinessValidator(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger<BusinessValidator>();
		}
		#endregion

		#region IBusinessValidator Methods
		public bool Validate(IEnumerable<Team> teamData, out List<Team> validTeams, bool ignoreInvalidData = true)
		{
			try
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
			catch (System.Exception)
			{

				throw;
			}
		}

		public bool ValidatePoints(Team team)
		{
			try
			{
				return team.Points == (
							(team.NumberOfWins * 3) +
							(team.NumberOfDraws * 1)
							);
			}
			catch (System.Exception)
			{

				throw;
			}

		}

		public bool ValidateMatches(Team team)
		{
			try
			{
				return team.NumberOfPlayed == (
						team.NumberOfWins +
						team.NumberOfLosses +
						team.NumberOfDraws
						);
			}
			catch (System.Exception)
			{

				throw;
			}
		} 
		#endregion
	}
}
