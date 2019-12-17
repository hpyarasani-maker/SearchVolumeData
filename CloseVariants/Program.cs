using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Net;
using System.IO;
using System.Threading;

namespace CloseVariants
{
    class Program
    {
        static string vWord = "";
        static string email = "";
        static string password = "";
        static DateTime tMonth;
        static int id = 0;
        static bool appTimeOut = false;
       
        static int Main(string[] args)
        {
            //string path = @"C:\inetpub\wwwroot\citibank_au.xml";
            //PostXML(path);
            //return 0;

            Program p = new Program();
            GetEmailID();

            string title = "Thread - " + id + " - " + email;

            Console.Title = title + " - With Timer 25 minutes";
            Timer t = new Timer(DisplayTimeEvent, null, (25 * 60000), 1000);

            System.Drawing.Size size = new System.Drawing.Size(1280, 1024);
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-spelling-auto-correct");
            options.AddArgument("--disable-cache");
            IWebDriver driver = new ChromeDriver(@".\ChromeDriver", options);
            driver.Manage().Window.Size = size;

            //driver.Url = "https://ads.google.com/aw/keywordplanner/home?ocid=193200943&__c=4281215607&authuser=0&__u=9516641019&enableAllBrowsers=1";
            driver.Navigate().GoToUrl("https://ads.google.com/aw/keywordplanner/home?ocid=193200943&__c=4281215607&authuser=0&__u=9516641019&enableAllBrowsers=1");
                   
            ////////////////////

            try
            {
                driver.FindElement(By.XPath("//*[@id='identifierId']")).SendKeys(email);
            }
            catch
            {
                driver.FindElement(By.XPath("//*[@id='Email']")).SendKeys(email);
            }
            try
            {
                driver.FindElement(By.XPath("//*[@id='identifierNext']")).Click();
            }
            catch
            {
                driver.FindElement(By.XPath("//*[@id='next']")).Click();
            }
            Console.WriteLine(driver.PageSource);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            try
            {
                driver.FindElement(By.XPath("//input[@name='password']")).SendKeys(password);
            }
            catch
            {
                driver.FindElement(By.XPath("//input[@name='Passwd']")).SendKeys(password);
            }
            try
            {
                driver.FindElement(By.XPath("//*[@id='passwordNext']")).Click();
            }
            catch
            {
                try
                {
                    driver.FindElement(By.CssSelector("#passwordNext")).Click();
                }
                catch
                {
                    driver.FindElement(By.CssSelector("#signIn")).Click();
                }
            }
            Console.WriteLine(driver.PageSource);
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                //driver.FindElement(By.CssSelector("div.HWIeKd")).Click();
                driver.FindElement(By.CssSelector("div.d2laFc")).Click();
            }
            catch
            {
                try
                {
                    driver.FindElement(By.CssSelector("#choose-account-0")).Click();
                }
                catch { }
            }

            ////////////////////

