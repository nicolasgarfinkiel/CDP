using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CartaDePorte.Core.Exception;
using CartaDePorte.Core.Domain;

namespace CartaDePorte.Core.DAO
{
    public class RemitoParaguayDAO : BaseDAO
    {

        private static RemitoParaguayDAO instance;
        public RemitoParaguayDAO() { }

        public static RemitoParaguayDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RemitoParaguayDAO();
                }
                return instance;
            }
        }

        public DataTable GetRemitoParaguay(int IdSolicitud)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                DataSet ds = new DataSet();
                var table = new DataTable("RemitoParaguayDS");

                table.Columns.Add(new DataColumn("idsolicitud"));
                table.Columns.Add(new DataColumn("Descripcion"));
                table.Columns.Add(new DataColumn("Cee"));
                table.Columns.Add(new DataColumn("TranspRazonSocial"));
                table.Columns.Add(new DataColumn("TransportistaCUIT"));
                table.Columns.Add(new DataColumn("FechaCreacion"));
                table.Columns.Add(new DataColumn("FechaVencimiento"));
                table.Columns.Add(new DataColumn("NumeroRemision"));
                table.Columns.Add(new DataColumn("FechaDeEmision"));
                table.Columns.Add(new DataColumn("RazonSocial"));
                table.Columns.Add(new DataColumn("CUIT"));
                table.Columns.Add(new DataColumn("Direccion"));
                table.Columns.Add(new DataColumn("MotivoTraslado"));
                table.Columns.Add(new DataColumn("CteDeVta"));
                table.Columns.Add(new DataColumn("EPDireccion"));
                table.Columns.Add(new DataColumn("LocPartida"));
                table.Columns.Add(new DataColumn("ProvPartida"));
                table.Columns.Add(new DataColumn("EDDireccion"));
                table.Columns.Add(new DataColumn("LocLlegada"));
                table.Columns.Add(new DataColumn("ProvLlegada"));
                table.Columns.Add(new DataColumn("KmRecorridos"));
                table.Columns.Add(new DataColumn("PatenteCamion"));
                table.Columns.Add(new DataColumn("PatenteAcoplado"));
                table.Columns.Add(new DataColumn("ChoferRazonSocial"));
                table.Columns.Add(new DataColumn("ChoferCUIT"));
                table.Columns.Add(new DataColumn("ChoferDomicilio"));
                table.Columns.Add(new DataColumn("MarcaVehiculo"));
                table.Columns.Add(new DataColumn("Cantidad"));
                table.Columns.Add(new DataColumn("KG"));
                table.Columns.Add(new DataColumn("DescripcionDetallada"));
                table.Columns.Add(new DataColumn("HabilitacionNum"));
                var sb = new StringBuilder();
                
                sb.AppendLine("SELECT TOP 1 idsolicitud, Descripcion, Cee, FechaCreacion, FechaVencimiento, NumeroRemision, FechaDeEmision, RazonSocial, CUIT, Direccion, MotivoTraslado, CteDeVta, ");
                sb.AppendLine("TranspRazonSocial, TransportistaCUIT, EPDireccion, LocPartida, ProvPartida, EDDireccion, LocLlegada, ProvLlegada, KmRecorridos, PatenteCamion, PatenteAcoplado, ChoferRazonSocial, ChoferCUIT, ");
                sb.AppendLine("ChoferDomicilio, MarcaVehiculo, Cantidad, KG, DescripcionDetallada, HabilitacionNum ");
                sb.AppendLine("FROM vRemitoParaguay ");
                sb.AppendLine("WHERE idsolicitud = " + IdSolicitud.ToString());

                SqlCommand command = new SqlCommand(sb.ToString());
                command.CommandType = CommandType.Text;

                conn1.Open();
                command.Connection = conn1;
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var row = table.NewRow();
                    row["idsolicitud"] = reader["idsolicitud"].ToString();
                    row["Descripcion"] = reader["Descripcion"].ToString();
                    row["Cee"] = reader["Cee"].ToString();
                    row["FechaCreacion"] = Convert.ToDateTime(reader["FechaCreacion"].ToString()).ToShortDateString();
                    row["FechaVencimiento"] = Convert.ToDateTime(reader["FechaVencimiento"].ToString()).ToShortDateString();
                    row["NumeroRemision"] = reader["NumeroRemision"].ToString();
                    row["FechaDeEmision"] = Convert.ToDateTime(reader["FechaDeEmision"].ToString()).ToShortDateString();
                    row["RazonSocial"] = reader["RazonSocial"].ToString();
                    row["CUIT"] = reader["CUIT"].ToString();
                    row["Direccion"] = reader["Direccion"].ToString();
                    row["MotivoTraslado"] = reader["MotivoTraslado"].ToString();
                    row["CteDeVta"] = reader["CteDeVta"].ToString();
                    row["EPDireccion"] = reader["EPDireccion"].ToString();
                    row["LocPartida"] = reader["LocPartida"].ToString();
                    row["ProvPartida"] = reader["ProvPartida"].ToString();
                    row["EDDireccion"] = reader["EDDireccion"].ToString();
                    row["LocLlegada"] = reader["LocLlegada"].ToString();
                    row["ProvLlegada"] = reader["ProvLlegada"].ToString();
                    row["KmRecorridos"] = reader["KmRecorridos"].ToString();
                    row["PatenteCamion"] = reader["PatenteCamion"].ToString();
                    row["PatenteAcoplado"] = reader["PatenteAcoplado"].ToString();
                    row["ChoferRazonSocial"] = reader["ChoferRazonSocial"].ToString();
                    row["ChoferCUIT"] = reader["ChoferCUIT"].ToString();
                    row["ChoferDomicilio"] = reader["ChoferDomicilio"].ToString();
                    row["MarcaVehiculo"] = reader["MarcaVehiculo"].ToString();
                    row["Cantidad"] = reader["Cantidad"].ToString();
                    row["KG"] = reader["KG"].ToString();
                    row["DescripcionDetallada"] = reader["DescripcionDetallada"].ToString();
                    row["TranspRazonSocial"] = reader["TranspRazonSocial"].ToString();
                    row["TransportistaCUIT"] = reader["TransportistaCUIT"].ToString();
                    row["HabilitacionNum"] = reader["HabilitacionNum"].ToString();
                    table.Rows.Add(row);
                }
                return table;
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "GetRemitoParaguay: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }
    }
}