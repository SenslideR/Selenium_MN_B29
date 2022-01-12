using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace csharp_example
{
    [TestFixture]
    public class Zadanie8
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            driver.FindElement(By.XPath("//input[@type='text']")).SendKeys("admin");
            driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("admin");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

        }

        [Test]
        public void CountryZoneSort()
        {
            var countries = driver.FindElements(By.XPath("//tr[@class='row']/td[5]/a"));
            var countryNames = new List<string>();

            foreach (IWebElement country in countries)
            {
                countryNames.Add(country.Text);
            }

            var countryNamesSorted = new List<string>(countryNames);
            countryNamesSorted.Sort();

            CollectionAssert.AreEqual(countryNamesSorted, countryNames);

            for (int i = 1; i <= countries.Count; i++)
            {
                if (!driver.FindElement(By.XPath("//tr[@class='row'][" + i + "]/td[6]")).Text.Equals("0"))
                {
                    driver.FindElement(By.XPath("//tr[@class='row'][" + i + "]/td[5]/a")).Click();
                    var geoZones = driver.FindElements(By.XPath("//table[@id='table-zones']//tr//td[3]"));
                    var geoZonesNames = new List<string>();

                    for (int j = 1; j < geoZones.Count - 1; j++)
                    {
                        geoZonesNames.Add(geoZones[j].Text);
                    }

                    var geoZonesNamesSorted = new List<string>(geoZonesNames);
                    geoZonesNamesSorted.Sort();

                    CollectionAssert.AreEqual(geoZonesNamesSorted, geoZonesNames);

                    driver.Navigate().Back();
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