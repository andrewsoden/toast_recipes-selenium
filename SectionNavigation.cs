using NUnit.Framework;
using NUnit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

using TestPropertiesFile;

[TestFixture]
public class TestSectionNavigation
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
    public void SectionNavigation()
    {
        ChromeOptions chromeOptions = new ChromeOptions();
        chromeOptions.AddArguments("start-maximized", "incognito");

        IWebDriver driver =  new ChromeDriver("/users/andrewsoden/Desktop/Andrew/git/toast_recipes-selenium/bin/drivers/", chromeOptions);

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        driver.Url ="https://andrewsoden.github.io/toast_recipes/#/";
        driver.Url = TestProperties.websiteURL;

        IWebElement header = driver.FindElement(By.ClassName("title"));
        Assert.True(header.Displayed);

        IWebElement homeNavigation = driver.FindElement(By.PartialLinkText("Home"));
        homeNavigation.Click();
        Assert.True(driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div/h2")).Text.Contains("Rejoice for this is the Home page")); 

        IWebElement informationNavigation = driver.FindElement(By.PartialLinkText("Information"));
        informationNavigation.Click();
        Assert.True(driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div/h2")).Text.Contains("Rejoice for this is the Information page"));

        IWebElement contactNavigation = driver.FindElement(By.PartialLinkText("Contact"));
        contactNavigation.Click();
        Assert.True(driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div/h1")).Text.Contains("Contact"));

        IWebElement loginNavigation = driver.FindElement(By.PartialLinkText("Login"));
        loginNavigation.Click();
        Assert.True(driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div/h2")).Text.Contains("Rejoice for this is the Login page"));
    
        driver.Quit();
    }
}