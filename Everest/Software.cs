using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Everest
{
    public class Software
    {
        public string caption;
        public string name;
        public string ostype;
        public string registeredUser;
        public string serialNumber;
        public string version;
        public string freeFizikalMemory;
        public string freeVirtualMemory;
        public string systemDrive;
        public string systemDirectory;
        public string buildNumber;
        public string IP;

        ManagementObjectSearcher searcher5 =
            new ManagementObjectSearcher("root\\CIMV2",
                "SELECT * FROM Win32_OperatingSystem");

        public Software() 
        {
            string host = System.Net.Dns.GetHostName();
            this.IP = Convert.ToString(  System.Net.Dns.GetHostByName(host).AddressList[0]);
            foreach (ManagementObject queryObj in searcher5.Get())
            {

                this.caption = Convert.ToString(queryObj["Caption"]);
                this.name = Convert.ToString(queryObj["Name"]);
                this.ostype = Convert.ToString(queryObj["OSType"]);
                this.registeredUser = Convert.ToString(queryObj["RegisteredUser"]);
                this.serialNumber = Convert.ToString(queryObj["SerialNumber"]);
                this.version = Convert.ToString(queryObj["Version"]);
                this.freeFizikalMemory = Convert.ToString(queryObj["FreePhysicalMemory"]);
                this.freeVirtualMemory = Convert.ToString(queryObj["FreeVirtualMemory"]);
                this.systemDrive = Convert.ToString(queryObj["SystemDrive"]);
                this.systemDirectory = Convert.ToString(queryObj["SystemDirectory"]);
                this.buildNumber = Convert.ToString(queryObj["BuildNumber"]);
            }
        }
    }
}
