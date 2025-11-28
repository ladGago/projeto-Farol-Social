using projeto.Models;

namespace projeto.Repositorio
{
    public interface IProjetoRepositorio
    {
        ProjetoModel ListarPorId(int id);
        List<ProjetoModel> BuscarTodos();
        ProjetoModel Adicionar(ProjetoModel projeto);
        ProjetoModel Atualizar(ProjetoModel projeto);

        bool Apagar(int id);
    }
}
