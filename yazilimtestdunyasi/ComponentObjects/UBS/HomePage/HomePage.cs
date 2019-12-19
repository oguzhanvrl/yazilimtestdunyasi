using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using yazilimtestdunyasi.Base;
using yazilimtestdunyasi.Common;
using yazilimtestdunyasi.ComponentObjects.BaseComponent;

namespace yazilimtestdunyasi.ComponentObjects.UBS.HomePage
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

        private const string StudentComboboxXPath = "//div[@id='navbar']/ul/li[4]/a[@role='button']";
        [FindsBy(How = How.XPath, Using = StudentComboboxXPath)] //Öğrenci Comboboxunun XPath i
        private IWebElement StudentCombobox; //Öğrenci Combobox u

        private const string UBSSelectionXPath = "//*[@id='navbar']/ul/li[4]/ul/li[1]/a"; //UBS Buttonu Xpath i
        [FindsBy(How = How.XPath, Using = UBSSelectionXPath)]
        private IWebElement UBSSelection;


        public void GoToLoginPage()
        {
            CustomElementWait.WaitUntilElementClickable(driver, StudentCombobox);
            StudentCombobox.Click();
            CustomElementWait.WaitUntilElementClickable(driver, UBSSelection);
            UBSSelection.Click();
            //driver.Navigate().Refresh();
            Thread.Sleep(3000);
        }
    }
}

