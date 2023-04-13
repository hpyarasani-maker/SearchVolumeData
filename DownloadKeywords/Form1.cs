using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DownloadKeywords
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        static string ReadAPI(string apiType)
        {
            XmlDocument xml = new XmlDocument();
            string fileName = @"C:\Inetpub\wwwroot\KPServerIP.xml";
            xml.Load(fileName);

            XmlNode node = xml.SelectSingleNode("ConnectionString/batchapi"); ;

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

        public void GetKeywordsFromAPI()
        {
            string kp_old_url = ReadAPI("batch");
            string authInfo = "pisoftware" + ":" + "r00t123456";
            ArrayList alKws = new ArrayList();
            StringBuilder stringBuilder = new StringBuilder();
            string value = string.Empty;
            Uri uri = new Uri(kp_old_url);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            //httpWebRequest.Timeout = 1000000;
            //httpWebRequest.KeepAlive = true;
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


                    string market = "";
                    string kd = "";

                    for (int i = 0; i < kwd.Count; i++)
                    {
                        kd = kwd[i].InnerText;
                        market = country[0].InnerText;
                        InsertintoDB(market, kd);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        }

        private void InsertintoDB(string market, string kwd)
        {
            string MissingKeywordInsertQuery = "insert into MissingKeywords(market,keyword,status)values('" + market + "',N'" + kwd.Replace("'", "''") + "',0)";
            try
            {
                using (SqlConnection con = new SqlConnection(ReadConnection()))
                {
                    con.Open();
                    using (SqlCommand comm = new SqlCommand(MissingKeywordInsertQuery, con))
                    {
                        comm.CommandTimeout = 0;
                        SqlDataReader dr = comm.ExecuteReader(CommandBehavior.CloseConnection);
                        dr.Close();
                    }
                }

            }
            catch (SqlException ex)
            {
                string errorMessage = "Database Error: \r\n";
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessage += "Index #" + i + "\n" +
                                     "Message: " + ex.Errors[i].Message + "\n" +
                                     "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                                     "Source: " + ex.Errors[i].Source + "\n" +
                                     "Procedure: " + ex.Errors[i].Procedure + "\n" +
                                     "Server: " + ex.Errors[i].Server + "\n";
                }

                throw new Exception(errorMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {

            
                Thread t = new Thread(new ThreadStart(GetKeywordsFromAPI));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
           

        }
    }
}
