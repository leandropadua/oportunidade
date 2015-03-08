using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MinutoSeguros
{
    class WordCounter
    {
        private List<string> _stopWords;

        public WordCounter()
        {
            _stopWords = LoadStopWords();
        }

        /// <summary>
        /// Carrega lista de StopWords de resources
        /// </summary>
        /// <returns>
        /// Lista com todas as stopwords
        /// </returns>
        private List<string> LoadStopWords()
        {
            string stopwords = Properties.Resources.PortugueseStopWords;
            return stopwords.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        /// <summary>
        /// Retorna a lista com as strings mais comuns e sua frequência
        /// </summary>
        /// <param name="str">String que terá suas palavras contadas</param>
        /// <param name="maxNumWords">Quantidade máxima de palavras na lista retornada</param>
        /// <returns></returns>
        public IEnumerable<IGrouping<string, string>> MostCommonWords(string str, int maxNumWords)
        {
            var mostCommonWords =
                Regex.Split(str.ToLower(), @"\W+")
                    .Where(s => s.Length > 2 && !_stopWords.Contains(s))
                    .GroupBy(s => s)
                    .OrderByDescending(g => g.Count()).Take(maxNumWords);
            return mostCommonWords;
        }

        /// <summary>
        /// Conta palavras de uma string
        /// </summary>
        /// <param name="str">String que terá as palavras contadas</param>
        /// <returns>Quantidade de palavras numa string</returns>
        public static int WordCount(string str)
        {
            return Regex.Split(str.ToLower(), "[\\w]+", RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled).Count();
        }
    }
}
