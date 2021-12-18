using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace csharp_example
{
    [TestFixture]
    public class Zadanie6
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.XPath("//input[@type='text']")).SendKeys("admin");
            driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("admin");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        [Test]
        public void MainMenuTest()
        {
            
            var MainMenu = driver.FindElements(By.XPath("//li[@id='app-']"));

            for (int i = 1; i <= MainMenu.Count; i++)
            {
                driver.FindElement(By.XPath("//li[@id='app-'][" + i + "]")).Click();
                Assert.True(driver.FindElement(By.XPath("//h1")).Displayed);
                
                var podMenu = driver.FindElements(By.XPath("//li[contains(@id, 'doc')]"));
                
                if (podMenu.Count != 0)
                {
                    for (int j = 1; j <= podMenu.Count; j++)
                    {
                        driver.FindElement(By.XPath("//li[contains(@id, 'doc')][" + j + "]")).Click();
                        Assert.True(driver.FindElement(By.XPath("//h1")).Displayed);
                    }
                }
            }
        }

        [TearDown]
        public void stop()
        {
          driver.Quit();
          driver = null;
        }
    }
}