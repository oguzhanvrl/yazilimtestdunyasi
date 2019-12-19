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

namespace yazilimtestdunyasi.ComponentObjects.HomePage
{
    class HomePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        public HomePage(IWebDriver driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(BaseTestValue.TimeoutWaitSeconds));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        [FindsBy(How = How.XPath, Using = "//div[@id='nav-tools']/a[1]")] //Giriş Yap butonunun XPath i
        private IWebElement SignInButton; //Giriş Yap butonu

        private const string CreateAnAcoountButtonXPath = "//a[@id='createAccountSubmit']"; //Amazon Hesabı Oluşturun butonun Xpath i
        [FindsBy(How = How.XPath, Using = CreateAnAcoountButtonXPath)]
        private IWebElement CreateAnAcoountButton;

        
        public void GoToCreateAccountPage()
        {
            CustomElementWait.WaitUntilElementClickable(driver, SignInButton);
            SignInButton.Click();
            CustomElementWait.WaitUntilElementVisible(driver,By.XPath(CreateAnAcoountButtonXPath));
            CreateAnAcoountButton.Click();
        }
    }
}
