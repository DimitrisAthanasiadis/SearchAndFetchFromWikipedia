using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

namespace Search_and_fetch_from_Wikipedia
{
    public partial class Search : Form
    {
        public Search()        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Search_Load(object sender, EventArgs e)
        {

        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            var webclient = new WebClient();
            var pageSourceCode = webclient.DownloadString("http://en.wikipedia.org/w/api.php?format=xml&action=query&prop=extracts&titles=" + textBox1.Text + "&redirects=true");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(pageSourceCode);

            var fnode = doc.GetElementsByTagName("extract")[0];

            try
            {
                string ss = fnode.InnerText;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                string.Format("Before:{0}", ss);
                ss = regex.Replace(ss, string.Empty);
                string result = String.Format(ss);
                richTextBox1.Text += result;
            }
            catch(Exception)
            {
                richTextBox1.Text = "error";
            }
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            const string message = "Are you sure you want to export ?";
            const string caption = "Export";
            DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.No)
            {
                
            }
            System.IO.File.WriteAllLines(@"C:\Users\New\Desktop\Wikipedia Exports\" + textBox1.Text + ".doc", richTextBox1.Lines);
        }
    }
}
