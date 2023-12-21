namespace ToDo.Servicios.Extensions
{
    public static class StringExtensions
    {
        public static string ToCapitalizate(this string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }
    }
}
