using System.Reflection;

namespace DataUtil.Util.Debug
{
    public static class ToString
    {
        public static string Struct2String(this object data)
        {
            var type = data.GetType();
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            string result = $"{type.Name} {{ ";

            foreach (var field in fields)
            {
                result += $"{field.Name}: {field.GetValue(data)}, ";
            }

            result = result.TrimEnd(',', ' ') + " }";
            return result;
        }
    }
}