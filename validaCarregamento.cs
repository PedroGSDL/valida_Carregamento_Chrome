using System.Diagnostics; // Namespace para usar Process
using OpenQA.Selenium; // Namespace para o Selenium WebDriver
using OpenQA.Selenium.Chrome; // Namespace para o ChromeDriver
using NAudio.Wave; // Namespace para reprodução de áudio
using OpenQA.Selenium.Support.UI; // Namespace para funcionalidades de suporte do Selenium

class Program
{
    static void Main()
    {
        // Loop infinito para executar o código repetidamente
        while (true)
        {
            // Chama a função para executar o código dentro do looping
            ExecutarCodigo();
        }
    }

    static void ExecutarCodigo()
    {
        // Caminho para o executável do ChromeDriver
        string caminhoChromeDriver = "Caminho do Chrome Driver";

        // Caminho para o .exe do AutoIT
        string caminhoAutoITExecutavel = "Caminho do Script AutoIT";

        // Configura as opções do Chrome
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--start-maximized"); // Maximiza a janela do navegador para que o script do AutoIT funcione corretamente

        // Inicia o driver do Chrome, com a versão certa
        using (IWebDriver driver = new ChromeDriver(caminhoChromeDriver, options))
        {
            try
            {
                // Caminho da página a ser acessada
                driver.Navigate().GoToUrl("Link do Site");

                // Executa o script do AutoIT
                Process.Start(caminhoAutoITExecutavel).WaitForExit();

                // Verifica se o título da página contém o nome respectivo do link (é por meio disso que o código faz a validação)
                if (driver.Title.Contains("T;itulo da Página"))
                {
                    // Esse é o bloco de código que o Rômulo fez para a pesquisa
                    driver.FindElement(By.Name("TxtNI")).SendKeys("123456"); // Insere um número no campo de texto
                    driver.FindElement(By.Name("BtnPesquisar")).Click(); // Clica no botão de pesquisa
                }
                else
                {
                    // Mensagem de erro caso a página não tenha carregado corretamente
                    Console.WriteLine("A página não foi carregada corretamente.");
                    ReproduzirAudio("Caminho do Áudio"); // Reproduz um áudio de aviso
                }
            }
            finally
            {
                // Fecha o navegador para evitar abrir múltiplas instâncias
                driver.Quit();
            }
        }
    }

    static void ReproduzirAudio(string caminhoDoAudio)
    {
        // Informa que o áudio está sendo reproduzido
        Console.WriteLine("Reproduzindo áudio...");
        
        // Inicia o player de áudio
        using (var audioFile = new AudioFileReader(caminhoDoAudio)) // Cria uma instância do leitor de arquivos de áudio
        using (var outputDevice = new WaveOutEvent()) // Cria uma instância do dispositivo de saída de áudio
        {
            outputDevice.Init(audioFile); // Inicializa o dispositivo de saída com o arquivo de áudio
            outputDevice.Play(); // Reproduz o áudio

            // Espera enquanto o áudio estiver sendo reproduzido
            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                System.Threading.Thread.Sleep(100); // Espera 100 ms
            }
        }

        // Mensagem de confirmação de reprodução
        Console.WriteLine("Áudio reproduzido.");
    }
}
