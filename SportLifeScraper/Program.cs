using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SportLifeScraper
{
    class Program
    {
        public static void Scraper()
        {
            var currentWinCoef = 1.00;
            var minimumCoef = 1.90;
            var totalMaximumCoef = 90.00;
            List<string> footballResults = new List<string>();
            IWebDriver driver = new ChromeDriver(@"C:\chromedriverfolder");
            driver.Url = "http://sportlife.com.mk/Results";

            driver.FindElement(By.XPath("//*[@id='prikaz_select']/option[2]")).Click();
            System.Threading.Thread.Sleep(2000);
            var divs = driver.FindElement(By.Id("template-container_0")).FindElements(By.ClassName("result-row"));
            foreach (var div in divs)
            {
                var tmpDiv = div.FindElements(By.ClassName("open-table-1")).ToList();
                if ((int.Parse(tmpDiv.First().Text.Substring(0, 2)) >= 11) && int.Parse(tmpDiv.First().Text.Substring(0, 2)) < 23)
                {
                    try
                    {
                        if ((float.Parse(div.FindElement(By.ClassName("select-qu")).Text.ToString(), System.Globalization.CultureInfo.InvariantCulture)) >= minimumCoef)
                        {
                            footballResults.Add(tmpDiv[3].Text.ToString() + " vs " + tmpDiv[7].Text.ToString() + " with coeficient " + div.FindElement(By.ClassName("select-qu")).Text.ToString());
                            currentWinCoef *= (float.Parse(div.FindElement(By.ClassName("select-qu")).Text.ToString(), System.Globalization.CultureInfo.InvariantCulture));
                            if (currentWinCoef > totalMaximumCoef)
                            {
                                footballResults.Add("Vkupno " + Math.Round(currentWinCoef,2) + " koeficient.");
                                break;
                            }
                        }
                    } catch { }                    
                }
            }
        }
        static void Main(string[] args)
        {
            Scraper();
            Console.ReadLine();
        }
    }
}
