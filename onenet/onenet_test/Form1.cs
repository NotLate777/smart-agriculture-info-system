using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace onenet_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string ID = "698227736";//设备ID
        string apikey = "qfJ6Ufa=eqEPWMgn9wC4F5mIp6U=";//Master-APIkey
        string data1 = "temp";//数据流名称


        private void Form1_Load(object sender, EventArgs e)
        {
            setData(data1);
        }

        private String setData(string text)
        {
            string url = "http://api.heclouds.com/devices/" + ID + "/datapoints?datastream_id=" + text;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
            BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(request.Headers, null) as NameValueCollection;
                collection["api-key"] = apikey;
            }
            request.Host = "api.heclouds.com";
            request.ProtocolVersion = new Version(1, 1);
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            textBox3.Text = retString;
            return Value(retString) + "   " + DateTime.Now.ToString("MM-dd hh:mm:ss");
        }

        public string Value(string temp)
        {
            string s = null;
            //textBox4.Text = humi;
            for (int i = 1; i < 9; i++)
            {
                if (checkstring(temp.Substring(99, i)) == true)
                {
                    s = temp.Substring(99, i);
                }
                else
                {
                    break;
                }

            }
            return s;
        }

        public string Value1(string temp)
        {
            string s = null;
            //textBox4.Text = humi;
            for (int i = 1; i < 9; i++)
            {
                if (checkstring(temp.Substring(100, i)) == true)
                {
                    s = temp.Substring(100, i);
                }
                else
                {
                    break;
                }

            }
            return s;
        }

        private bool checkstring(string a)
        {
            bool isnum = Regex.IsMatch(a,  @"^\d+$");
            return isnum;
        }

        public void writer(string path, string[] arr)
        {
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        sw.WriteLine(arr[i]);
                    }
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(@"d:\a.txt"))
                    for (int i = 0; i < arr.Length; i++)
                    {
                        sw.WriteLine(arr[i]);
                    }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox2.Text = setData(data1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = setData(data1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = setData(data1);
        }
    }
}
