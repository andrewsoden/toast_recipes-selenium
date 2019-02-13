using NUnit.Framework;
using NUnit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System;

using TestPropertiesFile;

namespace Test_SubmitContactForm
{
    public class Test_SubmitContactForm
    {
        private IWebDriver driver;

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
        public void SubmitContactForm()
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
    }
}