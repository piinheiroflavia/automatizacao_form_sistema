using SMAP.Infrastructure;
using SMAP.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SMAP.Controllers
{
    //[Authorize(Policy = "RequireAuthenticatedUser")]
    public class LoginController : Controller
    {
        private static AutomationHelper AutomationHelper = new AutomationHelper();


        [HttpGet]
        public ActionResult UserLogin()
        {
            return View("UserLogin");
        }

		[HttpPost]
		public async Task<IActionResult> userlogin(UserView model)
		{


			try
			{
				// preenche o formulário de login no site externo
				string externalsiteurl = AutomationHelper.PerformLoginAndKeepBrowserOpen(model.Username, model.Password);

				// simula informações do usuário obtidas após o login
				var claims = new List<Claim>
			    {
				    new Claim(ClaimTypes.Name, model.Username)
                    // Adicione mais claims conforme necessário
                };

				var claimsIdentity = new ClaimsIdentity(claims, "Usuario");
				var userPrincipal = new ClaimsPrincipal(claimsIdentity);



				HttpContext.Session.SetString("externalsiteurl", externalsiteurl);
				await HttpContext.SignInAsync(userPrincipal);


				return RedirectToAction("index", "home");
			}
			catch (UserAlreadyLoggedInException ex)
			{
				// Lidar com o caso em que o usuário já está logado
				Console.WriteLine("Erro: " + ex.Message);
				return RedirectToAction("UserLogin", "Login"); // Substitua com a ação e controlador reais

			}
			catch (LoginErrorException ex)
			{
				// Lidar com outros erros de login
				Console.WriteLine("Erro: " + ex.Message);
				return RedirectToAction("UserLogin", "Login"); // Substitua com a ação e controlador reais
			}




		}



		[HttpGet]
        public ActionResult RedirectToExternalSite()
        {
            try
            {
                // Obtém a URL específica do navegador armazenada no método UserLogin
                var externalSiteUrl = HttpContext.Session.GetString("ExternalSiteUrl");

                string novoUsuario = "";
                string novaSenha = "";
                string newExternalSiteUrl = AutomationHelper.PerformLoginAndKeepBrowserOpen(novoUsuario, novaSenha);

                // Passa a URL para a ViewBag para ser usada na página intermediária
                ViewBag.ExternalSiteUrl = newExternalSiteUrl;
          
                return RedirectToAction("Index", "Home");
            }
			catch (UserAlreadyLoggedInException ex)
			{
				// Lidar com o caso em que o usuário já está logado
				 Console.WriteLine("Erro UserAlreadyLoggedInException: " + ex.Message);
				return UserInvalid();

			}
			catch (LoginErrorException ex)
			{
				// Lidar com outros erros de login
				Console.WriteLine("Erro: " + ex.Message);
				return UserInvalid();
			}
		}

		private ActionResult UserInvalid()
		{
			TempData["ErrorMessage"] = "Usuário já logado.";

			return View("UserLogin");
		}


		[HttpPost]
        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("UserLogin", "Login");
        }
    }
}
