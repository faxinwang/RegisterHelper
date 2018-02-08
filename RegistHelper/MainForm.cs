using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

namespace RegistHelper
{
    public partial class MainForm : Form
    {
        private static string curPath = Application.StartupPath;
        public static string registerPath = curPath + "/Registers";
        private static string configFile = registerPath + "/config.xml";
        private static string logFile = registerPath + "/logs.txt";
        XmlDocument doc = null;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public MainForm()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            if (!Directory.Exists(registerPath)) Directory.CreateDirectory(registerPath);
            if (!File.Exists(configFile))
            {
                 doc = new XmlDocument();
                createInitialConfigFile();
                MessageBox.Show("增删配置文件中的item以增删签到项");
            }else
            {
                readConfigFile();
            }
            timer.Interval = 25*1000; //every 25 second trigers
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        //reset all Item to be have not done.
        private void resetAll()
        {
            foreach (Item item in registerList.Items) item.HasDone = false;
        }

        //every few second checks if it is time to deal with some of the items.
        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime curTime = DateTime.Now;
            foreach ( var ite in registerList.Items)
            {
                Item item = (Item)ite;
                if (item.HasDone) continue; //if a item has done, just skip it

                //if it is time to do a item or it did not finished successfully
                //keep trying to do it in the following 5 minute
                if (curTime.Hour == item.getHour() && Math.Abs(item.getMinute() - curTime.Minute)<=3 )
                {
                    //it is time to reset all
                    if ("ResetAll".Equals(item.getName())) {
                        resetAll();
                        continue;
                    } 
                    try
                    {
                        Thread th = new Thread(item.startProcess);
                        th.Start();
                        th.Join();
                    }
                    catch (Exception excep) { logToFile("Exception:" + excep.Message); }
                }
            }
        }

