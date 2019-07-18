using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Web;

namespace CombineFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootPath = @"C:\Users\ivan\Desktop\Sources"; //args[0];
            //string rootPath = @"C:\Users\ivan\Desktop\Sources\Backend\Appercode.Api\Install"; //args[0];

            string resultFileName = rootPath + @"\result.html";
            File.Delete(resultFileName);

            var files = Directory.GetFiles(rootPath, "*", SearchOption.AllDirectories);

            StringBuilder resultBuilder = new StringBuilder();
            resultBuilder.Append(@"<html>
   <head>
      <style>
         pre {
            overflow-x: auto;
            white-space: pre-wrap;
            white-space: -moz-pre-wrap;
            white-space: -pre-wrap;
            white-space: -o-pre-wrap;
            word-wrap: break-word;
         }
      </style>
   </head>

   <body>
");

            foreach (var fileName in files)
            {
                if (File.Exists(fileName) && !fileName.Contains("!"))
                {
                    string relativeFileName = fileName.Substring(rootPath.Length);
                    resultBuilder.AppendLine($"<h2>{relativeFileName}</h2>");
                                        
                    var fileText = HttpUtility.HtmlEncode(File.ReadAllText(fileName));
                    resultBuilder.AppendLine("<pre>");
                    resultBuilder.AppendLine(fileText);
                    resultBuilder.AppendLine("</pre>");
                }
            }

            resultBuilder.Append(@"   </body>
</html>");

            File.WriteAllText(resultFileName, resultBuilder.ToString());
        }
    }
}
