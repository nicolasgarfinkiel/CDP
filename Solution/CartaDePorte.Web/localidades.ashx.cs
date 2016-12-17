using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain;

namespace CartaDePorte.Web
{
    public class localidades : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            string prefix = context.Request.QueryString["q"];
            string prefixp = context.Request.QueryString["p"];
            context.Response.ContentType = "application/json";
            var obj = new List<jsObject>();

            IList<Localidad> locs = new List<Localidad>();

            if(!String.IsNullOrEmpty(prefixp))
                locs = LocalidadDAO.Instance.GetLocalidadByText(prefixp);

            if (!String.IsNullOrEmpty(prefix))
                locs = LocalidadDAO.Instance.GetLocalidadByFiltro(prefix);


            foreach (Localidad l in locs)
            {
                obj.Add(new jsObject { label = l.ToString() });
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

    public class jsObject
    {
        public string label { get; set; }
    }


}
