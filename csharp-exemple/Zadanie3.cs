using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace csharp_example
{
    [TestFixture]
    public class Zadanie3
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private readonly By LoginInputButtom = By.XPath("//input[@type='text']");
        private const string LoginInput = "admin";
        private readonly By PasswordInputButtom = By.XPath("//input[@type='password']");
        private const string PasswordInput = "admin";
        private readonly By LoginButtom = By.XPath("//button[@type='submit']");
        
        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Login()
        {
            driver.Url = "http://localhost/litecart/admin";
            driver.Manage().Window.Maximize();
            
            var login = driver.FindElement(LoginInputButtom);
            login.SendKeys(LoginInput);
            
            var password = driver.FindElement(PasswordInputButtom);
            password.SendKeys(PasswordInput);

            var enter = driver.FindElement(LoginButtom);
            enter.Click();
        }

        [TearDown]
        public void stop()
        {
           driver.Quit();
           driver = null;
        }
    }
}