using NUnit.Framework;
using NUnit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

[TestFixture]
public class TestProgram
{
    private IWebDriver driver;

    [SetUp]
    public void SetupTest()
    {
    }

    [TearDown]
    public void TeardownTest()
    {
    }

    [Test]
    public void testMethod()
    {
        ChromeOptions chromeOptions = new ChromeOptions();
        chromeOptions.AddArguments("start-maximized", "incognito");

        IWebDriver driver =  new ChromeDriver("/users/andrewsoden/Desktop/Andrew/git/toast_recipes-selenium/bin/drivers/", chromeOptions);

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        driver.Url ="https://andrewsoden.github.io/toast_recipes/#/";

        IWebElement header = driver.FindElement(By.ClassName("title"));
        Assert.True(header.Displayed);

        driver.Quit();
    }
}