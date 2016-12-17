using CartaDePorte.Core.Exception;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.DAO
{
    public class ReporteLoteCDPDAO : BaseDAO
    {
        private static ReporteLoteCDPDAO instance;
        public static ReporteLoteCDPDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReporteLoteCDPDAO();
                }
                return instance;
            }
        }

        public DataTable GetReporteCDP(DateTime FD, DateTime FH, string Grano, int Titular, int Intermediario, int RemitenteComercial, int Corredor, int RepresentanteEntregador, int Destinatario, int Transportista, int Chofer, int Procedencia, int Destino, int Cosecha)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                DataSet ds = new DataSet();
                var table = new DataTable("vReporteCDP");

                table.Columns.Add(new DataColumn("IdSolicitud"));
                table.Columns.Add(new DataColumn("TipoCarta"));
                table.Columns.Add(new DataColumn("ObservacionAfip"));
                table.Columns.Add(new DataColumn("NumeroCartaDePorte"));
                table.Columns.Add(new DataColumn("Cee"));
                table.Columns.Add(new DataColumn("Ctg"));
                table.Columns.Add(new DataColumn("FechaDeEmision"));
                table.Columns.Add(new DataColumn("FechaDeCarga"));
                table.Columns.Add(new DataColumn("FechaDeVencimiento"));
                table.Columns.Add(new DataColumn("TitularCDP"));
                table.Columns.Add(new DataColumn("Intermediario"));
                table.Columns.Add(new DataColumn("CteRemitenteComecial"));
                table.Columns.Add(new DataColumn("EsCanjeador"));
                table.Columns.Add(new DataColumn("CteCorredor"));
                table.Columns.Add(new DataColumn("Entregador"));
                table.Columns.Add(new DataColumn("Destinatario"));
                table.Columns.Add(new DataColumn("Destino"));
                table.Columns.Add(new DataColumn("Transportista"));
                table.Columns.Add(new DataColumn("CTransportista"));
                table.Columns.Add(new DataColumn("Chofer"));
                table.Columns.Add(new DataColumn("Grano"));
                table.Columns.Add(new DataColumn("NumeroContrato"));
                table.Columns.Add(new DataColumn("CargaPesadaDestino"));
                table.Columns.Add(new DataColumn("KilogramosEstimados"));
                table.Columns.Add(new DataColumn("ConformeCondicional"));
                table.Columns.Add(new DataColumn("PesoBruto"));
                table.Columns.Add(new DataColumn("PesoTara"));
                table.Columns.Add(new DataColumn("PesoNeto"));
                table.Columns.Add(new DataColumn("Observaciones"));
                table.Columns.Add(new DataColumn("EstProcedencia"));
                table.Columns.Add(new DataColumn("EstDestino"));
                table.Columns.Add(new DataColumn("PatenteCamion"));
                table.Columns.Add(new DataColumn("PatenteAcoplado"));
                table.Columns.Add(new DataColumn("KmRecorridos"));
                table.Columns.Add(new DataColumn("EstadoFlete"));
                table.Columns.Add(new DataColumn("CantHoras"));
                table.Columns.Add(new DataColumn("TarifaReferencia"));
                table.Columns.Add(new DataColumn("TarifaReal"));
                table.Columns.Add(new DataColumn("CtePagador"));
                table.Columns.Add(new DataColumn("EstadoEnAFIP"));
                table.Columns.Add(new DataColumn("EstadoEnSAP"));
                table.Columns.Add(new DataColumn("EstDestinoCambio"));
                table.Columns.Add(new DataColumn("CteDestinatarioCambio"));
                table.Columns.Add(new DataColumn("CodigoAnulacionAfip"));
                table.Columns.Add(new DataColumn("FechaAnulacionAfip"));
                table.Columns.Add(new DataColumn("CodigoRespuestaEnvioSAP"));
                table.Columns.Add(new DataColumn("CodigoRespuestaAnulacionSAP"));
                table.Columns.Add(new DataColumn("FechaCreacion"));
                table.Columns.Add(new DataColumn("UsuarioCreacion"));
                table.Columns.Add(new DataColumn("FechaModificacion"));
                table.Columns.Add(new DataColumn("UsuarioModificacion"));
                table.Columns.Add(new DataColumn("PHumedad"));
                table.Columns.Add(new DataColumn("POtros"));
                table.Columns.Add(new DataColumn("IdCosecha"));
                table.Columns.Add(new DataColumn("CosechaDescripcion"));

                var sb = new StringBuilder();

                sb.AppendLine("Select IdSolicitud, TipoCarta, ObservacionAfip, NumeroCartaDePorte, Cee, Ctg, FechaDeEmision, FechaDeCarga, FechaDeVencimiento, TitularCDP, Intermediario,  ");
                sb.AppendLine("CteRemitenteComecial, EsCanjeador, CteCorredor, Entregador, Destinatario, Destino, Transportista, CTransportista, Chofer, Grano, NumeroContrato,  ");
                sb.AppendLine("CargaPesadaDestino, KilogramosEstimados, ConformeCondicional, PesoBruto, PesoTara, PesoNeto, Observaciones, EstProcedencia, EstDestino, PatenteCamion,  ");
                sb.AppendLine("PatenteAcoplado, KmRecorridos, EstadoFlete, CantHoras, TarifaReferencia, TarifaReal, CtePagador, EstadoEnAFIP, EstadoEnSAP, EstDestinoCambio,  ");
                sb.AppendLine("CteDestinatarioCambio, CodigoAnulacionAfip, FechaAnulacionAfip, CodigoRespuestaEnvioSAP, CodigoRespuestaAnulacionSAP, FechaCreacion, UsuarioCreacion, ");
                sb.AppendLine("FechaModificacion, UsuarioModificacion, PHumedad, POtros, IdCosecha, CosechaDescripcion ");
                sb.AppendLine("FROM vReporteCDP ");
                sb.AppendLine("WHERE FechaDeEmision ");
                sb.AppendLine(string.Format("Between CONVERT(datetime,'{0}') And CONVERT(datetime,'{1} 23:59:59:999')", FD.ToString("yyyy-MM-dd"), FH.ToString("yyyy-MM-dd")));
                sb.AppendLine(string.Format("And IdEmpresa = {0} ", this.GetIdEmpresa()));
                sb.AppendLine(Condicion(Grano, Titular, Intermediario, RemitenteComercial, Corredor, RepresentanteEntregador, Destinatario, Transportista, Chofer, Procedencia, Destino, Cosecha));

                SqlCommand command = new SqlCommand(sb.ToString());
                command.CommandType = CommandType.Text;

                conn1.Open();
                command.Connection = conn1;
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var row = table.NewRow();
                    row["IdSolicitud"] = reader["IdSolicitud"].ToString();
                    row["TipoCarta"] = reader["TipoCarta"].ToString();
                    row["ObservacionAfip"] = reader["ObservacionAfip"].ToString();
                    row["NumeroCartaDePorte"] = reader["NumeroCartaDePorte"].ToString();
                    row["Cee"] = reader["Cee"].ToString();
                    row["Ctg"] = reader["Ctg"].ToString();
                    row["FechaDeEmision"] = reader["FechaDeEmision"].ToString();
                    row["FechaDeCarga"] = reader["FechaDeCarga"].ToString();
                    row["FechaDeVencimiento"] = reader["FechaDeVencimiento"].ToString();
                    row["TitularCDP"] = reader["TitularCDP"].ToString();
                    row["Intermediario"] = reader["Intermediario"].ToString();
                    row["CteRemitenteComecial"] = reader["CteRemitenteComecial"].ToString();
                    row["EsCanjeador"] = reader["EsCanjeador"].ToString();
                    row["CteCorredor"] = reader["CteCorredor"].ToString();
                    row["Entregador"] = reader["Entregador"].ToString();
                    row["Destinatario"] = reader["Destinatario"].ToString();
                    row["Destino"] = reader["Destino"].ToString();
                    row["Transportista"] = reader["Transportista"].ToString();
                    row["CTransportista"] = reader["CTransportista"].ToString();
                    row["Chofer"] = reader["Chofer"].ToString();
                    row["Grano"] = reader["Grano"].ToString();
                    row["NumeroContrato"] = reader["NumeroContrato"].ToString();
                    row["CargaPesadaDestino"] = reader["CargaPesadaDestino"].ToString();
                    row["KilogramosEstimados"] = reader["KilogramosEstimados"].ToString();
                    row["ConformeCondicional"] = reader["ConformeCondicional"].ToString();
                    row["PesoBruto"] = reader["PesoBruto"].ToString();
                    row["PesoTara"] = reader["PesoTara"].ToString();
                    row["PesoNeto"] = reader["PesoNeto"].ToString();
                    row["Observaciones"] = reader["Observaciones"].ToString();
                    row["EstProcedencia"] = reader["EstProcedencia"].ToString();
                    row["EstDestino"] = reader["EstDestino"].ToString();
                    row["PatenteCamion"] = reader["PatenteCamion"].ToString();
                    row["PatenteAcoplado"] = reader["PatenteAcoplado"].ToString();
                    row["KmRecorridos"] = reader["KmRecorridos"].ToString();
                    row["EstadoFlete"] = reader["EstadoFlete"].ToString();
                    row["CantHoras"] = reader["CantHoras"].ToString();
                    row["TarifaReferencia"] = reader["TarifaReferencia"].ToString();
                    row["TarifaReal"] = reader["TarifaReal"].ToString();
                    row["CtePagador"] = reader["CtePagador"].ToString();
                    row["EstadoEnAFIP"] = reader["EstadoEnAFIP"].ToString();
                    row["EstadoEnSAP"] = reader["EstadoEnSAP"].ToString();
                    row["EstDestinoCambio"] = reader["EstDestinoCambio"].ToString();
                    row["CteDestinatarioCambio"] = reader["CteDestinatarioCambio"].ToString();
                    row["CodigoAnulacionAfip"] = reader["CodigoAnulacionAfip"].ToString();
                    row["FechaAnulacionAfip"] = reader["FechaAnulacionAfip"].ToString();
                    row["CodigoRespuestaEnvioSAP"] = reader["CodigoRespuestaEnvioSAP"].ToString();
                    row["CodigoRespuestaAnulacionSAP"] = reader["CodigoRespuestaAnulacionSAP"].ToString();
                    row["FechaCreacion"] = reader["FechaCreacion"].ToString();
                    row["UsuarioCreacion"] = reader["UsuarioCreacion"].ToString();
                    row["FechaModificacion"] = reader["FechaModificacion"].ToString();
                    row["UsuarioModificacion"] = reader["UsuarioModificacion"].ToString();
                    row["PHumedad"] = reader["PHumedad"].ToString();
                    row["POtros"] = reader["POtros"].ToString();
                    row["CosechaDescripcion"] = reader["CosechaDescripcion"].ToString();
                    table.Rows.Add(row);
                }
                return table;
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "vReporteCDP: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }

        public string Condicion(string Grano, int Titular, int Intermediario, int RemitenteComercial, int Corredor, int RepresentanteEntregador, int Destinatario, int Transportista, int Chofer, int Procedencia, int Destino, int Cosecha)
        {
            StringBuilder Condicion = new StringBuilder();

            if (Grano != string.Empty)
                Condicion.Append(" And Grano = '" + Grano.ToString() + "'");
            if (Titular != 0)
                Condicion.Append(" And idProveedorTitularCartaDePorte = " + Titular.ToString());
            if (Intermediario != 0)
                Condicion.Append(" And IdClienteIntermediario = " + Intermediario.ToString());
            if (RemitenteComercial != 0)
                Condicion.Append(" And IdClienteRemitenteComercial = " + RemitenteComercial.ToString());
            if (Corredor != 0)
                Condicion.Append(" And IdClienteCorredor = " + Corredor.ToString());
            if (RepresentanteEntregador != 0)
                Condicion.Append(" And IdClienteEntregador = " + RepresentanteEntregador.ToString());
            if (Destinatario != 0)
                Condicion.Append(" And IdClienteDestinatario = " + Destinatario.ToString());
            if (Transportista != 0)
                Condicion.Append(" And IdProveedorTransportista = " + Transportista.ToString());
            if (Chofer != 0)
                Condicion.Append(" And IdChofer = " + Chofer.ToString());
            if (Procedencia != 0)
                Condicion.Append(" And IdEstablecimientoProcedencia = " + Procedencia.ToString());
            if (Destino != 0)
                Condicion.Append(" And IdEstablecimientoDestino = " + Destino.ToString());
            if (Cosecha != 0)
                Condicion.Append(" And IdCosecha = " + Cosecha.ToString());

            return Condicion.ToString();
        }

        public DataSet GetProveedoresDS()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                return SqlHelper.ExecuteDataset(conn1, "GetProveedor", 0, this.GetIdEmpresa());
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Proveedor: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }

        public DataSet GetEstablecimientoOrigenAllDS()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                var sb = new StringBuilder();
                sb.AppendLine("Select * From vEstablecimientoOrigen ");
                sb.AppendLine(string.Format("Where IdEmpresa = {0} ", this.GetIdEmpresa()));
                sb.AppendLine("Order By Descripcion");

                SqlCommand command = new SqlCommand(sb.ToString());
                command.CommandType = CommandType.Text;

                return SqlHelper.ExecuteDataset(conn1, command.CommandType, sb.ToString());
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get vEstablecimientoOrigen: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }

        public DataSet GetEstablecimientoDestinoAllDS()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                var sb = new StringBuilder();
                sb.AppendLine("Select * From vEstablecimientoDestino ");
                sb.AppendLine(string.Format("Where IdEmpresa = {0} ", this.GetIdEmpresa()));
                sb.AppendLine("Order By Descripcion");

                SqlCommand command = new SqlCommand(sb.ToString());
                command.CommandType = CommandType.Text;

                return SqlHelper.ExecuteDataset(conn1, command.CommandType, sb.ToString());
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get vEstablecimientoDestino: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }

        public DataSet GetClientesDS(string Tipo)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                var sb = new StringBuilder();
                sb.AppendLine("Select * From v" + Tipo);
                sb.AppendLine(string.Format("Where IdEmpresa = {0} ", this.GetIdEmpresa()));
                sb.AppendLine("Order By RazonSocial");

                SqlCommand command = new SqlCommand(sb.ToString());
                command.CommandType = CommandType.Text;

                return SqlHelper.ExecuteDataset(conn1, command.CommandType, sb.ToString());
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Cliente: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }

        public DataSet GetChoferesDS()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                var sb = new StringBuilder();
                sb.AppendLine("Select * From vChoferes ");
                sb.AppendLine(string.Format("Where IdEmpresa = {0} ", this.GetIdEmpresa()));
                sb.AppendLine("Order By RazonSocial");

                SqlCommand command = new SqlCommand(sb.ToString());
                command.CommandType = CommandType.Text;

                return SqlHelper.ExecuteDataset(conn1, command.CommandType, sb.ToString());
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get vChoferes: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }
    }
}
