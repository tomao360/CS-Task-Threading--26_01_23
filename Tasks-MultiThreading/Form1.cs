using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;

namespace Tasks_MultiThreading
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //Ex1 - Write 10 lines to a file
        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(WriteToFile);
        }

        private void WriteToFile()
        {
            string fileName = @"D:\Programming\Zionet\C#\Tasks-MultiThreading\Tasks-MultiThreading\bin\Debug\file1.txt";
            if (!File.Exists(fileName))
            {
                using (FileStream file = new FileStream(fileName, FileMode.Create)) ;

                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        writer.WriteLine("Hello World!");
                    }
                }
               
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        writer.WriteLine("Hello World!");
                    }
                }
            }
        }

        //-----------------------------------------------------------------//

        //Ex2 - Label Counter
        int labelCounter = 1;
        private async void button2_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 10; i++)
            {
                label1.Text = await RunThreadLabel();
                labelCounter++;              
            }           
        }

        public Task<string> RunThreadLabel()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(1000);
                return labelCounter.ToString();
            });
        }
        //-----------------------------------------------------------------//

        //Ex3 - Get the number of bytes from ynet home page
        private async void button3_Click(object sender, EventArgs e)
        {
            string ynetText = await GetYnetTxtBytes();
            richTextBox1.Text = ynetText;
            int byteCount = ynetText.Length;
            label2.Text = $"The page consists of {byteCount.ToString()} bytes";
        }

        public Task<string> GetYnetTxtBytes()
        {
            return Task.Run(() =>
            {
                HttpClient httpClient = new HttpClient();
                var websiteText = httpClient.GetStringAsync(@"https://www.ynet.co.il/home/0,7340,L-8,00.html");
                return websiteText;
            });
        }
    }
}
