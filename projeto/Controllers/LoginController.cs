using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using projeto.Helper;
using projeto.Models;
using projeto.Repositorio;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace projeto.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IProjetoRepositorio _projetoRepositorio;
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IProjetoRepositorio projetoRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _projetoRepositorio = projetoRepositorio;

        }    
        public IActionResult Index()
        { 
            // se o usuario estiver logadod redirecionar para a home
            if(_sessao.BuscarSessaoUsuario() !=  null) return RedirectToAction("Index","Home");
            return View();
        }
        public IActionResult Sobre()
        {
            return View();
        }
        public IActionResult Mapa()
        {
            List<ProjetoModel> projetos = _projetoRepositorio.BuscarTodos(); 
            return View(projetos); 
        }




        [AllowAnonymous] // garante que qualquer visitante possa acessar
        public IActionResult ProjetosVisitantes()
        {
            // Busca todos os projetos
            var projetos = _projetoRepositorio.BuscarTodos();
            return View(projetos); // vai para a view de cards que fizemos
        }

        public IActionResult DetalhesVisitante(int id)
        {
            // Busca o projeto pelo ID
            ProjetoModel projeto = _projetoRepositorio.ListarPorId(id);

            if (projeto == null)
            {
                TempData["MensagemErro"] = "Projeto não encontrado.";
                return RedirectToAction("Projetos"); // volta para a lista
            }

            return View(projeto);
        }



        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index","Login");
        }


        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario =  _usuarioRepositorio.BuscarPorLogin(loginModel.Login);
                    if (usuario == null)
                    {
                        TempData["MensagemErro"] = $"Usuário ou senha inválido, por favor tente novamente.";
                    }

                    if (usuario.SenhaValida(loginModel.Senha))
                    {
                        _sessao.CriarSessaoDoUsuario(usuario);
                        return RedirectToAction("Index", "Home");
                    }

                        TempData["MensagemErro"] = $"Usuário ou senha inválido, por favor tente novamente.";

                }
                return View("Login");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos efetuar o login, tente novamente. Detalhes do erro: {erro.Message}";
                return RedirectToAction("Login");
            }
        }
    }
}
