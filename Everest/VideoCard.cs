using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Everest
{
    class VideoCard
    {
        ManagementObjectSearcher searcher11 =
            new ManagementObjectSearcher("root\\CIMV2",
            "SELECT * FROM Win32_VideoController");

        public string adapterRAM;
        public string caption;
        public string description;
        public string videoProcessor;

        public VideoCard()
        {
            foreach (ManagementObject queryObj in searcher11.Get())
            {
                this.adapterRAM = Convert.ToString(queryObj["AdapterRAM"]);
                this.caption = Convert.ToString(queryObj["Caption"]);
                this.description = Convert.ToString(queryObj["Description"]);
                this.videoProcessor = Convert.ToString(queryObj["VideoProcessor"]);
            }
        }
    }
}
