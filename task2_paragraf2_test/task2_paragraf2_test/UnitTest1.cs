using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;
using OpenQA.Selenium.Support.UI;


namespace task2_paragraf2_test
{
    public class Tests
    {
        private IWebDriver driver;
        private readonly By search_field = By.XPath("//input[@placeholder = 'Search city']");
        private readonly By search_list = By.XPath("//li[@data-v-12f747a3 = ''][1]");
        private readonly By search_button = By.XPath("//button[@data-v-12f747a3 = '']");
        private readonly By metric_button = By.XPath("//div[@class = 'option'][1]");
        private readonly By imperial_button = By.XPath("//div[@class = 'option'][2]");
        private readonly By temper = By.XPath("//span[@class = 'heading']");


        private string[] list_of_cities = {  "������", "������-���", "������" };
        private const int number = 3;   //������� ���������� �������
        private int counter = 0;
        private int[] gr = { 0, 0, 0 };  //�������� ����������� ������� � ��������
        private int[] fr = { 0, 0, 0 };  //�������� ����������� ������� � ������������
        [SetUp]
        public void Setup()
        {
            if (counter < number)
            { 
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl(" https://openweathermap.org/");
            }
          
            string city;
           
            
            while (counter < number)    //���� �� ���������� �������
            {
                city = list_of_cities[counter];
                Thread.Sleep(1000);
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

                string ss = "";             //�������� ������� �� �������
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

                int d = int.Parse(ss);

                var imperial = driver.FindElement(imperial_button);
                imperial.Click();
                Thread.Sleep(2000);

                var temperature_f = driver.FindElement(temper).Text;

                string ss2 = "";
                foreach (char c2 in temperature_f)         //�������� ������� �� ����������
                {
                    if (c2 >= '0' && c2 <= '9')
                    {
                        ss2 = string.Concat(ss2, c2);
                    }
                    else
                    {
                        break;
                    }
                }
                int d2 = int.Parse(ss2);
                double dd = d * 1.8;
                d = (int)dd + 32;      //������������ ������� � ����������

                gr[counter] = d;
                fr[counter] = d2;
              
                counter++;
             
            }                  
            
        }

     
        [Test]   //���� �� �������� ����������� ����������� ������� ������
        public void Test1()  
        {
            Assert.AreEqual(gr[0], fr[0], list_of_cities[0] + " temperature conversion is incorrect   " + fr[0] + " listed on the website  " + gr[0] + " must be");
        }
        [Test] //���� �� �������� ����������� ����������� ������� ������
        public void Test2()
        {
            Assert.AreEqual(gr[1], fr[1], list_of_cities[1] + " temperature conversion is incorrect   " + fr[1] + " listed on the website  " + gr[1] + " must be");
        }
        [Test] //���� �� �������� ����������� ����������� �������� ������
        public void Test3()
        {
            Assert.AreEqual(gr[2], fr[2], list_of_cities[2] + " temperature conversion is incorrect   " + fr[2] + " listed on the website  " + gr[2] + " must be");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}