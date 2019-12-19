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
namespace yazilimtestdunyasi.ComponentObjects.CreateAccountPage 
{
    class CreateAccountPage : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        public CreateAccountPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(BaseTestValue.TimeoutWaitSeconds));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek

        }
        #region CreateAccountPageXPath

        private const string CreateAccountDivXPath = "//div[@class='a-box a-spacing-extra-large']";//CreateAccount div XPath
        [FindsBy(How = How.ClassName, Using = CreateAccountDivXPath)] 
        private IWebElement CreateAccountDiv;

        [FindsBy(How = How.Id, Using = "ap_customer_name")] //Ad soyad Input XPath
        private IWebElement CustomerNameInput;

        [FindsBy(How = How.Id, Using = "ap_email")] //Email Input XPath
        private IWebElement CustomerMailInput;

        [FindsBy(How = How.Id, Using = "ap_password")] //Password Input XPath
        private IWebElement CustomerPasswordInput;

        [FindsBy(How = How.Id, Using = "ap_password_check")] //Password Check Input XPath
        private IWebElement CustomerPasswordCheckInput;

        [FindsBy(How = How.Id, Using = "continue")] //Continue Button XPath
        private IWebElement ContinueButton;

      
        #endregion

        public bool EnterField(Dictionary<string,string> values)
        {
            CustomElementWait.WaitUntilElementVisible(driver, By.XPath(CreateAccountDivXPath)); //hesap oluşturma sayfası gözükene kadar bekletme kodu

            CustomElementWait.WaitUntilElementClickable(driver, CustomerNameInput);//name inputu tıklanabilir olana kadar bekletme
            ClearAndSenKeys(CustomerNameInput, values["name"]); // name inputunun girilmesi

            CustomElementWait.WaitUntilElementClickable(driver, CustomerMailInput);//mail inputu tıklanabilir olana kadar bekletme
            ClearAndSenKeys(CustomerMailInput, values["mail"]); //name inputunun girilmesi

            CustomElementWait.WaitUntilElementClickable(driver, CustomerPasswordInput);//password inputu tıklanabilir olana kadar bekletme
            ClearAndSenKeys(CustomerPasswordInput, values["password"]);//password inputunun girilmesi

            CustomElementWait.WaitUntilElementClickable(driver, CustomerPasswordCheckInput);//password-check inputu tıklanabilir olana kadar bekletme
            ClearAndSenKeys(CustomerPasswordCheckInput, values["password"]);//password inputunun girilmesi

            CustomElementWait.WaitUntilElementClickable(driver, ContinueButton);
            ContinueButton.Click();

          /*  CustomElementWait.WaitUntilElementClickable(driver, CustomerPasswordInput);//password inputu tıklanabilir olana kadar bekletme
            ClearAndSenKeys(CustomerPasswordInput, values["password"]);//password inputunun girilmesi

            CustomElementWait.WaitUntilElementClickable(driver, CustomerPasswordCheckInput);//password-check inputu tıklanabilir olana kadar bekletme
            ClearAndSenKeys(CustomerPasswordCheckInput, values["password"]);//password inputunun girilmesi

            CustomElementWait.WaitUntilElementClickable(driver, ContinueButton);
            ContinueButton.Click();*/

            IWebElement ConfirmMail = ExistsElement("//form[@action='verify']//h1[.='E-posta adresini doğrula']");
            if (ConfirmMail != null) return true;
            else return false;
        }
    }
}
