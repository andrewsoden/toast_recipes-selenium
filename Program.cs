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
        IWebDriver WebDriver = new ChromeDriver(@"./bin/drivers/");
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        driver.Url ="https://andrewsoden.github.io/toast_recipes/#/";
    }

    [TearDown]
    public void TeardownTest()
    {
        driver.Quit();
    }

    [Test]
    public void testMethod()
    {
    }
}