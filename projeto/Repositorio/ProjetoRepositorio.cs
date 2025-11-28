using Microsoft.EntityFrameworkCore;
using projeto.Data;
using projeto.Models;

namespace projeto.Repositorio
{
    public class ProjetoRepositorio : IProjetoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ProjetoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }


        public ProjetoModel ListarPorId(int id)
        {
            return _bancoContext.Projetos.FirstOrDefault(x => x.Id == id);
        }



        public ProjetoModel Adicionar(ProjetoModel projeto)
        {
            _bancoContext.Projetos.Add(projeto);
            _bancoContext.SaveChanges();
            return projeto;

        }

        public List<ProjetoModel> BuscarTodos()
        {
            return _bancoContext.Projetos.ToList();
        }

        public ProjetoModel Atualizar(ProjetoModel projeto)
        {
            ProjetoModel projetoDB = ListarPorId(projeto.Id);
            if (projetoDB == null)
            {
                throw new System.Exception("Houve um erro na atualização dos dados do Projeto");
            }

            projetoDB.NomeProjeto = projeto.NomeProjeto;
            projetoDB.Tipo = projeto.Tipo;
            projetoDB.Publico = projeto.Publico;
            projetoDB.Descricao = projeto.Descricao;
            projetoDB.Telefone = projeto.Telefone;
            projetoDB.Cep = projeto.Cep;
            projetoDB.Rua = projeto.Rua;
            projetoDB.Bairro = projeto.Bairro;
            projetoDB.Cidade = projeto.Cidade;
            projetoDB.Estado = projeto.Estado;
            projetoDB.Numero = projeto.Numero;


            _bancoContext.Projetos.Update(projetoDB);
            _bancoContext.SaveChanges();
            return projetoDB;
        }

        public bool Apagar(int id)
        {
            ProjetoModel projetoDB = ListarPorId(id);
            if (projetoDB == null) throw new System.Exception("Houve um erro na Exclusão do Projeto");

            _bancoContext.Projetos.Remove(projetoDB);
            _bancoContext.SaveChanges();
            return true;

        }

     
    }
}
