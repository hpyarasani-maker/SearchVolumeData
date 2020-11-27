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
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Diagnostics;
using System.Runtime.Serialization;
namespace ExactValuesSimilarBatchKeywords
{
    class Program
    {

        static string email = "";
        static string password = "";
        static int id = 0;
        static int x = 0;
        static string exactpath;
        static bool appTimeOut = false;

        static int Main(string[] args)
        {
            //Console.WriteLine("Enter Id");
            //int id = int.Parse(Console.ReadLine());
            GetEmailID();
            exactpath = @"C:\inetpub\wwwroot\exactvalues\" + email.Split('@')[0];

            string title = "Thread - " + id + " - " + email;
            Console.Title = title + " - Without Timer";

            //System.Threading.Timer t = new System.Threading.Timer(DisplayTimeEvent, null, (90 * 60000), 1000);
            //Console.Title = title;

            System.Drawing.Size size = new System.Drawing.Size(1280, 1024);

            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", exactpath + @"\downloads");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

            IWebDriver driver = new ChromeDriver(@".\ChromeDriver", chromeOptions);
            OpenQA.Selenium.Support.UI.WebDriverWait Wait = new WebDriverWait(driver, new TimeSpan(0, 0, 50));
            driver.Manage().Window.Size = size;

            //Goes to login page
            string url = "https://ads.google.com/aw/keywordplanner/home?ocid=193200943&__c=4281215607&authuser=0&__u=9516641019&enableAllBrowsers=1";
            driver.Navigate().GoToUrl(url);
            try
            {
                IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='identifierId']")));

                element.SendKeys(email);
            }
            catch//27-11-2020
            {
                try
                {
                    IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Email']")));

                    element.SendKeys(email);
                }
                catch (ElementNotVisibleException ex)
                {
                    LogError(ex, "Error While Entering Gmail ID, Element Is NotVisible");
                }
                catch (NoSuchElementException ex)
                {
                    LogError(ex, "Error While Entering Gmail ID, NoSuch Element Is Present");
                }
                catch (StaleElementReferenceException ex)
                {
                    LogError(ex, "Error While Entering Gmail ID, the target element is no longer valid in the document DOM");
                }
                catch (TimeoutException ex)
                {
                    LogError(ex, "Error While Entering Gmail ID, TimeoutError Occured");
                }
                catch (WebDriverException ex)
                {
                    LogError(ex, "Error While Entering Gmail ID, Clicking Actions Are Too Late");
                }
                catch (Exception ex)
                { LogError(ex, "Error While Entering Gmail ID"); }
            }
            try
            {
                IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='identifierNext']")));

