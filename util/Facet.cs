using System.Collections.Generic;
using System.Text;

namespace ModrinthSharp.util
{
    public class Facet
    { // todo also need to make a class for "filters"
        public FacetType Type { get; set; }
        public string Value { get; set; }

        public Facet(FacetType type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Type.ToString().ToLower()}:{Value}";
        }
        
        public static string ToJsArray(List<Facet> facets)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[");
            foreach (var facet in facets) stringBuilder.Append($"[\"{facet}\"],");
            if (facets.Count > 0) stringBuilder.Length--;
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }
    }
}