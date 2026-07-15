using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace ValoresData.Commands.CmdSolPract
{
    public class SqlWhereBuilder
    {
        private readonly List<string> _conditions = new();
        private readonly List<SqlParameter> _parameters = new();

        public void Add(string condition, SqlParameter parameter)
        {
            _conditions.Add(condition);
            _parameters.Add(parameter);
        }

        public void Add(string condition, params SqlParameter[] parameters)
        {
            _conditions.Add(condition);
            _parameters.AddRange(parameters);
        }

        public string BuildWhereClause()
        {
            return _conditions.Count == 0 ? string.Empty : "WHERE " + string.Join(" AND ", _conditions);
        }

        public object[] Parameters => _parameters.Cast<object>().ToArray();
    }
}
