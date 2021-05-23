using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Project3_test
{
    public class Tests
    {
        private IWebDriver driver;
        private readonly By button_sign = By.XPath("//a[@href = 'https://openweathermap.org/home/sign_in']");
        private readonly By login_field = By.XPath("//input[@id = 'user_email']");
        private readonly By password_field = By.XPath("//input[@id = 'user_password']");
        private readonly By button_submit = By.XPath("//input[@type = 'submit'][@value = 'Submit']");
        private readonly By user_login = By.XPath("//div[@class = 'inner-user-container']");
        private readonly By dropdown_user_button = By.XPath("//div[@class = 'inner-user-container']");
        private readonly By exit_button = By.XPath("//a[@class = 'logout']");
       
        private readonly By create_button = By.XPath("//a[@href = '/users/sign_up']");
        private readonly By create_site = By.XPath("//h3[@class = 'first-child']");

        private readonly By lost_button = By.XPath("//a[@href = '#']");
        private readonly By lost_email_field = By.XPath("//input[@class = 'form-control string email optional']");
        private readonly By lost_email_button = By.XPath("//input[@type= 'submit'][@value = 'Send']");
        private readonly By lost_email_sent = By.XPath("//div[@class= 'panel-body']");

        private readonly By remember_button = By.XPath("//label[@class= 'boolean optional']");

        private const string _login = "testkeys@yandex.ru";
        private const string _password = "testkeys";
        private const string correct_login = "testkeys";

        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl(" https://openweathermap.org/");
        }

        [Test]   //вход под зарегестрированным пользователем
        public void Test1()
        {
            Thread.Sleep(1000);
            var sign = driver.FindElement(button_sign);
            sign.Click();

            Thread.Sleep(1000);
            var login = driver.FindElement(login_field);
            login.SendKeys(_login);

            Thread.Sleep(1000);
            var password = driver.FindElement(password_field);
            password.SendKeys(_password);

            Thread.Sleep(1000);
            var submit = driver.FindElement(button_submit);
            submit.Click();

            Thread.Sleep(1000);
            var entered_login = driver.FindElement(user_login).Text;
            Assert.AreEqual(entered_login, correct_login,"didn't get in");
            
            Thread.Sleep(1000);
            var dropdown_button= driver.FindElement(dropdown_user_button);
            dropdown_button.Click();

            Thread.Sleep(1000);
            var exit = driver.FindElement(exit_button);
            exit.Click();

        }
        [Test]   // проверка кнопки "Not registered? Create an Account"
        public void Test2()
        {
            Thread.Sleep(1000);
            var sign = driver.FindElement(button_sign);
            sign.Click();

            Thread.Sleep(1000);
            var create = driver.FindElement(create_button);
            create.Click();

            Thread.Sleep(1000);
            var create_text = driver.FindElement(create_site).Text;
            Assert.AreEqual(create_text, "Create New Account", "create button doesn't work");
        }

        [Test]   // проверка кнопки "Lost your password? Click here to recover"
        public void Test3()
        {
            Thread.Sleep(1000);
            var sign = driver.FindElement(button_sign);
            sign.Click();

            Thread.Sleep(1000);
            var lost = driver.FindElement(lost_button);
            lost.Click();

            Thread.Sleep(1000);
            var lost_email = driver.FindElement(lost_email_field);
            lost_email.SendKeys(_login);

            Thread.Sleep(1000);
            var email_button = driver.FindElement(lost_email_button);
            email_button.Click();

            Thread.Sleep(1000);
            var lost_text = driver.FindElement(lost_email_sent).Text;
            Assert.AreEqual(lost_text, "You will receive an email with instructions on how to reset your password in a few minutes.", "lost button doesn't work");
        }
        [Test]   //проверка кнопки "Remember me"
        public void Test4()
        {
            Thread.Sleep(1000);
            var sign = driver.FindElement(button_sign);
            sign.Click();

            Thread.Sleep(1000);
            var login = driver.FindElement(login_field);
            login.SendKeys(_login);

            Thread.Sleep(1000);
            var password = driver.FindElement(password_field);
            password.SendKeys(_password);

            Thread.Sleep(1000);
            var remember = driver.FindElement(remember_button);
            remember.Click();

            Thread.Sleep(1000);
            var submit = driver.FindElement(button_submit);
            submit.Click();

            Thread.Sleep(1000);
            var dropdown_button = driver.FindElement(dropdown_user_button);
            dropdown_button.Click();

            Thread.Sleep(1000);
            var exit = driver.FindElement(exit_button);
            exit.Click();

            driver.Quit();
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl(" https://openweathermap.org/");

            Thread.Sleep(1000);
            var sign2 = driver.FindElement(button_sign);
            sign2.Click();

            Thread.Sleep(1000);
            var submit2 = driver.FindElement(button_submit);
            submit2.Click();

            //   Thread.Sleep(1000);
            //  var login2 = driver.FindElement(login_field).Text;
            //   Assert.AreEqual(login2, correct_login , "remember button doesn't work");

            Thread.Sleep(1000);
            var entered_login = driver.FindElement(user_login).Text;
            Assert.AreEqual(entered_login, correct_login, "remember button doesn't work");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}