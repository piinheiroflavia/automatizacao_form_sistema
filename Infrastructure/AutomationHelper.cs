using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Security.Cryptography;
using System.Threading;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SMAP.Infrastructure
{
    public class AutomationHelper
    {

        public static string PerformLoginAndKeepBrowserOpen(string username, string password)
        {
            // Crie a instância do ChromeDriver fora do bloco using
            IWebDriver driver = new ChromeDriver();
			      bool loginSuccessful = false;
			      string returnUrl = null;

			      try
            {
                //link que é pra acessar no GoToUrl(" ");
                driver.Navigate().GoToUrl(" ");

                // Preencha os campos de usuário e senha
                IWebElement inputUsername = driver.FindElement(By.Id("login"));
                IWebElement inputPassword = driver.FindElement(By.Id("senha"));
                inputUsername.SendKeys(username);
                inputPassword.SendKeys(password);

               
                IWebElement buttonLogin = driver.FindElement(By.CssSelector("input[type='image'][name='Image1']"));

                buttonLogin.Click();
                // Aguarde o login ser processado (opcional)
                //Thread.Sleep(100);

                // Espere até que a nova guia seja aberta ou que a URL mude
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                //link que é pra acessar noEquals(""));
                wait.Until(d => d.WindowHandles.Count > 1 || !d.Url.Equals(""));
    
                // Mude o foco para a nova guia (última guia aberta)
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                //link que é pra acessar noEquals(""));
                driver.Url = "";


				      returnUrl = driver.Url; // Define a URL se o login for bem-sucedido
				      loginSuccessful = true;
			      }
                  catch (Exception ex)
                  {
				      if (IsUserAlreadyLoggedInError(ex))
				      {
					      // Lidar com o caso em que o usuário já está logado
					      Console.WriteLine("O usuário já está logado.");
					      throw new UserAlreadyLoggedInException("O usuário já está logado.", ex);
				      }
				      else
				      {
					      Console.WriteLine(ex.Message);
					      return "redirect:/login?status=error";
					      throw new LoginErrorException("Erro durante o login.", ex);
				      }
			      }
            finally
            {
                // Não feche o navegador aqui para mantê-lo aberto
                // driver.Quit();
            }
			return loginSuccessful ? returnUrl : null;
		}

		private static bool IsUserAlreadyLoggedInError(Exception ex)
		{
			return ex.Message.Contains("usuário já está logado");
		}
	}
}
