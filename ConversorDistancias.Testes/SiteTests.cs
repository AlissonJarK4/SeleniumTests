using System;
using System.IO;
using System.Globalization;
using Xunit;
using Microsoft.Extensions.Configuration;
using ConversorDistancias.Testes.PageObjects;
using ConversorDistancias.Testes.Utils;

namespace ConversorDistancias.Testes
{
    public class SiteTests
    {
        private IConfiguration _configuration;

        public SiteTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");
            _configuration = builder.Build();

            var padroesBR = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = padroesBR;
            CultureInfo.DefaultThreadCurrentUICulture = padroesBR;
        }

        [Theory]
        [InlineData(Browser.Chrome, "casa", "casa")]
        [InlineData(Browser.Chrome, "carro", "carro")]
        [InlineData(Browser.Chrome, "bicicleta", "bicicleta")]
        public void TestSite(
            Browser browser, string input, string output)
        {
            ScreenTests scr =
                new ScreenTests(_configuration, browser);

            scr.LoadPage();
            scr.SetValue(input);
            scr.Proccess();
            string result = scr.ReturnValue();
            scr.Fechar();

            Assert.Equal(output, result);
        }
    }
}