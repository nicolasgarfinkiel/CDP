//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data.SqlClient;
//using CartaDePorte.Core.Domain;
//using System.Data;

//namespace CartaDePorte.Core.DAO
//{
//    public class CargaMasivaDAO
//    {

//        private static string connString;

//        public CargaMasivaDAO()
//        {
//            connString =  System.Configuration.ConfigurationSettings.AppSettings["connString"];

//        }

//        public SqlTransaction GetTransaction()
//        {
//            SqlConnection conn = new SqlConnection(connString);
//            conn.Open();
//            return conn.BeginTransaction();

//        }

//        public int GuardarProvincia(sbyte Codigo, String desc)
//        {
//            SqlConnection conn1 = null;
//            try
//            {
//                string sql = string.Empty;
//                conn1 = new SqlConnection(connString);

//                StringBuilder sb = new StringBuilder();
//                sb.Append("INSERT INTO Provincia (Codigo,Descripcion) VALUES");
//                sb.Append("(");
//                sb.Append(Convert.ToInt32(Codigo) + ",");
//                sb.Append("'" + desc + "'");
//                sb.Append(")");

//                sql = sb.ToString();
            
//                return SqlHelper.ExecuteNonQuery(conn1, CommandType.Text, sql);

//            }
//            catch (System.Exception e)
//            {
//                throw e;

//            }
//            finally
//            {
//                conn1.Close();
//            }

//        }

//        public int GuardarLocalidad(sbyte CodigoProv,int CodigoLoc, String desc)
//        {
//            SqlConnection conn1 = null;
//            try
//            {
//                string sql = string.Empty;
//                conn1 = new SqlConnection(connString);

//                StringBuilder sb = new StringBuilder();
//                sb.Append("INSERT INTO Localidad (IdProvincia,Codigo,Descripcion) VALUES");
//                sb.Append("(");
//                sb.Append(Convert.ToInt32(CodigoProv) + ",");
//                sb.Append(CodigoLoc + ",");
//                sb.Append("'" + desc + "'");
//                sb.Append(")");

//                sql = sb.ToString();

//                return SqlHelper.ExecuteNonQuery(conn1, CommandType.Text, sql);

//            }
//            catch (System.Exception e)
//            {
//                throw e;

//            }
//            finally
//            {
//                conn1.Close();
//            }

//        }


//        public int GrabarEspecie(int CodigoLoc, String desc)
//        {
//            SqlConnection conn1 = null;
//            try
//            {
//                string sql = string.Empty;
//                conn1 = new SqlConnection(connString);

//                StringBuilder sb = new StringBuilder();
//                sb.Append("INSERT INTO Especie (Codigo,Descripcion) VALUES");
//                sb.Append("(");
//                sb.Append(CodigoLoc + ",");
//                sb.Append("'" + desc + "'");
//                sb.Append(")");

//                sql = sb.ToString();

//                return SqlHelper.ExecuteNonQuery(conn1, CommandType.Text, sql);

//            }
//            catch (System.Exception e)
//            {
//                throw e;

//            }
//            finally
//            {
//                conn1.Close();
//            }

//        }
//        public int GrabarCosecha(String CodigoLoc, String desc)
//        {
//            SqlConnection conn1 = null;
//            try
//            {
//                string sql = string.Empty;
//                conn1 = new SqlConnection(connString);

//                StringBuilder sb = new StringBuilder();
//                sb.Append("INSERT INTO Cosecha (Codigo,Descripcion) VALUES");
//                sb.Append("(");
//                sb.Append(CodigoLoc + ",");
//                sb.Append("'" + desc + "'");
//                sb.Append(")");

//                sql = sb.ToString();

//                return SqlHelper.ExecuteNonQuery(conn1, CommandType.Text, sql);

//            }
//            catch (System.Exception e)
//            {
//                throw e;

//            }
//            finally
//            {
//                conn1.Close();
//            }

//        }
//        public int GrabarEstablecimiento(long establecimeintoAFIP)
//        {
//            SqlConnection conn1 = null;
//            try
//            {
//                string sql = string.Empty;
//                conn1 = new SqlConnection(connString);

//                StringBuilder sb = new StringBuilder();
//                sb.Append("INSERT INTO EstablecimeintoAFIP (CodigoEstablecimeintoAFIP) VALUES");
//                sb.Append("(");
//                sb.Append(establecimeintoAFIP);
//                sb.Append(")");

//                sql = sb.ToString();

//                return SqlHelper.ExecuteNonQuery(conn1, CommandType.Text, sql);

//            }
//            catch (System.Exception e)
//            {
//                throw e;

//            }
//            finally
//            {
//                conn1.Close();
//            }

//        }


//    }
//}
