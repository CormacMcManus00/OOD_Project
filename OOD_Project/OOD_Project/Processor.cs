using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    class Processor :IComparable<Processor>
    {
        //processor properties

        public string Manufacturer { get; set; }
        public string Platform { get; set; }
        public string Model { get; set; }
        public string Architecture { get; set; }
        public string ClockSpeed { get; set; }
        public int Cores { get; set; }
        public int Threads { get; set; }
        public string Virtualization { get; set; }
        public string L1Cache { get; set; }
        public string L2Cache { get; set; }
        public string L3Cache { get; set; }

        //processor constructor
        public Processor(string manufacturer, string platform, string model, string architecture, string clockspeed, int cores, int threads, string virtualization, string L1, string L2, string L3)
        {
            Manufacturer = manufacturer;
            Platform = platform;
            Model = model;
            Architecture = architecture;
            ClockSpeed = clockspeed;
            Cores = cores;
            Threads = threads;
            Virtualization = virtualization;
            L1Cache = L1;
            L2Cache = L2;
            L3Cache = L3;
        }

        //default values
        public Processor() : this("unknown","unknown", "unknown", "unknown", "unknown", 0, 0, "unknown", "unknown", "unknown", "unknown") { }

        public int CompareTo(Processor other)
        {
            return this.Model.CompareTo(other.Model);
        }

        //ToString override
        public override string ToString()
        {
            return string.Format($"{Manufacturer} - {Model} CPU");
        }

        public string FileWriteString()
        {
            string newLine = $"{Manufacturer},{Platform},{Model},{Architecture},{ClockSpeed},{Cores},{Threads},{Virtualization},{L1Cache},{L2Cache},{L3Cache}";

            return newLine;
        }
    }

    /***************************** FOR ABSTRACT PROCESSOR CLASS *******************************
    class AMDProcessor : Processor 
    {
        public AMDProcessor(string platform, string model, string architecture, string clockspeed, int cores, int threads, string virtualization, string L1, string L2, string L3)
        {
            Platform = platform;
            Model = model;
            Architecture = architecture;
            ClockSpeed = clockspeed;
            Cores = cores;
            Threads = threads;
            Virtualization = virtualization;
            L1Cache = L1;
            L2Cache = L2;
            L3Cache = L3;
        }

        public override string ToString()
        {
            return string.Format($"AMD {Model.ToUpper()}");
        }
    }

    class IntelProcessor : Processor
    {
        public IntelProcessor(string platform, string model, string architecture, string clockspeed, int cores, int threads, string virtualization, string L1, string L2, string L3)
        {
            platform = Platform;
            model = Model;
            architecture = Architecture;
            clockspeed = ClockSpeed;
            cores = Cores;
            threads = Threads;
            virtualization = Virtualization;
            L1 = L1Cache;
            L2 = L2Cache;
            L3 = L3Cache;
        }

        public override string ToString()
        {
            return string.Format($"Intel {Model}");
        }
    }

    class QualcommProcessor : Processor
    {
        public QualcommProcessor(string platform, string model, string architecture, string clockspeed, int cores, int threads, string virtualization, string L1, string L2, string L3)
        {
            platform = Platform;
            model = Model;
            architecture = Architecture;
            clockspeed = ClockSpeed;
            cores = Cores;
            threads = Threads;
            virtualization = Virtualization;
            L1 = L1Cache;
            L2 = L2Cache;
            L3 = L3Cache;
        }

        public override string ToString()
        {
            return string.Format($"Qualcomm {Model}");
        }
    }
    ************************************************************/

}
