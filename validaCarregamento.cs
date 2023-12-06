using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NAudio.Wave;
using OpenQA.Selenium.Support.UI;


class Program
{
    static void Main()
    {
        while (true)
        {
            //Chama a função para executar o código dentro do looping
            ExecutarCodigo();
        }
    }

    static void ExecutarCodigo()
    {
        //Caminho para o executável do ChromeDriver
        string caminhoChromeDriver = "Caminho do Chrome Driver";

        //Caminho pro .exe do AutoIT
        string caminhoAutoITExecutavel = "Caminho do Script AutoIT";

        //Configura as opções do Chrome
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--start-maximized"); // Maximiza a janela do navegador pra que o Script do AutoIT funcione corretamente

        //Inicia o driver do Chrome, com a versao certa
        using (IWebDriver driver = new ChromeDriver(caminhoChromeDriver, options))
        {
            try
            {
                //Caminho da página
                driver.Navigate().GoToUrl("Link do Site");

                //Executa o script do AutoIT
                Process.Start(caminhoAutoITExecutavel).WaitForExit();

                

                //Verifica se o título da página contém o nome respectivo do link (é por meio disso que o código faz a validação)
                if (driver.Title.Contains("T;itulo da Página"))
                {
                    //Esse é o bloco de código que o Rômulo fez para a pesquisa
                    driver.FindElement(By.Name("TxtNI")).SendKeys("123456");
                    driver.FindElement(By.Name("BtnPesquisar")).Click();

                }
                else
                {
                    Console.WriteLine("A página não foi carregada corretamente.");
                    ReproduzirAudio("Caminho do Áudio");
                }
            }
            finally
            {
                //Fecha o navegador para não bugar tudo com 600 navegadores abertos
                driver.Quit();
            }
        }
    }

    static void ReproduzirAudio(string caminhoDoAudio)
    {
        Console.WriteLine("Reproduzindo áudio...");
        //Inicia o player de áudio
        using (var audioFile = new AudioFileReader(caminhoDoAudio))
        using (var outputDevice = new WaveOutEvent())
        {
            outputDevice.Init(audioFile);
            outputDevice.Play();
            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                System.Threading.Thread.Sleep(100);
            }
        }

        Console.WriteLine("Áudio reproduzido.");
    }
}
