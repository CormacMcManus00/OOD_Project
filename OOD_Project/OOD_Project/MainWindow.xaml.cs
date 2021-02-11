using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace OOD_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        //location of file where information is read from (OOD_Project\OOD_Project\bin\Debug)
        static string FileLocation = @".\processors.txt";
        //StreamReader file = new System.IO.StreamReader(FileLocation);

        public MainWindow()
        {
            InitializeComponent();
        }

        // List of platforms & manufacturers for combo boxes
        string[] platformList = { "All", "Desktop", "Server", "Mobile" };
        string[] ManufacturerList = { "All", "Intel", "AMD", "Qualcomm" };


        //List of all processors
        List<Processor> ProcessorList = new List<Processor>();

        //Collection of all processors
        ObservableCollection<Processor> ProcessorCollection = new ObservableCollection<Processor>();

        //AMD processor collections
        ObservableCollection<Processor> AMDDesktop = new ObservableCollection<Processor>();
        ObservableCollection<Processor> AMDServer = new ObservableCollection<Processor>();
        ObservableCollection<Processor> AMDMobile = new ObservableCollection<Processor>();
        ObservableCollection<Processor> AMDAll = new ObservableCollection<Processor>();

        //Intel processor collections
        ObservableCollection<Processor> IntelDesktop = new ObservableCollection<Processor>();
        ObservableCollection<Processor> IntelServer = new ObservableCollection<Processor>();
        ObservableCollection<Processor> IntelMobile = new ObservableCollection<Processor>();
        ObservableCollection<Processor> IntelAll = new ObservableCollection<Processor>();

        //Qualcomm processor collections
        ObservableCollection<Processor> QCDesktop = new ObservableCollection<Processor>();
        ObservableCollection<Processor> QCServer = new ObservableCollection<Processor>();
        ObservableCollection<Processor> QCMobile = new ObservableCollection<Processor>();
        ObservableCollection<Processor> QCAll = new ObservableCollection<Processor>();

        //Platform specific collections
        ObservableCollection<Processor> AllDesktop = new ObservableCollection<Processor>();
        ObservableCollection<Processor> AllServer = new ObservableCollection<Processor>();
        ObservableCollection<Processor> AllMobile = new ObservableCollection<Processor>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //pass arguments in following order: string manufacturer string platform, string model, string architecture, string clockspeed, int cores, int threads, string virtualization, string L1, string L2, string L3

            // sets source for combo boxes
            PlatformCBX.ItemsSource = platformList;
            ManufacturerCBX.ItemsSource = ManufacturerList;

            // sets combo box defaults to 0 ("All")
            PlatformCBX.SelectedIndex = 0;
            ManufacturerCBX.SelectedIndex = 0;

            //'using StreamReader' makes sure that once reading / writing is done, file is closed so it can be accessed agin later without receiving an access error.
            using (StreamReader str = new StreamReader(FileLocation))
            {
                // variable to store currently accessed line in streamreader
                string line;
                while ((line = str.ReadLine()) != null)
                {

                    // stores the line thats read in as an array of characters
                    char[] commentCheckList = line.ToCharArray();

                    int commentcount = 0;

                    //if the first & second characters of the line are composed of //, /*, */ or ** nothing is done with the line, this is for comments in the file
                    if ((commentCheckList[0] == '/' || commentCheckList[0] == '*') && (commentCheckList[1] == '/' || commentCheckList[1] == '*'))
                    {
                        commentcount++;
                        Console.WriteLine($"{commentcount} comment line detected");
                    }
                    // if the first and second characters are not composed of //, /*, */ or **, the line is read in, turned into an array with each
                    // element seperated with a comma. these elements are then passed to the processor constructor to create a processor object which
                    // is added to a list of processors
                    else
                    {
                        string[] processorProperties = line.Split(',');
                        ProcessorList.Add(new Processor(processorProperties[0], processorProperties[1], processorProperties[2], processorProperties[3], processorProperties[4], int.Parse(processorProperties[5]), int.Parse(processorProperties[6]), processorProperties[7], processorProperties[8], processorProperties[9], processorProperties[10]));
                    }

                }
            }
            //list of processors is sorted with iComparable method in  processor class
            ProcessorList.Sort();

            //sorted processor list is copied to an observable collection
            foreach (Processor proc in ProcessorList)
            {
                ProcessorCollection.Add(proc);
            }

            //Source of cpu listbox is set as processor collection
            CPUlbx.ItemsSource = ProcessorCollection;



            //adds cpus to collections based on their manufacturer and platform to allow filtering with combo boxes
            addProcToCollection(ProcessorCollection);
        }


        //Method to sort processors into correct collections based on platform & manufacturer.
        private void addProcToCollection(ObservableCollection<Processor> collection)
        {
            foreach (Processor cpu in collection)
            {

                /*********************************** AMD *****************************************/

                //
                if (cpu.Manufacturer.ToUpper() == "AMD" && cpu.Platform.ToUpper() == "DESKTOP")
                {
                    AMDDesktop.Add(cpu as Processor);
                }
                //
                if (cpu.Manufacturer.ToUpper() == "AMD" && cpu.Platform.ToUpper() == "SERVER")
                {
                    AMDServer.Add(cpu as Processor);
                }
                //
                if (cpu.Manufacturer.ToUpper() == "AMD" && cpu.Platform.ToUpper() == "MOBILE")
                {
                    AMDMobile.Add(cpu as Processor);
                }
                //
                if (cpu.Manufacturer.ToUpper() == "AMD")
                {
                    AMDAll.Add(cpu as Processor);
                }


                /*********************************** INTEL *****************************************/

                //
                if (cpu.Manufacturer.ToUpper() == "INTEL" && cpu.Platform.ToUpper() == "DESKTOP")
                {
                    IntelDesktop.Add(cpu as Processor);
                }
                //
                if (cpu.Manufacturer.ToUpper() == "INTEL" && cpu.Platform.ToUpper() == "SERVER")
                {
                    IntelServer.Add(cpu as Processor);
                }
                //
                if (cpu.Manufacturer.ToUpper() == "INTEL" && cpu.Platform.ToUpper() == "MOBILE")
                {
                    IntelMobile.Add(cpu as Processor);
                }
                //
                if (cpu.Manufacturer.ToUpper() == "INTEL")
                {
                    IntelAll.Add(cpu as Processor);
                }

                /*********************************** QUALCOMM *****************************************/


                if (cpu.Manufacturer.ToUpper() == "QUALCOMM" && cpu.Platform.ToUpper() == "DESKTOP")
                {
                    QCDesktop.Add(cpu as Processor);
                }
                //
                if (cpu.Manufacturer.ToUpper() == "QUALCOMM" && cpu.Platform.ToUpper() == "SERVER")
                {
                    QCServer.Add(cpu as Processor);
                }
                //
                if (cpu.Manufacturer.ToUpper() == "QUALCOMM" && cpu.Platform.ToUpper() == "MOBILE")
                {
                    QCMobile.Add(cpu as Processor);
                }
                //
                if (cpu.Manufacturer.ToUpper() == "QUALCOMM")
                {
                    QCAll.Add(cpu as Processor);
                }


                /*********************************** ALL DESKTOP SERVER & MOBILE *****************************************/

                if (cpu.Platform.ToUpper() == "DESKTOP")
                {
                    AllDesktop.Add(cpu as Processor);
                }
                //
                if (cpu.Platform.ToUpper() == "SERVER")
                {
                    AllServer.Add(cpu as Processor);
                }
                //
                if (cpu.Platform.ToUpper() == "MOBILE")
                {
                    AllMobile.Add(cpu as Processor);
                }

            }
        }

        private void CPUlbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Processor selectedProcessor = CPUlbx.SelectedItem as Processor;

            //populates textboxes with relevant information from selected processor
            if (CPUlbx.SelectedItem != null)
            {
                platformTBX.Text = selectedProcessor.Platform;
                ManufacturerTBX.Text = selectedProcessor.Manufacturer;
                modelTBX.Text = selectedProcessor.Model;
                architectureTBX.Text = selectedProcessor.Architecture;
                clockspeedTBX.Text = selectedProcessor.ClockSpeed;
                coresTBX.Text = selectedProcessor.Cores.ToString();
                threadsTBX.Text = selectedProcessor.Threads.ToString();
                virtualizationTBX.Text = selectedProcessor.Virtualization;
                l1TBX.Text = selectedProcessor.L1Cache;
                l2TBX.Text = selectedProcessor.L2Cache;
                l3TBX.Text = selectedProcessor.L3Cache;
            }
        }



        private void ListboxSet()
        {
            string manufacturerCBXSelection = ManufacturerCBX.SelectedItem as string;
            string platformCBXSelection = PlatformCBX.SelectedItem as string;

            //sets CPU listbox source to relevant collection based on combo box selection

            // Manufacturer = "All" & Platform = "All"
            if (manufacturerCBXSelection == "All" && platformCBXSelection == "All")
            {
                CPUlbx.ItemsSource = ProcessorCollection;
            }


            // Manufacturer = "All" cases
            else if (manufacturerCBXSelection == "All" && platformCBXSelection == "Desktop")
            {
                CPUlbx.ItemsSource = AllDesktop;
            }
            else if (manufacturerCBXSelection == "All" && platformCBXSelection == "Server")
            {
                CPUlbx.ItemsSource = AllServer;
            }
            else if (manufacturerCBXSelection == "All" && platformCBXSelection == "Mobile")
            {
                CPUlbx.ItemsSource = AllMobile;
            }


            // Manufacturer = "AMD" cases
            else if (manufacturerCBXSelection == "AMD" && platformCBXSelection == "Desktop")
            {
                CPUlbx.ItemsSource = AMDDesktop;
            }
            else if (manufacturerCBXSelection == "AMD" && platformCBXSelection == "Server")
            {
                CPUlbx.ItemsSource = AMDServer;
            }
            else if (manufacturerCBXSelection == "AMD" && platformCBXSelection == "Mobile")
            {
                CPUlbx.ItemsSource = AMDMobile;
            }
            else if (manufacturerCBXSelection == "AMD" && platformCBXSelection == "All")
            {
                CPUlbx.ItemsSource = AMDAll;
            }

            // Manufacturer = "Intel" cases
            else if (manufacturerCBXSelection == "Intel" && platformCBXSelection == "Desktop")
            {
                CPUlbx.ItemsSource = IntelDesktop;
            }
            else if (manufacturerCBXSelection == "Intel" && platformCBXSelection == "Server")
            {
                CPUlbx.ItemsSource = IntelServer;
            }
            else if (manufacturerCBXSelection == "Intel" && platformCBXSelection == "Mobile")
            {
                CPUlbx.ItemsSource = IntelMobile;
            }
            else if (manufacturerCBXSelection == "Intel" && platformCBXSelection == "All")
            {
                CPUlbx.ItemsSource = IntelAll;
            }

            // Manufacturer = "Qualcomm" cases
            else if (manufacturerCBXSelection == "Qualcomm" && platformCBXSelection == "Desktop")
            {
                CPUlbx.ItemsSource = QCDesktop;
            }
            else if (manufacturerCBXSelection == "Qualcomm" && platformCBXSelection == "Server")
            {
                CPUlbx.ItemsSource = QCServer;
            }
            else if (manufacturerCBXSelection == "Qualcomm" && platformCBXSelection == "Mobile")
            {
                CPUlbx.ItemsSource = QCMobile;
            }
            else if (manufacturerCBXSelection == "Qualcomm" && platformCBXSelection == "All")
            {
                CPUlbx.ItemsSource = QCAll;
            }
        }

        private void ManufacturerCBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //sets list source depending on selections in manufacturer combo box and platform combo box
            ListboxSet();
        }

        private void PlatformCBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //sets list source depending on selections in manufacturer combo box and platform combo box
            ListboxSet();
        }

        private void WriteFileBTN_Click(object sender, RoutedEventArgs e)
        {
            //gets custom path from text box
            string customPath = custompathTBX.Text;

            try
            {
                using (StreamWriter mySW = new StreamWriter(customPath))
                {
                    // if the file does not already exist it is created
                    if (File.Exists(customPath) == false)
                    {
                        File.CreateText(customPath);
                    }

                    mySW.WriteLine($"***************************************");
                    mySW.WriteLine($"*                                     *");
                    mySW.WriteLine($"*        PROCESSOR INFORMATION        *");
                    mySW.WriteLine($"*             OUTPUT FILE             *");
                    mySW.WriteLine($"*                                     *");
                    mySW.WriteLine($"*        MADE BY CORMAC MCMANUS       *");
                    mySW.WriteLine($"*               S00197425             *");
                    mySW.WriteLine($"*                                     *");
                    mySW.WriteLine($"***************************************");
                    mySW.WriteLine($"");

                    foreach (Processor cpu in ProcessorCollection)
                    {
                        mySW.WriteLine($"***************************************");
                        mySW.WriteLine($"{cpu.Manufacturer} {cpu.Model}");
                        mySW.WriteLine($"***************************************");
                        mySW.WriteLine($"Platform: {cpu.Platform}");
                        mySW.WriteLine($"Architecture: {cpu.Architecture}");
                        mySW.WriteLine($"Clock speed: {cpu.ClockSpeed}");
                        mySW.WriteLine($"Cores: {cpu.Cores.ToString()}");
                        mySW.WriteLine($"Threads: {cpu.Threads.ToString()}");
                        mySW.WriteLine($"Virtualization Technology: {cpu.Virtualization}");
                        mySW.WriteLine($"L1 Cache: {cpu.L1Cache}");
                        mySW.WriteLine($"L2 Cache: {cpu.L2Cache}");
                        mySW.WriteLine($"L3 Cache: {cpu.L3Cache}");
                        mySW.WriteLine($"***************************************");
                        mySW.WriteLine($"");
                    }

                }


            }
            // manage unauthorized access exception
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(@"Unauthorised access exception, make sure your path ends with ...\filename.txt", "UNAUTHORIZED ACCESS", MessageBoxButton.OK);
            }
        }

        private void addNewCPUBTN_Click(object sender, RoutedEventArgs e)
        {

            //get values of each field
            string newProcManufacturer = ManufacturerTBX.Text;
            string newProcModel = modelTBX.Text;
            string newProcPlatform = platformTBX.Text;
            string newProcArchitecture = architectureTBX.Text;
            string newProcClockSpeed = clockspeedTBX.Text;
            int newProcCores = int.Parse(coresTBX.Text);
            int newProcThreads = int.Parse(threadsTBX.Text);
            string newProcVirtualization = virtualizationTBX.Text;
            string newProcL1 = l1TBX.Text;
            string newProcL2 = l2TBX.Text;
            string newProcL3 = l3TBX.Text;

            //pass arguments in following order: string manufacturer string platform, string model, string architecture, string clockspeed, int cores, int threads, string virtualization, string L1, string L2, string L3

            ProcessorCollection.Add(new Processor(newProcManufacturer, newProcPlatform, newProcModel, newProcArchitecture, newProcClockSpeed, newProcCores, newProcThreads, newProcVirtualization, newProcL1, newProcL2, newProcL3));
            //add to a collection so it can be passed to method for sorting into correct collections.
            ObservableCollection<Processor> newProcs = new ObservableCollection<Processor>();
            addProcToCollection(newProcs);
            //clear newProc collection so we can add more new processors without the previous one getting added to the list again.
            newProcs.Clear();

            //Write details of new processor to file.
            using (StreamWriter SW = new StreamWriter(FileLocation, true))
            {
                SW.WriteLine($"{newProcManufacturer},{newProcPlatform},{newProcModel},{newProcArchitecture},{newProcClockSpeed},{newProcCores},{newProcThreads},{newProcVirtualization},{newProcL1},{newProcL2},{newProcL3}");
            }

        }

        private void removeBTN_Click(object sender, RoutedEventArgs e)
        {

        }


        private void updateBTN_Click(object sender, RoutedEventArgs e)
        {
            // Start by geting the currently selected processor
            Processor selectedProcessor = CPUlbx.SelectedItem as Processor;

            // If the selection isn't empty, find & replace procedure continues
            if (selectedProcessor != null)
            {
                // get details of changes that need to be written
                Processor replacementProcessor = new Processor(ManufacturerTBX.Text, platformTBX.Text, modelTBX.Text, architectureTBX.Text, clockspeedTBX.Text, int.Parse(coresTBX.Text), int.Parse(threadsTBX.Text), virtualizationTBX.Text, l1TBX.Text, l2TBX.Text, l3TBX.Text);
                // find index of file to be written
                int index = CPUlbx.Items.IndexOf(selectedProcessor);
                //replace old processor with new in collection
                ProcessorCollection[index] = replacementProcessor;

                //clear file contents
                File.WriteAllText(FileLocation, string.Empty);

                using (StreamWriter sw = new StreamWriter(FileLocation, true))
                {

                    //write comments to file
                    sw.WriteLine("// Add new processors in the format:");
                    sw.WriteLine("// Manufacturer,Platform,Model,Architecture,Clock Speed,Cores,Threads,Virtualoization,L1 Cache,L2 Cache,L3 Cache");
                    sw.WriteLine("//");


                    foreach (Processor cpu in ProcessorCollection)
                    {
                        sw.WriteLine(cpu.FileWriteString());
                        Console.WriteLine(cpu.FileWriteString());
                    }
                }
            }
        }
    }
}
