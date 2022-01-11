using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;

namespace csharp_example
{
    [TestFixture]
    public class Zadanie14
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
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            driver.FindElement(By.XPath("//input[@type='text']")).SendKeys("admin");
            driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("admin");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }
       
        [Test]
        public void OpenNewWindow()
        {
            driver.FindElement(By.CssSelector("a.button")).Click();
            var links = driver.FindElements(By.CssSelector("i.fa.fa-external-link"));

            foreach (IWebElement link in links)
            {
                var mainWindow = driver.CurrentWindowHandle;
                var oldWindows = driver.WindowHandles;
                link.Click();
                wait.Until(driver => driver.WindowHandles.Count > oldWindows.Count);
                var newWindows = driver.WindowHandles;
                driver.SwitchTo().Window(newWindows[1]);
                driver.Close();
                driver.SwitchTo().Window(mainWindow);
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