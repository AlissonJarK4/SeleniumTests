using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ConversorDistancias.Testes.Utils;

namespace ConversorDistancias.Testes.PageObjects
{
    public class ScreenTests
    {
        private IConfiguration _configuration;
        private Browser _browser;
        private IWebDriver _driver;

        public ScreenTests(
            IConfiguration configuration, Browser browser)
        {
            _configuration = configuration;
            _browser = browser;

            string caminhoDriver = null;
            if (browser == Browser.Firefox)
            {
                caminhoDriver =
                    _configuration.GetSection("Selenium:FirefoxDriverPath").Value;
            }
            else if (browser == Browser.Chrome)
            {
                caminhoDriver =
                    _configuration.GetSection("Selenium:ChromeDriverPath").Value;
            }

            _driver = WebDriverFactory.CreateWebDriver(
                browser, caminhoDriver, true);
        }
        public void LoadPage()
        {
            _driver.LoadPage(
                TimeSpan.FromSeconds(Convert.ToInt32(
                    _configuration.GetSection("Selenium:Timeout").Value)),
                _configuration.GetSection("Selenium:AppUrl").Value);
        }

        public void SetValue(string text)
        {
            _driver.SetText(By.ClassName("nav-search-input"), text);
        }

        public void Proccess()
        {
            Thread.Sleep(3000);
            _driver.Submit(By.ClassName("nav-search-input"));

            WebDriverWait wait = new WebDriverWait(
                _driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => d.FindElement(By.ClassName("nav-search-input")) != null);
        }

        public string ReturnValue()
        {
            Thread.Sleep(3000);
            return _driver.GetText(By.ClassName("breadcrumb__title"));
        }

        public void Fechar()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}