using Microsoft.AspNetCore.Mvc;
using projeto.Filters;
using projeto.Models;
using projeto.Repositorio;
using System.Linq.Expressions;

namespace projeto.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ProjetosController : Controller
    {
        private readonly IProjetoRepositorio _projetoRepositorio;
        public ProjetosController(IProjetoRepositorio projetoRepositorio)
        {
            _projetoRepositorio = projetoRepositorio;

        } 

        public IActionResult Index()
        {
            List<ProjetoModel> projetos = _projetoRepositorio.BuscarTodos();
            return View(projetos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ProjetoModel projeto = _projetoRepositorio.ListarPorId(id);
            return View(projeto);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ProjetoModel projeto = _projetoRepositorio.ListarPorId(id);
            return View(projeto);
        }

        public IActionResult Apagar(int id)
        {
            try{
                bool apagado = _projetoRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Projeto apagado com sucesso";
                }
                else
                {
                    TempData["MensagemSucesso"] = "Ops, não foi possivel apagar o projeto";
                }
                
                return RedirectToAction("Index");
            }
            catch(SystemException erro){
                TempData["MensagemSucesso"] = $"Ops, não foi possivel apagar o projeto, mais detalhes: {erro.Message}";
                return RedirectToAction("Index");
            }
        }



        [HttpPost]
        public IActionResult Criar(ProjetoModel projeto)
        {
            try{
                if (!ModelState.IsValid)
                {
                    // Volta para a view com os erros
                    return View(projeto);
                }

                // Se for válido, aí sim adiciona
                _projetoRepositorio.Adicionar(projeto);
                TempData["MensagemSucesso"] = "Projeto cadastrado com sucesso";
                return RedirectToAction("Index");

            }
            catch(System.Exception erro){
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu projeto, tente novamente: detalhes do erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(ProjetoModel projeto)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    _projetoRepositorio.Atualizar(projeto);
                    TempData["MensagemSucesso"] = "Projeto Alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return View("Editar", projeto);

            } catch(SystemException erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos Atualizar seu cadastro do  Projeto, tente novamente: detalhes do erro:{erro.Message}";
                return RedirectToAction("Index");

            }




        }
    }
   
}
