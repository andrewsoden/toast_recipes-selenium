using NUnit.Framework;
using NUnit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.IO;

using EnvironmentConfig;
using ContactConfig;

namespace Test_SubmitContactForm
{
    public class Test_SubmitContactForm
    {
        private IWebDriver driver;
        private string currentTestName = NUnit.Framework.TestContext.CurrentContext.Test.FullName;

        [SetUp]
        public void SetUp()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("start-maximized", "incognito");

            driver =  new ChromeDriver(EnvironmentConfigValues.chromeDriverLocation, chromeOptions);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Url = EnvironmentConfigValues.websiteURL;
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void SubmitContactForm()
        {
            try
            {
                driver.FindElement(By.PartialLinkText(ContactConfigValues.textContactLink)).Click();
                driver.FindElement(By.CssSelector(ContactConfigValues.textFirstName)).SendKeys("Doctor");
                driver.FindElement(By.CssSelector(ContactConfigValues.textSurname)).SendKeys("Tester");
                driver.FindElement(By.CssSelector(ContactConfigValues.textEmail)).SendKeys("thetester@test.com.au");
                driver.FindElement(By.CssSelector(ContactConfigValues.textPassword)).SendKeys("secret2019");
                driver.FindElement(By.CssSelector(ContactConfigValues.textRePassword)).SendKeys("secret2019");

                // TODO - Does not select radio button
                IWebElement contactMethodField = driver.FindElement(By.Id("contactEmail"));
                contactMethodField.Click();

                SelectElement foundUsField = new SelectElement(driver.FindElement(By.CssSelector(ContactConfigValues.listHowHeadAboutUs)));
                foundUsField.SelectByText("Friends/family");

                driver.FindElement(By.CssSelector(ContactConfigValues.textComments))
                .SendKeys("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.!?@0123456789");

                IWebElement submitButton = driver.FindElement(By.Id(ContactConfigValues.buttonSubmit));
                Actions actions = new Actions(driver);
                actions.MoveToElement(submitButton);
                actions.Perform();
                submitButton.Submit();
            }
            catch(Exception ex)
            {
                using (System.IO.StreamWriter file = 
                new System.IO.StreamWriter(EnvironmentConfigValues.logDirectory, true))
                {
                    file.WriteLine("* * * * * * * * * * * * * * * * * *");
                    file.WriteLine("Test: " + NUnit.Framework.TestContext.CurrentContext.Test.Name);
                    file.WriteLine("Execution Time: " + DateTime.Now.ToString("HH:mm:ss"));
                    file.WriteLine("Message: " + ex.Message);
                    file.WriteLine("StackTrace: " + ex.StackTrace);
                }

                string FileName = Path.Combine(EnvironmentConfigValues.screenshotDirectory, NUnit.Framework.TestContext.CurrentContext.Test.Name + ".PNG");
                Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
                image.SaveAsFile(FileName, ScreenshotImageFormat.Png);

                Assert.Fail();
            }
        }
    }
}