using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yazilimtestdunyasi.Base;
using yazilimtestdunyasi.Common;
using yazilimtestdunyasi.ComponentObjects.BaseComponent;

namespace yazilimtestdunyasi.ComponentObjects.UBS.LoginPage
{
    internal class LoginPage:BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        public LoginPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(BaseTestValue.TimeoutWaitSeconds));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        #region CreateAccountPageXPath

        private const string LoginFormAreaXPath = "//form[@id='loginForm']";//LoginForm div XPathi
        [FindsBy(How = How.XPath, Using = LoginFormAreaXPath)]
        private IWebElement LoginFromArea;

        [FindsBy(How = How.Id, Using = "username")] //username Input XPathi
        private IWebElement UserNameInput;

        [FindsBy(How = How.Id, Using = "password")] //Password Input XPathi
        private IWebElement UserPasswordInput;

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")] //Giriş yap buttonu  XPathi
        private IWebElement LoginButton;

       
        [FindsBy(How = How.XPath, Using = "//a[@id='settings']")] //User Setting Button XPath
        private IWebElement UserSettingArea;

        private const string UserNameTextXPath = "//a[@id='btnShowProfile']/div/ul/li[1]";
        [FindsBy(How = How.XPath, Using = UserNameTextXPath)] //User Name Text XPath
        private IWebElement UserNameText;
        #endregion

        public string EnterField(Dictionary<string, string> values)
        {
            CustomElementWait.WaitUntilElementVisible(driver, By.XPath(LoginFormAreaXPath)); //hesap oluşturma sayfası gözükene kadar bekletme kodu

            CustomElementWait.WaitUntilElementClickable(driver, UserNameInput);//name inputu tıklanabilir olana kadar bekletme
            ClearAndSenKeys(UserNameInput, values["username"]); // name inputunun girilmesi

            CustomElementWait.WaitUntilElementClickable(driver, UserPasswordInput);//mail inputu tıklanabilir olana kadar bekletme
            ClearAndSenKeys(UserPasswordInput, values["password"]); //name inputunun girilmesi

            CustomElementWait.WaitUntilElementClickable(driver, LoginButton);//login butonu tıklanabilir olana kadar bekletme
            LoginButton.Click(); //login butonuna tıklatma 
            return GetUserName();
        }

        public string GetUserName()
        {
            CustomElementWait.WaitUntilElementClickable(driver, UserSettingArea); //User settings görünene kadar bekletme
            UserSettingArea.Click();

            CustomElementWait.WaitUntilElementVisible(driver, By.XPath(UserNameTextXPath));
            return UserNameText.Text;
        }
    }
}
