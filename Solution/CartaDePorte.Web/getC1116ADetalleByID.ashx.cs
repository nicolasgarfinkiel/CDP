using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain;

namespace CartaDePorte.Web
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    public class getC1116ADetalleByID : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {

            string prefix = context.Request.QueryString["q"];
            context.Response.ContentType = "application/json";
            var obj = new List<jsObjectDetalle>();

            IList<C1116ADetalle> detalles = new List<C1116ADetalle>();

            if (!String.IsNullOrEmpty(prefix))
                detalles = C1116ADAO.Instance.GetDetalle(prefix);


            foreach (C1116ADetalle d in detalles)
            {
                obj.Add(new jsObjectDetalle { Idc1116aDetalle = d.Idc1116aDetalle.ToString(), Idc1116a = d.Idc1116a.ToString(), NumeroCartaDePorte = d.NumeroCartaDePorte.ToString(),
                                              NumeroCertificadoAsociado = d.NumeroCertificadoAsociado.ToString(),
                                              KgBrutos = d.KgBrutos.ToString().Replace(",", "."),
                                              FechaRemesa = d.FechaRemesa.ToString("dd/MM/yyyy")
                });

            }

            var filtered = obj.ToList();

            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            string data = jss.Serialize(filtered);
            context.Response.Write(data);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class jsObjectDetalle
    {
        public string Idc1116aDetalle { get; set; }
        public string Idc1116a { get; set; }
        public string NumeroCartaDePorte { get; set; }
        public string NumeroCertificadoAsociado { get; set; }
        public string KgBrutos { get; set; }
        public string FechaRemesa { get; set; }

    }


}