            while (true)
            {
                if (appTimeOut)
                    break;

                DataTable dt = GetKeywords();
                if (dt == null || dt.Rows.Count <= 0)
                    break;

                foreach (DataRow row in dt.Rows)
                {
                    vWord = "";
                    string market = row[0].ToString();
                    string kws = row[1].ToString();
                    string country = row[2].ToString();
                    try
                    {
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                        driver.FindElement(By.CssSelector("div.forecasts-content")).Click();
                        Console.WriteLine(driver.PageSource);
                        driver.FindElement(By.CssSelector("material-input.text-input-component")).SendKeys(WebUtility.HtmlDecode(kws));
                        Console.WriteLine(driver.PageSource);
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                        driver.FindElement(By.CssSelector("material-button.get-results-button")).Click();
                        Console.WriteLine(driver.PageSource);
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(45);

                        Console.WriteLine(driver.PageSource);
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                        Console.WriteLine(driver.PageSource);
                        
                        Historical:
                        {
                            try
                            {
                                IWebElement tab = driver.FindElement(By.CssSelector("tab-button.tab-button:nth-child(3)"));
                                if (tab.Text == "HISTORICAL METRICS")
                                {
                                    tab.Click();

                                }
                                else
                                    goto Historical;
                            }
                            catch (Exception e)
                            {
                                goto Historical;

                            }

                        }
                        
                        // Location Selection.
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                        driver.FindElement(By.CssSelector(".location-button")).Click();
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                        if (driver.FindElements(By.CssSelector(".menu-lookalike")).Count > 0)
                        {
                            try
                            {
                                driver.FindElement(By.CssSelector("th.remove > material-icon:nth-child(1)")).Click();
                                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                try//31-10-2019
                                {
                                    driver.FindElement(By.CssSelector("label.input-container")).Click();
                                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                    driver.FindElement(By.CssSelector("label.input-container")).SendKeys(country);
                                }
                                catch
                                {
                                    try
                                    {
                                        driver.FindElement(By.CssSelector("label.input-container:nth-child(1)")).Click();
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                        driver.FindElement(By.CssSelector("label.input-container:nth-child(1)")).SendKeys(country);
                                    }
                                    catch
                                    {
                                        Console.WriteLine("=========Problem In Country Selection(Location Entry)==========");
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                driver.FindElement(By.CssSelector("location-data-suggestion-entry:nth-child(1)")).Click();
                                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(25);
                                try
                                {
                                    driver.FindElement(By.CssSelector(".highlighted")).Click();
                                    driver.FindElement(By.CssSelector(".save-cancel > material-button:nth-child(2)")).Click();
                                }
                                catch {}
                            }
                            catch { }
                        }

                        try
                        {
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                            vWord = driver.FindElement(By.CssSelector("div.particle-table-row.particle-table-last-row > ess-cell:nth-child(1)")).Text;
                            Console.WriteLine(vWord);
                            ProcessResultsKPOLD_48(market, kws);
                        }
                        catch
                        {

                            vWord = "NoData";
                            ProcessResultsKPOLD_48(market, kws);
                            if (appTimeOut)
                            {
                                driver.Close();
                                driver.Dispose();
                                return 0;
                            }
                        }
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                        try
                        {
                            driver.FindElement(By.CssSelector("material-button.back-button")).Click();
                        }
                        catch
                        {

                        }
                    }
                    catch 
                    {
                        driver.Navigate().GoToUrl("https://ads.google.com/aw/keywordplanner/home?ocid=193200943&__c=4281215607&authuser=0&__u=9516641019&enableAllBrowsers=1");
                        try
                        {
                            driver.FindElement(By.CssSelector("div.WBW9sf")).Click();
                        }
                        catch { }
                        
                        driver.Navigate().Refresh();
                    }
                }
            }

            driver.Close();
            driver.Dispose();

            Console.WriteLine();
            Console.WriteLine(" DONE ");

            return 0;
        }

        static void GetEmailID()
        {
            DataTable dt = new DataTable();
            string qry = "Select id, mailid, password from closeVariantMailIds Where id=9";
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
            dt = new DataTable();
            qry = "Select date from EmptyValuesMonth";
            using (SqlDataAdapter da = new SqlDataAdapter(qry, ReadConnection()))
            {
                da.Fill(dt);
            }
            if (dt.Rows.Count > 0)
            {
                tMonth = Convert.ToDateTime(dt.Rows[0][0]);
            }
        }

        static DataTable GetKeywords()
        {
            DataTable dt = new DataTable();
            string strQry = "GetCloseVariant_" + id;

            using (SqlDataAdapter da = new SqlDataAdapter(strQry, ReadConnection()))
            {
                da.Fill(dt);
            }
            return dt;
        }

        static void ProcessResultsKPOLD_48(string market, string kw)
        {
            try
            {
                string strUpd = "";

                if (kw == vWord)
                {
                    strUpd = "update [closevariant_old] set status=1 Where Market='" + market + "' And Keyword=N'" + kw.Replace("'", "''") + "' ;  ";
                    SendResultsToDB_48(strUpd);

                    Console.WriteLine();
                    Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                    Console.WriteLine("Similar Word Found ");
                    return;
                }

                string path = @"C:\Inetpub\wwwroot\KPOld_48_CV_" + email.Substring(0, email.IndexOf("@")) + ".xml";

                XmlTextWriter writer = new XmlTextWriter(path, Encoding.UTF8)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 2
                };

                if (vWord == "NoData")
                {
                    writer.WriteStartDocument();

                    writer.WriteStartElement("", "search-volume-data", "");
                    //volume-data start
                    writer.WriteStartElement("", "volume-data", "");

                    writer.WriteStartElement("", "keyword", "");
                    writer.WriteString(kw);
                    writer.WriteEndElement();

                    writer.WriteStartElement("", "source", "");
                    writer.WriteString("kp_old");
                    writer.WriteEndElement();

                    writer.WriteStartElement("", "country", "");
                    writer.WriteString(market);
                    writer.WriteEndElement();

                    writer.WriteStartElement("", "currency", "");
                    writer.WriteString("GBP");
                    writer.WriteEndElement();

                    writer.WriteStartElement("", "month", "");
                    writer.WriteString(tMonth.ToString("yyyy-MM"));
                    writer.WriteEndElement();

                    writer.WriteStartElement("", "monthly-volume", "");

                    for (int x = 1; x <= 48; x++)
                    {
                        string month = tMonth.AddMonths(-x + 1).ToString("yyyy-MM");
                        writer.WriteStartElement("", "volume", "");

                        writer.WriteStartElement("", "month", "");
                        writer.WriteString(month);
                        writer.WriteEndElement();

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteEndElement();

                    writer.WriteEndDocument();
                    writer.Flush();
                    writer.Close();

                    strUpd = "update [closevariant_old] set status=1 Where Market='" + market + "' And Keyword=N'" + kw.Replace("'", "''") + "' ;  ";

                    strUpd += "Insert into nullvalues (market, keyword, source_new, status, insertdate, currency, month) values('" + market + "', N'";
                    strUpd += kw.Replace("'", "''") + "', 'kp_old', 1, Convert(varchar(10),'" + DateTime.Today.ToString("yyyy-MM-dd") + "',20), 'GBP', Convert(varchar(10),'" + DateTime.Today.AddMonths(-1).ToString("yyyy-MM") + "',20) );";

                }

                else
                {
                    writer.WriteStartDocument();

                    writer.WriteStartElement("", "search-volume-data", "");
                    //volume-data start
                    writer.WriteStartElement("", "close-variant", "");

                    writer.WriteStartElement("", "keyword", "");
                    writer.WriteString(kw);
                    writer.WriteEndElement();

                    writer.WriteStartElement("", "country", "");
                    writer.WriteString(market);
                    writer.WriteEndElement();

                    writer.WriteStartElement("", "root-keyword", "");
                    writer.WriteString(WebUtility.HtmlDecode(vWord));
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndElement();

                    writer.WriteEndDocument();

                    writer.Flush();
                    writer.Close();

                    strUpd = "update [closevariant_old] set status=1 Where Market='" + market + "' And Keyword=N'" + kw.Replace("'", "''") + "' ;  ";

                    strUpd += "Insert into closevariantdata_old (market, keyword, closevariant, status_close, insertdate) values('" + market + "', N'";
                    strUpd += kw.Replace("'", "''") + "', N'" + WebUtility.HtmlDecode(vWord).Replace("'", "''") + "', 1, Convert(varchar(10),'" + DateTime.Now.ToString("yyyy-MM-dd") + "',20) );";
                }

                try
                {
                    PostXML(path);

                    Console.WriteLine("Xml Completed.");

                    Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                    Console.WriteLine("             Start sending to database ");


                    SendResultsToDB_48(strUpd);

                    Console.WriteLine("     xxxxxxxxxxx     Completed sending to database       xxxxxxxxxx");

                }
                catch (SqlException e)
                {
                    Console.WriteLine("Database Error: " + e.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void SendResultsToDB_48(string qry)
        {
            if (qry == "") return;

            using (SqlConnection con = new SqlConnection(ReadConnection()))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = con;
                    con.Open();
                    comm.CommandTimeout = 0;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = qry;

                    try
                    {
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("========================== Database Error =======================");

                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        static void PostXML(string fileName)
        {
            string submitURL = ReadAPI();

            string user = "pisoftware";
            string pwd = "r00t123456";
            try
            {
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(submitURL);
                httpWReq.UseDefaultCredentials = true;
                httpWReq.PreAuthenticate = true;
                httpWReq.Credentials = CredentialCache.DefaultCredentials;

                Encoding encoding = new UTF8Encoding();
                string postData = GetTextFromXMLFile(fileName);
                byte[] data = encoding.GetBytes(postData);

                httpWReq.ProtocolVersion = HttpVersion.Version11;
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded"; //charset=UTF-8";  


                string auth = string.Format("{0}:{1}", user, pwd);
                string enc = Convert.ToBase64String(Encoding.ASCII.GetBytes(auth));
                string cred = string.Format("{0} {1}", "Basic", enc);


                httpWReq.Headers[HttpRequestHeader.Authorization] = cred;
                httpWReq.ContentLength = data.Length;


                Stream stream = httpWReq.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                string s = response.ToString();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                Thread.Sleep(2000);
                string xmlResponse = "";
                string temp = null;
                while ((temp = reader.ReadLine()) != null)
                {
                    xmlResponse += temp;
                }
                reader.Close();
                response.Close();
            }
            catch (WebException ex)
            {
                string error = "";
                string message = "";

                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    error = string.Format("Error:{0}", httpResponse.StatusCode);

                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        message = reader.ReadToEnd();
                    }
                }

                throw new Exception(error + "\n" + message);

            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }

        }

        static string GetTextFromXMLFile(string file)
        {
            StreamReader reader = new StreamReader(file);
            string ret = reader.ReadToEnd();
            reader.Close();
            return ret;
        }

        static string ReadConnection()
        {
            XmlDocument xml = new XmlDocument();
            string fileName = @"C:\Inetpub\wwwroot\KPServerIP.xml";
            xml.Load(fileName);

            XmlNode node = xml.SelectSingleNode("ConnectionString/con");
            string name = node.InnerText;

            return name;
        }

        static string ReadAPI()
        {
            XmlDocument xml = new XmlDocument();
            string fileName = @"C:\Inetpub\wwwroot\KPServerIP.xml";
            xml.Load(fileName);

            XmlNode node = null;
            node = xml.SelectSingleNode("ConnectionString/submitapi");

            string name = node.InnerText;
            return name;
        }

        private static void DisplayTimeEvent(Object o)
        {
            //appTimeOut = true;
        }
    }
}
