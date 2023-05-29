using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Reflection;
using Wizzi.Helpers;
using Wizzi.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using Wizzi.Dtos.Reportes;

namespace Wizzi.Services
{
    public class ServiceProcedureSql : IProcedureSql
    {
        public readonly DataContext _datacontext;
        public ServiceProcedureSql(DataContext dataContext)
        {
            _datacontext = dataContext;
        }
        public List<RepAgendamientoAtencion> ExecuteProcedureSql(string nameProcedure, MySqlParameter[] param)
        {
            List<RepAgendamientoAtencion> resutl = new List<RepAgendamientoAtencion>();
            using (var conn = _datacontext.Database.GetDbConnection())
            {
                var cmm = conn.CreateCommand();
                cmm.CommandType = System.Data.CommandType.StoredProcedure;
                cmm.CommandText = nameProcedure;
                cmm.Parameters.AddRange(param);
                cmm.Connection = conn;
                conn.Open();
                var reader = cmm.ExecuteReader();
                resutl = DataReaderMapToList<RepAgendamientoAtencion>(reader);
                
            }
            return resutl;
        }

        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name].ToString(), null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        public MySqlParameter[] ParameterMysqlObject(Object oss)
        {
            Type t = oss.GetType();
            PropertyInfo[] pi = t.GetProperties();
            MySqlParameter[] param = new MySqlParameter[pi.Length];
            int contador = 0;
            foreach (PropertyInfo p in pi)
            {
                var idCab = p.GetValue(oss);
                param[contador] = (new MySqlParameter() { ParameterName = $"@{p.Name}", Value = idCab });

            }
            return param;
        }
    }
}