                element.Click();
            }
            catch//27-11-2020
            {
                try
                {
                    IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='next']")));

                    element.Click();
                }
                catch (ElementNotVisibleException ex)
                {
                    LogError(ex, "Error While clicking Next Button(Mail LogIn), Element Is NotVisible");
                }
                catch (NoSuchElementException ex)
                {
                    LogError(ex, "Error While clicking Next Button(Mail LogIn), NoSuch Element Is Present");
                }
                catch (StaleElementReferenceException ex)
                {
                    LogError(ex, "Error While clicking Next Button(Mail LogIn), the target element is no longer valid in the document DOM");
                }
                catch (TimeoutException ex)
                {
                    LogError(ex, "Error While clicking Next Button(Mail LogIn), TimeoutError Occured");
                }
                catch (WebDriverException ex)
                {
                    LogError(ex, "Error While clicking Next Button(Mail LogIn), Clicking Actions Are Too Late");
                }
                catch (Exception ex)
                { LogError(ex, "Error While clicking Next Button(Mail LogIn)"); }
            }

            try
            {
                IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@name='password']")));

                element.SendKeys(password);
            }
            catch//27-11-2020
            {
                try
                {
                    IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@name='Passwd']")));

                    element.SendKeys(password);
                }
                catch (ElementNotVisibleException ex)
                {
                    LogError(ex, "Error While Entering Gmail Password, Element Is NotVisible");
                }
                catch (NoSuchElementException ex)
                {
                    LogError(ex, "Error While Entering Gmail Password, NoSuch Element Is Present");
                }
                catch (StaleElementReferenceException ex)
                {
                    LogError(ex, "Error While Entering Gmail Password, the target element is no longer valid in the document DOM");
                }
                catch (TimeoutException ex)
                {
                    LogError(ex, "Error While Entering Gmail Password, TimeoutError Occured");
                }
                catch (WebDriverException ex)
                {
                    LogError(ex, "Error While Entering Gmail Password, Clicking Actions Are Too Late");
                }
                catch (Exception ex)
                { LogError(ex, "Error While Entering Gmail Password"); }
            }
            try
            {
                IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='passwordNext']")));

                element.Click();
            }
            catch
            {
                try
                {
                    IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#passwordNext")));

                    element.Click();
                }
                catch//27-11-2020
                {
                    try
                    {

                        IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#signIn")));

                        element.Click();
                    }
                    catch (ElementNotVisibleException ex)
                    {
                        LogError(ex, "Error While clicking SignIn Button, Element Is Not Visible");
                    }
                    catch (NoSuchElementException ex)
                    {
                        LogError(ex, "Error While clicking SignIn Button, NoSuch Element Is Present");
                    }
                    catch (StaleElementReferenceException ex)
                    {
                        LogError(ex, "Error While clicking SignIn Button, the target element is no longer valid in the document DOM");
                    }
                    catch (TimeoutException ex)
                    {
                        LogError(ex, "Error While clicking SignIn Button, TimeoutError Occured");
                    }
                    catch (WebDriverException ex)
                    {
                        LogError(ex, "Error While clicking SignIn Button, Clicking Actions Are Too Late");
                    }
                    catch (Exception ex)
                    { LogError(ex, "Error While clicking SignIn Button"); }
                }
            }
            Console.WriteLine(driver.PageSource);
            try
            {
                //choose an account after login wait time 10 seconds

                IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.d2laFc")));
                element.Click();

            }
            catch//27-11-2020
            {
                try
                {
                    IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#choose-account-0")));
                    element.Click();

                }
                catch (ElementNotVisibleException ex)
                {
                    LogError(ex, "Error While clicking AccountChooser Window, Element Is NotVisible");
                }
                catch (NoSuchElementException ex)
                {
                    LogError(ex, "Error While clicking AccountChooser Window, NoSuch Element Is Present");
                }
                catch (StaleElementReferenceException ex)
                {
                    LogError(ex, "Error While clicking AccountChooser Window, the target element is no longer valid in the document DOM");
                }
                catch (TimeoutException ex)
                {
                    LogError(ex, "Error While clicking AccountChooser Window, TimeoutError Occured");
                }
                catch (WebDriverException ex)
                {
                    LogError(ex, "Error While clicking AccountChooser Window, Clicking Actions Are Too Late");
                }
                catch (Exception ex)
                { LogError(ex, "Error While clicking AccountChooser Window"); }
            }

            while (true)
            {
                if (appTimeOut)
                    break;

                ArrayList alKeywords = new ArrayList();
                try
                {
                    //method downloads keywords from Pi API
                    //alKeywords = GetKeywordsFromDB();
                    //alKeywords = GetKeywordsManully();//06-07-2020
                    alKeywords = GetBatchSimilarKeywordsApi().Result; //14-09-2020
                    if (alKeywords.Count <= 0)
                    {
                        //it will try again keywords are not downloaded and wait for 10 seconds to download again
                        Thread.Sleep(10000);
                        continue;
                    }
                }
                catch (Exception ex)//27-11-2020
                {
                    LogError(ex, "Error While Downloading Keywords From API");
                    //any error repeats again for 10 secs
                    Thread.Sleep(10000);
                    continue;
                }

                foreach (string kwItem in alKeywords)
                {
                    string[] item = kwItem.Split(':');
                    string market = item[0];
                    string country = item[1];
                    string kws = "Keyword, " + item[2]; //20-08-2020 
                    string keyword = item[2];
                    try
                    {
                        //method deletes previous downloaded csv files
                        try//27-11-2020
                        {
                            DeleteFile();
                        }
                        catch (FileNotFoundException ex)
                        {
                            LogError(ex, "Error While Deleting CSV File, File Not Found");
                        }
                        catch (PathTooLongException ex)
                        {
                            LogError(ex, "Error While Deleting CSV File, File Path Is Too Long");
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            LogError(ex, "Error While Deleting CSV File, No Permission To Access FIle");
                        }
                        catch (Exception ex)
                        {
                            LogError(ex, "Error While Deleting CSV File");
                        }
                        WebDriverWait tensecondswait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));

                        try//27-11-2020
                        {
                            WriteToCsv(kws);
                        }
                        catch (FileNotFoundException ex)
                        {
                            LogError(ex, "Error Writing Keywords To CSV, File Not Found");
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            LogError(ex, "Error Writing Keywords To CSV, No Permission To Access FIle");
                        }
                        catch (Exception ex)
                        {
                            LogError(ex, "Error Writing Keywords To CSV");
                        } // for batch keywords 

                        try
                        {

                            IWebElement element = tensecondswait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.WBW9sf")));
                            element.Click();

                        }
                        catch(Exception ex) { ex.Message.ToString(); }
                        try
                        {

                            IWebElement element = tensecondswait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".particle-table-header > header-tools-cell:nth-child(1) > div:nth-child(1) > mat-checkbox:nth-child(1)")));
                            element.Click();
                            //if any plans are stored in draft it will remove the list in first page

                            IWebElement removeplan = tensecondswait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.element:nth-child(5) > element:nth-child(1) > toolbelt-material-menu:nth-child(2) > material-menu:nth-child(1)")));

                            if (removeplan.Text.Contains("Remove plan"))
                                removeplan.Click();
                            IWebElement focus = tensecondswait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".is-focused")));
                            focus.Click();

                            try
                            {
                                IWebElement accountchooser = tensecondswait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.WBW9sf")));
                                accountchooser.Click();
                            }
                            catch { }
                        }
                        catch//27-11-2020
                        {
                            try
                            {
                                IWebElement removeplan = tensecondswait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.element:nth-child(5) > element:nth-child(1) > toolbelt-material-menu:nth-child(2) > material-menu:nth-child(1)")));

                                if (removeplan.Text.Contains("Remove plan"))
                                    removeplan.Click();
                                IWebElement focus = tensecondswait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".is-focused")));
                                focus.Click();
                            }
                            catch (ElementNotVisibleException ex)
                            {
                                LogError(ex, "Error While Remove plan, Element Is NotVisible");
                            }
                            catch (NoSuchElementException ex)
                            {
                                LogError(ex, "Error While Remove plan, NoSuch Element Is Present");
                            }
                            catch (StaleElementReferenceException ex)
                            {
                                LogError(ex, "Error While Remove plan, the target element is no longer valid in the document DOM");
                            }
                            catch (TimeoutException ex)
                            {
                                LogError(ex, "Error While Remove plan, TimeoutError Occured");
                            }
                            catch (WebDriverException ex)
                            {
                                LogError(ex, "Error While Remove plan, Clicking Actions Are Too Late");
                            }
                            catch (Exception ex)
                            { LogError(ex, "Error While Removing Plans"); }
                        }
                        try
                        {
                            IWebElement accountchooser = tensecondswait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.WBW9sf")));
                            accountchooser.Click();
                        }
                        catch { }

                        try//27-11-2020
                        {

                            IWebElement forecast = tensecondswait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".forecasts-content > div:nth-child(1) > div:nth-child(3) > material-icon:nth-child(1) > i:nth-child(1)")));
                            forecast.Click();
                            Console.WriteLine(driver.PageSource);
                        }
                        catch (ElementNotVisibleException ex)
                        {
                            LogError(ex, "Error While Clicking GetForecast Window, Element Is NotVisible");
                        }
                        catch (NoSuchElementException ex)
                        {
                            LogError(ex, "Error While Clicking GetForecast Window, NoSuch Element Is Present");
                        }
                        catch (StaleElementReferenceException ex)
                        {
                            LogError(ex, "Error While Clicking GetForecast Window, the target element is no longer valid in the document DOM");
                        }
                        catch (TimeoutException ex)
                        {
                            LogError(ex, "Error While Clicking GetForecast Window, TimeoutError Occured");
                        }
                        catch (WebDriverException ex)
                        {
                            LogError(ex, "Error While Clicking GetForecast Window, Clicking Actions Are Too Late");
                        }
                        catch (Exception ex)
                        { LogError(ex, "Error While Clicking GetForecast Window"); }

                    // 07-08-2020
                    //for batch keywords 
                    //downloaded keywords file uploading.
                    skip:
                        {

                            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                            try
                            {
                                WebDriverWait minwait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
                                Console.WriteLine(driver.PageSource);
                                IWebElement upload = minwait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".upload-button")));
                                upload.Click();

                            }
                            catch
                            {
                                driver.FindElement(By.CssSelector(".upload-button")).Click();
                            }
                            if (driver.FindElements(By.XPath("//*[@id='select-overlay-target']")).Count > 0)
                            {
                                IWebElement choosefilebutton = Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='select-overlay-target']")));
                                Thread.Sleep(2000);
                                choosefilebutton.Click();
                                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                Thread.Sleep(3000);
                                string filename = exactpath + @"\keywords\keywords.csv";
                                SendKeys.SendWait(filename);
                                SendKeys.SendWait(@"{Enter}");


                            }
                        }

                        Console.WriteLine(driver.PageSource);

                        try//27-11-2020
                        {
                            IWebElement save = tensecondswait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".save-button")));
                            save.Click();
                        }
                        catch (ElementNotVisibleException ex)
                        {
                            LogError(ex, "Error While Clicking Save Button, Element Is NotVisible");
                        }
                        catch (NoSuchElementException ex)
                        {
                            LogError(ex, "Error While Clicking Save Button, NoSuch Element Is Present");
                        }
                        catch (StaleElementReferenceException ex)
                        {
                            LogError(ex, "Error While Clicking Save Button, the target element is no longer valid in the document DOM");
                        }
                        catch (TimeoutException ex)
                        {
                            LogError(ex, "Error While Clicking Save Button, TimeoutError Occured");
                        }
                        catch (WebDriverException ex)
                        {
                            LogError(ex, "Error While Clicking Save Button, Clicking Actions Are Too Late");
                        }
                        catch (Exception ex)
                        { LogError(ex, "Error While Clicking Save Button"); }

                        if (driver.FindElements(By.CssSelector(".save-button.is-disabled")).Count > 0)
                        {
                            IWebElement cancel = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".cancel-button")));
                            cancel.Click();
                            SendKeys.SendWait(@"{Esc}");
                            //downloaded keywords file is missing then it will try for one more..
                            goto skip;
                        }
                        //try
                        //{
                        // WebDriverWait buttonwait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
                        // IWebElement savebutton = buttonwait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".save-button")));
                        //savebutton.Click();
                        //}
                        //catch
                        //{ }
                        try//27-11-2020
                        {

                            IWebElement itemelement = Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div/skinny-nav-item[3]/a"))); //23-10-2020 changed selector
                            itemelement.Click();
                        }
                        catch (ElementNotVisibleException ex)
                        {
                            LogError(ex, "Error While Clicking Keywords Window, Element Is NotVisible");
                        }
                        catch (NoSuchElementException ex)
                        {
                            LogError(ex, "Error While Clicking Keywords Window, NoSuch Element Is Present");
                        }
                        catch (StaleElementReferenceException ex)
                        {
                            LogError(ex, "Error While Clicking Keywords Window, the target element is no longer valid in the document DOM");
                        }
                        catch (TimeoutException ex)
                        {
                            LogError(ex, "Error While Clicking Keywords Window, TimeoutError Occured");
                        }
                        catch (WebDriverException ex)
                        {
                            LogError(ex, "Error While Clicking Keywords Window, Clicking Actions Are Too Late");
                        }
                        catch (Exception ex)
                        { LogError(ex, "Error While Clicking Keywords Window"); }

                        try//27-11-2020
                        {
                            IWebElement tab = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("tab-button.tab-button:nth-child(3)")));
                            tab.Click();
                        }
                        catch (ElementNotVisibleException ex)
                        {
                            LogError(ex, "Error While Clicking Historical Metrics Window, Element Is NotVisible");
                        }
                        catch (NoSuchElementException ex)
                        {
                            LogError(ex, "Error While Clicking Historical Metrics Window, NoSuch Element Is Present");
                        }
                        catch (StaleElementReferenceException ex)
                        {
                            LogError(ex, "Error While Clicking Historical Metrics Window, the target element is no longer valid in the document DOM");
                        }
                        catch (TimeoutException ex)
                        {
                            LogError(ex, "Error While Clicking Historical Metrics Window, TimeoutError Occured");
                        }
                        catch (WebDriverException ex)
                        {
                            LogError(ex, "Error While Clicking Historical Metrics Window, Clicking Actions Are Too Late");
                        }
                        catch (Exception ex)
                        { LogError(ex, "Error While Clicking Historical Metrics Window"); }

                        // end for batch keywords 
                        // end 07-08-2020

                        /* 07-08-2020
                        ////////////////////
                        // for single keyword.

                        IWebElement itemelement = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("material-input.text-input-component")));
                        itemelement.SendKeys(WebUtility.HtmlDecode(keyword));

                        WebDriverWait minwait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));

                        IWebElement resbtn = minwait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("material-button.get-results-button")));
                        resbtn.Click();

                        // end for single keyword.
                        ////////////////////
                        end 07-08-2020 */

                        //06-08-2020
                        Thread.Sleep(10000);

                        //20-08-2020 // added try block.
                        try
                        {
                            //IWebElement el = driver.FindElement(By.CssSelector(".location-button > div:nth-child(1) > div:nth-child(2)"));
                            //if (el.Text.ToLower() == country.ToLower())
                            IWebElement el = driver.FindElement(By.CssSelector(".location-button"));
                            if (el.Text.Split(':')[1].ToLower() == country.ToLower())
                            {
                                goto LOCATION;
                            }
                        }
                        catch { }
                        // Location Selection. 
                        WebDriverWait hiswait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
                        try//27-11-2020
                        {
                            IWebElement location = hiswait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".location-button")));
                            location.Click();
                        }
                        catch (ElementNotVisibleException ex)
                        {
                            LogError(ex, "Error While Clicking Location Button, Element Is NotVisible");
                        }
                        catch (NoSuchElementException ex)
                        {
                            LogError(ex, "Error While Clicking Location Button, NoSuch Element Is Present");
                        }
                        catch (StaleElementReferenceException ex)
                        {
                            LogError(ex, "Error While Clicking Location Button, the target element is no longer valid in the document DOM");
                        }
                        catch (TimeoutException ex)
                        {
                            LogError(ex, "Error While Clicking Location Button, TimeoutError Occured");
                        }
                        catch (WebDriverException ex)
                        {
                            LogError(ex, "Error While Clicking Location Button, Clicking Actions Are Too Late");
                        }
                        catch (Exception ex)
                        { LogError(ex, "Error While Clicking Location Button"); }
                        //end 06-08-2020

                        //23-10-2020
                        try//27-11-2020
                        {
                            IWebElement removecountry = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("th.remove > material-icon:nth-child(1)")));
                            removecountry.Click();
                        }
                        catch (ElementNotVisibleException ex)
                        {
                            LogError(ex, "Error While Removing Previous Country, Element Is NotVisible");
                        }
                        catch (NoSuchElementException ex)
                        {
                            LogError(ex, "Error While Removing Previous Country, NoSuch Element Is Present");
                        }
                        catch (StaleElementReferenceException ex)
                        {
                            LogError(ex, "Error While Removing Previous Country, the target element is no longer valid in the document DOM");
                        }
                        catch (TimeoutException ex)
                        {
                            LogError(ex, "Error While Removing Previous Country, TimeoutError Occured");
                        }
                        catch (WebDriverException ex)
                        {
                            LogError(ex, "Error While Removing Previous Country, Clicking Actions Are Too Late");
                        }
                        catch (Exception ex)
                        { LogError(ex, "Error While Removing Previous Country"); }


                        if (driver.FindElements(By.CssSelector(".menu-lookalike")).Count > 0)
                        {

                            try//27-11-2020
                            {
                                WebDriverWait inputwait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
                                IWebElement labelinput = inputwait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("label.input-container:nth-child(1)")));
                                labelinput.Click();

                                labelinput.SendKeys(country);
                            }
                            catch//27-11-2020
                            {
                                //Also checks locations which throws exceptions 
                                try
                                {
                                    IWebElement labelinput = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("label.input-container")));

                                    labelinput.SendKeys(country);

                                }
                                catch (ElementNotVisibleException ex)
                                {
                                    LogError(ex, "Error While Location Entry, Element Is NotVisible");
                                }
                                catch (NoSuchElementException ex)
                                {
                                    LogError(ex, "Error While Location Entry, NoSuch Element Is Present");
                                }
                                catch (StaleElementReferenceException ex)
                                {
                                    LogError(ex, "Error While Location Entry, the target element is no longer valid in the document DOM");
                                }
                                catch (TimeoutException ex)
                                {
                                    LogError(ex, "Error While Location Entry, TimeoutError Occured");
                                }
                                catch (WebDriverException ex)
                                {
                                    LogError(ex, "Error While Location Entry, Clicking Actions Are Too Late");
                                }
                                catch (Exception ex)
                                {
                                    LogError(ex, "Error In Country Selection(Location Entry)");
                                    Console.WriteLine("=========Problem In Country Selection(Location Entry)==========");
                                    throw new Exception();
                                }
                            }


                            try//27-11-2020
                            {
                                //25-11-2020
                                Console.WriteLine(driver.PageSource);
                                WebDriverWait countryWait = new WebDriverWait(driver, new TimeSpan(0, 1, 5));
                                IWebElement locationsuggestion = countryWait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("location-data-suggestion-entry:nth-child(1)")));
                                locationsuggestion.Click();
                                //25-11-2020 END
                            }
                            catch (ElementNotVisibleException ex)
                            {
                                LogError(ex, "Error  In Target Selection Issue, Element Is NotVisible");
                            }
                            catch (NoSuchElementException ex)
                            {
                                LogError(ex, "Error In Target Selection Issue, NoSuch Element Is Present");
                            }
                            catch (StaleElementReferenceException ex)
                            {
                                LogError(ex, "Error In Target Selection Issue, the target element is no longer valid in the document DOM");
                            }
                            catch (TimeoutException ex)
                            {
                                LogError(ex, "Error In Target Selection Issue, TimeoutError Occured");
                            }
                            catch (WebDriverException ex)
                            {
                                LogError(ex, "Error In Target Selection Issue, Clicking Actions Are Too Late");
                            }
                            catch (Exception ex)
                            {
                                LogError(ex, "Error In Country Selection(Target Selection Issue)==");
                                Console.WriteLine("==Problem In Country Selection(Target Selection)==");
                                throw new Exception();
                            }


                            try
                            {

                                IWebElement highlight = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".highlighted")));
                                highlight.Click();

                                //IWebElement element = driver.FindElement(By.CssSelector(".location-button > div:nth-child(1) > div:nth-child(2)")); //20-08-2020 commented
                                IWebElement element = driver.FindElement(By.CssSelector(".location-button")); //20-08-2020
                                //If any country miss match then continue to next keyword.
                                if (element.Text.Contains("All locations") || element.Text.Split(':')[1].ToLower() != country.ToLower()) //20-08-2020
                                {
                                    LogError(null, "In Country Selection(Missmatched Location)" + "Actual Country is " + country + " selected country is " + element.Text);
                                    Console.WriteLine("==Problem In Country Selection(Missmatched Location)=" + "Actual Country is " + country + " selected country is " + element.Text);

                                    //27-11-2020
                                    //Signout(driver);
                                    //driver.Close();
                                    //driver.Dispose();
                                    //return 0;

                                    throw new Exception();
                                    //end 27-11-2020
                                }
                                else
                                    Console.WriteLine("=====Country Selected Successfully=====");

                            }
                            catch (ElementNotVisibleException ex)
                            {
                                LogError(ex, "Error  While Saving Location, Element Is NotVisible");
                            }
                            catch (NoSuchElementException ex)
                            {
                                LogError(ex, "Error While Saving Location, NoSuch Element Is Present");
                            }
                            catch (StaleElementReferenceException ex)
                            {
                                LogError(ex, "Error While Saving Location, the target element is no longer valid in the document DOM");
                            }
                            catch (TimeoutException ex)
                            {
                                LogError(ex, "Error While Saving Location, TimeoutError Occured");
                            }
                            catch (WebDriverException ex)
                            {
                                LogError(ex, "Error While Saving Location, Clicking Actions Are Too Late");
                            }
                            catch (Exception ex)
                            {
                                //If any error occured in country selection then signout and exit..
                                Console.WriteLine("==Problem In Country Selection=={0]", ex.Message.ToString());
                                throw new Exception("Country selection problem");
                                //25-11-2020 commented below lines
                                //Signout(driver);
                                //driver.Close();
                                //driver.Dispose();
                                //return 0;
                                //end 25-11-2020
                            }


                        }


                        LOCATION:
                        /* 07-08-2020
                        ////////////////////
                        // for single keyword.
                        //06-08-2020
                        try
                        {
                            WebDriverWait tabwait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
                            IWebElement tab = tabwait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("tab-button.tab-button:nth-child(3)")));
                            tab.Click();
                            //end 06-08-2020
                        }
                        catch { }
                        // end for single keyword.
                        ////////////////////
                        end 07-08-2020 */

                        //Date Selection
                        try
                        {
                            try//27-11-2020
                            {
                                IWebElement dropdown = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".dropdown")));
                                dropdown.Click();
                            }
                            catch (ElementNotVisibleException ex)
                            {
                                LogError(ex, "Error  While Selecting Date, Element Is NotVisible");
                            }
                            catch (NoSuchElementException ex)
                            {
                                LogError(ex, "Error While Selecting Date, NoSuch Element Is Present");
                            }
                            catch (StaleElementReferenceException ex)
                            {
                                LogError(ex, "Error While Selecting Date, the target element is no longer valid in the document DOM");
                            }
                            catch (TimeoutException ex)
                            {
                                LogError(ex, "Error While Selecting Date, TimeoutError Occured");
                            }
                            catch (WebDriverException ex)
                            {
                                LogError(ex, "Error While Selecting Date, Clicking Actions Are Too Late");
                            }
                            catch (Exception ex)
                            { LogError(ex, "Error While Selecting Date in DropDownlList"); }

                            IWebElement e = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.range-button:nth-child(5)")));
                            if (e.Text.Contains("All available"))
                            {
                                e.Click();

                            }
                            IWebElement month = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".date-popup-button")));
                            // if the date is selected other then 48 months then signout and exit
                            if (month.Text.Contains("All available") != true)
                            {
                                LogError(new Exception(), "Error while selecting All available Months");
                                Console.WriteLine("===========Problem While Selecting Months============"); //25-11-2020
                                Signout(driver);
                                driver.Close();
                                driver.Dispose();
                                return 0;
                            }
                        }
                        catch(Exception ex)
                        {
                            LogError(ex, "Error In Date Selection");
                            //if any error occurs in date selection then signout and exit
                            Console.WriteLine("Error In Date Selection");

                            Signout(driver);
                            driver.Close();
                            driver.Dispose();
                            return 0;
                        }

                        //Download CSV File

                        historical:
                        {
                            try//27-11-2020
                            {
                                //27-11-2020 removed try block
                                try
                                {
                                    IWebElement download = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".download")));
                                    download.Click();
                                }
                                catch (ElementNotVisibleException ex)
                                {
                                    LogError(ex, "Error  While clicking CSV Download Button, Element Is NotVisible");
                                }
                                catch (NoSuchElementException ex)
                                {
                                    LogError(ex, "Error While While clicking CSV Download Button, NoSuch Element Is Present");
                                }
                                catch (StaleElementReferenceException ex)
                                {
                                    LogError(ex, "Error While While clicking CSV Download Button, the target element is no longer valid in the document DOM");
                                }
                                catch (TimeoutException ex)
                                {
                                    LogError(ex, "Error While clicking CSV Download Button, TimeoutError Occured");
                                }
                                catch (WebDriverException ex)
                                {
                                    LogError(ex, "Error While clicking CSV Download Button, Clicking Actions Are Too Late");
                                }
                                catch (Exception ex)
                                { LogError(ex, "Error While clicking CSV Download Button"); }

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
                            catch (Exception ex)
                            { LogError(ex, "Error While Downloading CSV File"); }
                        }
                        //downloading csv file waiting time..
                        Thread.Sleep(25000);
                        try
                        {
                            // 48 months
                            ProcessResultsKPOLD_48(market, keyword, country);
                        }
                        catch (Exception ex)
                        {
                            //in case account returns ranges or APP timeout then signout and exit 
                            if (ex.Message.StartsWith("Ranges started") || appTimeOut)
                            {
                                LogError(ex, "Ranges started");
                                // Thread.Sleep(10000);
                                Signout(driver);
                                driver.Close();
                                driver.Dispose();
                                return 0;
                            }
                            else
                                LogError(ex, "Error While Processing Results");
                        }
                        //goes to first page for next batch keywords
                        try//27-11-2020
                        {

                            IWebElement backbutton = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("material-button.back-button")));
                            backbutton.Click();
                        }
                        catch (ElementNotVisibleException ex)
                        {
                            LogError(ex, "Error  While clicking Clicking Back Button, Element Is NotVisible");
                        }
                        catch (NoSuchElementException ex)
                        {
                            LogError(ex, "Error While While clicking Clicking Back Button, NoSuch Element Is Present");
                        }
                        catch (StaleElementReferenceException ex)
                        {
                            LogError(ex, "Error While While clicking Clicking Back Button, the target element is no longer valid in the document DOM");
                        }
                        catch (TimeoutException ex)
                        {
                            LogError(ex, "Error While clicking Clicking Back Button, TimeoutError Occured");
                        }
                        catch (WebDriverException ex)
                        {
                            LogError(ex, "Error While clicking Clicking Back Button, Clicking Actions Are Too Late");
                        }
                        catch (Exception ex)
                        { LogError(ex, "Error While Clicking Back Button"); }
                        //24-11-2020 updated code return to KP first page
                        try//27-11-2020
                        {
                            Console.WriteLine(driver.PageSource);
                            if (driver.FindElements(By.CssSelector(".forecasts-content > div:nth-child(1) > div:nth-child(3) > material-icon:nth-child(1) > i:nth-child(1)")).Count == 0)
                                driver.Navigate().GoToUrl("https://ads.google.com/aw/keywordplanner/home");

                        }
                        catch (Exception ex)
                        { LogError(ex, "Error While Navigating To Home Page"); }
                        //end 24-11-2020
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            LogError(ex, "Something Went Wrong"); //27-11-2020
                            //if any error occurs other then above exceptions in entire process this returns to login page
                            Console.WriteLine(ex.Message);
                            driver.Navigate().GoToUrl("https://ads.google.com/aw/keywordplanner/home?ocid=325109181&euid=331594490&__u=5223172010&uscid=325109181&__c=2039899669&authuser=0&enableAllBrowsers=1");
                            try
                            {
                                IWebElement chooseaccount = Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.WBW9sf")));
                                chooseaccount.Click();
                            }
                            catch { }
                        }
                        catch
                        {

                        }
                    }
                }
            }

            Signout(driver);
            driver.Close();
            driver.Dispose();

            Console.WriteLine();
            Console.WriteLine(" DONE ");

            return 0;
        }
        static void LogError(Exception ex = null, string custommessage = "")
        {
            try
            {
                int line = 0;
                if (ex != null)
                {
                    //StackTrace st = new StackTrace(ex, true);
                    StackFrame CallStack = new StackFrame(1, true);
                    line = CallStack.GetFileLineNumber();
                }
                string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += "------------------START------------------------------";
                message += Environment.NewLine;
                message += ex != null ? "Exception Raised At Line No:" + line.ToString() : "Exception : Unspecified Exception";
                message += Environment.NewLine;
                message += ex != null ? "Exception:" + ex.Message : "Exception : Unspecified Exception";
                message += Environment.NewLine;
                message += string.Format("Custom Message: {0}", custommessage);
                message += Environment.NewLine;
                message += "-----------------------END--------------------------------";
                message += Environment.NewLine;
                string path = exactpath + @"\errorlog.txt";
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }
            }
            catch (Exception e)
            {

            }


        }
        static ArrayList GetKeywordsManully()
        {
            //string kwd = "0 finance laptops";
            string kwd = "apple mac laptop air";
            ArrayList alKws = new ArrayList();
            string kwdList = "gb" + ":" + "United Kingdom" + ":" + kwd;
            alKws.Add(kwdList);
            return alKws;
        }
        static ArrayList GetKeywordsFromDB()
        {
            ArrayList alKws = new ArrayList();
            try
            {
                DataTable dt = new DataTable();
                //string strQry = "select Market,countryname,Keyword from [48MonthsKeywordsData_Old_SimilarKeywords] where status_old=0";
                string strQry = "[dbo].[GetBulkSimilarKeywords_1]";//100 batch keyword all keywords
                //string strQry = "[dbo].[GetSimilarKeywords]"; //All keywords
                //string strQry = "[dbo].[GetSimilarKeywords_1]";
                //string strQry = "[dbo].[GetSimilarKeywords_2]";
                //string strQry = "[dbo].[GetSimilarKeywords_3]";
                //string strQry = "[dbo].[GetSimilarKeywords_4]";
                //string strQry = "[dbo].[GetSimilarKeywords_5]";
                //string strQry = "[dbo].[GetSimilarKeywords_6]";
                //string strQry = "[dbo].[GetSimilarKeywords_7]";
                //string strQry = "[dbo].[GetSimilarKeywords_8]";
                //string strQry = "[dbo].[GetSimilarKeywords_8]";
                using (SqlDataAdapter da = new SqlDataAdapter(strQry, ReadConnection()))
                {
                    da.Fill(dt);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    string keywordItem = dr[0].ToString() + ":" + dr[1].ToString() + ":" + dr[2].ToString();
                    alKws.Add(keywordItem);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return alKws;
        }


        static void Signout(IWebDriver driver)
        {
            try
            {
                driver.FindElement(By.CssSelector(".trigger")).Click();
                // Thread.Sleep(2000);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(By.CssSelector(".sign-out")).Click();
                //Thread.Sleep(5000);
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

            // for batch keywords 
            // delete from keywords folder
            fName = exactpath + @"\keywords";
            dinfo2 = new DirectoryInfo(fName);
            Files2 = dinfo2.GetFiles("*.csv");
            if (Files2.Count() > 0)
            {
                fName = Files2[0].FullName;
                File.Delete(fName);
            }
            // end for batch keywords 

        }

        private static void WriteToCsv(string kws)
        {
            string file = exactpath + @"\keywords\keywords.csv";
            StreamWriter sw = new StreamWriter(file, false);
            sw.Write(kws.Replace(",",Environment.NewLine));
            sw.Close();
        }

        static void GetEmailID()
        {
            DataTable dt = new DataTable();
            //string qry = "Select id, mailid, password from closeVariantMailIds Where id=3";
            string qry = "Select id, mailid, password from ExactValueMailIds Where id=10";
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
                try
                {
                    File.WriteAllText(fName, File.ReadAllText(fName), Encoding.UTF8);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while keywords file writing");
                    throw new Exception(e.Message);
                }
                ArrayList lst = GetCsvValues_48(fName);
                ArrayList monthsList = getValuesList(lst);
                string[] hdr = monthsList[0] as string[];
                string[] values = monthsList.Count > 1 ? monthsList[1] as string[] : null;

                string[] kwds = kw.Split(',');

                if (!string.IsNullOrEmpty(values[3]) && string.IsNullOrEmpty(values[12])) //27-11-2020
                {
                    Console.WriteLine("========================================================");
                    Console.WriteLine("------------------ Ranges Started ----------------------");
                    Console.WriteLine("========================================================");

                    throw new Exception("Ranges started");
                }

                string path = @"C:\Inetpub\wwwroot\KPDataOld_48_" + email.Split('@')[0] + ".xml";

                try
                {
                    long yearValue;
                    int n = 0;
                    bool isCloseVariant;

                    foreach (string kwd in kwds) //20-08-2020
                    {
                        string s = kwd.Trim();
                        if (s == "Keyword") continue;

                        if (appTimeOut)
                        {
                            throw new Exception("TimeOut");
                        }

                        string qry = "";//27-07-2020
                        qry = "update [48MonthsKeywordsData_Old_SimilarKeywords] set status_old=1 where Market='" + market + "' and Keyword=N'" + s.Replace("'", "''") + "';  "; //14-08-2020 //single keyword is updating //27-07-2020

                        if (!string.IsNullOrEmpty(values?[0]))
                        {
                            yearValue = 0;
                            isCloseVariant = false;
                            //string kwd = WebUtility.HtmlDecode(s);
                            string[] values1 = GetMonthValues(s, monthsList);

                            if (values1 == null && monthsList[0] != null)
                            {
                                isCloseVariant = true;

                                Console.WriteLine("========================================================");
                                Console.WriteLine("--- Unmatched keyword found ---");
                                Console.WriteLine("========================================================");

                                /* 07-08-2020
                                try
                                {
                                    //qry = ""; // 29-07-2020  -- no need to update the status for similarkeywords table.
                                    qry += "insert into [48MonthsKeywordsData_Old_Batch] ([Market],[Keyword],[countryname],[source_old],[status_old],[status_close],[insertdate]) values('";
                                    qry += market + "', N'" + s.Replace("'", "''") + "', N'" + country + "', 'kp_old', 1, 0, Convert(varchar(10),'" + DateTime.Now.ToString("yyyy-MM-dd") + "',20) );  ";

                                    //27-07-2020
                                    qry += "insert into [48MonthsKeywordsData_Old_EmptyValues] ([Market],[Keyword],[countryname],[Currency],[source_old],[status_old],[status_close],[insertdate],[month48],[month48value]";
                                    qry += ",[month47],[month47value],[month46],[month46value],[month45],[month45value],[month44],[month44value],[month43],[month43value],[month42],[month42value]";
                                    qry += ",[month41],[month41value],[month40],[month40value],[month39],[month39value],[month38],[month38value],[month37],[month37value],[month36]";
                                    qry += ",[month36value],[month35],[month35value],[month34],[month34value],[month33],[month33value],[month32],[month32value],[month31],[month31value]";
                                    qry += ",[month30],[month30value],[month29],[month29value],[month28],[month28value],[month27],[month27value],[month26],[month26value],[month25]";
                                    qry += ",[month25value],[month24],[month24value],[month23],[month23value],[month22],[month22value],[month21],[month21value],[month20],[month20value]";
                                    qry += ",[month19],[month19value],[month18],[month18value],[month17],[month17value],[month16],[month16value],[month15],[month15value],[month14]";
                                    qry += ",[month14value],[month13],[month13value],[month12],[month12value],[month11],[month11value],[month10],[month10value],[month9],[month9value]";
                                    qry += ",[month8],[month8value],[month7],[month7value],[month6],[month6value],[month5],[month5value],[month4],[month4value],[month3]";
                                    qry += ",[month3value],[month2],[month2value],[month1],[month1value],[annualvalue],[cpc],[cpclow],[cpchigh],[competition],[impressions],[closeVariant],[Errorcode],[ErrorMessage] ) values('";
                                    qry += market + "', N'" + s.Replace("'", "''") + "', N'" + country + "','" + "INR" + "', 'kp_old', 1, 0, Convert(varchar(10),'";
                                    qry += DateTime.Now.ToString("yyyy-MM-dd") + "',20), ";

                                    for (int i = 48; i >= 1; i--)
                                    {
                                        string month = Convert.ToDateTime(hdr[i + 11]).ToString("yyyy-MM");
                                        qry += "'" + month + "', " + "null" + ", ";

                                    }
                                    qry += yearValue + ", 0, " + "null" + ", " + "null" + ", " + 0 + ", 0 , " + "null" + ", " + "null" + ", " + "null" + ");";
                                    //end of 27-07-2020

                                    SendResultsToDB_48(qry);

                                    //stores root keyword in closeVariant table
                                    SendMissedKeywordResult(market, s, country);
                                }
                                catch (SqlException e)
                                {
                                    Console.WriteLine("Database error updating cv data:");
                                    Console.WriteLine("Error: " + e.Message);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error updating cv data: " + ex.Message);
                                }
                                end 07-08-2020 */
                                continue;
                            }
                            else if (values1 == null && monthsList[0] == null)
                            {
                                continue;
                            }

                            XmlTextWriter writer = new XmlTextWriter(path, Encoding.UTF8);

                            writer.Formatting = Formatting.Indented;
                            writer.Indentation = 2;

                            writer.WriteStartDocument();

                            writer.WriteStartElement("", "search-volume-data", "");
                            //volume-data start
                            writer.WriteStartElement("", "volume-data", "");

                            writer.WriteStartElement("", "keyword", "");
                            writer.WriteString(s);
                            writer.WriteEndElement();

                            writer.WriteStartElement("", "source", "");
                            writer.WriteString("kp_old");
                            writer.WriteEndElement();

                            writer.WriteStartElement("", "country", "");
                            writer.WriteString(market);
                            writer.WriteEndElement();

                            string smonth = Convert.ToDateTime(hdr[59]).ToString("yyyy-MM");

                            //Sending Empty months
                            if (values1[0].ToLower() == s.ToLower() && values1[12] == "")
                            {
                                //inserting empty months
                                qry += "insert into [48MonthsKeywordsData_Old_Batch] ([Market],[Keyword],[countryname],[Currency],[source_old],[status_old],[status_close],[insertdate]) values('";
                                qry += market + "', N'" + s.Replace("'", "''") + "', N'" + country + "', '" + values1[1] + "', 'kp_old', 1, 0, Convert(varchar(10),'" + DateTime.Now.ToString("yyyy-MM-dd") + "',20) );";

                                writer.WriteStartElement("", "currency", "");
                                writer.WriteString(values1[1]);
                                writer.WriteEndElement();

                                writer.WriteStartElement("", "month", "");
                                writer.WriteString(smonth);

                                writer.WriteEndElement();

                                writer.WriteStartElement("", "monthly-volume", "");

                                for (int i = 48; i >= 1; i--)
                                {
                                    string month = Convert.ToDateTime(hdr[i + 11]).ToString("yyyy-MM");
                                    writer.WriteStartElement("", "volume", "");
                                    writer.WriteStartElement("", "month", "");
                                    writer.WriteString(month);
                                    writer.WriteEndElement();
                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();
                            }
                            //storing exact values
                            else if (values1[0].ToLower() == s.ToLower())
                            {
                                qry += "insert into [48MonthsKeywordsData_Old_Batch] ([Market],[Keyword],[countryname],[Currency],[source_old],[status_old],[status_close],[insertdate],[month48],[month48value]";
                                qry += ",[month47],[month47value],[month46],[month46value],[month45],[month45value],[month44],[month44value],[month43],[month43value],[month42],[month42value]";
                                qry += ",[month41],[month41value],[month40],[month40value],[month39],[month39value],[month38],[month38value],[month37],[month37value],[month36]";
                                qry += ",[month36value],[month35],[month35value],[month34],[month34value],[month33],[month33value],[month32],[month32value],[month31],[month31value]";
                                qry += ",[month30],[month30value],[month29],[month29value],[month28],[month28value],[month27],[month27value],[month26],[month26value],[month25]";
                                qry += ",[month25value],[month24],[month24value],[month23],[month23value],[month22],[month22value],[month21],[month21value],[month20],[month20value]";
                                qry += ",[month19],[month19value],[month18],[month18value],[month17],[month17value],[month16],[month16value],[month15],[month15value],[month14]";
                                qry += ",[month14value],[month13],[month13value],[month12],[month12value],[month11],[month11value],[month10],[month10value],[month9],[month9value]";
                                qry += ",[month8],[month8value],[month7],[month7value],[month6],[month6value],[month5],[month5value],[month4],[month4value],[month3]";
                                qry += ",[month3value],[month2],[month2value],[month1],[month1value],[annualvalue],[cpc],[cpclow],[cpchigh],[competition],[impressions] ) values('";
                                qry += market + "', N'" + s.Replace("'", "''") + "', N'" + country + "', '" + values1[1] + "', 'kp_old', 1, 0, Convert(varchar(10),'";
                                qry += DateTime.Now.ToString("yyyy-MM-dd") + "',20), ";

                                writer.WriteStartElement("", "currency", "");
                                writer.WriteString(values1[1]);
                                writer.WriteEndElement();

                                writer.WriteStartElement("", "month", "");
                                writer.WriteString(smonth);
                                writer.WriteEndElement();

                                writer.WriteStartElement("", "monthly-volume", "");
                                n = 0;
                                for (int i = 48; i >= 1; i--)
                                {
                                    string month = Convert.ToDateTime(hdr[i + 11]).ToString("yyyy-MM");
                                    writer.WriteStartElement("", "volume", "");

                                    writer.WriteStartElement("", "month", "");
                                    writer.WriteString(month);
                                    writer.WriteEndElement();

                                    writer.WriteStartElement("", "value", "");
                                    writer.WriteString(values1[i + 11]);
                                    writer.WriteEndElement();

                                    qry += "'" + month + "', " + values1[i + 11] + ", ";

                                    writer.WriteEndElement();

                                    if (n++ < 12)
                                        try
                                        {
                                            yearValue += Convert.ToInt64(string.IsNullOrEmpty(values1[i + 11]) ? "0" : values1[i + 11]);
                                        }
                                        catch { }
                                }

                                writer.WriteEndElement();

                                if (!string.IsNullOrEmpty(values1[6]))
                                {
                                    writer.WriteStartElement("", "cpc-low", "");
                                    //writer.WriteString(string.IsNullOrEmpty(values1[6]) ? "0" : values1[6]);
                                    writer.WriteString(values1[6]);
                                    writer.WriteEndElement();
                                }

                                if (!string.IsNullOrEmpty(values1[7]))
                                {
                                    writer.WriteStartElement("", "cpc-high", "");
                                    //writer.WriteString(string.IsNullOrEmpty(values1[7]) ? "0" : values1[7]);
                                    writer.WriteString(values1[7]);
                                    writer.WriteEndElement();
                                }

                                decimal comp = 0;
                                if (!string.IsNullOrEmpty(values1[5]))
                                {
                                    //decimal comp = string.IsNullOrEmpty(values1[5].Trim()) ? 0 : (Convert.ToDecimal(values1[5]) / 100);
                                    comp = Convert.ToDecimal(values1[5]) / 100;

                                    writer.WriteStartElement("", "competition", "");
                                    writer.WriteString(comp.ToString());
                                    writer.WriteEndElement();
                                }

                                writer.WriteStartElement("", "impressions", "");
                                writer.WriteString("0");
                                writer.WriteEndElement();

                                if (yearValue > 0) yearValue = yearValue / 12;

                                writer.WriteStartElement("", "annual-volume-value", "");
                                writer.WriteString(yearValue.ToString());
                                writer.WriteEndElement();

                                qry += yearValue + ", 0, " + (string.IsNullOrEmpty(values1[6]) ? "0" : values1[6]) + ", " + (string.IsNullOrEmpty(values1[7]) ? "0" : values1[7]) + ", " + comp + ", 0 );";

                            }

                            writer.WriteEndElement();

                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                            writer.Flush();
                            writer.Close();
                            //Results submitting to Pi APi
                            try
                            {
                                Console.WriteLine(s);
                                if (!string.IsNullOrEmpty(values[0]) && !isCloseVariant)
                                    PostXML(path, s, market);//06-07-2020 //included market parameter

                                SendResultsToDB_48(qry);

                            }
                            catch (SqlException e)
                            {
                                Console.WriteLine("Database Error: " + e.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Sending Xml Error: " + ex.Message);
                            }
                        }
                        else
                        {
                            //if csv file contains total empty returns message
                            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            Console.WriteLine("+++++++++++++++++++  NULL VALUES FOUND  +++++++++++++++++++++");
                            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

                            /* 07-08-2020
                            qry += "insert into [48MonthsKeywordsData_Old_EmptyValues] ([Market],[Keyword],[countryname]) ";
                            qry += "Values('" + market + "', N'" + s.Replace("'", "''") + "', N'" + country + "');";

                            SendResultsToDB_48(qry);
                            end 07-08-2020 */

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                lst = null;
                monthsList = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        static void PostXML(string fileName, string kn, string market)//06-07-2020 included market parameter
        {
            string submitURL = ReadAPI("submit");

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
                StreamReader reader = new StreamReader(response.GetResponseStream());
                String xmlResponse = "";
                String temp = null;
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
                            string qry = "update [48MonthsKeywordsData_Old_SimilarKeywords] set status_old=1, errorcode=" + error + ",ErrorMessage='" + message + "' where Market='" + market + "' and Keyword=N'" + kn.Replace("'", "''") + "'";
                            SendResultsToDB_48(qry);
                        }
                    }
                    //06-07-2020
                }

                throw new Exception(error + "\n" + message);

            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
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
                    catch (SqlException ex)
                    {
                        Console.WriteLine("========================== Database Error (Sql Ex) =======================");
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("========================== Database Error =======================");
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        static void SendMissedKeywordResult(string market, string kw, string country)
        {
            Console.WriteLine("========================================================");
            Console.WriteLine("----------- Sending to close variant app ---------------");
            Console.WriteLine("========================================================");

            using (SqlConnection con = new SqlConnection(ReadConnection()))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = con;
                    con.Open();

                    comm.CommandType = System.Data.CommandType.Text;
                    //comm.CommandText = "InsertCloseVariantKeywords";
                    // 13-07-2020 -- no need to insert the kwd again in this table.
                    comm.CommandText = "Update closevariant_old set status=0 where market='@Market' and keyword=N'@Keyword'";

                    comm.CommandTimeout = 0;

                    comm.Parameters.Add(new SqlParameter("@Market", System.Data.SqlDbType.NVarChar, 100)).Value = market;
                    comm.Parameters.Add(new SqlParameter("@Keyword", System.Data.SqlDbType.NVarChar, 255)).Value = kw.Replace("'", "''");
                    //comm.Parameters.Add(new SqlParameter("@Country", System.Data.SqlDbType.NVarChar, 100)).Value = country;   // 13-07-2020
                    try
                    {
                        comm.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("========================== Close Variant Database Error (SQL Ex) =======================");
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("========================== Close Variant Database Error =======================");
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        static string GetTextFromXMLFile(string file)
        {
            StreamReader reader = new StreamReader(file);
            string ret = reader.ReadToEnd();
            reader.Close();
            return ret;
        }

        static async Task<ArrayList> GetBatchSimilarKeywordsApi()//14-09-2020
        {
            //string url = "http://82.136.46.2:8080/api/GetBulkSimilarKeywords";
            string url = "https://similarkeywordapis.azurewebsites.net/api/GetBulkSimilarKeywords";
            /*Uri queryUri = new Uri(url);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(queryUri);
            req.Headers.Clear();
            req.Method = "Get";
            req.ContentType = "application/json";
            req.Headers.Clear();
            ArrayList alKws = new ArrayList();
            string response;
            try
            {
                HttpWebResponse res = (HttpWebResponse)await req.GetResponseAsync();
                using (StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
                {
                    response = reader.ReadToEnd();
                }
                res.Close();
                if (response != "null")
                {
                    JArray jo = JArray.Parse(response);
                    foreach (var item in jo)
                    {
                        string kwdList = item["market"] + ":" + item["countryname"] + ":" + item["keyword"];
                        alKws.Add(kwdList);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }*/
            ArrayList alKws = new ArrayList();
            string response = string.Empty;
            Uri ul = new Uri(url);
            using (var client = new HttpClient())
            {

                try
                {

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    response = client.GetStringAsync(ul).Result;
                    if (response != "null")
                    {
                        JArray jo = JArray.Parse(response);
                        foreach (var item in jo)
                        {
                            string kwdList = item["market"] + ":" + item["countryname"] + ":" + item["keyword"];
                            alKws.Add(kwdList);
                        }
                    }

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return await Task.FromResult(alKws);

        }

        static ArrayList GetKeywordsFromAPI()
        {
            string kp_old_url = ReadAPI("batch");
            string authInfo = "pisoftware" + ":" + "r00t123456";
            ArrayList alKws = new ArrayList();
            StringBuilder stringBuilder = new StringBuilder();
            string value = string.Empty;
            Uri uri = new Uri(kp_old_url);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Timeout = 1000000;
            httpWebRequest.KeepAlive = true;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
            XmlDocument doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings { CheckCharacters = false };

            using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
            using (XmlReader reader = XmlReader.Create(response.GetResponseStream(), settings))
            {
                try
                {
                    reader.MoveToContent();
                    doc.Load(reader);
                    reader.Close();
                    XmlNodeList msg = doc.GetElementsByTagName("message");
                    if (msg.Count > 0)
                    {
                        throw new Exception(msg[0].InnerText);
                    }
                    XmlNodeList country = doc.GetElementsByTagName("country");
                    XmlNodeList kwd = doc.GetElementsByTagName("keyword");
                    XmlNodeList source = doc.GetElementsByTagName("source");
                    XmlNodeList priority = doc.GetElementsByTagName("priority");

                    string market = "";
                    string kd = "";
                    string src = "";
                    string pr = "";
                    string countryName = "";

                    for (int i = 0; i < kwd.Count; i++)
                    {
                        //market = country[i].InnerText;
                        kd += kwd[i].InnerText + ",";
                        //src = source[i].InnerText;
                        //pr = priority[i].InnerText;                    
                    }
                    kd = kd.Remove(kd.Length - 1);
                    market = country[0].InnerText;
                    src = source[0].InnerText;
                    pr = priority[0].InnerText;
                    countryName = GetCountryName(market);

                    string keywordItem = market + ":" + countryName + ":" + kd;
                    alKws.Add(keywordItem);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return alKws;
        }

        static string GetCountryName(string market)
        {
            string cName = "";
            try
            {
                using (SqlConnection con = new SqlConnection(ReadConnection()))
                {
                    con.Open();
                    using (SqlCommand comm = new SqlCommand("select countryname from countrymarket where market='" + market + "'", con))
                    {
                        comm.CommandTimeout = 0;
                        using (SqlDataReader dr = comm.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                cName = dr.GetValue(0).ToString();
                            }
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Country name not returned from database.");
            }

            return cName;
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
                    string[] values = new string[60];
                    for (int i = 0; i < 60; i++)
                    {
                        values[i] = val[i].Contains("Searches:") ? val[i].Split(':')[1].Trim() :
                            i > 2 ? val[i].Replace("&#x13;", "-").Replace("  ", "-").Replace("K", "000").Replace("M", "000000").Trim() :
                            val[i].Trim();
                    }
                    lst.Add(values);
                    r++;
                }
            }
            catch (Exception)
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
            XmlDocument xml = new XmlDocument();
            string fileName = @"C:\Inetpub\wwwroot\KPServerIP.xml";
            xml.Load(fileName);

            XmlNode node = xml.SelectSingleNode("ConnectionString/con");
            string name = node.InnerText;

            return name;
        }

        private static void DisplayTimeEvent(Object o)
        {
            //Time to quit app..
            //appTimeOut = true;            
        }

    }
}
