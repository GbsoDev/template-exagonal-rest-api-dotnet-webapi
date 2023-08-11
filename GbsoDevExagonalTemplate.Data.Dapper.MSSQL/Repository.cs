using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GbsoDevExagonalTemplate.Data.Dapper.MSSQL
{
	public abstract class Repository
	{
		protected object AddPropery(object obj, string name, object value)
		{
			var dictionary = ConvertObjectToDictionary(obj);
			dictionary.TryAdd(name, value);

			dynamic expando = new ExpandoObject();
			var expandoDict = expando as IDictionary<string, object>;
			foreach (var kvp in dictionary)
			{
				expandoDict![kvp.Key] = kvp.Value;
			}
			return expando;
		}

		protected Dictionary<string, object> ConvertObjectToDictionary(object obj)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			PropertyInfo[] properties = obj.GetType().GetProperties();

			foreach (var property in properties)
			{
				dictionary[property.Name] = property.GetValue(obj)!;
			}
			return dictionary;
		}
	}
}
