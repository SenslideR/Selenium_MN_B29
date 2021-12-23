using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace csharp_example
{
    [TestFixture]
    public class Zadanie11
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
        public void UserRegistration()
        {
            driver.FindElement(By.XPath("//a[contains(@href,'create')]")).Click();
            driver.FindElement(By.XPath("//input[@name='tax_id']")).SendKeys("taxIDTest");
            driver.FindElement(By.XPath("//input[@name='company']")).SendKeys("companyTest");
            driver.FindElement(By.XPath("//input[@name='firstname']")).SendKeys("firstnameTest");
            driver.FindElement(By.XPath("//input[@name='lastname']")).SendKeys("lastnameTest");
            driver.FindElement(By.XPath("//input[@name='address1']")).SendKeys("address1Test");
            driver.FindElement(By.XPath("//input[@name='address2']")).SendKeys("address2Test");
            driver.FindElement(By.XPath("//input[@name='postcode']")).SendKeys("12345");
            driver.FindElement(By.XPath("//input[@name='city']")).SendKeys("cityTest");
            driver.FindElement(By.XPath("//b[@role='presentation']")).Click();
            driver.FindElement(By.XPath("//input[@class='select2-search__field']")).SendKeys("United States");
            driver.FindElement(By.XPath("//li[@class='select2-results__option select2-results__option--highlighted']")).Click();
            Random rnd = new Random();
            string email = "test" + rnd.Next() + "@test.ru";
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys(email);
            driver.FindElement(By.XPath("//input[@name='phone']")).SendKeys("+79005006050");
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("123test");
            driver.FindElement(By.XPath("//input[@name='confirmed_password']")).SendKeys("123test");
            driver.FindElement(By.XPath("//button[@name='create_account']")).Click();
            driver.FindElement(By.XPath("//a[contains(@href, 'logout')]")).Click();
            //повторный вход
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys(email);
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("123test");
            driver.FindElement(By.XPath("//button[@name='login']")).Click();
            driver.FindElement(By.XPath("//a[contains(@href, 'logout')]")).Click();
        }

        [TearDown]
        public void stop()
        {
          driver.Quit();
          driver = null;
        }
    }
}