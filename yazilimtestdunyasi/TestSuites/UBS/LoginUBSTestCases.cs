using NUnit.Framework;
using System;
using System.Collections.Generic;
using yazilimtestdunyasi.Base;
using yazilimtestdunyasi.Common.Generator;
using yazilimtestdunyasi.Common.FileOperations;
using yazilimtestdunyasi.ComponentObjects.BaseComponent;
using yazilimtestdunyasi.ComponentObjects.CreateAccountPage;
using yazilimtestdunyasi.ComponentObjects.UBS.HomePage;
using yazilimtestdunyasi.ComponentObjects.UBS.LoginPage;
using System.Linq;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support;

namespace yazilimtestdunyasi.TestSuites.UBS
{
    class LoginUBSTestCases:BaseUITestCase
    {
        LoginPage loginPage;
        Dictionary<string, string> AccountData;
        [SetUp]
        public void GoToLoginPage()
        {
            SwitchToTab(0);
            new HomePage(driver).GoToLoginPage();
            SwitchToTab(1);
            loginPage = new LoginPage(driver);
        }
        [Order(1)]
        [TestCase("Bilal Aksal")]//valid name
        public void LoginAndCheckUserNameWithValidNameTestMethod(string ExpectedUserName)
        {//test case de kullanılacak datanın ayarlanması
            var capabilities = ((RemoteWebDriver)driver).Capabilities;//browser hakkında bilgi alıp loglamada kullanacağız
            string username = FileOperations.dosyadanOku().Split('-').First();//login datalarının txt den okunması
            string password = FileOperations.dosyadanOku().Split('-').Last();//login datalarının txt den okunması
            AccountData = new Dictionary<string, string>(){
                   { "username", username},
                    { "password", password
                }};
            string UserName = loginPage.EnterField(AccountData);
            Assert.IsTrue(UserName == ExpectedUserName, "Sitede Kayıtlı User Name ile beklenen User Name Uyuşmuyordu!" + Environment.NewLine + "Sitedeki User Name => " + UserName + Environment.NewLine + "Olması Gereken User Name => " + ExpectedUserName + Environment.NewLine + "Testin Yapıldığı Browser =>" + capabilities.GetCapability("browserName") + "  version: " + capabilities.GetCapability("browserVersion"));

            driver.Manage().Cookies.DeleteAllCookies();//bir sonraki case de oturumun açık kalmaması için çerezleri silip o pencereyi kapatıyoruz.
            driver.Close();
        }
        [Order(2)]
        [TestCase("Oguzhan Varol")]//invalid name
        public void LoginAndCheckUserNameWithInValidNameTestMethod(string ExpectedUserName)
        {
            var capabilities = ((RemoteWebDriver)driver).Capabilities;//browser hakkında bilgi alıp loglamada kullanacağız
            string username = FileOperations.dosyadanOku().Split('-').First();//login datalarının txt den okunması
            string password = FileOperations.dosyadanOku().Split('-').Last();//login datalarının txt den okunması
            AccountData = new Dictionary<string, string>(){
                   { "username", username},
                    { "password", password
                }};
            string UserName = loginPage.EnterField(AccountData);
            Assert.IsTrue(UserName == ExpectedUserName, "Sitede Kayıtlı User Name ile beklenen User Name Uyuşmuyordu!" + Environment.NewLine + "Sitedeki User Name => " + UserName + Environment.NewLine + "Olması Gereken User Name => " + ExpectedUserName + Environment.NewLine + "Testin Yapıldığı Browser =>" + capabilities.GetCapability("browserName")+ "  version: "+capabilities.GetCapability("browserVersion"));
        }
        [TearDown]
        public void After()
        {
           
            base.AfterMethod(TestContext.CurrentContext);
            
        }
    }
}
