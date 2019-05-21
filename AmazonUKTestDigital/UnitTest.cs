using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AmazonUKTestDigital
{
    [TestFixture]
    public class UnitTest
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Setup Driver");
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://www.amazon.co.uk/";
        }

        [TearDown]
        public void Cleanup()
        {
            Console.WriteLine("Exit browser");
            driver.Quit();
        }

        [Test]
        public void SearchItem()
        {
            // Find Element search box and search text TShirt
            IWebElement searchBox = driver.FindElement(By.Id("twotabsearchtextbox"));
            searchBox.SendKeys("TShirt");
            // Find Element search button and click to search
            IWebElement searchButton = driver.FindElement(By.ClassName("nav-input"));
            searchButton.Click();

            // Wait finding Element result list
            new WebDriverWait(driver, TimeSpan.FromSeconds(2.0)).Until(ExpectedConditions.ElementExists(By.ClassName("s-result-list")));
            IWebElement result = driver.FindElement(By.ClassName("s-result-list"));
            Assert.NotNull(result, "Result list is found!");
            Assert.IsTrue(result.Displayed);
        }

        [Test]
        public void CreateAccount()
        {
            // Find Element signin button and click to redirect to signin form
            IWebElement signinButton = driver.FindElement(By.CssSelector("#nav-tools>a"));
            signinButton.Click();

            // Wait finding Element create account and click to redirect to create account form
            new WebDriverWait(driver, TimeSpan.FromSeconds(2.0)).Until(ExpectedConditions.ElementExists(By.CssSelector(".a-button-inner>a")));
            IWebElement createAccount = driver.FindElement(By.CssSelector(".a-button-inner>a"));
            createAccount.Click();

            /*
             * Wait finding Element create account and click to redirect to create account form
             * Fill form of creating account
             */
            new WebDriverWait(driver, TimeSpan.FromSeconds(2.0)).Until(ExpectedConditions.ElementExists(By.ClassName("a-button-input")));
            IWebElement name = driver.FindElement(By.Id("ap_customer_name"));
            name.SendKeys("Phanna");
            IWebElement email = driver.FindElement(By.Id("ap_email"));
            email.SendKeys("pp@test.com");
            IWebElement password = driver.FindElement(By.Id("ap_password"));
            password.SendKeys("123456");
            IWebElement repassword = driver.FindElement(By.Id("ap_password_check"));
            repassword.SendKeys("123456");
            IWebElement createAccountButton = driver.FindElement(By.ClassName("a-button-input"));

            Assert.NotNull(createAccountButton, "Button submit element found!");
            Assert.IsTrue(createAccountButton.Displayed);
        }
    }
}
