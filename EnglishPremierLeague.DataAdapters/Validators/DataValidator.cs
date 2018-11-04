using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using EnglishPremierLeague.Common.Entities;

namespace EnglishPremierLeague.Data.Adapters.Validators
{
	public abstract class DataValidator : IValidator
	{
		public abstract bool Validate(string rowData, bool isHeaderRow, out Team team);

		public bool ValidateColumnType(string data, Type dataType, out object convertedValue)
		{
			var converter = TypeDescriptor.GetConverter(dataType);
			convertedValue = converter.ConvertFrom(data);

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
