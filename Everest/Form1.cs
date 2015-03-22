using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;

namespace Everest
{
    public partial class MainForm : Form
    {
        RAM ram = new RAM();
        VideoCard videoCard = new VideoCard();
        BaseBoard baseBoard = new BaseBoard();
        Processor processor = new Processor();
        Software soft = new Software();
        public void AllFalse()
        {
            timer1.Stop();
            richTextBoxOS.Clear();
            richTextBoxOS.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            comboBox1.Visible = false;

        }
        public void StepRichTextBox()
        {
            richTextBoxOS.Visible = true;
        }
        public void Step1()
        {
            label8.Visible = true;
            label11.Visible = true;
        }
        public void Step2()
        {
            label9.Visible = true;
            label12.Visible = true;
        }

        public void Step3()
        {
            label10.Visible = true;
            label13.Visible = true;
        }

        public void Step4()
        {
            label1.Visible = true;
            label2.Visible = true;
        }
        public void Step5()
        {
            label3.Visible = true;
            label4.Visible = true;
        }
        public void Step6()
        {
            label5.Visible = true;
            label6.Visible = true;
        }
        public void Step7()
        {
            label14.Visible = true;
            label15.Visible = true;
        }
        public void Step8()
        {
            label16.Visible = true;
            label17.Visible = true;
        }
        public void Step9()
        {
            label18.Visible = true;
            label19.Visible = true;
        }
        public void PZ()
        {
            string registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(registryKey))
            {
                var query = from a in key.GetSubKeyNames()
                            let r = key.OpenSubKey(a)
                            select new
                            {
                                Application = r.GetValue("DisplayName"),
                                InstallLocation = r.GetValue("InstallLocation")
                            };
                foreach (var item in query)
                {
                    if (item.Application != null && item.InstallLocation != null)
                    {
                        richTextBoxOS.AppendText(item.Application + " - " + item.InstallLocation + "\n");
                    }
                    if (item.Application != null)
                    {
                        richTextBoxOS.AppendText(item.Application + "\n");
                    }
                }
            }
        }
        public void Process()
        {
            System.Diagnostics.Process[] processes;
            processes = System.Diagnostics.Process.GetProcesses();
            dataGridView3.ColumnCount = 5;
            dataGridView3.Columns[0].Name = "id";
            dataGridView3.Columns[1].Name = "назва";
            dataGridView3.Columns[2].Name = "id сесії";
            dataGridView3.Columns[3].Name = "фізична память";
            dataGridView3.Columns[4].Name = "оперативна память";
            dataGridView3.Columns[0].Width = 50;
            dataGridView3.Columns[1].Width = 255;
            dataGridView3.Columns[2].Width = 50;
            dataGridView3.Columns[3].Width = 80;
            dataGridView3.Columns[4].Width = 80;

            foreach (System.Diagnostics.Process instance in processes)
            {
                string[] row0 = {Convert.ToString( instance.Id),
                                     instance.ProcessName,
                                     Convert.ToString(instance.SessionId),
                                     Convert.ToString(instance.PagedMemorySize)+" байт",
                                     Convert.ToString(instance.VirtualMemorySize)+" байт"};
                dataGridView3.Rows.Add(row0);

            }
        }
        public void Logic()
        {
            DriveInfo[] alldrivers = DriveInfo.GetDrives();
            int i = 0;
            string[] dr_name = Environment.GetLogicalDrives();
            int a = dr_name.Length;
            string[] dr_type = new string[a];
            string[] dr_label = new string[a];
            string[] dr_format = new string[a];
            string[] dr_sp1 = new string[a];
            string[] dr_sp2 = new string[a];

            foreach (DriveInfo d in alldrivers)
            {
                dr_type[i] = d.DriveType.ToString();

                if (d.IsReady == true)
                {
                    dr_label[i] = d.VolumeLabel;
                    dr_format[i] = d.DriveFormat;
                    dr_sp1[i] = d.AvailableFreeSpace.ToString();
                    dr_sp2[i] = d.TotalSize.ToString();
                }
                else
                {
                    dr_label[i] = "Unknown";
                    dr_format[i] = "Unknown";
                    dr_sp1[i] = "Unknown";
                    dr_sp2[i] = "Unknown";
                }
                i++;
            }
            PerformanceCounter ram = new PerformanceCounter("Memory", "Available MBytes");
            label7.Text = "Кількість вільної памяті: " + ram.NextValue().ToString() + " Mb";
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "назва";
            dataGridView1.Columns[1].Name = "тип";
            dataGridView1.Columns[2].Name = "заголовок";
            dataGridView1.Columns[3].Name = "файлова система";
            dataGridView1.Columns[4].Name = "вільна память";
            dataGridView1.Columns[5].Name = "загальна память";
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 70;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 140;
            dataGridView1.Columns[5].Width = 140;

            for (int j = 0; j < i; j++)
            {
                string[] row0 = { dr_name[j], dr_type[j], dr_label[j], 
            dr_format[j], dr_sp1[j],  dr_sp2[j]};
                dataGridView1.Rows.Add(row0);
            }
        }
        public void Phyzic()
        {
            using (ManagementClass devs = new ManagementClass(@"Win32_Diskdrive"))
            {
                ManagementObjectCollection moc = devs.GetInstances();
                dataGridView2.ColumnCount = 2;
                dataGridView2.Columns[0].Name = "модель";
                dataGridView2.Columns[1].Name = "серійний номер";
                dataGridView2.Columns[0].Width = 235;
                dataGridView2.Columns[1].Width = 300;
                foreach (ManagementObject mo in moc)
                {
                    string[] arr = { mo["Model"].ToString(), mo["SerialNumber"].ToString() };
                    dataGridView2.Rows.Add(arr);
                }
            }
        }
        public void FillComboBox()
        {
            var scope = new ManagementScope(@"\\localhost\root\cimv2");
            scope.Connect();
            var query = new ObjectQuery(@"SELECT * FROM Win32_NetworkAdapter WHERE PhysicalAdapter = True");
            var searcher = new ManagementObjectSearcher(scope, query);
            var networkInterfaces = searcher.Get();

            foreach (var networkInterface in networkInterfaces)
            {
                comboBox1.Items.Add(networkInterface["Name"]);
            }
            comboBox1.SelectedIndex = 0;
        }
        public void Network()
        {
            var scope = new ManagementScope(@"\\localhost\root\cimv2");
            scope.Connect();
            var query = new ObjectQuery(@"SELECT * FROM Win32_NetworkAdapter WHERE PhysicalAdapter = True");
            var searcher = new ManagementObjectSearcher(scope, query);
    
            var networkInterfaces = searcher.Get();
            int i = 0;
            foreach (var networkInterface in networkInterfaces)
            {
                if (i == comboBox1.SelectedIndex)
                {
                    label8.Text = "MAC-адреса:";
                    label9.Text = "ID:";
                    label10.Text = "заголовок:";
                    label1.Text = "тип:";
                    label3.Text = "тип адаптеру:";
                    label5.Text = "опис:";
                    label11.Text = Convert.ToString(  networkInterface["MACAddress"]);
                    label12.Text = Convert.ToString(  networkInterface["DeviceID"]);
                    label13.Text = Convert.ToString(  networkInterface["Caption"]);
                    label2.Text =  Convert.ToString( networkInterface["ProductName"]);
                    label4.Text =  Convert.ToString( networkInterface["AdapterType"]);
                    label6.Text = Convert.ToString(  networkInterface["NetConnectionID"]);
                }                
                i++;
            }
        }
        public MainForm()
        {
            InitializeComponent();
            AllFalse();
            TreeNode node2 = new TreeNode("процессор");
            TreeNode node3 = new TreeNode("материнська плата");
            TreeNode node4 = new TreeNode("оперативна пам'ять");

            TreeNode[] Hardware = new TreeNode[] { node2, node3, node4 };
            TreeNode treeNode = new TreeNode("Системна плата", Hardware);
            treeView1.Nodes.Add(treeNode);

            node2 = new TreeNode("процеси");
            node3 = new TreeNode("ОС");
            node4 = new TreeNode("ПЗ");
            TreeNode[] Software = new TreeNode[] { node2, node3, node4 };

            treeNode = new TreeNode("Операційна система", Software);
            treeView1.Nodes.Add(treeNode);

            node2 = new TreeNode("монітор");
            node3 = new TreeNode("відеокарта");
            TreeNode[] Video = new TreeNode[] { node2, node3 };

            treeNode = new TreeNode("Дисплей", Video);
            treeView1.Nodes.Add(treeNode);

            node2 = new TreeNode("фізична");
            node3 = new TreeNode("логічна");
            TreeNode[] Memory = new TreeNode[] { node2, node3 };

            treeNode = new TreeNode("Пам'ять", Memory);
            treeView1.Nodes.Add(treeNode);
            treeNode = new TreeNode("Мережа");
            treeView1.Nodes.Add(treeNode);
            dataGridView1.Visible = false;
            // richTextBoxOS.BackColor = Color.Transparent;
            Logic();
            label8.ForeColor = Color.Red;
            label9.ForeColor = Color.Red;
            label10.ForeColor = Color.Red;
            label1.ForeColor = Color.Red;
            label3.ForeColor = Color.Red;
            label5.ForeColor = Color.Red;
            label14.ForeColor = Color.Red;
            label16.ForeColor = Color.Red;
            label18.ForeColor = Color.Red;
            Phyzic();
            Process();
            FillComboBox();
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            AllFalse();
            new Thread(delegate()
            {
                this.BackgroundImage = Properties.Resources.Fon;
            }).Start();

            if (treeView1.SelectedNode.Text == "Мережа")
            {
                comboBox1.Visible = true;
                Step1();
                Step2();
                Step3();
                Step4();
                Step5();
                Step6();
                Network();
               


            }

            if (treeView1.SelectedNode.Text == "фізична")
            {
                dataGridView2.Visible = true;
            }
            if (treeView1.SelectedNode.Text == "процеси")
            {
                dataGridView3.Visible = true;
                
            }
            if (treeView1.SelectedNode.Text == "монітор")
            {
                new Thread(delegate()
                {
                    this.BackgroundImage = Properties.Resources.Monitor;
                }).Start();
                Step1();
                Step2();
                label8.Text = "розширення по висоті:";
                label9.Text = "розширення по ширині:";
                label11.Text = Convert.ToString(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height);
                label12.Text = Convert.ToString(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width);
            }
            if (treeView1.SelectedNode.Text == "відеокарта")
            {
                new Thread(delegate()
                {
                    this.BackgroundImage = Properties.Resources.VideoCard;
                }).Start();
                Step1();
                Step2();
                Step3();

                label8.Text = "номер:";
                label9.Text = "адаптер:";
                label10.Text = "графічний процесор:";
                label11.Text = videoCard.adapterRAM;
                label12.Text = videoCard.caption;
                label13.Text = videoCard.videoProcessor;
            }
            if (treeView1.SelectedNode.Text == "материнська плата")
            {
                new Thread(delegate()
                {
                    this.BackgroundImage = Properties.Resources.MainBoard;
                }).Start();
                Step1();
                Step2();
                Step3();
                label8.Text = "виробник:";
                label9.Text = "серійний номер:";
                label10.Text = "версія:";
                label11.Text = baseBoard.manufacturer;
                label12.Text = baseBoard.serialNumber;
                label13.Text = baseBoard.version;
            }
            if (treeView1.SelectedNode.Text == "процессор")
            {
                new Thread(delegate()
                {
                    this.BackgroundImage = Properties.Resources.processor;
                }).Start();
                Step1();
                Step2();
                Step3();
                Step4();
                Step5();
                Step6();
                Step7();
                Step8();

                label8.Text = "назва:";
                label9.Text = "кількість ядер:";
                label10.Text = "id:";
                label11.Text = processor.name;
                label12.Text = processor.numberOfCores;
                label13.Text = processor.processorId;
                label1.Text = "мітка:";
                label3.Text = "опис:";
                label5.Text = "кеш 2 рівня:";
                label14.Text = "виробник:";
                label16.Text = "ревізія:";

                label2.Text =  processor.Caption;
                label4.Text =  processor.Description;
                label6.Text =  processor.L2CacheSize;
                label15.Text = processor.Manufacturer;
                label17.Text = processor.Revision;
            }
            if (treeView1.SelectedNode.Text == "оперативна пам'ять")
            {
                new Thread(delegate()
                {
                    this.BackgroundImage = Properties.Resources.RAM;
                }).Start();
                Step1();
                Step2();
                Step3();
                label8.Text = "порт:";
                label9.Text = "ємність:";
                label10.Text = "швидкість:";
                label11.Text = ram.bankLabel;
                label12.Text = ram.capacity;
                label12.Text += "гб";
                label13.Text = ram.speed + "mhz";
            }
            if (treeView1.SelectedNode.Text == "ОС")
            {
                new Thread(delegate()
                {
                    this.BackgroundImage = Properties.Resources.OS;
                }).Start();


                Step1();
                Step2();
                Step3();
                Step4();
                Step5();
                Step6();
                Step7();
                Step8();
                Step9();
                label8.Text = "назва:";
                label9.Text = "тип:";
                label10.Text = "серійний номер:";
                label1.Text = "системна папка:";
                label3.Text = "системний диск:";
                label5.Text = "версія:";
                label14.Text = "підпис:";
                label16.Text = "номер:";
                label18.Text = "IP:";
                label11.Text = soft.name;
                label12.Text = soft.ostype;
                label13.Text = soft.serialNumber;
                label2.Text = soft.systemDirectory;
                label4.Text = soft.systemDrive;
                label6.Text = soft.version;
                label15.Text = soft.caption;
                label17.Text = soft.buildNumber;
                label19.Text = soft.IP;
            }
            if (treeView1.SelectedNode.Text == "логічна")
            {
                dataGridView1.Visible = true;
                label7.Visible = true;
                timer1.Start();
            }
            if (treeView1.SelectedNode.Text == "ПЗ")
            {

                StepRichTextBox();
                PZ();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PerformanceCounter ram = new PerformanceCounter("Memory", "Available MBytes");
            label7.Text = "Кількість вільної памяті: " + ram.NextValue().ToString() + " Mb";
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

               string name = saveFileDialog1.FileName;
               using (XmlWriter writer = XmlWriter.Create(name))
               {
                   writer.WriteStartElement("iNFORMATIONABOUTPC");
                       writer.WriteStartElement("Processor");
                           writer.WriteElementString("Name", processor.name);
                           writer.WriteElementString("NumberOfCores", processor.numberOfCores);
                           writer.WriteElementString("Id", processor.processorId);
                       writer.WriteEndElement();
                       writer.WriteStartElement("BaseBoard");
                           writer.WriteElementString("Manufacturer", baseBoard.manufacturer);
                           writer.WriteElementString("Product", baseBoard.product);
                           writer.WriteElementString("SerialNumber", baseBoard.serialNumber);
                           writer.WriteElementString("Version", baseBoard.version);
                       writer.WriteEndElement();
                       writer.WriteStartElement("RAM");
                           writer.WriteElementString("Bank", ram.bankLabel);
                           writer.WriteElementString("Capacity", ram.capacity);
                           writer.WriteElementString("Speed", ram.speed);
                       writer.WriteEndElement();
                       writer.WriteStartElement("OS");
                           writer.WriteElementString("BuildNumber", soft.buildNumber);
                           writer.WriteElementString("Caption", soft.caption);
                           writer.WriteElementString("FreeFizikalMemory", soft.freeFizikalMemory);
                           writer.WriteElementString("FreeVirtualMemory", soft.freeVirtualMemory);
                           writer.WriteElementString("IP", soft.IP);
                           writer.WriteElementString("Name", soft.name);
                           writer.WriteElementString("OSType", soft.ostype);
                           writer.WriteElementString("RegisteredUser", soft.registeredUser);
                           writer.WriteElementString("SerialNumber", soft.serialNumber);
                           writer.WriteElementString("SystemDirectory", soft.systemDirectory);
                           writer.WriteElementString("SystemDrive", soft.systemDrive);
                           writer.WriteElementString("Version", soft.version);
                       writer.WriteEndElement();
                       writer.WriteStartElement("Monitor");
                           writer.WriteElementString("ExpansionHeight", Convert.ToString(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height));
                           writer.WriteElementString("ExpansionWidth", Convert.ToString(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width));
                       writer.WriteEndElement();
                       writer.WriteStartElement("VideoCard");
                           writer.WriteElementString("AdapterRAM", videoCard.adapterRAM);
                           writer.WriteElementString("Caption", videoCard.caption);
                           writer.WriteElementString("Description", videoCard.description);
                           writer.WriteElementString("VideoProcessor", videoCard.videoProcessor);
                       writer.WriteEndElement();
                   writer.WriteStartElement("FizikalMemory");
                   writer.WriteElementString("BuildNumber", soft.buildNumber);
                       using (ManagementClass devs = new ManagementClass(@"Win32_Diskdrive"))
                       {
                           ManagementObjectCollection moc = devs.GetInstances();
                           int i = 0;
                           foreach (ManagementObject mo in moc)
                           {
                               writer.WriteStartElement("FizikalDrive" + i);
                               writer.WriteElementString("Model", mo["Model"].ToString());
                               writer.WriteElementString("SerialNumber", mo["SerialNumber"].ToString());
                               i++;
                               writer.WriteEndElement();
                           }
                       }
                   writer.WriteEndElement();
                   writer.WriteStartElement("Logic");
                       DriveInfo[] alldrivers = DriveInfo.GetDrives();
                       int j = 0;
                       string[] dr_name = Environment.GetLogicalDrives();
                       int a = dr_name.Length;
                       string[] dr_type = new string[a];
                       string[] dr_label = new string[a];
                       string[] dr_format = new string[a];
                       string[] dr_sp1 = new string[a];
                       string[] dr_sp2 = new string[a];
               
                       foreach (DriveInfo d in alldrivers)
                       {
                           writer.WriteStartElement("LogicDrive" + j);
                           writer.WriteElementString("VolumeLabel", d.DriveType.ToString());
                           if (d.IsReady == true)
                           {
                               writer.WriteElementString("VolumeLabel", dr_label[j]);
                               writer.WriteElementString("DriveFormat", d.DriveFormat);
                               writer.WriteElementString("AvailableFreeSpace", d.AvailableFreeSpace.ToString());
                               writer.WriteElementString("TotalSize", d.TotalSize.ToString());
                           }
                           else
                           {
                               writer.WriteElementString("VolumeLabel", "Unknown");
                               writer.WriteElementString("DriveFormat", "Unknown");
                               writer.WriteElementString("AvailableFreeSpace", "Unknown");
                               writer.WriteElementString("TotalSize", "Unknown");
                           }
                           j++;
                           writer.WriteEndElement();
                       }
                   writer.WriteEndElement();
                   writer.WriteEndElement();
                   writer.WriteEndDocument();
               }
            }
        }
        int NetworkAdapterSelectedIndex = 0;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            NetworkAdapterSelectedIndex = comboBox1.SelectedIndex;
            Network();
        }

    }
}
