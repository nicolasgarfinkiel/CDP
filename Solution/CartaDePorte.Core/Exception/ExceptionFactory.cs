using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CartaDePorte.Common;


namespace CartaDePorte.Core.Exception
{
    public class ExceptionFactory
    {
        public ExceptionFactory() { }


        public static BusinessException CreateBusiness(System.Exception exception)
        {
            Tools.Logger.Error(exception);
            return new BusinessException(exception.Message);
        }


        public static BusinessException CreateBusiness(System.Exception exception, String i18nKey)
        {
            Tools.Logger.Error(exception);
            return new BusinessException(exception, i18nKey);
        }

        //public static BusinessException CreateBusiness(String i18nKey)
        //{
        //    return new BusinessException(i18nKey);
        //}


      
    }

}
