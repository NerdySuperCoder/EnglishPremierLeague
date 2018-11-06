using EnglishPremierLeague.Common.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace EnglishPremierLeague.Data.Adapters.Validators.CSVValidator
{
	public class CSVValidator : DataValidator
	{
		#region Private fields and properties
		
		private char[] Delimiters { get; set; }
		private List<Column> Columns { get; set; }
		#endregion

		#region Constructor
		public CSVValidator(ILoggerFactory loggerFactory): base(loggerFactory)
		{
			var columnList = new XmlSerializer(typeof(List<Column>), new XmlRootAttribute("Columns"));
			Columns = (List<Column>)columnList.Deserialize(new FileStream(@".\Validators\CSVValidator\CSVTemplate.xml", FileMode.Open));
			Delimiters = new char[] { ',' };
		}
		#endregion

		#region Overidden Methods
		public override bool Validate(string rowData, bool isHeaderRow, out Team team)
		{
			try
			{
				bool isValid = false;
				team = null;
				var columnValues = rowData.Split(Delimiters);

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
						if (ValidateColumnType(columnValue.value, columnType, out convertedValue))
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
