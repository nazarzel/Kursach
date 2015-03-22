using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Everest
{
    class Processor
    {
        public string name;
        public string numberOfCores;
        public string processorId;
        public string Caption;
        public string Description;
        public string L2CacheSize;
        public string Manufacturer;
        public string Revision;


        ManagementObjectSearcher searcher8 =
            new ManagementObjectSearcher("root\\CIMV2",
            "SELECT * FROM Win32_Processor");
  
        public Processor()
        {
            foreach (ManagementObject queryObj in searcher8.Get())
            {
                this.name = Convert.ToString( queryObj["Name"]);
                this.numberOfCores = Convert.ToString(queryObj["NumberOfCores"]);
                this.processorId = Convert.ToString(queryObj["ProcessorId"]);
            }
            ObjectQuery ProcessorQuery = new System.Management.ObjectQuery("select Caption, Description, L2CacheSize, Manufacturer, Revision from Win32_Processor where ProcessorType = 3");
            ManagementObjectSearcher ProcessorSearcher = new ManagementObjectSearcher(ProcessorQuery);
            ManagementObjectCollection ProcessorCollection = ProcessorSearcher.Get();
            foreach (ManagementObject ProcessorInfo in ProcessorCollection)
            {
                this.Caption = ProcessorInfo["Caption"].ToString();
                this.Description = ProcessorInfo["Description"].ToString();
                this.L2CacheSize = ProcessorInfo["L2CacheSize"].ToString();
                this.Manufacturer = ProcessorInfo["Manufacturer"].ToString();
                this.Revision = ProcessorInfo["Revision"].ToString();
            }
        }

    }
}
