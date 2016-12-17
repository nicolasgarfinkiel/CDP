using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Exception
{
    public class BusinessException : AbstractException
    {
        private String i18nKey;

        public BusinessException(String i18nKey)
            : this(null, i18nKey)
        {
        }

        public BusinessException(System.Exception exception, String i18nKey)
            : base(exception, i18nKey)
        {
            this.i18nKey = i18nKey;
        }

        public override string Message
        {
            // Acá se debe ir a buscar el mensaje para esta clave.
            get { return this.i18nKey; }
        }
    }
}
