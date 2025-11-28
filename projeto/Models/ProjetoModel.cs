using System.ComponentModel.DataAnnotations;

namespace projeto.Models
{
    public class ProjetoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do Projeto")]
        public string NomeProjeto { get; set; }
        [Required(ErrorMessage = "Digite o tipo do Projeto")]
        public string Tipo  { get; set; }
        [Required(ErrorMessage = "Digite o publico do Projeto")]
        public string Publico { get; set; }
        [Required(ErrorMessage = "Digite uma breve descrição para o  Projeto")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Digite o telefone do Projeto")]
        [Phone(ErrorMessage ="O celular informado não é valido")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "Digite o CEP do Projeto")]
        public string Cep { get; set; }
        [Required(ErrorMessage = "Digite a rua do Projeto")]
        public string Rua { get; set; }
        [Required(ErrorMessage = "Digite o Bairro do Projeto")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Digite a cidade do Projeto")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Digite o estado do Projeto")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "Digite o numero do Projeto")]
        public int Numero { get; set; }

    }
}
