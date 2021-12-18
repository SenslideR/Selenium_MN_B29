using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace csharp_example
{
    [TestFixture]
    public class Zadanie7
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = "http://localhost/litecart";
        }

        [Test]
        public void StickersOfProducts()
        {

            var products = driver.FindElements(By.XPath("//li[contains(@class, 'product')]"));

            for (int i = 1; i <= products.Count; i++)
            {
                Assert.True(driver.FindElement(By.XPath("//div[contains(@class, 'sticker')]")).Displayed);
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