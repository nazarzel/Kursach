using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Everest
{
    class RAM
    {
        ManagementObjectSearcher searcher12 =
            new ManagementObjectSearcher("root\\CIMV2",
            "SELECT * FROM Win32_PhysicalMemory");

        public string bankLabel;
        public string capacity;
        public string speed;

        public RAM()
        {
            foreach (ManagementObject queryObj in searcher12.Get())
            {
                this.bankLabel = Convert.ToString(queryObj["BankLabel"]);
                this.capacity = Convert.ToString(Math.Round(System.Convert.ToDouble(queryObj["Capacity"]) / 1024 / 1024 / 1024, 2));
                this.speed = Convert.ToString(queryObj["Speed"]);
            }
        }
    }
}
