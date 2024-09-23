using System;
using System.Collections.Generic;
using System.Data.SQLite;
using biblioteca2024.Model;

namespace biblioteca2024.Data
{
    public class DALBiblioteca
    {

        //criacao da tabela no banco de dados
        public static void CriarTabelaLivros()
        {
            try
            {
                using var conexao = DbConnection.GetConnection();
                using var comando = conexao.CreateCommand();
                comando.CommandText = @"
                CREATE TABLE IF NOT EXISTS livros (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Titulo TEXT NOT NULL,
                    Autor TEXT NOT NULL,
                    Editora TEXT NOT NULL,
                    AnoPublicacao INTEGER NOT NULL
                );";
                comando.ExecuteNonQuery();
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("Erro SQL ao criar tabela. Detalhes: " + e.Message);
                throw;
            }
            catch (Exception e1)
            {
                Console.WriteLine("Erro genérico ao criar tabela. Detalhes: " + e1.Message);
                throw;
            }
        }

        //buscando os liros no banco de dados
        public static List<Livro> GetLivros()
        {
            List<Livro> livros = new List<Livro>();

            try
            {
                using (var conexao = DbConnection.GetConnection())
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = "SELECT * FROM livros";

                    using (var leitor = comando.ExecuteReader())
                    {
                        while (leitor.Read())
                        {
                            Livro livro = new Livro(
                                Convert.ToInt32(leitor["Id"]),
                                leitor["Titulo"].ToString(),
                                leitor["Autor"].ToString(),
                                leitor["Editora"].ToString(),
                                Convert.ToInt32(leitor["anoPublicacao"])
                            );
                            livros.Add(livro);
                        }
                    }
                }

                return livros;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("Erro SQL ao realizar consulta SQL. Detalhes: " + e.Message);
                throw;
            }
            catch (Exception e1)
            {
                Console.WriteLine("Erro genérico ao realizar consulta SQL. Detalhes: " + e1.Message);
                throw;
            }
        }

        // Método para inserir novos livros no banco de dados
        public static void InserirLivros()
        {
            try
            {
                Console.WriteLine("Digite o ID do livro Para Inserir: ");
                int Id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Digite o Título do livro: ");
                string Titulo = Console.ReadLine();

                Console.WriteLine("Digite o Autor do livro: ");
                string Autor = Console.ReadLine();

                Console.WriteLine("Digite a Editora do livro: ");
                string Editora = Console.ReadLine();

                Console.WriteLine("Digite o Ano de Publicação do livro: ");
                int AnoPublicacao = Convert.ToInt32(Console.ReadLine());

                using (var conexao = DbConnection.GetConnection())
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = "INSERT INTO livros (id, titulo, autor, editora, anoPublicacao) VALUES (@id, @titulo, @autor, @editora, @anoPublicacao)";
                    comando.Parameters.AddWithValue("@id", Id);
                    comando.Parameters.AddWithValue("@titulo", Titulo);
                    comando.Parameters.AddWithValue("@autor", Autor);
                    comando.Parameters.AddWithValue("@editora", Editora);
                    comando.Parameters.AddWithValue("@anoPublicacao", AnoPublicacao);

                    comando.ExecuteNonQuery();
                }

                Console.WriteLine("Dados inseridos com sucesso!\n");
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("Erro SQL ao realizar consulta SQL. Detalhes: " + e.Message);
                throw;
            }
            catch (Exception e1)
            {
                Console.WriteLine("Erro genérico ao realizar consulta SQL. Detalhes: " + e1.Message);
                throw;
            }
        }

        // Método para apagar um livro pelo ID
        public static void ApagarLivro()
        {
            try
            {
                Console.WriteLine("Digite o ID do Livro que Você Deseja Apagar: ");
                int Id = Convert.ToInt32(Console.ReadLine());

                var livroExiste = BuscarPorId(Id);

                if (livroExiste != null)
                {
                    using (var conexao = DbConnection.GetConnection())
                    using (var comando = conexao.CreateCommand())
                    {
                        comando.CommandText = "DELETE FROM livros WHERE id = @id";
                        comando.Parameters.AddWithValue("@id", Id);

                        int rowsAffected = comando.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Livro apagado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Nenhum livro foi apagado.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Nenhum livro foi encontrado.");
                }
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("Erro SQL ao realizar consulta SQL. Detalhes: " + e.Message);
                throw;
            }
            catch (Exception e1)
            {
                Console.WriteLine("Erro genérico ao realizar consulta SQL. Detalhes: " + e1.Message);
                throw;
            }
        }

        // Método para buscar um livro pelo ID
        public static Livro BuscarPorId(int id)
        {
            try
            {
                using (var conexao = DbConnection.GetConnection())
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = "SELECT * FROM livros WHERE id = @id";
                    comando.Parameters.AddWithValue("@id", id);

                    using (var leitor = comando.ExecuteReader())
                    {
                        if (leitor.Read())
                        {
                            Livro livro = new Livro(
                                Convert.ToInt32(leitor["Id"]),
                                leitor["Titulo"].ToString(),
                                leitor["Autor"].ToString(),
                                leitor["Editora"].ToString(),
                                Convert.ToInt32(leitor["AnoPublicacao"])
                            );

                            return livro;
                        }
                        else
                        {
                            Console.WriteLine("Nenhum livro encontrado com o ID informado.");
                            return null;
                        }
                    }
                }
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("Erro SQL ao buscar livro. Detalhes: " + e.Message);
                throw;
            }
            catch (Exception e1)
            {
                Console.WriteLine("Erro genérico ao buscar livro. Detalhes: " + e1.Message);
                throw;
            }
        }
    }
}
