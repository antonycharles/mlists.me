using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Me.Mlists.Web.ViewModels
{
    public class EmailComButtonViewModel
    {
        public EmailComButtonViewModel(
            string titulo, 
            string linkImagem,
            string descricao, 
            string link, 
            string linkDescricao)
        {
            Titulo = titulo;
            LinkImagem = linkImagem;
            Descricao = descricao;
            Link = link;
            LinkDescricao = linkDescricao;
        }

        public string Titulo { get; set; }
        public string LinkImagem { get; set; }
        public string Descricao { get; set; }
        public string Link { get; set; }
        public string LinkDescricao { get; set; }
    }
}
