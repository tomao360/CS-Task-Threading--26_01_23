using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Tasks_MultiThreading
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //Ex4 - Get the last report from ynet
        private async void button1_Click(object sender, EventArgs e)
        {
            // Canceles the thread after one hour
            CancellationTokenSource cancellation = new CancellationTokenSource(TimeSpan.FromHours(1));
            
            // Thes while loop runs until a cancellation request is made
            while (!cancellation.IsCancellationRequested) 
            {
                string ynetText = await GetYnetNews();

                // Get the index of the start and end of the first item element in the file
                int startItem = ynetText.IndexOf("<item>");
                int endItem = ynetText.IndexOf("</item>");
                // Extract the item element from the file
                string item = ynetText.Substring(startItem, endItem - startItem);

                // Get the index of the start and end of the title element in the file
                int startTitle = item.IndexOf("<title>");
                int endTitle = item.IndexOf("</title>");
                // Extract the title element from the file
                string title = item.Substring(startTitle, endTitle - startTitle + 7);

                // Get the index of the start and end of the description element in the file
                int startDescription = item.IndexOf("<description>");
                int endDescription = item.IndexOf("</description>");
                // Extract the description element from the file
                string description = item.Substring(startDescription, endDescription - startDescription + 13);

                richTextBox1.Text = title + description;
            }

            // Exit the winform when the while loop stops
            Application.Exit();
        }
        
        public Task<string> GetYnetNews()
        {                   
            return Task.Run(() =>
            {
                HttpClient httpClient = new HttpClient();
                var websiteText = httpClient.GetStringAsync(@"http://www.ynet.co.il/Integration/StoryRss2.xml");
                // Wait 2 minutes before making another request
                Thread.Sleep(2*60*1000);
                return websiteText;
            });
        }
    }
}
