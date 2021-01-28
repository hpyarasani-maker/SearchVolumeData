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
using System.Collections;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
namespace CloseVariantsSimilarKeywords
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
            string exactpath = @"C:\inetpub\wwwroot\closevariants\" + email.Split('@')[0];  //closevariants folder must in wwwroot
            DeleteFile(exactpath); //28-01-2021
            Console.Title = title + " - With Timer 28 minutes";
            Timer t = new Timer(DisplayTimeEvent, null, (28 * 60000), 1000);
            Console.Title = title;
            System.Drawing.Size size = new System.Drawing.Size(1280, 1024);
            // ChromeOptions chromeOptions = new ChromeOptions();
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", exactpath + @"\downloads");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            chromeOptions.AddArgument("--disable-spelling-auto-correct");
            chromeOptions.AddArgument("--disable-cache");
            IWebDriver driver = new ChromeDriver(@".\ChromeDriver", chromeOptions);
            OpenQA.Selenium.Support.UI.WebDriverWait Wait = new WebDriverWait(driver, new TimeSpan(0, 0, 50));
            driver.Manage().Window.Size = size;

            //driver.Url = "https://ads.google.com/aw/keywordplanner/home?ocid=193200943&__c=4281215607&authuser=0&__u=9516641019&enableAllBrowsers=1";
            driver.Navigate().GoToUrl("https://ads.google.com/aw/keywordplanner/home?ocid=193200943&__c=4281215607&authuser=0&__u=9516641019&enableAllBrowsers=1");



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
            try
            {
                Console.WriteLine(driver.PageSource);
                driver.FindElement(By.CssSelector("material-list-item.user-customer-list-item:nth-child(1)")).Click();//28-01-2021 updated selector for cancelled account click event
            }
            catch { }
            ////////////////////
            
            while (true)
            {
                if (appTimeOut)
                    break;
                //string[] arr = new string[] { "videos", "gifts" };
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
                        DeleteFile(exactpath);
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

                        LOCATION: //19-08-2020
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
                                try
                                {
                                    driver.FindElement(By.CssSelector("label.input-container")).Click();
                                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                    driver.FindElement(By.CssSelector("label.input-container")).SendKeys(country);
                                }
                                catch
                                {
                                    //Also checks locations which throws exceptions 
                                    try
                                    {
                                        driver.FindElement(By.CssSelector("label.input-container:nth-child(1)")).Click();
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                        driver.FindElement(By.CssSelector("label.input-container:nth-child(1)")).SendKeys(country);
                                    }
                                    catch
                                    {
                                        Console.WriteLine("=========Problem In Country Selection(Location Entry)==========");
                                        throw new Exception();
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
                                catch { }
                            }
                            catch { }
                        }

                        try
                        {
                            //19-08-2020
                            IWebElement element = driver.FindElement(By.CssSelector(".location-button"));
                            string[] countrytext = element.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None); //15-01-2021 getting location text in new line
                            if (countrytext[1].ToString().ToLower() != country.ToLower())
                            {
                                goto LOCATION;
                            }// end 15-01-2021
                            //if (element.Text.Split(':')[1].ToLower() != country.ToLower())
                            //{
                            //    goto LOCATION;
                            //}
                            //end 19-08-2020

                            //Date Selection //20-08-2020
                            try
                            {
                                try
                                {
                                    driver.FindElement(By.CssSelector(".dropdown-icon")).Click(); //15-01-2021
                                }
                                catch { }
                                Thread.Sleep(1500);
                                IWebElement e = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.range-button:nth-child(5)")));
                                if (e.Text.Contains("All available"))
                                {
                                    e.Click();
                                }
                                IWebElement month = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".date-popup-button")));
                                if (month.Text.Contains("All available") != true)
                                {
                                    Console.WriteLine("Error In Date Selection");
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Error In Date Selection");
                            }
                            //end 20-08-2020

                            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                            //vWord = driver.FindElement(By.CssSelector("div.particle-table-row.particle-table-last-row > ess-cell:nth-child(1)")).Text;
                            //Console.WriteLine(vWord);
                            //02 - 07 - 2020 UnComment below code for download csv file
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
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
                            Thread.Sleep(10000);



                            string fName = exactpath + @"\downloads";
                            DirectoryInfo dinfo2 = new DirectoryInfo(fName);
                            FileInfo[] Files2 = dinfo2.GetFiles("*.csv");
                            if (Files2.Length > 0)
                                fName = Files2[0].FullName;
                            else
                                throw new Exception("File not downloaded.");
                            try
                            {
                                File.WriteAllText(fName, File.ReadAllText(fName), Encoding.UTF8);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error while keywords file writing");
                                throw new Exception(e.Message);
                            }
                            //15 - 05 - 2020 End of Downloading csv file
                            ArrayList lst = GetCsvValues_48(fName);//15-05-2020 
                            ArrayList monthsList = getValuesList(lst);//15-05-2020 getting values from downloaded csv file
                            string[] keys = new string[2];//15-05-2020 
                            try    //15-05-2020 getting closevariant from arraylist object
                            {
                                foreach (string[] k in monthsList)
                                {
                                    keys[0] = k[0];
                                    keys[1] = k[1];
                                    break;
                                }
                            }
                            catch
                            {
                                continue;
                            }
                            vWord = keys[1].ToString();

                            if (vWord == null)
                                continue;
                            else
                            {
                                Console.WriteLine(vWord);

                            }
                            //02 - 07 - 2020

                            ProcessResultsKPOLD_48(market, kws, country);
                            DeleteFile(exactpath);


                        }
                        catch
                        {

                            vWord = "NoData";
                            ProcessResultsKPOLD_48(market, kws, country);
                            DeleteFile(exactpath);
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
        static void DeleteFile(string exactpath)
        {

            // delete from downloads folder
            string fName = exactpath + @"\downloads";
            DirectoryInfo dinfo2 = new DirectoryInfo(fName);
            FileInfo[] Files2 = dinfo2.GetFiles("*.csv");
            if (Files2.Length > 0)
            {
                foreach (var file in Files2)
                {
                    //28-01-2021
                    try
                    {
                        fName = file.FullName;
                        File.Delete(fName);
                    }
                    catch
                    {
                        Console.WriteLine();
                        Console.WriteLine("ERROR: Downloaded file deleting error.");
                        Console.WriteLine();
                        Environment.Exit(Environment.ExitCode);
                    }
                    //end 28-01-2021
                }
            }
            //try
            //{
            //    // delete from keywords folder
            //    fName = exactpath + @"\keywords";
            //    dinfo2 = new DirectoryInfo(fName);
            //    Files2 = dinfo2.GetFiles("*.csv");
            //    if (Files2.Length > 0)
            //    {
            //        fName = Files2[0].FullName;
            //        File.Delete(fName);
            //    }
            //}
            //catch
            //{

            //}
        }
        static ArrayList getValuesList(ArrayList arList)
        {
            ArrayList lst = new ArrayList();
            string[] values = new string[2];
            int i = 0; int j = 0;
            try
            {
                int r = 0;
                foreach (string[] val in arList)
                {

                    for (i = 0; i == 0; i++)
                    {
                        values[j] = val[0].ToString();
                    }
                    j++;
                    lst.Add(values);
                    r++;
                }
            }
            catch (Exception ex)
            {
            }

            return lst;
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
        static void GetEmailID()
        {
            DataTable dt = new DataTable();
            string qry = "Select id, mailid, password from closeVariantMailIds Where id=1";
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
        static void SendSimilarKeywordToDb(string market, string kw, string country)
        {
            using (SqlConnection con = new SqlConnection(ReadConnection()))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = con;
                    con.Open();
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    //comm.CommandText = "insert into [48MonthsKeywordsData_Old_SimilarKeywords](Market,Keyword,countryname)values(@Market,@Keyword,@Country);";
                    comm.CommandText = "InsertSimilarKeywords"; //12-07-2020
                    comm.CommandTimeout = 0;
                    comm.Parameters.Add(new SqlParameter("@Market", System.Data.SqlDbType.NVarChar, 100)).Value = market;
                    comm.Parameters.Add(new SqlParameter("@Keyword", System.Data.SqlDbType.NVarChar, 255)).Value = kw.Replace("'", "''");
                    comm.Parameters.Add(new SqlParameter("@Country", System.Data.SqlDbType.NVarChar, 100)).Value = country;
                    try
                    {
                        comm.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("========================== Similar keyword saving error (SQL Ex) =======================");
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("========================== Similar keyword saving error =======================");
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        static void ProcessResultsKPOLD_48(string market, string kw, string country)
        {
            try
            {
                string strUpd = "";

                if (kw == vWord)
                {
                    strUpd = "update [closevariant_old] set status=1 Where Market='" + market + "' And Keyword=N'" + kw.Replace("'", "''") + "' ;  ";
                    SendResultsToDB_48(strUpd);
                    SendSimilarKeywordToDb(market, kw, country);
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
                    PostXML(path, market, kw);

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

        static void PostXML(string fileName, string market, string kw)
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
                //06-07-2020
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(message);
                XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/search-volume-data");
                foreach (XmlNode node in nodeList)
                {
                    XmlNode nd = node.SelectSingleNode(".//code");
                    if (nd != null)
                        error = nd.InnerText;
                    XmlNode nd1 = node.SelectSingleNode(".//message");
                    if (nd1 != null)
                        message = nd1.InnerText;
                    if (error != null && message != null)//changes
                    {

                        string qry = "update [closevariant_old] set status=1 Where Market='" + market + "' And Keyword=N'" + kw.Replace("'", "''") + "' ;  ";

                        SendResultsToDB_48(qry);
                    }
                }
                //06 - 07 - 2020
                throw new Exception(error + "\n" + message);//14-07-2020 uncommented exception error message for calling database method...

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
           appTimeOut = true;
        }
    }
}
