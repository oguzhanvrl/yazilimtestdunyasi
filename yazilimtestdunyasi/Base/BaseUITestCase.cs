using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using System.Threading;
using yazilimtestdunyasi.Base.Helpers;
using yazilimtestdunyasi.Common;
using Logger = yazilimtestdunyasi.Base.Helpers.Logger;

namespace yazilimtestdunyasi.Base
{
    [TestFixture]
    public abstract class BaseUITestCase
    {
        protected static IWebDriver driver;
        Logger UITestLog = Logger.getInstance();
        string errorMessage;
        string BrowserProcessName = "";
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            commandPromptEnterCommand();//selenium grid i direkt komut satırından çalıştırma fonksiyonu -- Hub için
            commandPromptEnterCommand(false); // Node için
            BeforeMethod(BrowserType.Chrome);//burdan seçtiğimiz browser a göre webdriver i ayaga kaldırıyoruz.
        }
        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            //açılan browserlerin kapatılması 
            //
            CloseDriver();

            #region BrowserKill
            Process[] chromeDriverProcesses = Process.GetProcessesByName(BrowserProcessName);
            foreach (var chromeDriverProcess in chromeDriverProcesses)
            {
                try
                {
                    chromeDriverProcess.Kill();
                }
                catch (Exception)
                {
                    continue;
                }
            }
            #endregion

            #region CMDKill
            //Selenium Grid de kullanılan command line ların kapatılması
            Process[] WindowsCommandLines2 = Process.GetProcesses();
            Process[] WindowsCommandLines = Process.GetProcessesByName("java");
            foreach (var commandlines in WindowsCommandLines)
            {
                try
                {
                    commandlines.Kill();
                }
                catch (Exception)
                {
                    continue;
                }
            }
            Process[] commandlines2 = Process.GetProcessesByName("cmd");
            foreach (var cmd in commandlines2)
            {
                try
                {
                    cmd.Kill();
                }
                catch (Exception)
                {
                    continue;
                }
            }
            #endregion
        }


        public void AfterMethod(TestContext currentResult)
        {
            //log
            var status = currentResult.Result.Outcome.Status;
            var stackTrace = string.IsNullOrEmpty(currentResult.Result.StackTrace);
            var message = string.IsNullOrEmpty(currentResult.Result.Message);
            errorMessage = TestContext.CurrentContext.Result.Message;
            
            switch (status)
            {
                case TestStatus.Inconclusive:
                    CustomScreenShoot.TakeScreenshot(driver,currentResult); //ss
                    break;
                case TestStatus.Skipped:

                    break;
                case TestStatus.Passed:
                    break;
                case TestStatus.Warning:
                    CustomScreenShoot.TakeScreenshot(driver, currentResult); //ss
                    break;
                case TestStatus.Failed:
                    //Test Case fail verince ilgili sayfanın screenshot u alınıp log düşülüyor.
                    UITestLog.ExceptionLog(driver.Url, errorMessage, status);
                    CustomScreenShoot.TakeScreenshot(driver, currentResult); //ss  
                    break;

                default:
                                     
                    break;

            }

        }

        internal void SwitchToTab(int tabIndex)
        {
            driver.SwitchTo().Window(driver.WindowHandles[tabIndex]);
            CustomElementWait.WaitForLoad(driver);
        }
        #region CommandLineRun
        public void commandPromptEnterCommand(bool isHub = true)
        {
            string command;
            if (isHub)
            {
                command = @"java -jar internetexplorer.jar -role hub";
            }
            else
            {
                command = @"java -Dwebdriver.chrome.driver=""C:\Users\bilal\OneDrive\Masaüstü\yazilimtestdunyasi\yazilimtestdunyasi\Resources\drivers\chromedriver.exe"" -jar ""selenium-server-standalone-3.141.59.jar"" -role node -hub http://localhost:4444/grid/register/";
            }
            var proc1 = new ProcessStartInfo();
            proc1.UseShellExecute = true;
            proc1.WorkingDirectory = @"C:\Users\bilal\OneDrive\Masaüstü\yazilimtestdunyasi\yazilimtestdunyasi\Resources\SeleniumGrid";
            proc1.FileName = @"C:\Windows\System32\cmd.exe";
            proc1.Verb = "runas";
            proc1.Arguments = "/K " + command;
            proc1.WindowStyle = ProcessWindowStyle.Normal;
            Process.Start(proc1);
            Thread.Sleep(5000);
        }
        #endregion
        public void CloseDriver()
        {
            if (driver != null)
            {
                driver.Close();
                driver.Quit();
            }
        }
        public enum BrowserType
        {
            Chrome,
            Firefox,
            InternetExplorer
        }
        public void BeforeMethod(BrowserType browser)
        {
            try
            {
                if (browser == BrowserType.Chrome)
                {//Chrome Driver in ayaga kaldırılması
                    BrowserProcessName = "chromedriver";
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--test-type");
                    options.AddArgument("--enable-automation");
                    options.AddArgument("--window-size=1920,1080");
                    options.AddArgument("--enable-precise-memory-info");
                    options.SetLoggingPreference(LogType.Browser, LogLevel.All);
                    var capabilities = options.ToCapabilities();
                    // driver = new RemoteWebDriver(new Uri(GridURL), capabilities);
                    driver = new ChromeDriver(@"C:\Users\bilal\OneDrive\Masaüstü\yazilimtestdunyasi\yazilimtestdunyasi\Resources\drivers", options);
                }
                else if (browser == BrowserType.Firefox)
                {//Firefox Driver in ayaga kaldırılması
                    BrowserProcessName = "geckodriver";
                    FirefoxOptions option = new FirefoxOptions();
                    var capabilities = new FirefoxOptions().ToCapabilities();
                    driver = new RemoteWebDriver(new Uri(BaseTestValue.GridURL), capabilities);

                }
                else if (browser == BrowserType.InternetExplorer)
                {//İE Driver in ayaga kaldırılması
                    BrowserProcessName = "IEDriverServer";
                    InternetExplorerOptions options = new InternetExplorerOptions();
                    options.IgnoreZoomLevel = true;
                    options.RequireWindowFocus = true;
                    options.EnableNativeEvents = false;
                    options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    options.EnablePersistentHover = true;
                    var capabilities = options.ToCapabilities();// Node da çalışacak Browser (Internet-Option / Internet-Profile)
                    driver = new RemoteWebDriver(new Uri(BaseTestValue.GridURL), capabilities);//driveri kök drivere baglama
                }
                driver.Manage().Cookies.DeleteAllCookies();
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(BaseTestValue.TimeoutWaitSeconds); // 10 saniye
                //driver.Navigate().GoToUrl(BaseTestValue.AmazonHomePageURL); amazon home page için
                driver.Navigate().GoToUrl(BaseTestValue.HomePageURL);
              
            }
            catch (Exception e)
            {
                errorMessage = TestContext.CurrentContext.Result.Message;
                UITestLog.ExceptionLog(errorMessage, "", e);
            }

        }

       
    }
}
