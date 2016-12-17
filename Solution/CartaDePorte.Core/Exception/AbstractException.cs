using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Exception
{
    public abstract class AbstractException : System.Exception
    {
        private System.Exception cause = null;

        public AbstractException()
        {
        }

        public AbstractException(System.Exception cause, String message)
            : base(message, cause)
        {
            this.cause = cause;
        }

        public AbstractException(System.Exception cause)
            : this(cause, cause.Message)
        {
            this.cause = cause;
        }

        public System.Exception Cause
        {
            get { return this.cause; }
        }
    }
}
