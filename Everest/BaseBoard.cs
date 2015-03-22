using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Everest
{
    class BaseBoard
    {
        ManagementObjectSearcher searcher12 =
            new ManagementObjectSearcher("root\\CIMV2",
            "SELECT * FROM Win32_BaseBoard");

        public string manufacturer;
        public string product;
        public string serialNumber;
        public string version;

        public BaseBoard()
        {
            foreach (ManagementObject queryObj in searcher12.Get())
            {
                this.manufacturer = Convert.ToString(queryObj["Manufacturer"]);
                this.product = Convert.ToString(queryObj["Product"]);
                this.serialNumber = Convert.ToString(queryObj["SerialNumber"]);
                this.version = Convert.ToString(queryObj["Version"]);

            } 
        }
    }
}
