﻿using System;
using System.Globalization;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;


namespace csharp_example
{
    [TestFixture]
    public class Zadanie10
    {
        private IWebDriver driver;
        private WebDriverWait wait;

                

            [SetUp]
            public void Start()
            {
                driver = new InternetExplorerDriver();
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Manage().Window.Maximize();
                driver.Url = "http://localhost/litecart/";

            }

            [Test]
            public void ProductPageTest()
            {
                

                var product = driver.FindElement(By.CssSelector("div[id=box-campaigns] li.product"));

                var expectedHeader = product.FindElement(By.CssSelector("div.name")).Text;
                var expectedOldPrice = product.FindElement(By.CssSelector("s.regular-price"));
                var expectedOldPriceText = expectedOldPrice.Text;
                var expectedOldPriceColor = expectedOldPrice.GetCssValue("color");
                var expectedOldPriceStyle = expectedOldPrice.GetCssValue("text-decoration");
                var expectedOldPriceSize = expectedOldPrice.GetCssValue("font-size");
                var expectedCampaignPrice = product.FindElement(By.CssSelector("strong.campaign-price"));
                var expectedCampaignPriceColor = expectedCampaignPrice.GetCssValue("color");
                var expectedCapmaignPriceStyle = int.Parse(expectedCampaignPrice.GetCssValue("font-weight"));
                var expectedCampaignPriceSize = expectedCampaignPrice.GetCssValue("font-size");

                RGB(expectedOldPriceColor, out int rExpectedOld, out int gExpectedOld, out int bExpectedOld);
                RGB(expectedCampaignPriceColor, out _, out int gExpectedCampaign, out int bExpectedCampaign);
                PriceSize(expectedOldPriceSize, out float sizeExpectedOld);
                PriceSize(expectedCampaignPriceSize, out float sizeExpectedCampaign);

                Assert.That(new[] { rExpectedOld, gExpectedOld, bExpectedOld }, Is.All.EqualTo(rExpectedOld));
                Assert.That(new[] { gExpectedCampaign, bExpectedCampaign }, Is.All.EqualTo(0));
                StringAssert.Contains("line-through", expectedOldPriceStyle);
                Assert.IsTrue(expectedCapmaignPriceStyle >= 700);
                Assert.IsTrue(sizeExpectedOld < sizeExpectedCampaign);

                driver.FindElement(By.CssSelector("div[id=box-campaigns] li.product a.link")).SendKeys(Keys.Enter);

                var actualHeader = driver.FindElement(By.CssSelector("h1.title")).Text;
                var actualOldPrice = driver.FindElement(By.CssSelector("s.regular-price"));
                var actualOldPriceText = actualOldPrice.Text;
                var actualOldPriceColor = actualOldPrice.GetCssValue("color");
                var actualOldPriceStyle = actualOldPrice.GetCssValue("text-decoration");
                var actualOldPriceSize = actualOldPrice.GetCssValue("font-size");
                var actualCampaignPrice = driver.FindElement(By.CssSelector("strong.campaign-price"));
                var actualCampaignPriceColor = actualCampaignPrice.GetCssValue("color");
                var actualCampaignPriceStyle = int.Parse(actualCampaignPrice.GetCssValue("font-weight"));
                var actualCampaignPriceSize = actualCampaignPrice.GetCssValue("font-size");

                RGB(actualOldPriceColor, out int rActualOld, out int gActualOld, out int bActualOld);
                RGB(actualCampaignPriceColor, out _, out int gActualCampaign, out int bActualCampaign);
                PriceSize(actualOldPriceSize, out float sizeActualOld);
                PriceSize(actualCampaignPriceSize, out float sizeActualCampaign);

                Assert.That(new[] { rActualOld, gActualOld, bActualOld }, Is.All.EqualTo(rActualOld));
                Assert.That(new[] { gActualCampaign, bActualCampaign }, Is.All.EqualTo(0));
                StringAssert.Contains("line-through", actualOldPriceStyle);
                Assert.IsTrue(actualCampaignPriceStyle >= 700);
                Assert.IsTrue(sizeActualOld < sizeActualCampaign);

                Assert.AreEqual(expectedHeader, actualHeader);
                Assert.AreEqual(expectedOldPriceText, actualOldPriceText);
            }

            private void RGB(string color, out int r, out int g, out int b)
            {
                color = color.Replace("rgba(", "").Replace("rgb(", "").Replace(")", "").Replace(" ", "");
                var colorArray = color.Split(",");
                r = int.Parse(colorArray[0]);
                g = int.Parse(colorArray[1]);
                b = int.Parse(colorArray[2]);
            }

            private void PriceSize(string sizeString, out float sizeFloat)
            {
                var sizeCut = sizeString.Remove(sizeString.Length - 2, 2);
                sizeFloat = float.Parse(sizeCut, CultureInfo.InvariantCulture.NumberFormat);
            }

            [TearDown]
            public void stop()
            {
                driver.Quit();
                driver = null;
            }
        }
    
}