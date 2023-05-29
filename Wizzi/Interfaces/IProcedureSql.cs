using MySql.Data.MySqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using Wizzi.Dtos.Reportes;

namespace Wizzi.Interfaces
{
    public interface IProcedureSql
    {
        public List<RepAgendamientoAtencion> ExecuteProcedureSql(string nameProcedure, MySqlParameter[] param);
        public MySqlParameter[] ParameterMysqlObject(Object clase);
    }
}
