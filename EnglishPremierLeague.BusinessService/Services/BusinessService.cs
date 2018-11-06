using EnglishPremierLeague.BusinessServices.Validators;
using EnglishPremierLeague.Common.Entities;
using EnglishPremierLeague.Data.Adapters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishPremierLeague.BusinessServices.Services
{
	public class BusinessService : IBusinessService
	{
		#region Private Variables
		private readonly IBusinessValidator _businessValidator;
		private readonly ILogger<BusinessService> _logger;
		private readonly IEnumerable<Team> _teams;
		private List<Team> _validTeams;
		#endregion
		
		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dataAdapter"></param>
		/// <param name="businessValidator"></param>
		/// <param name="loggerFactory"></param>
		public BusinessService(IDataAdapter dataAdapter, IBusinessValidator businessValidator, ILoggerFactory loggerFactory)
		{
			_businessValidator = businessValidator;
			_teams = dataAdapter.GetRepository();
			_logger = loggerFactory.CreateLogger<BusinessService>();
		} 
		#endregion

		#region IBusinessService Implmentations
		/// <summary>
		/// Get the team who won the championship
		/// </summary>
		/// <returns></returns>
		public string GetTeamLeader()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get the team with the lowest goal difference between for and against goals
		/// </summary>
		/// <returns></returns>
		public List<Team> GetTeamWithLowDifferenceInGoals()
		{
			try
			{

				if (Validate())
				{
					var lowestGoalDifferenceTeam = _validTeams.OrderBy(t => t.GoalDifference).FirstOrDefault();
					return _validTeams.Where(t => t.GoalDifference == lowestGoalDifferenceTeam.GoalDifference).ToList();
				}
					
				throw new Exception("Repository not valid");
			}
			catch (Exception)
			{

				throw;
			}
		}

		#endregion

		#region Private methods
		private bool Validate()
		{
			try
			{
				_logger.LogDebug("Validating the repository based on business rules");
				return _businessValidator.Validate(_teams, out _validTeams);
			}
			catch (Exception)
			{

				throw;
			}
		}
		#endregion
	}
}
