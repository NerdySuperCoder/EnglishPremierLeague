using EnglishPremierLeague.Common.Entities;
using EnglishPremierLeague.Data.Adapters.Validators;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EnglishPremierLeague.Data.Validators.DATValidator
{
	public class DATValidator: DataValidator
	{
		#region Private fields and properties
		
		private int[] _splitLengthArray;
		private List<Column> Columns { get; set; }
		#endregion

		#region Constructor
		public DATValidator(ILoggerFactory loggerFactory): base(loggerFactory)
		{
			var columnList = new XmlSerializer(typeof(List<Column>), new XmlRootAttribute("Columns"));
			Columns = (List<Column>)columnList.Deserialize(new FileStream(@".\Validators\DATValidator\DATTemplate.xml", FileMode.Open));
			_splitLengthArray = Columns.Select(t => t.Length).ToArray();
		}
		#endregion

		#region Overidden Methods
		public override bool Validate(string rowData, bool isHeaderRow, out Team team)
		{
			try
			{
				bool isValid = false;
				team = null;

				var columnValues = SplitByLength(rowData, _splitLengthArray);

				if (!ValidateColumnCount(columnValues.Length, Columns.Count))
				{
					_logger.LogDebug("Column count does not match");
					if (isHeaderRow)
						throw new Exception("Column count does not match with the template");

					return isValid;
				}


				if (isHeaderRow)
				{
					_logger.LogDebug("Header row. Validating column names with the template");
					foreach (var columnValue in columnValues)
					{
						if (!ValidateColumnName(columnValue, Columns.Find(t => t.Index == (Array.IndexOf(columnValues, columnValue) + 1)).Name))
							return isValid;
					}
					isValid = true;
				}
				else
				{
					_logger.LogDebug("Data row. Validating data with the template for data type");
					team = new Team();
					foreach (var columnValue in columnValues.Select((value, index) => new { index, value }))
					{
						Column column = Columns.Find(t => t.Index == (columnValue.index + 1));
						var columnType = Type.GetType(column.Type);
						object convertedValue;
						if (ValidateColumnType(columnValue.value.Trim(), columnType, out convertedValue))
						{
							var propertyInfo = (team.GetType()).GetProperty(column.PropertyName);
							if (propertyInfo != null)
								propertyInfo.SetValue(team, convertedValue);
						}
						else
						{
							_logger.LogDebug("Row data is not valid. Ignoring row data");
							_logger.LogDebug(rowData);
							return isValid;
						}

					}
					isValid = true;
				}

				return isValid;
			}
			catch (Exception)
			{

				throw;
			}
		} 
		#endregion
	}
}
