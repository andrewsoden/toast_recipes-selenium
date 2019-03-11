using NUnit.Framework;
using NUnit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.IO;

using EnvironmentConfig;
using HomeConfig;
using InformationConfig;
using ContactConfig;
using LoginConfig;

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
        public void SectionNavigation()
        {
            try
            {
                var header = driver.FindElement(By.ClassName("title"));
                Assert.True(header.Displayed);

                driver.FindElement(By.PartialLinkText(HomeConfigValues.textHomeLink)).Click();
                Assert.True(driver.FindElement(By.XPath(HomeConfigValues.pageHeading)).Text.Contains(HomeConfigValues.textHomeHeading)); 

                driver.FindElement(By.PartialLinkText(InformationConfigValues.textInformationLink)).Click();
                Assert.True(driver.FindElement(By.XPath(InformationConfigValues.pageHeading)).Text.Contains(InformationConfigValues.textInformationHeading));

                driver.FindElement(By.PartialLinkText(ContactConfigValues.textContactLink)).Click();
                Assert.True(driver.FindElement(By.XPath(ContactConfigValues.pageHeading)).Text.Contains(ContactConfigValues.textContactHeading));

                driver.FindElement(By.PartialLinkText(LoginConfigValues.textLoginLink)).Click();
                Assert.True(driver.FindElement(By.XPath(LoginConfigValues.pageHeading)).Text.Contains(LoginConfigValues.textLoginHeading));
            }
            catch (Exception ex)
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