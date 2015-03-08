using System;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using MinutoSeguros.Properties;

namespace MinutoSeguros
{
    class Program
    {
        static void Main(string[] args)
        {
            //Feed de dados do blog
            const string uri = "http://www.minutoseguros.com.br/blog/feed/";
            XElement xml = null;

            //Download do XML do Feed
            try
            {
                xml = XElement.Load(uri);
            }
            catch (WebException)
            {
                //Imprime mensagem de erro no caso de falha no download e finaliza programa
                Console.WriteLine(Resources.ConectionErrorMessage);
                Console.ReadLine();
                return;
            }

            //Seleciona os dez itens de conteúdo
            XNamespace content = "http://purl.org/rss/1.0/modules/content/";
            var contents = xml.Descendants(content + "encoded").Take(10);
            
            // Início do trecho de contagem de palavras e extração do texto do conteúdo.         
            var contentBuilder = new StringBuilder();
            Console.WriteLine("Palavras por artigo:");

            // Para cada artigo conta-se as palavras e concatena no StringBuilder 
            for (var i = 0; i < contents.Count(); i++ )
            {
                //Salva o conteúdo do artigo em contentStr
                var contentStr = contents.ElementAt(i).Value;

                //São removidas as partes que não são exibidas do post, texto em inglês
                contentStr = HTMLParser.RemoveDefaultPartOfContent(contentStr, "The post <a rel=");

                //São removidas as tags HTML
                contentStr = HTMLParser.ExtractTextFromHtml(contentStr);

                //Conta-se a quantidade de palavras do artigo e imprime na tela.
                int wordCount = WordCounter.WordCount(contentStr);
                Console.WriteLine("Artigo {0}: {1}", i+1, wordCount);
                contentBuilder.Append(" " + contentStr);
            }

            //Transforma o conteúdo dos artigos em uma única string
            var str = contentBuilder.ToString();

            //Indica a quantidade de palavras que deseja-se no ranking
            const int maxNumWords = 10;

            //Cria um ranking com as palavras e seu número de aparições
            var wordCounter = new WordCounter();
            var mostCommonWords = wordCounter.MostCommonWords(str, maxNumWords);

            //Imprime as palavras mais comuns
            if(mostCommonWords.Any())
            {
                Console.WriteLine("\nAs palavras mais comuns nos artigos:");
                foreach (var word in mostCommonWords)
                {
                    Console.WriteLine("{0}: {1}", word.Key, word.Count());
                }
            }

            //Aguarda finalização do programa
            Console.ReadLine();
        }
    }
}
