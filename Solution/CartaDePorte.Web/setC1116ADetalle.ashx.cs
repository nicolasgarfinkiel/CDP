using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CartaDePorte.Web
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    public class setC1116ADetalle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            string prefix = context.Request.QueryString["q"];
            context.Response.ContentType = "application/json";
            IList<C1116ADetalle> detalles = new List<C1116ADetalle>();

            var obj = JObject.Parse(prefix);
            var events = (JArray)obj["lineas"];
            foreach (JObject evt in events)
            {
                String Idc1116aDetalle = (evt["Idc1116aDetalle"] != null) ? evt["Idc1116aDetalle"].Value<string>() : "0";
                String Idc1116a = (evt["Idc1116a"] != null) ? evt["Idc1116a"].Value<string>() : "0"; 
                String NumeroCartaDePorte = evt["NumeroCartaDePorte"].Value<string>();
                String NumeroCertificadoAsociado = evt["NumeroCertificadoAsociado"].Value<string>();
                String KgBrutos = evt["KgBrutos"].Value<string>().Replace(".", ",");
                String FechaRemesa = evt["FechaRemesa"].Value<string>();

                C1116ADetalle det = new C1116ADetalle();
                det.Idc1116aDetalle = Convert.ToInt32(Idc1116aDetalle);
                det.Idc1116a = Convert.ToInt32(Idc1116a);
                det.NumeroCartaDePorte = Convert.ToInt64(NumeroCartaDePorte);
                det.NumeroCertificadoAsociado = Convert.ToInt64(NumeroCertificadoAsociado);
                det.KgBrutos = Convert.ToDecimal(KgBrutos);
                det.FechaRemesa = Convert.ToDateTime(FechaRemesa);
                detalles.Add(det);


            }

            

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }


}

