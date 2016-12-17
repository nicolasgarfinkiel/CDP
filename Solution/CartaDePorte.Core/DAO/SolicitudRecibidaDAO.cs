using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CartaDePorte.Core.Domain;
using System.Data.SqlClient;
using CartaDePorte.Core.Exception;
using System.Data;
using CartaDePorte.Core.Servicios;
using System.Reflection;

namespace CartaDePorte.Core.DAO
{
    public class SolicitudRecibidaDAO : BaseDAO
    {

        private static SolicitudRecibidaDAO instance;
        private static string pepe = string.Empty;
        public SolicitudRecibidaDAO() { }

        public static SolicitudRecibidaDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SolicitudRecibidaDAO();
                }
                return instance;
            }
        }

        public int SaveOrUpdate(SolicitudRecibida solicitud)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                if (solicitud.IdSolicitudRecibida > 0)
                {
                    resul = SqlHelper.ExecuteScalar(conn1, "GuardarSolicitudRecibida",
                        solicitud.IdSolicitudRecibida,
                        Convert.ToInt32(solicitud.TipoDeCarta),
                        solicitud.NumeroCartaDePorte,
                        solicitud.Cee,
                        solicitud.Ctg.Replace(".",""),
                        solicitud.FechaDeEmision,
                        solicitud.CuitProveedorTitularCartaDePorte,
                        solicitud.CuitClienteIntermediario,
                        solicitud.CuitClienteRemitenteComercial,
                        solicitud.CuitClienteCorredor,
                        solicitud.CuitClienteEntregador,
                        solicitud.CuitClienteDestinatario,
                        solicitud.CuitClienteDestino,
                        solicitud.CuitProveedorTransportista,
                        solicitud.CuitChofer,                        
                        solicitud.Grano.CosechaAfip.Codigo,
                        solicitud.Grano.EspecieAfip.Codigo,
                        solicitud.NumeroContrato,
                        solicitud.CargaPesadaDestino,
                        solicitud.KilogramosEstimados,
                        Convert.ToInt32(solicitud.ConformeCondicional),
                        solicitud.PesoBruto,
                        solicitud.PesoTara,
                        solicitud.Observaciones,
                        solicitud.CodigoEstablecimientoProcedencia,
                        solicitud.IdLocalidadEstablecimientoProcedencia,
                        solicitud.CodigoEstablecimientoDestino,
                        solicitud.PatenteCamion,
                        solicitud.PatenteAcoplado,
                        solicitud.KmRecorridos,
                        Convert.ToInt32(solicitud.EstadoFlete),
                        solicitud.CantHoras,
                        solicitud.TarifaReferencia,
                        solicitud.TarifaReal,
                        solicitud.Grano.IdGrano,
                        solicitud.CodigoEstablecimientoDestinoCambio,
                        solicitud.IdLocalidadEstablecimientoDestinoCambio,
                        solicitud.CuitEstablecimientoDestinoCambio,
                        solicitud.FechaDeDescarga,
                        solicitud.FechaDeArribo,
                        solicitud.PesoNetoDescarga,
                        solicitud.UsuarioModificacion,
                        this.GetIdEmpresa()
                        );
                        
                    
                }
                else
                    resul = SqlHelper.ExecuteScalar(conn1, "GuardarSolicitudRecibida",
                        solicitud.IdSolicitudRecibida,
                        Convert.ToInt32(solicitud.TipoDeCarta),
                        solicitud.NumeroCartaDePorte,
                        solicitud.Cee,
                        solicitud.Ctg.Replace(".", ""),
                        solicitud.FechaDeEmision,
                        solicitud.CuitProveedorTitularCartaDePorte,
                        solicitud.CuitClienteIntermediario,
                        solicitud.CuitClienteRemitenteComercial,
                        solicitud.CuitClienteCorredor,
                        solicitud.CuitClienteEntregador,
                        solicitud.CuitClienteDestinatario,
                        solicitud.CuitClienteDestino,
                        solicitud.CuitProveedorTransportista,
                        solicitud.CuitChofer,
                        solicitud.Grano.CosechaAfip.Codigo,
                        solicitud.Grano.EspecieAfip.Codigo,
                        solicitud.NumeroContrato,
                        solicitud.CargaPesadaDestino,
                        solicitud.KilogramosEstimados,
                        Convert.ToInt32(solicitud.ConformeCondicional),
                        solicitud.PesoBruto,
                        solicitud.PesoTara,
                        solicitud.Observaciones,
                        solicitud.CodigoEstablecimientoProcedencia,
                        solicitud.IdLocalidadEstablecimientoProcedencia,
                        solicitud.CodigoEstablecimientoDestino,
                        solicitud.PatenteCamion,
                        solicitud.PatenteAcoplado,
                        solicitud.KmRecorridos,
                        Convert.ToInt32(solicitud.EstadoFlete),
                        solicitud.CantHoras,
                        solicitud.TarifaReferencia,
                        solicitud.TarifaReal,
                        solicitud.Grano.IdGrano,
                        solicitud.CodigoEstablecimientoDestinoCambio,
                        solicitud.IdLocalidadEstablecimientoDestinoCambio,
                        solicitud.CuitEstablecimientoDestinoCambio,
                        solicitud.FechaDeDescarga,
                        solicitud.FechaDeArribo,
                        solicitud.PesoNetoDescarga,
                        solicitud.UsuarioCreacion,
                        this.GetIdEmpresa()
                        );

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar Solicitud Recibida: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
            if (solicitud.IdSolicitudRecibida > 0)
            {
                return solicitud.IdSolicitudRecibida;
            }
            else {
                return Convert.ToInt32(resul);
            }                
        }
        public IList<SolicitudRecibida> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudRecibida", 0, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<SolicitudRecibida> result = new List<SolicitudRecibida>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        SolicitudRecibida sol = new SolicitudRecibida();

                        sol.IdSolicitudRecibida = Convert.ToInt32(row["IdSolicitudRecibida"]);
                        sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                        sol.Cee = row["Cee"].ToString();
                        sol.Ctg = row["Ctg"].ToString();
                        sol.TipoDeCarta = (Enums.TipoCartaDePorteRecibida)Convert.ToInt32(row["IdTipoDeCarta"]);
                        sol.CuitProveedorTitularCartaDePorte = row["idProveedorTitularCartaDePorte"].ToString();
                        sol.CodigoEstablecimientoProcedencia = row["CodigoEstablecimientoProcedencia"].ToString();

                        if (!(row["FechaCreacion"] is System.DBNull))
                            sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                        if (!(row["FechaModificacion"] is System.DBNull))
                            sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
                        sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();


                        result.Add(sol);
                    }

                    return result;
                }
                else
                {
                    return new List<SolicitudRecibida>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAll Solicitud Recibida: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public IList<SolicitudRecibida> GetFiltro(string busqueda)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudRecibidaFiltro", busqueda, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<SolicitudRecibida> result = new List<SolicitudRecibida>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        SolicitudRecibida sol = new SolicitudRecibida();

                        pepe = "IDSolicitud: " + Convert.ToInt32(row["IdSolicitudRecibida"]).ToString();

                        sol.IdSolicitudRecibida = Convert.ToInt32(row["IdSolicitudRecibida"]);
                        sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                        sol.Cee = row["Cee"].ToString();
                        sol.Ctg = row["Ctg"].ToString();
                        sol.TipoDeCarta = (Enums.TipoCartaDePorteRecibida)Convert.ToInt32(row["IdTipoDeCarta"]);
                        sol.CuitProveedorTitularCartaDePorte = row["idProveedorTitularCartaDePorte"].ToString();
                        sol.CodigoEstablecimientoProcedencia = row["CodigoEstablecimientoProcedencia"].ToString();

                        if (!(row["FechaCreacion"] is System.DBNull))
                            sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                        if (!(row["FechaModificacion"] is System.DBNull))
                            sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
                        sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                        result.Add(sol);
                    }

                    return result;
                }
                else
                {
                    return new List<SolicitudRecibida>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetFiltro Solicitud Recibida: " + pepe + " - " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public SolicitudRecibida GetOne(int IdSolicitud)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudRecibida", IdSolicitud, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<SolicitudRecibida> result = new List<SolicitudRecibida>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        SolicitudRecibida sol = new SolicitudRecibida();
                        sol.IdSolicitudRecibida = Convert.ToInt32(row["IdSolicitudRecibida"]);
                        sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                        sol.Cee = row["Cee"].ToString();
                        sol.Ctg = row["Ctg"].ToString();
                        sol.TipoDeCarta = (Enums.TipoCartaDePorteRecibida)(Convert.ToInt32(row["IdTipoDeCarta"]));
                        sol.CuitProveedorTitularCartaDePorte = row["idProveedorTitularCartaDePorte"].ToString();

                        if (!(row["FechaDeEmision"] is System.DBNull))
                            sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]);

                        sol.CuitClienteIntermediario = row["IdClienteIntermediario"].ToString();
                        sol.CuitClienteRemitenteComercial = row["IdClienteRemitenteComercial"].ToString();
                        sol.CuitClienteCorredor = row["IdClienteCorredor"].ToString();
                        sol.CuitClienteEntregador = row["IdClienteEntregador"].ToString();
                        sol.CuitClienteDestinatario = row["IdClienteDestinatario"].ToString();

                        sol.CuitClienteDestino = row["IdClienteDestino"].ToString();
                        sol.CuitProveedorTransportista = row["IdProveedorTransportista"].ToString();

                        sol.CuitChofer = row["IdChofer"].ToString();
                        sol.Grano = GranoDAO.Instance.GetOne(Convert.ToInt32(row["IdGrano"]));

                        if (!(row["NumeroContrato"] is System.DBNull))
                            sol.NumeroContrato = Convert.ToInt32(row["NumeroContrato"]);

                        sol.CargaPesadaDestino = Convert.ToBoolean(row["CargaPesadaDestino"]);

                        sol.ConformeCondicional = (Enums.ConformeCondicional)Convert.ToInt32(row["IdConformeCondicional"]);

                        if (sol.CargaPesadaDestino)
                        {
                            sol.KilogramosEstimados = (long)Convert.ToDecimal(row["KilogramosEstimados"]);
                        }
                        else
                        {
                            sol.PesoBruto = (long)Convert.ToDecimal(row["PesoBruto"]);
                            sol.PesoTara = (long)Convert.ToDecimal(row["PesoTara"]);
                            sol.PesoNeto = (sol.PesoBruto - sol.PesoTara);
                        }

                        sol.Observaciones = row["Observaciones"].ToString();
                        sol.LoteDeMaterial = row["LoteDeMaterial"].ToString();

                        sol.CodigoEstablecimientoProcedencia = row["CodigoEstablecimientoProcedencia"].ToString();
                        sol.IdLocalidadEstablecimientoProcedencia = Convert.ToInt32(row["IdLocalidadEstablecimientoProcedencia"]);

                        sol.PatenteCamion = row["PatenteCamion"].ToString();
                        sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
                        sol.KmRecorridos = (long)Convert.ToDecimal(row["KmRecorridos"]);
                        sol.EstadoFlete = (Enums.EstadoFlete)Convert.ToInt32(row["EstadoFlete"]);
                        sol.CantHoras = (long)Convert.ToDecimal(row["CantHoras"]);
                        sol.TarifaReferencia = Convert.ToDecimal(row["TarifaReferencia"]);
                        sol.TarifaReal = Convert.ToDecimal(row["TarifaReal"]);


                        sol.CodigoEstablecimientoDestinoCambio = row["CodigoEstablecimientoDestinoCambio"].ToString();
                        sol.IdLocalidadEstablecimientoDestinoCambio = Convert.ToInt32(row["IdLocalidadEstablecimientoDestinoCambio"]);
                        sol.CuitEstablecimientoDestinoCambio = row["CuitEstablecimientoDestinoCambio"].ToString();

                        if (!(row["FechaDeDescarga"] is System.DBNull))
                            sol.FechaDeDescarga = Convert.ToDateTime(row["FechaDeDescarga"].ToString());

                        if (!(row["FechaDeArribo"] is System.DBNull))
                            sol.FechaDeArribo = Convert.ToDateTime(row["FechaDeArribo"].ToString());

                        if (!(row["PesoNetoDescarga"] is System.DBNull))
                            sol.PesoNetoDescarga = (long)Convert.ToDecimal(row["PesoNetoDescarga"]); 


                        if (!(row["FechaCreacion"] is System.DBNull))
                            sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        

                        if (!(row["FechaModificacion"] is System.DBNull))
                            sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
                        sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();
                        
                        result.Add(sol);
                    }

                    if (result.Count > 0)
                        return result.First();
                    else
                        return null;
                }
                else
                {
                    return null;
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get GetSolicitudRecibida: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();

            }

        }
        public IList<String> GetCuitAutoComplete(string campo,string dato)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetCuitAutoComplete", campo, dato, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<String> result = new List<String>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        result.Add(row["cuit"].ToString());                        
                    }

                    return result;
                }
                else
                {
                    return new List<String>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetCuitAutoComplete: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        public IList<SolicitudRecibida> GetAllReporteRecibidas(DateTime FD, DateTime FH)
        {

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudReporteRecibidas", FD, FH, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {

                    IList<SolicitudRecibida> result = new List<SolicitudRecibida>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        SolicitudRecibida sol = new SolicitudRecibida();
                        sol.IdSolicitudRecibida = Convert.ToInt32(row["IdSolicitudRecibida"]);
                        sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                        sol.Cee = row["Cee"].ToString();
                        sol.Ctg = row["Ctg"].ToString();
                        sol.TipoDeCarta = (Enums.TipoCartaDePorteRecibida)(Convert.ToInt32(row["IdTipoDeCarta"]));
                        sol.CuitProveedorTitularCartaDePorte = row["idProveedorTitularCartaDePorte"].ToString();

                        if (!(row["FechaDeEmision"] is System.DBNull))
                            sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]);

                        sol.CuitClienteIntermediario = row["IdClienteIntermediario"].ToString();
                        sol.CuitClienteRemitenteComercial = row["IdClienteRemitenteComercial"].ToString();
                        sol.CuitClienteCorredor = row["IdClienteCorredor"].ToString();
                        sol.CuitClienteEntregador = row["IdClienteEntregador"].ToString();
                        sol.CuitClienteDestinatario = row["IdClienteDestinatario"].ToString();

                        sol.CuitClienteDestino = row["IdClienteDestino"].ToString();
                        sol.CuitProveedorTransportista = row["IdProveedorTransportista"].ToString();

                        sol.CuitChofer = row["IdChofer"].ToString();
                        sol.Grano = GranoDAO.Instance.GetOne(Convert.ToInt32(row["IdGrano"]));

                        if (!(row["NumeroContrato"] is System.DBNull))
                            sol.NumeroContrato = Convert.ToInt32(row["NumeroContrato"]);

                        sol.CargaPesadaDestino = Convert.ToBoolean(row["CargaPesadaDestino"]);

                        sol.ConformeCondicional = (Enums.ConformeCondicional)Convert.ToInt32(row["IdConformeCondicional"]);

                        if (sol.CargaPesadaDestino)
                        {
                            sol.KilogramosEstimados = (long)Convert.ToDecimal(row["KilogramosEstimados"]);
                        }
                        else
                        {
                            sol.PesoBruto = (long)Convert.ToDecimal(row["PesoBruto"]);
                            sol.PesoTara = (long)Convert.ToDecimal(row["PesoTara"]);
                            sol.PesoNeto = (sol.PesoBruto - sol.PesoTara);
                        }

                        sol.Observaciones = row["Observaciones"].ToString();
                        sol.LoteDeMaterial = row["LoteDeMaterial"].ToString();

                        sol.CodigoEstablecimientoProcedencia = row["CodigoEstablecimientoProcedencia"].ToString();
                        sol.IdLocalidadEstablecimientoProcedencia = Convert.ToInt32(row["IdLocalidadEstablecimientoProcedencia"]);

                        sol.PatenteCamion = row["PatenteCamion"].ToString();
                        sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
                        sol.KmRecorridos = (long)Convert.ToDecimal(row["KmRecorridos"]);
                        sol.EstadoFlete = (Enums.EstadoFlete)Convert.ToInt32(row["EstadoFlete"]);
                        sol.CantHoras = (long)Convert.ToDecimal(row["CantHoras"]);
                        sol.TarifaReferencia = Convert.ToDecimal(row["TarifaReferencia"]);
                        sol.TarifaReal = Convert.ToDecimal(row["TarifaReal"]);


                        sol.CodigoEstablecimientoDestinoCambio = row["CodigoEstablecimientoDestinoCambio"].ToString();
                        sol.IdLocalidadEstablecimientoDestinoCambio = Convert.ToInt32(row["IdLocalidadEstablecimientoDestinoCambio"]);
                        sol.CuitEstablecimientoDestinoCambio = row["CuitEstablecimientoDestinoCambio"].ToString();

                        if (!(row["FechaDeDescarga"] is System.DBNull))
                            sol.FechaDeDescarga = Convert.ToDateTime(row["FechaDeDescarga"].ToString());

                        if (!(row["FechaDeArribo"] is System.DBNull))
                            sol.FechaDeArribo = Convert.ToDateTime(row["FechaDeArribo"].ToString());

                        if (!(row["PesoNetoDescarga"] is System.DBNull))
                            sol.PesoNetoDescarga = (long)Convert.ToDecimal(row["PesoNetoDescarga"]);


                        if (!(row["FechaCreacion"] is System.DBNull))
                            sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();


                        if (!(row["FechaModificacion"] is System.DBNull))
                            sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
                        sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();


                        result.Add(sol);
                    }

                    if (result.Count > 0)
                        return result;
                    else
                        return new List<SolicitudRecibida>();



                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAllReporte SolicitudRecibida: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }


            return new List<SolicitudRecibida>();
        }


        private static DataSet GetDataTableFromIListGeneric<T>(IList<T> aIList)
        {
            DataTable dTable = new DataTable();
            object baseObj = aIList[0];
            Type objectType = baseObj.GetType();
            PropertyInfo[] properties = objectType.GetProperties();

            DataColumn col;
            foreach (PropertyInfo property in properties)
            {
                if ((string)property.Name != "EmpresaNoDataSet")
                {
                    col = new DataColumn();
                    col.ColumnName = (string)property.Name;
                    //col.DataType = property.PropertyType;
                    dTable.Columns.Add(col);
                }
            }
            //Adds the rows to the table
            DataRow row;
            foreach (object objItem in aIList)
            {
                row = dTable.NewRow();

                foreach (PropertyInfo property in properties)
                {
                    if (property.Name != "EmpresaNoDataSet")
                    {
                        row[property.Name] = property.GetValue(objItem, null);
                    }
                }
                dTable.Rows.Add(row);
            }

            DataSet ds = new DataSet("Resultado");
            ds.Tables.Add(dTable);
            return ds;

        }




    }

}
