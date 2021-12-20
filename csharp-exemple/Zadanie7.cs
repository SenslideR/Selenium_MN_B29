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

            foreach (var stickers in products)
            {
                var sticker1 = stickers.FindElements(By.CssSelector("div.sticker"));
                Assert.AreEqual(1, sticker1.Count);
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