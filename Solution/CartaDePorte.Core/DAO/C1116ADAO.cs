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
    public class C1116ADAO : BaseDAO
    {
         
        private static C1116ADAO instance;
        public C1116ADAO() { }
        public static C1116ADAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new C1116ADAO();
                }
                return instance;
            }
        }

        public int SaveOrUpdate(C1116A o)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                resul = SqlHelper.ExecuteScalar(conn1, "GuardarC1116A", o.Idc1116a,o.NroCertificadoc1116a ,
                    o.CodigoEstablecimiento ,o.CuitProveedor ,o.RazonSocialProveedor ,o.TipoDomicilio ,o.CalleRutaProductor ,o.NroKmProductor ,o.PisoProductor ,o.OficinaDtoProductor ,o.CodigoLocalidadProductor ,o.CodigoPartidoProductor ,
                    o.CodigoPostalProductor ,o.CodigoEspecie ,o.Cosecha ,o.AlmacenajeDiasLibres ,o.TarifaAlmacenaje ,o.GastoGenerales ,o.Zarandeo ,o.SecadoDe ,o.SecadoA ,o.TarifaSecado ,o.PuntoExceso ,o.TarifaOtros ,o.CodigoPartidoOrigen ,
                    o.CodigoPartidoEntrega ,o.NumeroAnalisis ,o.NumeroBoletin ,o.FechaAnalisis ,o.Grado ,o.Factor ,o.ContenidoProteico ,o.CuitLaboratorio ,o.NombreLaboratorio ,o.PesoBruto ,o.MermaVolatil ,o.MermaZarandeo ,o.MermaSecado ,
                    o.PesoNeto ,o.FechaCierre ,o.ImporteIVAServicios ,o.TotalServicios ,o.NumeroCAC,o.UsuarioCreacion);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR SaveOrUpdate C1116A: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

            return Convert.ToInt32(resul);

        }
        public int SaveOrUpdateDetalle(C1116ADetalle o)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                resul = SqlHelper.ExecuteScalar(conn1, "GuardarC1116ADetalle", o.Idc1116aDetalle, o.Idc1116a,
                    o.NumeroCartaDePorte,o.NumeroCertificadoAsociado,o.KgBrutos,o.FechaRemesa);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR SaveOrUpdate C1116A: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }

            return Convert.ToInt32(resul);

        }

        public int deleteDetalleByIDC1116A(string id)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                resul = SqlHelper.ExecuteNonQuery(conn1, "DeleteC1116ADetalle",id);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR SaveOrUpdate C1116A: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }

            return Convert.ToInt32(resul);

        }

        public IList<C1116A> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetC1116A", 0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<C1116A> result = new List<C1116A>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        C1116A c1116A = new C1116A();
                        c1116A.Idc1116a = Convert.ToInt32(row["Idc1116a"].ToString());
                        c1116A.NroCertificadoc1116a = row["NroCertificadoc1116a"].ToString();
                        c1116A.CodigoEstablecimiento = Convert.ToInt32(row["CodigoEstablecimiento"].ToString());
                        c1116A.CuitProveedor = row["CuitProveedor"].ToString();
                        c1116A.RazonSocialProveedor = row["RazonSocialProveedor"].ToString();
                        c1116A.TipoDomicilio = (Enums.TipoDomicilio)Convert.ToInt32(row["TipoDomicilio"].ToString());
                        c1116A.CalleRutaProductor = row["CalleRutaProductor"].ToString();
                        c1116A.NroKmProductor = Convert.ToInt32(row["NroKmProductor"].ToString());
                        c1116A.PisoProductor = row["PisoProductor"].ToString();
                        c1116A.OficinaDtoProductor = row["OficinaDtoProductor"].ToString();
                        c1116A.CodigoLocalidadProductor = Convert.ToInt32(row["CodigoLocalidadProductor"].ToString());
                        c1116A.CodigoPartidoProductor = Convert.ToInt32(row["CodigoPartidoProductor"].ToString());
                        c1116A.CodigoPostalProductor = row["CodigoPostalProductor"].ToString();
                        c1116A.CodigoEspecie = Convert.ToInt32(row["CodigoEspecie"].ToString());
                        c1116A.Cosecha = row["Cosecha"].ToString();
                        c1116A.AlmacenajeDiasLibres = Convert.ToInt32(row["AlmacenajeDiasLibres"].ToString());
                        c1116A.TarifaAlmacenaje = Convert.ToDecimal(row["TarifaAlmacenaje"].ToString());
                        c1116A.GastoGenerales = Convert.ToDecimal(row["GastoGenerales"].ToString());
                        c1116A.Zarandeo = Convert.ToDecimal(row["Zarandeo"].ToString());
                        c1116A.SecadoDe = Convert.ToDecimal(row["SecadoDe"].ToString());
                        c1116A.SecadoA = Convert.ToDecimal(row["SecadoA"].ToString());
                        c1116A.TarifaSecado = Convert.ToDecimal(row["TarifaSecado"].ToString());
                        c1116A.PuntoExceso = Convert.ToDecimal(row["PuntoExceso"].ToString());
                        c1116A.TarifaOtros = Convert.ToDecimal(row["TarifaOtros"].ToString());
                        c1116A.CodigoPartidoOrigen = Convert.ToInt32(row["CodigoPartidoOrigen"].ToString());
                        c1116A.CodigoPartidoEntrega = Convert.ToInt32(row["CodigoPartidoEntrega"].ToString());
                        c1116A.NumeroAnalisis = row["NumeroAnalisis"].ToString();
                        c1116A.NumeroBoletin = row["NumeroBoletin"].ToString();
                        c1116A.FechaAnalisis = Convert.ToDateTime(row["FechaAnalisis"].ToString());
                        c1116A.Grado = row["Grado"].ToString();
                        c1116A.Factor = Convert.ToDecimal(row["Factor"].ToString());
                        c1116A.ContenidoProteico = Convert.ToDecimal(row["ContenidoProteico"].ToString());
                        c1116A.CuitLaboratorio = row["CuitLaboratorio"].ToString();
                        c1116A.NombreLaboratorio = row["NombreLaboratorio"].ToString();
                        c1116A.PesoBruto = Convert.ToDecimal(row["PesoBruto"].ToString());
                        c1116A.MermaVolatil = Convert.ToDecimal(row["MermaVolatil"].ToString());
                        c1116A.MermaZarandeo = Convert.ToDecimal(row["MermaZarandeo"].ToString());
                        c1116A.MermaSecado = Convert.ToDecimal(row["MermaSecado"].ToString());
                        c1116A.PesoNeto = Convert.ToDecimal(row["PesoNeto"].ToString());
                        c1116A.FechaCierre = Convert.ToDateTime(row["FechaCierre"].ToString());
                        c1116A.ImporteIVAServicios = Convert.ToDecimal(row["ImporteIVAServicios"].ToString());
                        c1116A.TotalServicios = Convert.ToDecimal(row["TotalServicios"].ToString());
                        c1116A.NumeroCAC = row["NumeroCAC"].ToString();
                        
                        c1116A.UsuarioCreacion = row["UsuarioCreacion"].ToString();                        
                        if (!(row["FechaCreacion"] is System.DBNull))
                            c1116A.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        
                        c1116A.UsuarioModificacion = row["UsuarioModificacion"].ToString();                        
                        if (!(row["FechaModificacion"] is System.DBNull))
                            c1116A.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);



                        result.Add(c1116A);
                    }

                    return result;
                }
                else
                {
                    return new List<C1116A>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAll C1116A: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public C1116A GetOne(int id)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetC1116A", id);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<C1116A> result = new List<C1116A>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        C1116A c1116A = new C1116A();
                        c1116A.Idc1116a = Convert.ToInt32(row["Idc1116a"].ToString());
                        c1116A.NroCertificadoc1116a = row["NroCertificadoc1116a"].ToString();
                        c1116A.CodigoEstablecimiento = Convert.ToInt32(row["CodigoEstablecimiento"].ToString());
                        c1116A.CuitProveedor = row["CuitProveedor"].ToString();
                        c1116A.RazonSocialProveedor = row["RazonSocialProveedor"].ToString();
                        c1116A.TipoDomicilio = (Enums.TipoDomicilio)Convert.ToInt32(row["TipoDomicilio"].ToString());
                        c1116A.CalleRutaProductor = row["CalleRutaProductor"].ToString();
                        c1116A.NroKmProductor = Convert.ToInt32(row["NroKmProductor"].ToString());
                        c1116A.PisoProductor = row["PisoProductor"].ToString();
                        c1116A.OficinaDtoProductor = row["OficinaDtoProductor"].ToString();
                        c1116A.CodigoLocalidadProductor = Convert.ToInt32(row["CodigoLocalidadProductor"].ToString());
                        c1116A.CodigoPartidoProductor = Convert.ToInt32(row["CodigoPartidoProductor"].ToString());
                        c1116A.CodigoPostalProductor = row["CodigoPostalProductor"].ToString();
                        c1116A.CodigoEspecie = Convert.ToInt32(row["CodigoEspecie"].ToString());
                        c1116A.Cosecha = row["Cosecha"].ToString();
                        c1116A.AlmacenajeDiasLibres = Convert.ToInt32(row["AlmacenajeDiasLibres"].ToString());
                        c1116A.TarifaAlmacenaje = Convert.ToDecimal(row["TarifaAlmacenaje"].ToString());
                        c1116A.GastoGenerales = Convert.ToDecimal(row["GastoGenerales"].ToString());
                        c1116A.Zarandeo = Convert.ToDecimal(row["Zarandeo"].ToString());
                        c1116A.SecadoDe = Convert.ToDecimal(row["SecadoDe"].ToString());
                        c1116A.SecadoA = Convert.ToDecimal(row["SecadoA"].ToString());
                        c1116A.TarifaSecado = Convert.ToDecimal(row["TarifaSecado"].ToString());
                        c1116A.PuntoExceso = Convert.ToDecimal(row["PuntoExceso"].ToString());
                        c1116A.TarifaOtros = Convert.ToDecimal(row["TarifaOtros"].ToString());
                        c1116A.CodigoPartidoOrigen = Convert.ToInt32(row["CodigoPartidoOrigen"].ToString());
                        c1116A.CodigoPartidoEntrega = Convert.ToInt32(row["CodigoPartidoEntrega"].ToString());
                        c1116A.NumeroAnalisis = row["NumeroAnalisis"].ToString();
                        c1116A.NumeroBoletin = row["NumeroBoletin"].ToString();
                        c1116A.FechaAnalisis = Convert.ToDateTime(row["FechaAnalisis"].ToString());
                        c1116A.Grado = (row["Grado"].ToString());
                        c1116A.Factor = Convert.ToDecimal(row["Factor"].ToString());
                        c1116A.ContenidoProteico = Convert.ToDecimal(row["ContenidoProteico"].ToString());
                        c1116A.CuitLaboratorio = (row["CuitLaboratorio"].ToString());
                        c1116A.NombreLaboratorio = row["NombreLaboratorio"].ToString();
                        c1116A.PesoBruto = Convert.ToDecimal(row["PesoBruto"].ToString());
                        c1116A.MermaVolatil = Convert.ToDecimal(row["MermaVolatil"].ToString());
                        c1116A.MermaZarandeo = Convert.ToDecimal(row["MermaZarandeo"].ToString());
                        c1116A.MermaSecado = Convert.ToDecimal(row["MermaSecado"].ToString());
                        c1116A.PesoNeto = Convert.ToDecimal(row["PesoNeto"].ToString());
                        c1116A.FechaCierre = Convert.ToDateTime(row["FechaCierre"].ToString());
                        c1116A.ImporteIVAServicios = Convert.ToDecimal(row["ImporteIVAServicios"].ToString());
                        c1116A.TotalServicios = Convert.ToDecimal(row["TotalServicios"].ToString());
                        c1116A.NumeroCAC = (row["NumeroCAC"].ToString());

                        c1116A.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        if (!(row["FechaCreacion"] is System.DBNull))
                            c1116A.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);

                        c1116A.UsuarioModificacion = row["UsuarioModificacion"].ToString();
                        if (!(row["FechaModificacion"] is System.DBNull))
                            c1116A.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);


                        result.Add(c1116A);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetOne C1116A: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public C1116A GetByCertificado(int numero)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetC1116AByCertificado", numero);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<C1116A> result = new List<C1116A>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        C1116A c1116A = new C1116A();
                        c1116A.Idc1116a = Convert.ToInt32(row["Idc1116a"].ToString());
                        c1116A.NroCertificadoc1116a = (row["NroCertificadoc1116a"].ToString());
                        c1116A.CodigoEstablecimiento = Convert.ToInt32(row["CodigoEstablecimiento"].ToString());
                        c1116A.CuitProveedor = (row["CuitProveedor"].ToString());
                        c1116A.RazonSocialProveedor = row["RazonSocialProveedor"].ToString();
                        c1116A.TipoDomicilio = (Enums.TipoDomicilio)Convert.ToInt32(row["TipoDomicilio"].ToString());
                        c1116A.CalleRutaProductor = row["CalleRutaProductor"].ToString();
                        c1116A.NroKmProductor = Convert.ToInt32(row["NroKmProductor"].ToString());
                        c1116A.PisoProductor = row["PisoProductor"].ToString();
                        c1116A.OficinaDtoProductor = row["OficinaDtoProductor"].ToString();
                        c1116A.CodigoLocalidadProductor = Convert.ToInt32(row["CodigoLocalidadProductor"].ToString());
                        c1116A.CodigoPartidoProductor = Convert.ToInt32(row["CodigoPartidoProductor"].ToString());
                        c1116A.CodigoPostalProductor = row["CodigoPostalProductor"].ToString();
                        c1116A.CodigoEspecie = Convert.ToInt32(row["CodigoEspecie"].ToString());
                        c1116A.Cosecha = row["Cosecha"].ToString();
                        c1116A.AlmacenajeDiasLibres = Convert.ToInt32(row["AlmacenajeDiasLibres"].ToString());
                        c1116A.TarifaAlmacenaje = Convert.ToDecimal(row["TarifaAlmacenaje"].ToString());
                        c1116A.GastoGenerales = Convert.ToDecimal(row["GastoGenerales"].ToString());
                        c1116A.Zarandeo = Convert.ToDecimal(row["Zarandeo"].ToString());
                        c1116A.SecadoDe = Convert.ToDecimal(row["SecadoDe"].ToString());
                        c1116A.SecadoA = Convert.ToDecimal(row["SecadoA"].ToString());
                        c1116A.TarifaSecado = Convert.ToDecimal(row["TarifaSecado"].ToString());
                        c1116A.PuntoExceso = Convert.ToDecimal(row["PuntoExceso"].ToString());
                        c1116A.TarifaOtros = Convert.ToDecimal(row["TarifaOtros"].ToString());
                        c1116A.CodigoPartidoOrigen = Convert.ToInt32(row["CodigoPartidoOrigen"].ToString());
                        c1116A.CodigoPartidoEntrega = Convert.ToInt32(row["CodigoPartidoEntrega"].ToString());
                        c1116A.NumeroAnalisis = row["NumeroAnalisis"].ToString();
                        c1116A.NumeroBoletin = (row["NumeroBoletin"].ToString());
                        c1116A.FechaAnalisis = Convert.ToDateTime(row["FechaAnalisis"].ToString());
                        c1116A.Grado = (row["Grado"].ToString());
                        c1116A.Factor = Convert.ToDecimal(row["Factor"].ToString());
                        c1116A.ContenidoProteico = Convert.ToDecimal(row["ContenidoProteico"].ToString());
                        c1116A.CuitLaboratorio = (row["CuitLaboratorio"].ToString());
                        c1116A.NombreLaboratorio = row["NombreLaboratorio"].ToString();
                        c1116A.PesoBruto = Convert.ToDecimal(row["PesoBruto"].ToString());
                        c1116A.MermaVolatil = Convert.ToDecimal(row["MermaVolatil"].ToString());
                        c1116A.MermaZarandeo = Convert.ToDecimal(row["MermaZarandeo"].ToString());
                        c1116A.MermaSecado = Convert.ToDecimal(row["MermaSecado"].ToString());
                        c1116A.PesoNeto = Convert.ToDecimal(row["PesoNeto"].ToString());
                        c1116A.FechaCierre = Convert.ToDateTime(row["FechaCierre"].ToString());
                        c1116A.ImporteIVAServicios = Convert.ToDecimal(row["ImporteIVAServicios"].ToString());
                        c1116A.TotalServicios = Convert.ToDecimal(row["TotalServicios"].ToString());
                        c1116A.NumeroCAC = (row["NumeroCAC"].ToString());

                        c1116A.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        if (!(row["FechaCreacion"] is System.DBNull))
                            c1116A.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);

                        c1116A.UsuarioModificacion = row["UsuarioModificacion"].ToString();
                        if (!(row["FechaModificacion"] is System.DBNull))
                            c1116A.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                        result.Add(c1116A);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetByCertificado C1116A: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public IList<C1116A> GetByFiltro(string texto)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetC1116AByFiltro", texto);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<C1116A> result = new List<C1116A>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        C1116A c1116A = new C1116A();
                        c1116A.Idc1116a = Convert.ToInt32(row["Idc1116a"].ToString());
                        c1116A.NroCertificadoc1116a = (row["NroCertificadoc1116a"].ToString());
                        c1116A.CodigoEstablecimiento = Convert.ToInt32(row["CodigoEstablecimiento"].ToString());
                        c1116A.CuitProveedor = (row["CuitProveedor"].ToString());
                        c1116A.RazonSocialProveedor = row["RazonSocialProveedor"].ToString();
                        c1116A.TipoDomicilio = (Enums.TipoDomicilio)Convert.ToInt32(row["TipoDomicilio"].ToString());
                        c1116A.CalleRutaProductor = row["CalleRutaProductor"].ToString();
                        c1116A.NroKmProductor = Convert.ToInt32(row["NroKmProductor"].ToString());
                        c1116A.PisoProductor = row["PisoProductor"].ToString();
                        c1116A.OficinaDtoProductor = row["OficinaDtoProductor"].ToString();
                        c1116A.CodigoLocalidadProductor = Convert.ToInt32(row["CodigoLocalidadProductor"].ToString());
                        c1116A.CodigoPartidoProductor = Convert.ToInt32(row["CodigoPartidoProductor"].ToString());
                        c1116A.CodigoPostalProductor = row["CodigoPostalProductor"].ToString();
                        c1116A.CodigoEspecie = Convert.ToInt32(row["CodigoEspecie"].ToString());
                        c1116A.Cosecha = row["Cosecha"].ToString();
                        c1116A.AlmacenajeDiasLibres = Convert.ToInt32(row["AlmacenajeDiasLibres"].ToString());
                        c1116A.TarifaAlmacenaje = Convert.ToDecimal(row["TarifaAlmacenaje"].ToString());
                        c1116A.GastoGenerales = Convert.ToDecimal(row["GastoGenerales"].ToString());
                        c1116A.Zarandeo = Convert.ToDecimal(row["Zarandeo"].ToString());
                        c1116A.SecadoDe = Convert.ToDecimal(row["SecadoDe"].ToString());
                        c1116A.SecadoA = Convert.ToDecimal(row["SecadoA"].ToString());
                        c1116A.TarifaSecado = Convert.ToDecimal(row["TarifaSecado"].ToString());
                        c1116A.PuntoExceso = Convert.ToDecimal(row["PuntoExceso"].ToString());
                        c1116A.TarifaOtros = Convert.ToDecimal(row["TarifaOtros"].ToString());
                        c1116A.CodigoPartidoOrigen = Convert.ToInt32(row["CodigoPartidoOrigen"].ToString());
                        c1116A.CodigoPartidoEntrega = Convert.ToInt32(row["CodigoPartidoEntrega"].ToString());
                        c1116A.NumeroAnalisis = row["NumeroAnalisis"].ToString();
                        c1116A.NumeroBoletin = (row["NumeroBoletin"].ToString());
                        c1116A.FechaAnalisis = Convert.ToDateTime(row["FechaAnalisis"].ToString());
                        c1116A.Grado = (row["Grado"].ToString());
                        c1116A.Factor = Convert.ToDecimal(row["Factor"].ToString());
                        c1116A.ContenidoProteico = Convert.ToDecimal(row["ContenidoProteico"].ToString());
                        c1116A.CuitLaboratorio = (row["CuitLaboratorio"].ToString());
                        c1116A.NombreLaboratorio = row["NombreLaboratorio"].ToString();
                        c1116A.PesoBruto = Convert.ToDecimal(row["PesoBruto"].ToString());
                        c1116A.MermaVolatil = Convert.ToDecimal(row["MermaVolatil"].ToString());
                        c1116A.MermaZarandeo = Convert.ToDecimal(row["MermaZarandeo"].ToString());
                        c1116A.MermaSecado = Convert.ToDecimal(row["MermaSecado"].ToString());
                        c1116A.PesoNeto = Convert.ToDecimal(row["PesoNeto"].ToString());
                        c1116A.FechaCierre = Convert.ToDateTime(row["FechaCierre"].ToString());
                        c1116A.ImporteIVAServicios = Convert.ToDecimal(row["ImporteIVAServicios"].ToString());
                        c1116A.TotalServicios = Convert.ToDecimal(row["TotalServicios"].ToString());
                        c1116A.NumeroCAC = (row["NumeroCAC"].ToString());

                        c1116A.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        if (!(row["FechaCreacion"] is System.DBNull))
                            c1116A.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);

                        c1116A.UsuarioModificacion = row["UsuarioModificacion"].ToString();
                        if (!(row["FechaModificacion"] is System.DBNull))
                            c1116A.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);


                        result.Add(c1116A);
                    }

                    return result;
                }
                else
                {
                    return new List<C1116A>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetOneByFiltro C1116A: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }




        public IList<C1116ADetalle> GetDetalle(string id)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetC1116ADetalle", id);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<C1116ADetalle> result = new List<C1116ADetalle>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        C1116ADetalle c1116Adetalle = new C1116ADetalle();
                        c1116Adetalle.Idc1116a = Convert.ToInt32(row["Idc1116a"].ToString());
                        c1116Adetalle.Idc1116aDetalle = Convert.ToInt32(row["Idc1116aDetalle"].ToString());
                        c1116Adetalle.NumeroCartaDePorte = Convert.ToInt64(row["NumeroCartaDePorte"].ToString());
                        c1116Adetalle.NumeroCertificadoAsociado = Convert.ToInt64(row["NumeroCertificadoAsociado"].ToString());
                        c1116Adetalle.KgBrutos = Convert.ToDecimal(row["KgBrutos"].ToString());
                        c1116Adetalle.FechaRemesa = Convert.ToDateTime(row["FechaRemesa"].ToString());

                        result.Add(c1116Adetalle);
                    }

                    return result;
                }
                else
                {
                    return new List<C1116ADetalle>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetDetalle C1116A: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }






    }
}
