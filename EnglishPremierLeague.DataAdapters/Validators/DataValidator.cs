using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using EnglishPremierLeague.Common.Entities;
using Microsoft.Extensions.Logging;

namespace EnglishPremierLeague.Data.Adapters.Validators
{
	public abstract class DataValidator : IValidator
	{
		protected readonly ILogger<DataValidator> _logger;

		public DataValidator(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger<DataValidator>();
		}

		#region Abstract methods
		public abstract bool Validate(string rowData, bool isHeaderRow, out Team team);
		#endregion

		#region Public methods
		public bool ValidateColumnType(string data, Type dataType, out object convertedValue)
		{
			convertedValue = null;
			try
			{
				var converter = TypeDescriptor.GetConverter(dataType);
				if (converter != null)
					convertedValue = converter.ConvertFrom(data);


			}
			catch (Exception ex)
			{
				_logger.LogDebug("Suppressing the conversion failure for data in DataValidator: Data: {0}" +
					"Datatype:{1} {2}",data,dataType.ToString(), ex.StackTrace);
				
			}
			return convertedValue != null;
		}

		public bool ValidateColumnName(string data, string columnName)
		{
			return data?.Trim().ToUpper() == columnName?.ToUpper();
		}

		public bool ValidateColumnCount(int splitCount, int columnCount)
		{
			return splitCount == columnCount;
		}

		public string[] SplitByLength(string data, int[] lengthArray)
		{
			try
			{
				List<string> splitStrings = new List<string>();

				var previousIndex = -1;
				foreach (var length in lengthArray.Select((value, index) => new { index, value }))
				{
					var currentIndex = previousIndex + 1;

					string splitString;
					if ((currentIndex + length.value) < data.Length)
						splitString = data.Substring(currentIndex, length.value);
					else
						splitString = data.Substring(currentIndex, data.Length - currentIndex);

					splitStrings.Add(splitString);
					previousIndex = previousIndex + length.value;
				}

				return splitStrings.ToArray();
			}
			catch (Exception)
			{

				throw;
			}
		}
		#endregion
	}

}
