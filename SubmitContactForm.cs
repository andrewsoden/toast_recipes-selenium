using NUnit.Framework;
using NUnit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using System;

using TestPropertiesFile;

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

            driver =  new ChromeDriver(TestProperties.chromeDriverLocation, chromeOptions);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Url = TestProperties.websiteURL;
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
                driver.FindElement(By.PartialLinkText("Contact")).Click();

                driver.FindElement(By.CssSelector("#root > div > div:nth-child(2) > div > div > form > div > div > div:nth-child(1) > input"))
                .SendKeys("Doctor");

                driver.FindElement(By.CssSelector("#root > div > div:nth-child(2) > div > div > form > div > div > div:nth-child(2) > input"))
                .SendKeys("Tester");

                driver.FindElement(By.CssSelector("#root > div > div:nth-child(2) > div > div > form > div > div > div:nth-child(3) > input"))
                .SendKeys("thetester@test.com.au");

                driver.FindElement(By.CssSelector("#root > div > div:nth-child(2) > div > div > form > div > div > div:nth-child(4) > input"))
                .SendKeys("secret2019");

                driver.FindElement(By.CssSelector("#root > div > div:nth-child(2) > div > div > form > div > div > div:nth-child(5) > input"))
                .SendKeys("secret2019");

                // TODO - Does not select radio button
                IWebElement contactMethodField = driver.FindElement(By.Id("contactEmail"));
                contactMethodField.Click();

                SelectElement foundUsField = new SelectElement(driver.FindElement(By.CssSelector("#root > div > div:nth-child(2) > div > div > form > div > div > div:nth-child(7) > select")));
                foundUsField.SelectByText("Friends/family");

                driver.FindElement(By.CssSelector("#root > div > div:nth-child(2) > div > div > form > div > div > div:nth-child(8) > textarea"))
                .SendKeys("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.!?@0123456789");

                IWebElement submitButton = driver.FindElement(By.Id("submitButton"));
                Actions actions = new Actions(driver);
                actions.MoveToElement(submitButton);
                actions.Perform();
                submitButton.Submit();
            }
            catch(Exception ex)
            {
                using (System.IO.StreamWriter file = 
                new System.IO.StreamWriter(TestProperties.logDirectory, true))
                {
                    file.WriteLine("* * * * * * * * * * * * * * * * * *");
                    file.WriteLine("Test: " + currentTestName);
                    file.WriteLine("Execution Time: " + DateTime.Now.ToString("HH:mm:ss"));
                    file.WriteLine("Message: " + ex.Message);
                    file.WriteLine("StackTrace: " + ex.StackTrace);
                }

                Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
                image.SaveAsFile("/users/andrewsoden/Desktop/Andrew/git/toast_recipes-selenium/logs/Screenshot1.Png"
                , ScreenshotImageFormat.Png);

                Assert.Fail();
            }
        }
    }
}