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
    public class CuitHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string campo = context.Request.QueryString["campo"];
            string dato = context.Request.QueryString["dato"];
        
            context.Response.ContentType = "application/json";
            var obj = new List<jsObject>();

            IList<String> cuits = new List<String>();

            cuits = SolicitudRecibidaDAO.Instance.GetCuitAutoComplete(campo, dato);

            foreach (String cuit in cuits)
            {
                obj.Add(new jsObject { label = cuit.ToString() });
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
}
