using NUnit.Framework;
using NUnit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System;

using TestPropertiesFile;

[TestFixture]
public class TestSubmitContactForm
{
    [SetUp]
    public void SetupTest()
    {
    }

    [TearDown]
    public void TeardownTest()
    {
    }

    [Test]
    public void SubmitContactForm()
    {
        ChromeOptions chromeOptions = new ChromeOptions();
        chromeOptions.AddArguments("start-maximized", "incognito");

        IWebDriver driver =  new ChromeDriver(TestProperties.chromeDriverLocation, chromeOptions);

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        driver.Url = TestProperties.websiteURL;
        
        IWebElement contactNavigation = driver.FindElement(By.PartialLinkText("Contact"));
        contactNavigation.Click();

        IWebElement firstNameField = driver.FindElement(By.CssSelector("#root > div > div:nth-child(2) > div > div > form > div > div > div:nth-child(1) > input"));
        firstNameField.Click();
        firstNameField.Clear();
        firstNameField.SendKeys("Doctor");

        IWebElement surnameField = driver.FindElement(By.CssSelector("#root > div > div:nth-child(2) > div > div > form > div > div > div:nth-child(2) > input"));
        surnameField.Click();
        surnameField.Clear();
        surnameField.SendKeys("Tester");

        IWebElement emailField = driver.FindElement(By.CssSelector("#root > div > div:nth-child(2) > div > div > form > div > div > div:nth-child(3) > input"));
        emailField.Click();
        emailField.Clear();
        emailField.SendKeys("thetester@test.com.au");

        IWebElement passwordField = driver.FindElement(By.CssSelector("#root > div > div:nth-child(2) > div > div > form > div > div > div:nth-child(4) > input"));
        passwordField.Click();
        passwordField.Clear();
        passwordField.SendKeys("secret2019");

        IWebElement reEnterPasswordField = driver.FindElement(By.CssSelector("#root > div > div:nth-child(2) > div > div > form > div > div > div:nth-child(5) > input"));
        reEnterPasswordField.Click();
        reEnterPasswordField.Clear();
        reEnterPasswordField.SendKeys("secret2019");

        // TODO - Does not select radio button
        IWebElement contactMethodField = driver.FindElement(By.Id("contactEmail"));
        contactMethodField.Click();

        SelectElement foundUsField = new SelectElement(driver.FindElement(By.CssSelector("#root > div > div:nth-child(2) > div > div > form > div > div > div:nth-child(7) > select")));
        foundUsField.SelectByText("Friends/family");

        IWebElement commentField = driver.FindElement(By.CssSelector("#root > div > div:nth-child(2) > div > div > form > div > div > div:nth-child(8) > textarea"));
        commentField.Click();
        commentField.Clear();
        commentField.SendKeys("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.!?@0123456789");

        IWebElement submitButton = driver.FindElement(By.Id("submitButton"));
        Actions actions = new Actions(driver);
        actions.MoveToElement(submitButton);
        actions.Perform();
        submitButton.Submit();

        driver.Quit();
    }
}