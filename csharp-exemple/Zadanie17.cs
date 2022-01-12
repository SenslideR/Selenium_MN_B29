using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;
using System.Collections.Generic;
using System.Threading;

namespace csharp_example
{
    [TestFixture]
    public class Zadanie17
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = "http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";
            driver.FindElement(By.XPath("//input[@type='text']")).SendKeys("admin");
            driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("admin");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        [Test]
        public void CheckBrowserLogsTest()
        {
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("#app-:nth-of-type(2)")).Click();
            wait.Until(driver => driver.FindElement(By.TagName("h1")).Displayed);
            var products = driver.FindElements(By.CssSelector("tr.row td:nth-of-type(3) a[href*=product_id]"));
            for (int i = 0; i < products.Count; i++)
            {
                driver.FindElements(By.CssSelector("tr.row td:nth-of-type(3) a[href*=product_id]"))[i].Click();
                wait.Until(driver => driver.FindElement(By.TagName("h1")).Displayed);
                var logs = new List<LogEntry>();
                foreach (LogEntry l in driver.Manage().Logs.GetLog("browser"))
                {
                    Console.WriteLine(l);
                    logs.Add(l);
                }
                Assert.IsEmpty(logs);
                driver.Navigate().Back();
                wait.Until(driver => driver.FindElement(By.TagName("h1")).Displayed);
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