        //write messages to local file
        public static void logToFile(string str)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(logFile, true);//append to file
                writer.WriteLine(DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString());
                writer.WriteLine(str);
                writer.WriteLine();
            }
            catch (Exception) { }
            finally { if (writer != null) writer.Close(); }
        }

        //create a config file if it's the first time to run this application or the config file has been removed
        private void createInitialConfigFile()
        {
            //add document declaration
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", ""));
            //create root element
            XmlNode root = doc.CreateElement("RegisterList");
            doc.AppendChild(root);
            addItem(root, "DoRegist_qmx.py", "9:00");
            addItem(root, "DoRegist_qmx.py", "15:00");
            addItem(root, "DoRegist_qmx.py", "20:00");
            addItem(root, "DoRegist_nyoj.py", "18:10");
            doc.Save(configFile);
        }

        //create and append a child to parent with the given name and value
        private XmlNode appendChild(XmlDocument doc, XmlNode parent, string name, string value)
        {
            XmlNode node = doc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parent.AppendChild(node);
            return node;
        }

        private void addItem(XmlNode root, string name, string time)
        {
            XmlNode item = doc.CreateElement("item");
            appendChild(doc, item, "name", name);
            appendChild(doc, item, "time", time);
            root.AppendChild(item);
        }

        //read regist items from config file
        private void readConfigFile()
        {
            XElement doc = XElement.Load(configFile);
            IEnumerable<XElement> items = from elem in doc.Elements("item") select elem;
            List<string> invalidItems = new List<string>();
            List<Item> itemList = new List<Item>();

            foreach (XElement elem in items)
            {
                string name = elem.Element("name").Value.Trim();
                string time = elem.Element("time").Value.Trim();
                string[] tm = time.Split(':');
                if(tm.Length >=2)
                {
                    try
                    {
                        int h = Int16.Parse(tm[0]);
                        int m = Int16.Parse(tm[1]);
                        if (0 <= h && h <= 23 && 0 <= m && m <= 59)
                        {
                            Item item = new Item(name, time, h, m);
                            if (!itemList.Contains(item)) itemList.Add(item);
                        }
                        else invalidItems.Add(name + " : " + time);
                    }
                    catch (Exception) { invalidItems.Add(name + " : "+ time); }
                }else
                {
                    invalidItems.Add(name + " : " + time);
                }
            }
            
            //show invalid items
            if(invalidItems.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("下列签到项配置的时间不合法:\n");
                foreach (string str in invalidItems) sb.Append(str + "\n");
                MessageBox.Show(sb.ToString());
            }
            //add items to ListBox
            if (itemList.Count == 0) MessageBox.Show("签到列表空空如也,闲得很呢!");
            else
            {
                itemList.Sort();
                foreach (Item item in itemList) registerList.Items.Add(item);
            }
        }

        //hide main window if minimized
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                //notifier.ShowBalloonTip(3000);
                notifier.Visible = true;
            }
        }

        //show main window
        private void notifier_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        
        //query user if he really wants to close
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("关闭该窗体将会退出应用程序！如你只是想隐藏窗体，请点击最小化！\n是否退出？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel) e.Cancel = true;
        }

        //exit the application
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Stop();
            timer.Dispose();
            System.Environment.Exit(0);
        }

        //use different colors to draw items of ListBox
        private void registerList_DrawItem(object sender, DrawItemEventArgs e)
        {
            DateTime curTime = DateTime.Now;
            ListBox listBox = sender as ListBox;
            if(e.Index >= 0)
            {
                Item item = listBox.Items[e.Index] as Item;
                Brush brush = null;
                if (item.HasDone)
                {
                    //if the item has done, give it color DarkGreen
                    brush = Brushes.DarkGreen;
                }else
                {
                    //if the item has not done, but the time has passed, give it color Orange
                    if (item.getHour() < curTime.Hour || (item.getHour()==curTime.Hour && item.getMinute() < curTime.Minute) )
                        brush = Brushes.Orange;
                    else
                        brush = Brushes.Red; //if the time has not reached, set it to Red
                }
                e.DrawBackground();
                e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, brush, e.Bounds);
            }
        }

    }

    class Item :IComparable
    {
        string name, time;
        int hour, minute;
        bool hasDone;
        public bool HasDone { get { return hasDone; } set { hasDone = value; } }
        public Item(string name,string time, int hour, int minute)
        {
            this.name = name;
            this.time = time;
            this.hour = hour;
            this.minute = minute;
            this.hasDone = false;
        }
        public string getName() { return name; }
        public int getHour() { return hour; }
        public int getMinute() { return minute; }

        int IComparable.CompareTo(object obj)
        {
            Item item = (Item)obj;
            if (this.hour != item.hour) return this.hour - item.hour;
            return this.minute - item.minute;
        }
        
        public override string ToString()
        {
            if (hasDone) return string.Format("{0}(done!) \t{1}", name, time);
            else return string.Format("{0} \t{1}" ,name, time);
        }

        public override bool Equals(object obj)
        {
            Item item = obj as Item;
            return item != null && item.name.Equals(name) && item.time.Equals(time);
        }
        public override int GetHashCode() { return base.GetHashCode(); }


        //this method will run in a new Thread when it is time to call
        public void startProcess()
        {
            Process p = new Process();
            string fileName = MainForm.registerPath + "/" + name;

            if (!File.Exists(fileName))
            {
                MainForm.logToFile(string.Format("File [{0}] not exist!", fileName));
                return;
            }
            //if this is a python file, then call python interpreter
            if(name.EndsWith(".py"))
            {
                p.StartInfo.FileName = "python3";
                p.StartInfo.Arguments = fileName;
            }
            //if this is a executable file, just call it
            else if(name.EndsWith(".exe"))
            {
                p.StartInfo.FileName = fileName;
            }
            //if this is a jar file, then call java
            else if(name.EndsWith(".jar"))
            {
                p.StartInfo.FileName = "java";
                p.StartInfo.Arguments = fileName;
            }
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.RedirectStandardInput = true;
            try
            {
                p.Start();
                string line;
                while((line =p.StandardOutput.ReadLine())!= null)
                {
                    if (line.Contains("res_success") || line.Contains("RES_SUCCESS")) hasDone = true;
                    else MainForm.logToFile(line);
                }
               // p.WaitForExit(20000);//20s
                p.Kill();
                p.Close();
            }
            catch (Exception e)
            {
                p.Close();
                MainForm.logToFile(string.Format("Exception on file {0} :\n{1}", name, e.Message));
            }
        }
    }
}
