using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExample.Utility.Common
{
	public interface ICrudWrapper
	{
		Task<List<List<object>>> ExecuteQuery (string query, Dictionary<string, object> parameters = null);
		Task<bool> ExeNonQuery (string query, Dictionary<string, object> parameters);
	}
}
