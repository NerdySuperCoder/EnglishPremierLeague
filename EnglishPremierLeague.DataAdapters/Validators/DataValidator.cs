using System;
using System.ComponentModel;
using EnglishPremierLeague.Common.Entities;

namespace EnglishPremierLeague.Data.Adapters.Validators
{
	public abstract class DataValidator : IValidator
	{
		public abstract bool Validate(string rowData, bool isHeaderRow, out Team team);

		public bool ValidateColumnType(string data, Type dataType, out object convertedValue)
		{
			convertedValue = null;
			try
			{
				var converter = TypeDescriptor.GetConverter(dataType);
				if(converter!= null)
					convertedValue = converter.ConvertFrom(data);

				
			}
			catch (Exception ex)
			{
				//Log the value is not converted
			}
			return convertedValue != null;
		}

		public bool ValidateColumnName(string data, string columnName)
		{
			return data.Trim().ToUpper() == columnName.ToUpper();
		}

		public bool ValidateColumnCount(int splitCount, int columnCount)
		{
			return splitCount == columnCount;
		}
	}
}
