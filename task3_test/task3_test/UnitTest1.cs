using NUnit.Framework;
using System.Net;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace task3_test
{
    public class WeatherResponse
    {
        public TemperatureInfo Main { get; set; }
        public string Name { get; set; }
    }

    public class TemperatureInfo
    {
        public float Temp { get; set; }
    }

    public class Tests
    {
        private const string city = "Йошкар-Ола"; //город у которого проверяем температуру
        private const string url = "http://api.openweathermap.org/data/2.5/weather?q="+city+"&units=metric&appid=178f5dad34cc64066f8482f84262c943";
        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        private IWebDriver driver;
        private readonly By search_field = By.XPath("//input[@placeholder = 'Search city']");
        private readonly By search_list = By.XPath("//li[@data-v-12f747a3 = ''][1]");
        private readonly By search_button = By.XPath("//button[@data-v-12f747a3 = '']");
        private readonly By metric_button = By.XPath("//div[@class = 'option'][1]");
        private readonly By temper = By.XPath("//span[@class = 'heading']");
        private int d; //градусы полученные на сайте
        private int d2; //градусы полученные по API
        [SetUp]
        public void Setup()
        {
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response); //получили город и температуру

            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl(" https://openweathermap.org/");

            var login = driver.FindElement(search_field);
            login.SendKeys(city);

            Thread.Sleep(1000);
            var search = driver.FindElement(search_button);
            search.Click();

            Thread.Sleep(2000);
            var search_city = driver.FindElement(search_list);
            search_city.Click();

            Thread.Sleep(3000);
            var metric = driver.FindElement(metric_button);
            metric.Click();

            Thread.Sleep(2000);
            var temperature_c = driver.FindElement(temper).Text;

            string ss = "";             //получаем градусы по Цельсию
            foreach (char c in temperature_c)
            {
                if (c >= '0' && c <= '9')
                {
                    ss = string.Concat(ss, c);
                }
                else
                {
                    break;
                }
            }
             d = int.Parse(ss);
             d2 = (int)weatherResponse.Main.Temp;
             driver.Quit();
        }

        [Test]
        public void Test1()
        {
            
            Assert.AreEqual(d, d2,"temperature is not correct");
         
        }
     
    }
}