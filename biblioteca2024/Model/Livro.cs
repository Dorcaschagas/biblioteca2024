using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca2024.Model{
    public class Livro{
        public int Id {get; set;}
        public string Titulo {get; set;}
        public string Autor {get; set;}
        public string Editora {get; set;}
        public int AnoPublicacao {get; set;}

        public Livro(int id, string titulo, string autor, string editora, int anoPublicacao){
            this.Id = id;
            this.Titulo = titulo;
            this.Autor = autor;
            this.Editora = editora;
            this.AnoPublicacao = anoPublicacao;
        }
    }
}
