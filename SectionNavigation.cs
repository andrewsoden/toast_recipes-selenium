using NUnit.Framework;
using NUnit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using System;

using TestPropertiesFile;

namespace Test_SectionNavigation
{
    class Test_SectionNavigation
    {
        private IWebDriver driver;
        private string currentTestName = NUnit.Framework.TestContext.CurrentContext.Test.FullName;

        [SetUp]
        public void SetUp()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("start-maximized", "incognito");

            driver =  new ChromeDriver(TestProperties.chromeDriverLocation, chromeOptions);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Url = TestProperties.websiteURL;
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void SectionNavigation()
        {
            try
            {
                var header = driver.FindElement(By.ClassName("title"));
                Assert.True(header.Displayed);

                var homeNavigation = driver.FindElement(By.PartialLinkText("Home"));
                homeNavigation.Click();
                Assert.True(driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div/h2")).Text.Contains("Rejoice for this is the Home page")); 

                var informationNavigation = driver.FindElement(By.PartialLinkText("Information"));
                informationNavigation.Click();
                Assert.True(driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div/h2")).Text.Contains("Rejoice for this is the Information page"));

                var contactNavigation = driver.FindElement(By.PartialLinkText("Contact"));
                contactNavigation.Click();
                Assert.True(driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div/h1")).Text.Contains("Contact"));

                var loginNavigation = driver.FindElement(By.PartialLinkText("Login"));
                loginNavigation.Click();
                Assert.True(driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div/h2")).Text.Contains("Rejoice for this is the Login page"));
            }
            catch (Exception ex)
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
                image.SaveAsFile("/users/andrewsoden/Desktop/Andrew/git/toast_recipes-selenium/logs/Screenshot2.Png"
                , ScreenshotImageFormat.Png);

                Assert.Fail();
            }
        }
    }
}