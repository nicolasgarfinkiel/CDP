using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using CartaDePorte.Common;

namespace CartaDePorte.Core.DAO
{
    public abstract class BaseDAO
    {
        public static string connString;
        public BaseDAO() {
            connString = ConfigurationManager.AppSettings["connString"];            
        }

        public SqlTransaction GetTransaction()
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            return conn.BeginTransaction();

        }



        protected int GetIdEmpresa()
        {
            var idEmpresa = 0;
            try
            {
                idEmpresa = App.Usuario.IdEmpresa;
            }
            catch 
            {
                Tools.Logger.InfoFormat("BaseDAO.GetIdEmpresa idEmpresa=0");
            }

            return idEmpresa;
        }

        protected int GetIdGrupoEmpresa()
        {
            var IdGrupoEmpresa = 0;
            try
            {
                IdGrupoEmpresa = App.Usuario.IdGrupoEmpresa;
            }
            catch 
            {
                Tools.Logger.InfoFormat("BaseDAO.GetIdGrupoEmpresa IdGrupoEmpresa=0");
            }

            return IdGrupoEmpresa;
        }


        protected int ConvertToInt32(object value)
        {
            return Tools.Value2<int>(value, 0);
        }
        protected long ConvertToInt64(object value)
        {
            return Tools.Value2<long>(value, 0);
        }
        protected bool ConvertToBoolean(object value)
        {
            return Tools.Value2<bool>(value, false);
        }
        protected decimal ConvertToDecimal(object value)
        {
            return Tools.Value2<decimal>(value, 0);
        }
    }
}
