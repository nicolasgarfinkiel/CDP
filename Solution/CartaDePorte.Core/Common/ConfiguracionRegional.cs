namespace CartaDePorte.Common
{
    public static class ConfiguracionRegional
    {
        public static string ChagedTextCulture(string Value, string Pais)
        {
            if (Pais == "ARGENTINA")
                Value = Value.Replace("NIT", "CUIT").Replace("RUC", "CUIT");
            else if (Pais == "BOLIVIA")
                Value = Value.Replace("CUIT", "NIT").Replace("RUC", "NIT").Replace("CTG/", string.Empty);
            else if (Pais == "PARAGUAY")
                Value = Value.Replace("NIT", "RUC").Replace("CUIT", "RUC").Replace("CTG/", string.Empty);

            return Value;
        }
    }
}
