using System.Collections.Generic;
using System.Text;

namespace ModrinthSharp.util
{
    public class Utils
    {
        
        public static string ConvertToJsArray(List<string> strings)
        {
            StringBuilder jsArray = new StringBuilder("[");
            for (int i = 0; i < strings.Count; i++)
            {
                jsArray.Append($"\"{strings[i]}\"");
                if (i < strings.Count - 1) jsArray.Append(", ");
            }
            jsArray.Append("]");
            return jsArray.ToString();
        }
        
        public static string ConvertToJsArray(string @string)
        {
            return ConvertToJsArray(new List<string> { @string });
        }
        
    }
}