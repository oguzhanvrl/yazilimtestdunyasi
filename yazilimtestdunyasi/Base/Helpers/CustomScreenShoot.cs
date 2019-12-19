using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yazilimtestdunyasi.Base;
namespace yazilimtestdunyasi.Base.Helpers
{
    class CustomScreenShoot
    {
        public static void TakeScreenshot(IWebDriver driver,TestContext result)
        {
            try
            {
                string ssName = string.Format("{0}{1}-({2}).{3}", result.Result.Outcome.Status, result.Test.MethodName,DateTime.Now.ToShortDateString(), ScreenshotImageFormat.Png.ToString());
                //Take the screenshot
                Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
                image.SaveAsFile(BaseTestValue.ScreenShotPath+@"\"+ssName, ScreenshotImageFormat.Png);

            }
            catch (Exception)
            {
                //loglama yapılabilir
                throw;
            }
        }
    }
}
