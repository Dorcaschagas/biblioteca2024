
using biblioteca2024.Model;
using biblioteca2024.Data;
using System.ComponentModel;

namespace biblioteca2024 {
    public class Program
    {
        public static void Main(string[] args)
        {

            DALBiblioteca.CriarTabelaLivros();

            MostrarLivroPorId();
            //MostrarLivros(); //mostra o estado do banco antes de inserir
            
            DALBiblioteca.InserirLivros(); //insere uma nova linha
            
            MostrarLivros(); ////mostra o estado do banco depois de inserir

            DALBiblioteca.ApagarLivro();

            MostrarLivros(); ////mostra o estado do banco depois de inserir



            //DALBiblioteca.ApagarLivro();


            //não é interessante ter também um select com parâmetro?

            //InserirLivros(); //Console.ReadLine();

            //DeletarLivros(); //baseado em qual campo da tabela vai deletar? normalmente é o ID

            //AtualizarLivros(); //qual informação vai ser atualizada?


        }


        //método para exibir os livros

        public static void MostrarLivros() {
            //para exibir os resultados da consulta SQL
            List<Livro> Livros = DALBiblioteca.GetLivros();
            foreach (Livro Livro in Livros)
            {
                Console.WriteLine("Id: {0}", Livro.Id);
                Console.WriteLine("Autor: {0}", Livro.Autor); //forma padrão por índice
                //Console.WriteLine("ID: " + livro.Id); // concatenação
                //Console.Writeline($"ID: {livro.Id}"); //string interpolada
                Console.WriteLine("Título: {0}", Livro.Titulo);
                Console.WriteLine("Editora: " + Livro.Editora);
                Console.WriteLine("Ano de Publicação: " + Livro.AnoPublicacao);
                Console.WriteLine();
            }
        }

        //método para exibir um livro

        // Método para exibir um livro
        public static void MostrarLivroPorId()
        {
            Console.WriteLine("Qual livro você quer buscar? (Digite o ID)");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int id))
            {
                Livro livro = DALBiblioteca.BuscarPorId(id);

                if (livro != null)
                {
                    Console.WriteLine("Id: {0}", livro.Id);
                    Console.WriteLine("Autor: {0}", livro.Autor);
                    Console.WriteLine("Título: {0}", livro.Titulo);
                    Console.WriteLine("Editora: {0}", livro.Editora);
                    Console.WriteLine("Ano de Publicação: {0}", livro.AnoPublicacao);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Nenhum livro encontrado com o ID informado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido. Por favor, digite um número inteiro.");
            }
        }

    }
}
