using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace csharp_example
{
    [TestFixture]
    public class Zadanie12
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.XPath("//input[@type='text']")).SendKeys("admin");
            driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("admin");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            

        }

        [Test]
        public void AddNewProduct()
        {
            //General
            driver.FindElement(By.XPath("//li[@id='app-'][2]")).Click();
            driver.FindElement(By.XPath("//a[@class='button'][2]")).Click();
            driver.FindElement(By.XPath("//input[@type='radio'][1]")).Click();
            var ProductName = "Product" + DateTime.Now.Millisecond;
            driver.FindElement(By.XPath("//input[@name='name[en]']")).SendKeys(ProductName);
            driver.FindElement(By.XPath("//input[@name='code']")).SendKeys("T" + DateTime.Now.Millisecond);
            driver.FindElement(By.XPath("//input[@data-name='Rubber Ducks']")).Click();
            driver.FindElement(By.XPath("//select[@name='default_category_id']/option[@value='1']")).Click();
            driver.FindElement(By.XPath("//input[@value='1-3']")).Click();
            driver.FindElement(By.XPath("//input[@name='quantity']")).Clear();
            driver.FindElement(By.XPath("//input[@name='quantity']")).SendKeys("1");
            var workingDirectory = AppContext.BaseDirectory;
            var projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            var pathToFile = Path.Combine(projectDirectory, "img/dk.jpg");
            driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(pathToFile);
            driver.FindElement(By.XPath("//input[@name='date_valid_from']")).SendKeys("15.11.2021");
            driver.FindElement(By.XPath("//input[@name='date_valid_to']")).SendKeys("15.11.2022");
            //Information
            driver.FindElement(By.XPath("//a[@href='#tab-information']")).Click();
            driver.FindElement(By.XPath("//select[@name='manufacturer_id']/option[@value='1']")).Click();
            driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys("Keywords_test");
            driver.FindElement(By.XPath("//input[@name='short_description[en]']")).SendKeys("Short_Description_test");
            driver.FindElement(By.XPath("//div[@class='trumbowyg-editor']")).SendKeys("Description_test");
            driver.FindElement(By.XPath("//input[@name='head_title[en]']")).SendKeys("Head_title_test");
            driver.FindElement(By.XPath("//input[@name='meta_description[en]']")).SendKeys("Meta Description_test");
            //Prices
            driver.FindElement(By.XPath("//a[@href='#tab-prices']")).Click();
            driver.FindElement(By.XPath("//input[@name='purchase_price']")).Clear();
            driver.FindElement(By.XPath("//input[@name='purchase_price']")).SendKeys("100");
            driver.FindElement(By.XPath("//option[@value='EUR']")).Click();
            driver.FindElement(By.XPath("//input[@name='prices[USD]']")).SendKeys("120");
            driver.FindElement(By.XPath("//input[@name='prices[EUR]']")).SendKeys("100");
            driver.FindElement(By.XPath("//button[@name='save']")).Click();
            //Проверка, что товар добавлен
            //driver.Url = "http://localhost/litecart/";
            Assert.True(driver.FindElement(By.XPath("//*[.='" + ProductName + "']")).Displayed);
        }

        [TearDown]
        public void stop()
        {
          driver.Quit();
          driver = null;
        }
    }
}