using System;
using System.Text.RegularExpressions;

namespace MinutoSeguros
{
    public abstract class HTMLParser
    {
        /// <summary>
        /// Remove as tags HTML de uma string
        /// </summary>
        /// <param name="html">Texto contendo tags HTML</param>
        /// <returns>Texto sem as tags HTML</returns>
        public static string ExtractTextFromHtml(string html)
        {
            var tagMatch = new Regex("<[^>]+>");
            return tagMatch.Replace(html, "");
        }

        /// <summary>
        /// Remove um conteúdo ao final de uma string
        /// </summary>
        /// <param name="contentStr">String que terá seu conteúdo removido</param>
        /// <param name="defaultPart">Padrão de que inicia a parte do conteúdo a ser removida</param>
        /// <returns>String sem o conteúdo indesejado</returns>
        public static string RemoveDefaultPartOfContent(string contentStr, string defaultPart)
        {
            var indexOfUselessPart = contentStr.IndexOf(defaultPart, StringComparison.Ordinal);
            return (indexOfUselessPart != -1) ? contentStr.Substring(0, indexOfUselessPart) : contentStr;
        }
    }
}
