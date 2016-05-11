using System;
using way2_ta01.model;
using way2_ta01.service;

namespace way2_ta01
{
    class Program
    {
        static void Main(string[] args)
        {
            String word, cats = null;
            bool exit = false;
            FindService fs = new FindService();

            do
            {
                try
                {
                    Console.WriteLine("Digite uma palavra e pressione Enter para pesquisar ou pressione Enter para sair.");
                    word = Console.ReadLine();
                    exit = string.IsNullOrEmpty(word);

                    if (!exit)
                    {
                        FindResult result = fs.Search(word);

                        Console.Clear();

                        cats = string.Format("{0} {1} para realizar esta pesquisa.", result.Tentatives, result.Tentatives > 1 ? "gatinhos foram mortos" : "gatinho foi morto");

                        if (result.Success)
                            Console.WriteLine("A palavra {0} está na posicão {1} | {2}", result.Word, result.Index, cats);
                        else
                            Console.WriteLine("A palavra {0} não foi encontrada | {1}", result.Word, cats);
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("Erro: {0}", e.Message);
                }
            } while (!exit);

            fs.Dispose();
        }
    }
}
