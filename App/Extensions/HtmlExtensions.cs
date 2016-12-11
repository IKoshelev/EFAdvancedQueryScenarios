using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Extensions
{
    public static class HtmlExtensions
    {
        public static string Template<T>(this IEnumerable<T> source, Func<T, string> template)
        {
            return string.Join("", source.Select(template));
        }

        public static void SaveAsHtmlTableFile<T>(this IEnumerable<T> source, string fileName)
        {
            var tableHtml = source.ToHtmlTable();
            FileSaver.SaveStringToFile(fileName, tableHtml);
        }

        public static string ToHtmlTable<T>(this IEnumerable<T> source)
        {
            var style = $@"<style>
table {{
 border-collapse: collapse;
}}
table, th, td {{
 border: 1px solid black;
}}
</style>";

            var type = typeof(T);
            var props = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

            var table = 
$@"
<table>
<thead>
    {props.Template(x => $"<th>{x.Name}</th>")}
</thead>
<body>
    {source.Template(x => 
    $@"<tr>{props.Template(y => $@"
            <td>{y.GetValue(x).ToJson()}</td>")}
        </tr>")}
</tbody>
</table>";

            return style + table;
        }
    }
}
