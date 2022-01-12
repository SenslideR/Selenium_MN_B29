using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace csharp_example
{
    [TestFixture]
    public class Zadanie9
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";
            driver.FindElement(By.XPath("//input[@type='text']")).SendKeys("admin");
            driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("admin");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

        }

        [Test]
        public void GeoZoneSort()
        {
            var countries = driver.FindElements(By.XPath("//tr[@class='row']/td[3]/a"));

            for (int i = 1; i <= countries.Count; i++)
            {
                driver.FindElement(By.XPath("//tr[@class='row'][" + i + "]/td[3]/a")).Click();

                var geoZones = driver.FindElements(By.CssSelector("select[name*=zones]:not([class*=hidden])"));
                var geoZonesNames = new List<string>();

                foreach (IWebElement geoZone in geoZones)
                {
                    geoZonesNames.Add(geoZone.Text);
                }

                var geoZonesNamesSorted = new List<string>(geoZonesNames);
                geoZonesNamesSorted.Sort();

                CollectionAssert.AreEqual(geoZonesNamesSorted, geoZonesNames);

                driver.Navigate().Back();
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
