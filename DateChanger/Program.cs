using System;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Net;
using System.IO;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace DateChanger
{
    class Program
    {
        static string email = "";
        static string password = "";
        //static DateTime tMonth;       
        static int id = 0;
        static int x = 0;
        static string exactpath;
        static readonly bool appTimeOut = false;

        static int Main(string[] args)
        {

            GetEmailID();
            exactpath = @"C:\inetpub\wwwroot\exactvalues\" + email.Split('@')[0];

            string title = "Thread - " + id + " - " + email;
            Console.Title = title + " - Selenium KeywordPlanner Date Changer";

            System.Drawing.Size size = new System.Drawing.Size(1280, 1024);

            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", exactpath + @"\downloads");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            //chromeOptions.AddArgument("headless");

            IWebDriver driver = new ChromeDriver(@".\ChromeDriver", chromeOptions);

            driver.Manage().Window.Size = size;

            string url = "https://ads.google.com/aw/keywordplanner/home?ocid=193200943&__c=4281215607&authuser=0&__u=9516641019&enableAllBrowsers=1";
            driver.Navigate().GoToUrl(url);

            while (driver.FindElements(By.XPath("//*[@id='identifierId']")).Count == 0 || driver.FindElements(By.CssSelector("#identifierId")).Count == 0)
            {
                driver.Navigate().GoToUrl(url);
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.XPath("//*[@id='identifierId']")).SendKeys(email);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            try
            {
                driver.FindElement(By.XPath("//*[@id='identifierNext']")).Click();
            }
            catch
            {
                driver.FindElement(By.CssSelector("#identifierNext")).Click();
            }
            Console.WriteLine(driver.PageSource);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys(password);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            try
            {
                driver.FindElement(By.XPath("//*[@id='passwordNext']")).Click();
            }
            catch
            {
                driver.FindElement(By.CssSelector("#passwordNext")).Click();
            }




            Console.WriteLine(driver.PageSource);
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                driver.FindElement(By.CssSelector("div.HWIeKd")).Click();
            }
            catch { }

            string keywordItem = "uk:United Kingdom:facebook,youtube";
            ArrayList alKeywords = new ArrayList();
            alKeywords.Add(keywordItem);

            foreach (string kwItem in alKeywords)
            {
                string[] item = kwItem.Split(':');
                string market = item[0];
                string country = item[1];
                string kws = item[2];

                try
                {
                    WebDriverWait tensecondswait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
                    OpenQA.Selenium.Support.UI.WebDriverWait Wait = new WebDriverWait(driver, new TimeSpan(0, 0, 50));
                    DeleteFile(); // delete downloaded files
                    try
                    {
                        IWebElement accountchooser = tensecondswait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.WBW9sf")));
                        accountchooser.Click();
                    }
                    catch { }
                    //WriteToCsv(kws); // write to keywords file
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.FindElement(By.CssSelector(".forecasts-content > div:nth-child(1) > div:nth-child(3) > material-icon:nth-child(1) > i:nth-child(1)")).Click();
                    Console.WriteLine(driver.PageSource);

                    driver.FindElement(By.CssSelector("material-input.text-input-component")).SendKeys(WebUtility.HtmlDecode(kws));

                    Console.WriteLine(driver.PageSource);

                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.FindElement(By.CssSelector("material-button.get-results-button")).Click();
                    Console.WriteLine(driver.PageSource);
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    //driver.FindElement(By.CssSelector("tab-button.tab-button:nth-child(3)")).Click();
                    Console.WriteLine(driver.PageSource);





                    //Date Selection
                    DateSelection:
                    try
                    {
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                        //driver.FindElement(By.CssSelector(".dropdown")).Click();
                        Console.WriteLine(driver.PageSource);
                        try
                        {
                            WebDriverWait tabwait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
                            IWebElement tab = tabwait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("tab-button.tab-button:nth-child(3)")));
                            tab.Click();
                            //end 06-08-2020
                        }
                        catch { }
                        try
                        {
                            IWebElement dropdown = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".dropdown")));
                            dropdown.Click();
                        }
                        catch
                        {
                            goto DateSelection;
                        }
                    }
                    catch
                    {

                    }
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                    IWebElement e = driver.FindElement(By.CssSelector("div.range-button:nth-child(5)"));
                    if (e.Text.Contains("All available"))
                        e.Click();
                    else
                    {
                        driver.Navigate().Refresh();
                        continue;
                    }

                    //Download CSV File
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    ////driver.FindElement(By.CssSelector(".download")).Click();
                    //try
                    //{
                    //    IWebElement download = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".download")));
                    //    download.Click();
                    //}
                    //catch { }
                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    //IWebElement ele = driver.FindElement(By.CssSelector(".group > material-select-item:nth-child(3)"));
                    //if (ele.Text.Contains("Plan historical metrics (.csv)"))
                    //{
                    //    ele.Click();
                    //    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    //}
                    //else
                    //{
                    //    driver.Navigate().Refresh();
                    //    continue;
                    //}
                    historical:
                    {
                        try
                        {
                            try
                            {
                                IWebElement download = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".download")));
                                download.Click();
                            }
                            catch { }

                            IWebElement ele = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".group > material-select-item:nth-child(3)")));
                            if (ele.Text.Contains("Plan historical metrics (.csv)"))
                            {
                                ele.Click();
                            }
                            //15-05-2020
                            else if ((ele = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".group:nth-child(2) > material-select-item:nth-child(2)")))).Text.Contains(".csv"))
                            {
                                ele.Click();
                            }   //end 15-05-2020
                            else
                                goto historical;
                        }
                        catch
                        {

                        }
                    }
                    Thread.Sleep(15000);

                    try
                    {

                        ProcessResultsKPOLD_48(market, kws, country);
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(10000);
                        Signout(driver);
                        driver.Close();
                        driver.Dispose();
                        return 0;
                    }

                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                    driver.FindElement(By.CssSelector("material-button.back-button")).Click();

                }
                catch (Exception ex)
                {
                    driver.Navigate().Refresh();
                }
            }

            Signout(driver);
            driver.Close();
            driver.Dispose();

            Console.WriteLine();
            Console.WriteLine(" DONE ");

            return 0;
        }

        static void Signout(IWebDriver driver)
        {
            try
            {
                driver.FindElement(By.CssSelector(".trigger")).Click();
                Thread.Sleep(2000);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(By.CssSelector(".sign-out")).Click();
                Thread.Sleep(5000);
            }
            catch { }
        }

        private static void DeleteFile()
        {
            // delete from downloads folder
            string fName = exactpath + @"\downloads";
            DirectoryInfo dinfo2 = new DirectoryInfo(fName);
            FileInfo[] Files2 = dinfo2.GetFiles("*.csv");
            if (Files2.Count() > 0)
            {
                fName = Files2[0].FullName;
                File.Delete(fName);
            }

        }

        private static void WriteToCsv(string kws)
        {
            string file = exactpath + @"\keywords\keywords.csv";
            StreamWriter sw = new StreamWriter(file, false);
            sw.Write(kws.Replace(",", Environment.NewLine));
            sw.Close();
        }

        static void GetEmailID()
        {
            DataTable dt = new DataTable();
            //string qry = "Select id, mailid, password from closeVariantMailIds Where id=2";
            string qry = "Select id, mailid, password from ExactValueMailIds Where id=1";
            using (SqlDataAdapter da = new SqlDataAdapter(qry, ReadConnection()))
            {
                da.Fill(dt);
            }
            if (dt.Rows.Count > 0)
            {
                id = Convert.ToInt32(dt.Rows[0][0]);
                email = dt.Rows[0][1].ToString();
                password = dt.Rows[0][2].ToString();
            }
            dt.Dispose();


        }

        static void ProcessResultsKPOLD_48(string market, string kw, string country)
        {
            string fName = exactpath + @"\downloads";
            DirectoryInfo dinfo2 = new DirectoryInfo(fName);
            FileInfo[] Files2 = dinfo2.GetFiles("*.csv");
            if (Files2.Count() > 0)
                fName = Files2[0].FullName;
            else
                throw new Exception("File not downloaded.");

            try
            {
                ArrayList lst = GetCsvValues_48(fName);
                ArrayList monthsList = getValuesList(lst);
                string[] hdr = monthsList[0] as string[];
                //string[] values = monthsList[1] as string[];

                string[] kwds = kw.Split(',');

                try
                {

                    //if (!string.IsNullOrEmpty(values[0]))
                    if (hdr.Count() == 60) //18-08-2020  changed from 62 to 60 columns
                    {
                        foreach (string s in kwds)
                        {
                            if (s == "Keyword") continue;

                            string smonth = Convert.ToDateTime(hdr[59]).ToString("yyyy-MM");

                            try
                            {
                                SetAPIMonth(smonth);
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Update error: " + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                        Console.WriteLine("+++++++++++++++++++  NULL VALUES FOUND  +++++++++++++++++++++");
                        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                lst = null;
                //monthsList = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        static ArrayList GetCsvValues_48(string fName)
        {
            string[] values = { "" };
            ArrayList arList = new ArrayList();
            try
            {
                StreamReader sr = new StreamReader(fName);
                int r = 0;
                while (!sr.EndOfStream)
                {
                    r++;
                    string ln = sr.ReadLine();
                    //if (r == 1) date = ln.Split('\t')[0];
                    if (r <= 5 && r != 3) continue;
                    values = ln.Split('\t');
                    arList.Add(values);
                }
                sr.Close();
                return arList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

                return null;
            }
        }

        static ArrayList getValuesList(ArrayList arList)
        {
            ArrayList lst = new ArrayList();

            try
            {
                int r = 0;
                foreach (string[] val in arList)
                {
                    string[] values = new string[60]; //18-08-2020 changed from 62 columns to 60
                    for (int i = 0; i < 60; i++) 
                    {
                        if (i == 60)
                        {
                            string s1 = "";
                        }
                        values[i] = val[i].Contains("Searches:") ? (val[i].Split(':')[1].Trim()) :
                            i > 2 ? (val[i].Replace("&#x13;", "-").Replace("  ", "-").Replace("K", "000").Replace("M", "000000")) :
                            val[i];
                    }
                    lst.Add(values);
                    r++;
                }
            }
            catch (Exception ex)
            {
            }

            return lst;
        }

        static string[] GetMonthValues(string kwd, ArrayList monthsList)
        {
            string[] s = null;
            int i = 0;
            foreach (string[] st in monthsList)
            {
                if (i++ == 0) continue;
                if (st[0].ToLower() == kwd.ToLower())
                {
                    s = st;
                    break;
                }
            }

            return s;
        }

        static void GetLastValue()
        {
            x = 0;
            try
            {
                if (File.Exists(Environment.CurrentDirectory + "\\index.txt"))
                {
                    StreamReader sw = new StreamReader(Environment.CurrentDirectory + "\\index.txt");
                    string val = sw.ReadLine();
                    sw.Close();
                    if (!string.IsNullOrEmpty(val.Trim()))
                        x = Convert.ToInt32(val) + 1;
                }
            }
            catch { x = 0; }
            finally { }
        }

        static void SetLastValue(int val)
        {
            try
            {
                StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "\\index.txt", false);
                sw.WriteLine(val);
                sw.Close();
            }
            finally { }
        }

        static string ReadAPI(string apiType)
        {
            XmlDocument xml = new XmlDocument();
            string fileName = @"C:\Inetpub\wwwroot\KPServerIP.xml";
            xml.Load(fileName);

            XmlNode node = null;
            if (apiType == "submit")
                node = xml.SelectSingleNode("ConnectionString/submitapi");
            else
                node = xml.SelectSingleNode("ConnectionString/batchapi");

            string name = node.InnerText;
            return name;
        }

        static string ReadConnection()
        {
            //return strCon;

            XmlDocument xml = new XmlDocument();
            string fileName = @"C:\Inetpub\wwwroot\KPServerIP.xml";
            xml.Load(fileName);

            XmlNode node = xml.SelectSingleNode("ConnectionString/con");
            string name = node.InnerText;

            return name;

        }

        static void SetAPIMonth(string month)
        {
            XmlDocument xml = new XmlDocument();
            string fileName = @"C:\Inetpub\wwwroot\KPServerIP.xml";
            xml.Load(fileName);

            XmlNode node = null;
            node = xml.SelectSingleNode("ConnectionString/batchapi");

            // year-month=2019-08&
            string name = node.InnerText;
            Regex rx = new Regex(@"\d\d\d\d-\d\d", RegexOptions.Singleline);
            Match mc = rx.Match(name);
            if (mc.Success)
            {
                string s = mc.Value;
                name = name.Replace(s, month);
                node.InnerText = name;
                xml.Save(fileName);
            }
        }

        private static void DisplayTimeEvent(Object o)
        {
            //appTimeOut = true;            
        }
    }
}
