using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.IO;

namespace CodingXChange_HTML6
{
    public partial class Form1 : Form
    {
        static bool first = true;
        static string temp = "";
        const int numTimers = 7;
        static List<Timer> timersToCall = new List<Timer>(); //actual num -> numInQueue
        static DateTime[] starts = new DateTime[numTimers];
        static bool[] intos = new bool[numTimers];
        static string[] whichMarket = new string[numTimers];
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.nowinstock.net/videogaming/consoles/sonyps5/");
            //webBrowser1.Navigate("http://sample12345.somee.com/Default.aspx");
            webBrowser1.ScriptErrorsSuppressed = true;
            timer1.Start();

            timersToCall.Add(timer2);
            timersToCall.Add(timer3);
            timersToCall.Add(timer4);
            timersToCall.Add(timer5);
            timersToCall.Add(timer6);
            timersToCall.Add(timer7);
            timersToCall.Add(timer8);
        }
        private void Check(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.nowinstock.net/videogaming/consoles/sonyps5/");
            //webBrowser1.Navigate("http://sample12345.somee.com/Default.aspx");
            webBrowser1.ScriptErrorsSuppressed = true;
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsolutePath != (sender as WebBrowser).Url.AbsolutePath)
                return;
            string whole = webBrowser1.DocumentText.ToString();
            label1.Text = webBrowser1.DocumentText.Length.ToString();
            //string check = "</tr><tr onMouseOver=\"this.bgColor='#99FF99'\" onMouseOut=\"this.bgColor=''\"><td>Dec 02 - 8:14 AM EST</td><td>Target : Console Digital Edition Out of Stock</td>";
            //string check = "</tr><tr onMouseOver=\"this.bgColor='#99FF99'\" onMouseOut=\"this.bgColor=''\"><td>Dec 08 - 12:52 PM EST</td><td>Walmart : Console Digital Edition Out of Stock</td>";
            //string check = "</tr><tr onMouseOver=\"this.bgColor='#99FF99'\" onMouseOut=\"this.bgColor=''\"><td>Dec 09 - 9:12 AM EST</td><td>GameStop : Bundle: Disc All-In-One System Bundle Out of Stock</td>";
            //string check = "</tr><tr onMouseOver=\"this.bgColor='#99FF99'\" onMouseOut=\"this.bgColor=''\"><td>Dec 13 - 3:40 PM EST</td><td>Walmart : Console Out of Stock</td>";
            //string check = "</tr><tr onMouseOver=\"this.bgColor='#99FF99'\" onMouseOut=\"this.bgColor=''\"><td>Dec 16 - 7:48 AM EST</td><td>Target : Console Out of Stock</td>";
            string check = "";
            if (first)
            {
                //check = "</tr><tr onMouseOver=\"this.bgColor='#99FF99'\" onMouseOut=\"this.bgColor=''\"><td>Dec 16 - 2:33 PM EST</td><td>Best Buy : Console Out of Stock</td>";
                //check = "</tr><tr onMouseOver=\"this.bgColor='#99FF99'\" onMouseOut=\"this.bgColor=''\"><td>Dec 16 - 3:04 PM EST</td><td>Best Buy : Console Out of Stock</td>";
                check = "</tr><tr onMouseOver=\"this.bgColor='#99FF99'\" onMouseOut=\"this.bgColor=''\"><td>Dec 16 - 2:05 PM EST</td><td>Best Buy4 : Out of Stock</td>";
            }
            else
            {
                //Console.WriteLine("in1");
                check = temp;
                for (int i = 0; i < 7; i++)
                {
                    if ((intos[i] == true) && (DateTime.Now - starts[i]).TotalMinutes > 10)
                    {
                        timersToCall[i].Stop();
                        timersToCall[i].Enabled = false;
                        if (i == 0)
                        {
                            Console.WriteLine("Timer2 Stopped");
                            button1.BackColor = Color.Red;
                            button1.Text = "button1";
                        }
                        else if (i == 1)
                        {
                            Console.WriteLine("Timer3 Stopped");
                            button2.BackColor = Color.Red;
                            button2.Text = "button2";
                            break;
                        }
                        else if (i == 2)
                        {
                            Console.WriteLine("Timer4 Stopped");
                            button3.BackColor = Color.Red;
                            button3.Text = "button3";
                            break;
                        }
                        else if (i == 3)
                        {
                            Console.WriteLine("Timer5 Stopped");
                            button4.BackColor = Color.Red;
                            button4.Text = "button4";
                        }
                        else if (i == 4)
                        {
                            Console.WriteLine("Timer6 Stopped");
                            button5.BackColor = Color.Red;
                            button5.Text = "button5";
                        }
                        else if (i == 5)
                        {
                            Console.WriteLine("Timer7 Stopped");
                            button6.BackColor = Color.Red;
                            button6.Text = "button6";
                        }
                        else if (i == 6)
                        {
                            Console.WriteLine("Timer8 Stopped");
                            button7.BackColor = Color.Red;
                            button7.Text = "button7";
                        }
                        Console.WriteLine(DateTime.Now.ToLongTimeString());
                        Console.WriteLine("stopped");
                        intos[i] = false;
                    }
                }
            }
            string check2 = "";
            string check3 = "";
            int count = 0;
            int count2 = 0;
            string write = "";
            int counter = 0;
            int _td = 0;
            for (int i = 0; i < whole.Length - 15; i++)
            {
                if (whole.Substring(i, 15).Contains("<th>Status</th>"))
                {
                    check = check.Replace("&#39;", "'").Replace("&quot;", "\"").Replace("\"", "@");

                    counter = 0;
                    //Console.WriteLine(whole);
                    for (int j = i + 16; j < whole.Length - (5 - 1); j++)
                    {
                        if (whole.Substring(j, 5).Contains("</td>"))
                        {
                            //Console.WriteLine("test5");
                            //Console.WriteLine("test8");
                            counter++;
                            if (counter == 2)
                            {
                                //Console.WriteLine("test9");
                                _td = j + 4;
                                break;
                            }
                            j += 3;
                        }
                    }
                    //Console.WriteLine(i + 16);
                    //Console.WriteLine(_td);
                    //check2 = whole.Substring(i + 17 - 1, check.Length).Replace("\"", "@");
                    check2 = whole.Substring(i + 16, _td - (i + 16) + 1).Replace("&#39;", "'").Replace("&quot;", "\"").Replace("\"", "@");
                    //Console.WriteLine("check " + check);
                    //Console.WriteLine("check2 " + check2);
                    //Console.WriteLine("end");
                    if (check2.Contains(check))
                    {
                        if (check.Contains("Out of Stock"))
                        {
                            //Console.WriteLine("test1");
                            label2.Text = "Still not restock";
                            //Console.WriteLine(DateTime.Now.ToLongTimeString());
                            //Console.WriteLine("Still not restock");
                        }
                        //else: do nothing
                    }
                    else
                    {
                        //Console.WriteLine("test2");
                        if (!check2.Contains("Out of Stock"))
                        {
                            //Console.WriteLine("test3");
                            label2.Text = "Buy";
                            counter = 0;
                            int stop = 0;
                            int td = 0;
                            for (int j = 0; j < timersToCall.Count; j++)
                            {
                                if (timersToCall[j].Enabled == false)
                                {
                                    timersToCall[j].Enabled = true;
                                    timersToCall[j].Start();
                                    //timersToCall[j].Interval = 2 * 60 * 1000;
                                    for (int k = 0; k < check2.Length - 4; k++)
                                    {
                                        if (check2.Substring(k, 4).Contains("<td>"))
                                        {
                                            //Console.WriteLine("test5");
                                            counter++;
                                            if (counter == 2)
                                            {
                                                //Console.WriteLine("test6");
                                                td = k;
                                                break;
                                            }
                                            k += 3;
                                        }
                                    }
                                    //Console.WriteLine(td);
                                    check3 = check2.Substring(td + 3 + 1, check2.Length - (td + 3 + 1));
                                    //Console.WriteLine("check3: " + check3);
                                    //for (int k = 0; k < check2.Length - (td + 1); k++)
                                    for (int k = 0; k < check3.Length; k++)
                                    {
                                        if (check3[k] == ':')
                                        {
                                            //Console.WriteLine("TEST");
                                            //stop = k - 2 + td + 1;
                                            stop = k - 2;
                                            break;
                                        }
                                    }
                                    //whichMarket[i] = check2.Substring(td + 1, stop - (td + 1) + 1);
                                    //Console.WriteLine(check3.Substring(0, stop + 1));
                                    whichMarket[j] = check3.Substring(0, stop + 1);
                                    if (j == 0)
                                    {
                                        button1.Text = whichMarket[j].ToString();
                                        button1.BackColor = Color.Green;
                                    }
                                    else if (j == 1)
                                    {
                                        button2.Text = whichMarket[j].ToString();
                                        button2.BackColor = Color.Green;
                                    }
                                    else if (j == 2)
                                    {
                                        button3.Text = whichMarket[j].ToString();
                                        button3.BackColor = Color.Green;
                                    }
                                    else if (j == 3)
                                    {
                                        button4.Text = whichMarket[j].ToString();
                                        button4.BackColor = Color.Green;
                                    }
                                    else if (j == 4)
                                    {
                                        button5.Text = whichMarket[j].ToString();
                                        button5.BackColor = Color.Green;
                                    }
                                    else if (j == 5)
                                    {
                                        button6.Text = whichMarket[j].ToString();
                                        button6.BackColor = Color.Green;
                                    }
                                    else if (j == 6)
                                    {
                                        button7.Text = whichMarket[j].ToString();
                                        button7.BackColor = Color.Green;
                                    }
                                    starts[j] = DateTime.Now;
                                    intos[j] = true;
                                    break;
                                }
                            }
                            var call = CallResource.Create(
                                url: new Uri("https://quicksounds.com/uploads/tracks/2093812876_565129762_740364446.mp3"),
                                to: new Twilio.Types.PhoneNumber("+16269774859"),
                                from: new Twilio.Types.PhoneNumber("+17245586945")
                            );

                            Console.WriteLine(call.Sid);
                            count = i + 17 - 1;
                            count2 = 0;
                            while (whole[count] != '\n')
                            {
                                count2++;
                                count++;
                            }
                            Console.WriteLine(DateTime.Now.ToLongTimeString());
                            Console.WriteLine(count2);
                            Console.WriteLine(check);
                            check = whole.Substring(i + 17 - 1, count2).ToString();
                            write = "";
                            for (int j = 0; j < check.Length; j++)
                            {
                                if (check[j] == '"')
                                {
                                    write += "\\" + "\"";

                                }
                                else
                                {
                                    write += check[j].ToString();
                                }
                            }
                            using (StreamWriter writetext = new StreamWriter("newCheck.txt"))
                            {
                                writetext.WriteLine(write);
                            }
                            //////////////////////////////////Console.WriteLine(check);
                            //////////////////////////////////temp = check;
                            first = false;
                        }
                        else
                        {
                            //Console.WriteLine("Still not restock");
                            label2.Text = "Still not restock";
                            int stop = 0;
                            int td = 0;
                            counter = 0;
                            for (int j = 0; j < check2.Length - 4; j++)
                            {
                                if (check2.Substring(j, 4).Contains("<td>"))
                                {
                                    //Console.WriteLine("test8");
                                    counter++;
                                    if (counter == 2)
                                    {
                                        //Console.WriteLine("test9");
                                        td = j;
                                        break;
                                    }
                                    j += 3;
                                }
                            }

                            check3 = check2.Substring(td + 3 + 1, check2.Length - (td + 3 + 1));
                            //Console.WriteLine("check33: " + check3);
                            //for (int k = 0; k < check2.Length - (td + 1); k++)
                            for (int j = 0; j < check3.Length; j++)
                            {
                                if (check3[j] == ':')
                                {
                                    //Console.WriteLine("TEST");
                                    //stop = k - 2 + td + 1;
                                    stop = j - 2;
                                    break;
                                }
                            }
                            //for(int j = 0; j < 7; j++) {
                            //if(button1.Text == check3.Substring(0, stop + 1))
                            //Console.WriteLine(button1.Text);
                            //Console.WriteLine(check3.Substring(0, stop + 1));
                            if (button1.Text.Contains(check3.Substring(0, stop + 1)) == true)
                            {
                                Console.WriteLine("Timer2 Stopped");
                                timer2.Stop();
                                timer2.Enabled = false;
                                button1.BackColor = Color.Red;
                                button1.Text = "button1";
                                break;
                            }
                            else if (button2.Text.Contains(check3.Substring(0, stop + 1)) == true)
                            {
                                Console.WriteLine("Timer3 Stopped");
                                timer3.Stop();
                                timer3.Enabled = false;
                                button2.BackColor = Color.Red;
                                button2.Text = "button2";
                                break;
                            }
                            else if (button3.Text.Contains(check3.Substring(0, stop + 1)) == true)
                            {
                                Console.WriteLine("Timer4 Stopped");
                                timer4.Stop();
                                timer4.Enabled = false;
                                button3.BackColor = Color.Red;
                                button3.Text = "button3";
                                break;
                            }
                            else if (button4.Text.Contains(check3.Substring(0, stop + 1)) == true)
                            {
                                Console.WriteLine("Timer5 Stopped");
                                timer5.Stop();
                                timer5.Enabled = false;
                                button4.BackColor = Color.Red;
                                button4.Text = "button4";
                            }
                            else if (button5.Text.Contains(check3.Substring(0, stop + 1)) == true)
                            {
                                Console.WriteLine("Timer6 Stopped");
                                timer6.Stop();
                                timer6.Enabled = false;
                                button5.BackColor = Color.Red;
                                button5.Text = "button5";
                            }
                            else if (button6.Text.Contains(check3.Substring(0, stop + 1)) == true)
                            {
                                Console.WriteLine("Timer7 Stopped");
                                timer7.Stop();
                                timer7.Enabled = false;
                                button6.BackColor = Color.Red;
                                button6.Text = "button6";
                            }
                            else if (button7.Text.Contains(check3.Substring(0, stop + 1)) == true)
                            {
                                Console.WriteLine("Timer8 Stopped");
                                timer8.Stop();
                                timer8.Enabled = false;
                                button7.BackColor = Color.Red;
                                button7.Text = "button7";
                            }
                            //}
                        }
                        temp = check;
                    }
                    break;
                }
                //Console.WriteLine(i.ToString());
            }
            //Console.WriteLine(DateTime.Now.ToLongTimeString());
            //////////////////////////////////Console.WriteLine("out");
            /*if (label2.Text == "Buy")
            {
                while (true)
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    Thread.Sleep(1000);
                }
            }*/
            webBrowser1.ScriptErrorsSuppressed = true;
        }
        private void timerToCall_Tick(object sender, EventArgs e)
        {
            string wid = ((Timer)sender).ToString();
            string id = "";
            for (int i = 0; i < wid.Length - 8; i++)
            {
                if (wid.Substring(i, 8).Contains("Interval"))
                {
                    id = wid[i + 15].ToString();
                    break;
                }
            }
            var call = CallResource.Create(
                url: new Uri("https://quicksounds.com/uploads/tracks/2093812876_565129762_740364446.mp3"),
                to: new Twilio.Types.PhoneNumber("+16269774859"),
                from: new Twilio.Types.PhoneNumber("+17245586945")
            );
            Console.WriteLine(call.Sid);
            Console.WriteLine("Timer" + id + " is calling.");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int num = 0;
            for (int i = 0; i < 7; i++)
            {
                if (button1.Text == whichMarket[i].ToString())
                {
                    num = i;
                    break;
                }
            }
            timersToCall[num].Stop();
            timersToCall[num].Enabled = false;
            intos[num] = false;
            button1.BackColor = Color.Red;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int num = 0;
            for (int i = 0; i < 7; i++)
            {
                if (button2.Text == whichMarket[i].ToString())
                {
                    num = i;
                    break;
                }
            }
            timersToCall[num].Stop();
            timersToCall[num].Enabled = false;
            intos[num] = false;
            button2.BackColor = Color.Red;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int num = 0;
            for (int i = 0; i < 7; i++)
            {
                if (button3.Text == whichMarket[i].ToString())
                {
                    num = i;
                    break;
                }
            }
            timersToCall[num].Stop();
            timersToCall[num].Enabled = false;
            intos[num] = false;
            button3.BackColor = Color.Red;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int num = 0;
            for (int i = 0; i < 7; i++)
            {
                if (button4.Text == whichMarket[i].ToString())
                {
                    num = i;
                    break;
                }
            }
            timersToCall[num].Stop();
            timersToCall[num].Enabled = false;
            intos[num] = false;
            button4.BackColor = Color.Red;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int num = 0;
            for (int i = 0; i < 7; i++)
            {
                if (button5.Text == whichMarket[i].ToString())
                {
                    num = i;
                    break;
                }
            }
            timersToCall[num].Stop();
            timersToCall[num].Enabled = false;
            intos[num] = false;
            button5.BackColor = Color.Red;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            int num = 0;
            for (int i = 0; i < 7; i++)
            {
                if (button6.Text == whichMarket[i].ToString())
                {
                    num = i;
                    break;
                }
            }
            timersToCall[num].Stop();
            timersToCall[num].Enabled = false;
            intos[num] = false;
            button6.BackColor = Color.Red;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int num = 0;
            for (int i = 0; i < 7; i++)
            {
                if (button7.Text == whichMarket[i].ToString())
                {
                    num = i;
                    break;
                }
            }
            timersToCall[num].Stop();
            timersToCall[num].Enabled = false;
            intos[num] = false;
            button7.BackColor = Color.Red;
        }
    }
}
