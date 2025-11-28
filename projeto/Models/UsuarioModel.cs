using projeto.Enums;
using System.ComponentModel.DataAnnotations;

namespace projeto.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome  do usuário")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o Login  do usuário")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite o Email  do usuário")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Selecione o tipo de Perfil  do usuário")]
        public  PerfilEnum? Perfil  { get; set; }
        [Required(ErrorMessage = "Digite a  senha  do usuário")]
        public string Senha { get; set; }
        public DateTime DataCadrastro { get; set; }
        public DateTime? dataAtualizacao { get; set; }



        public bool SenhaValida(string senha) 
        {
            return Senha == senha;
        }

    }
}
