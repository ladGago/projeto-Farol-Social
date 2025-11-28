using Microsoft.AspNetCore.Mvc;
using projeto.Filters;
using projeto.Models;
using projeto.Repositorio;

namespace projeto.Controllers
{
    [PaginaParaAdministradores]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;

        }

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }


        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MenssagemSucesso"] = "Usuário apagado com sucesso";
                }
                else
                {
                    TempData["MenssagemSucesso"] = "Ops, não foi possivel apagar o Usuário";
                }

                return RedirectToAction("Index");
            }
            catch (SystemException erro)
            {
                TempData["MensagemSucesso"] = $"Ops, não foi possivel apagar o Usuário, mais detalhes: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Volta para a view com os erros
                    return View(usuario);
                }

                // Se for válido, aí sim adiciona
                usuario = _usuarioRepositorio.Adicionar(usuario);
                TempData["MenssagemSucesso"] = "Usuário cadastrado com sucesso";
                return RedirectToAction("Index");

            }
            catch (System.Exception erro)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos cadastrar seu Usuário, tente novamente: detalhes do erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Editar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel usuario = null;
                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenhaModel.Id,
                        Nome = usuarioSemSenhaModel.Nome,
                        Login = usuarioSemSenhaModel.Nome,
                        Email = usuarioSemSenhaModel.Email,
                        Perfil = usuarioSemSenhaModel.Perfil
                    };
                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MenssagemSucesso"] = "Usuário Alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(usuario);

            }
            catch (SystemException erro)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos Atualizar seu Usuário, tente novamente: detalhes do erro:{erro.Message}";
                return RedirectToAction("Index");

            }




        }


    }
}
