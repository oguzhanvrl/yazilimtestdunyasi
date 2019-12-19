using NUnit.Framework;
using System;
using System.Collections.Generic;
using yazilimtestdunyasi.Base;
using yazilimtestdunyasi.Common.Generator;
using yazilimtestdunyasi.ComponentObjects.CreateAccountPage;
using yazilimtestdunyasi.ComponentObjects.HomePage;

namespace yazilimtestdunyasi.TestSuites.CreateAccountTestCase
{
    [TestFixture]
    class CreateAccountTestCase: BaseUITestCase
    {
        CreateAccountPage createAccountPage;
        [SetUp]
        public void GoToCreateAccountPage()
        {
            new HomePage(driver).GoToCreateAccountPage();
            createAccountPage = new CreateAccountPage(driver);
        }
        [TestCase("@selenium.com")]//valid domain
        [TestCase("selenium.com")]//invalid domain
        public void CreateAnAccount(string domain)
        {
            //test case de kullanılacak datanın ayarlanması
            Dictionary<string,string> AccountData=new Dictionary<string, string>() {
                    { "name", "Selenium Name"},
                    { "mail", RandomDataGenerator.RandomMail(10)+domain},
                    { "password", RandomDataGenerator.RandomPassword(10)
                }};
            Assert.IsTrue(createAccountPage.EnterField(AccountData),"Hesap Oluşturulurken Bir Hata Meydana Geldi");
        }

        [TearDown]
        public void After()
        {
            base.AfterMethod(TestContext.CurrentContext);
        }
    }
}
