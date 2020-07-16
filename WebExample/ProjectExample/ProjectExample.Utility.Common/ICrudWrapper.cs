using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExample.Utility.Common
{
	public interface ICrudWrapper
	{
		string connectionString { get; }
		List<List<object>> ExecuteQuery (string query, Dictionary<string, object> parameters = null);
		string ExeNonQuery (string query, Dictionary<string, object> parameters);
	}
}
