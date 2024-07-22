using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TesterProgram
{
    public class MainViewModel : VMBase
    {
        private string str_A_1 = @"D:\PPIWAFER2_Wafer_1\";
        private string str_A_2 = "2";
        private string str_A_3 = "PPIWAFER2_Wafer_"; //+"str_A_2"+@"\"
        private int int_A_1 = 5;
        private ICommand cMD_A_1;
        private string str_B_1 = @"D:\PPIWAFER2_Wafer_";
        private string str_B_2 = "1,2,3,4,5";
        private string str_B_3;
        private ICommand cMD_B_1;
        private string str_C_1 = "[START]LOT_ID=30018A,VISION_RECIPE_ID=PPIWAFER2,CASSETTE_ID=CAS01,WAFER_ID=1,SLOT_ID=1,TOTAL_SLOT=25";
        private string str_C_2 = "1,2,3,4,5";
        private string str_C_3;
        private ICommand cMD_C_1;
        private string str_D_1 = @"D:\offline_image";
        private string str_D_2 = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25";
        private string str_D_3 = @"D:\ToVisionProcessor";
        private string str_D_4 = "s";
        private ICommand cMD_D_1;
        private ICommand cMD_D_2;
        private ICommand cMD_E_1;
        private ICommand cMD_F_1;
        private string str_G_1 = @"E:\offline_image";
        private string str_G_2 = @"E:\ToVisionProcessor";
        private string str_G_3 = "1";
        private string str_G_4 = "ms";
        private ICommand cMD_G_1;
        private ICommand cMD_G_2;
        private ICommand cMD_Log_1;
        private ObservableCollection<string> _runLog = new ObservableCollection<string>();
        private string str_H_1 = @"E:\offline_xml";
        private string str_H_2 = @"E:\dest_xml";
        private string str_H_3 = "1";
        private string str_H_4 = "ms";
        private ICommand cMD_H_1;
        private ICommand cMD_H_2;
        private ICommand cMD_H_3;
        //add public member here below

        private bool startcopy = false;
        public MainViewModel()
        {
        }
        // Method to start a new task
        void StartTask()
        {
            Task tsk = Task.Run(() =>
            {
                string[] splitted_str_D_2 = str_D_2.Trim().Split(new char[] { ',' });
                int index = 0;
                // Check if cancellation has been requested
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Task cancellation requested. Task.Run");
                    //cts.Token.ThrowIfCancellationRequested();
                }

                foreach (var waferID in splitted_str_D_2)
                {
                    // Check if cancellation has been requested
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Task cancellation requested. splitted_str_D_2");
                        //cts.Token.ThrowIfCancellationRequested();
                    }

                    index++;
                    int msTimeout = index * 10000;
                    var result = int.TryParse(str_D_4, out int mstimeout);
                    Thread.Sleep(mstimeout);
                    try
                    {
                        string foldername = str_D_1; // @"D:\PPIDUMMY"
                        DirectoryInfo Dir = new DirectoryInfo(foldername);
                        FileInfo[] FI = Dir.GetFiles("*.tif");

                        //Task.Run(() =>
                        //{
                        DirectoryInfo Dir_PPIDUMMY = new DirectoryInfo(str_D_1);
                        FileInfo[] FI_PPIDUMMY = Dir_PPIDUMMY.GetFiles("*.tif");

                        foreach (var item in FI)
                        {
                            // Check if cancellation has been requested
                            if (token.IsCancellationRequested)
                            {
                                Console.WriteLine("Task cancellation requested. FI");
                                cts.Token.ThrowIfCancellationRequested();
                            }

                            string input = "CAS01_anything_1_25";
                            string pattern = @"CAS01_\w+_1_25";
                            string sourceFileName = item.FullName;//3_18A_PPIWAFER2_CAS01_1_1_25_[]_[]
                            string sourceFolderName = Path.GetFileName(Path.GetDirectoryName(sourceFileName));
                            string sourcedir = Path.GetDirectoryName(sourceFileName);
                            string destFileName = item.FullName.Replace("CAS01_1_1_25", "CAS01_" + waferID + "_1_25").Replace(sourcedir, str_D_3);

                            if (!Directory.Exists(System.IO.Path.GetDirectoryName(destFileName)))
                                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destFileName));

                            if (!File.Exists(destFileName))
                            {
                                File.Copy(sourceFileName, destFileName);
                            }
                            else
                            {
                                //File.Delete(destFileName);
                                //File.Copy(sourceFileName, destFileName);
                            }
                        }
                        //});
                    }
                    catch (Exception ex)
                    {
                        string logfilepath = @"D:\" + "TesterLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; Exception; CMD_D_1; {ex}" + "\n");
                    }
                }
            }, token);

            // Optionally handle task completion
            tsk.ContinueWith(t =>
            {
                if (t.IsCanceled)
                {
                    Console.WriteLine("The task was cancelled.");
                }
                else if (t.IsFaulted)
                {
                    Console.WriteLine("The task faulted.");
                }
                else
                {
                    Console.WriteLine("The task completed successfully.");
                }
            });
        }

        public string Str_A_1
        {
            get => str_A_1; set
            {
                str_A_1 = value;
                OnPropertyChanged(nameof(Str_A_1));
            }
        }
        public string Str_A_2
        {
            get => str_A_2; set
            {
                str_A_2 = value;
                OnPropertyChanged(nameof(Str_A_2));
            }
        }
        public string Str_A_3
        {
            get => str_A_3; set
            {
                str_A_3 = value;
                OnPropertyChanged(nameof(Str_A_3));
            }
        }

        public int Int_A_1
        {
            get { return int_A_1; }
            set
            {
                int_A_1 = value;
                OnPropertyChanged(nameof(Int_A_1));
            }
        }

        public ICommand CMD_A_1
        {
            get
            {
                return cMD_A_1 ?? (cMD_A_1 = new RelayCommand<object>(param =>
                {
                    try
                    {

                        Task.Run(() =>
                        {
                            DirectoryInfo Dir = new DirectoryInfo(str_A_1);
                            FileInfo[] FI = Dir.GetFiles("*.tif");

                            foreach (var item in FI)
                            {
                                string sourceFileName = item.FullName;//3_18A_PPIWAFER2_CAS01_1_1_25_[]_[]
                                string sourceFolderName = Path.GetFileName(Path.GetDirectoryName(sourceFileName));
                                string destFileName = item.FullName.Replace("CAS01_1_1_25", "CAS01_" + str_A_2 + "_1_25").Replace(sourceFolderName, str_A_3 + str_A_2);

                                if (!Directory.Exists(System.IO.Path.GetDirectoryName(destFileName)))
                                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destFileName));

                                if (!File.Exists(destFileName))
                                {
                                    File.Copy(sourceFileName, destFileName);
                                }
                                else
                                {
                                    //File.Delete(destFileName);
                                    //File.Copy(sourceFileName, destFileName);
                                }
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        string logfilepath = @"D:\" + "TesterLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; Exception; CMD_A_1; {ex}");
                    }
                }));
            }
            set
            {
                cMD_A_1 = value;
            }
        }

        public string Str_B_1
        {
            get { return str_B_1; }
            set
            {
                str_B_1 = value;
                OnPropertyChanged(nameof(Str_B_1));
            }
        }
        public string Str_B_2
        {
            get => str_B_2; set
            {
                str_B_2 = value;
                OnPropertyChanged(nameof(Str_B_2));
            }
        }
        public string Str_B_3
        {
            get => str_B_3; set
            {
                str_B_3 = value;
                OnPropertyChanged(nameof(Str_B_3));
            }
        }

        private List<FileInfo> ImageList = new List<FileInfo>();
        public ICommand CMD_B_1
        {
            get
            {
                return cMD_B_1 ?? (cMD_B_1 = new RelayCommand<object>(param =>
                {
                    try
                    {
                        string[] splitted_str_B_2 = str_B_2.Trim().Split(new char[] { ',' });

                        foreach (var waferID in splitted_str_B_2)
                        {
                            try
                            {
                                string foldername = str_B_1 + waferID; // @"D:\PPIWAFER2_Wafer_" + waferID
                                DirectoryInfo Dir = new DirectoryInfo(foldername);
                                FileInfo[] FI = Dir.GetFiles("*.tif");

                                foreach (var tif in FI)
                                {
                                    lock (ImageList)
                                    {
                                        if (ImageList.All(e => e.FullName != tif.FullName))
                                        {
                                            ImageList.Add(tif);

                                            string logfilepath = @"D:\" + "TesterLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                                            File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; ImageList; Add; " + tif.Name + "\n");
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                string logfilepath = @"D:\" + "TesterLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                                File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; Exception; CMD_B_1; {ex}" + "\n");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string logfilepath = @"D:\" + "TesterLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; Exception; CMD_B_1; {ex}" + "\n");
                    }
                }));
            }
            set
            {
                cMD_B_1 = value;
            }
        }

        public string Str_C_1
        {
            get { return str_C_1; }
            set
            {
                str_C_1 = value;
                OnPropertyChanged(nameof(Str_C_1));
            }
        }
        public string Str_C_2
        {
            get => str_C_2; set
            {
                str_C_2 = value;
                OnPropertyChanged(nameof(Str_C_2));
            }
        }
        public string Str_C_3
        {
            get => str_C_3; set
            {
                str_C_3 = value;
                OnPropertyChanged(nameof(Str_C_3));
            }
        }

        private Queue<string> WaferID_Queue = new Queue<string>();
        public ICommand CMD_C_1
        {
            get
            {
                return cMD_C_1 ?? (cMD_C_1 = new RelayCommand<object>(param =>
                {
                    try
                    {
                        string[] splitted_str_C_2 = str_C_2.Trim().Split(new char[] { ',' });

                        foreach (var sWaferID in splitted_str_C_2)
                        {
                            try
                            {

                                string wafercommand = "WAFER_ID=" + sWaferID;
                                string TCPCommand = str_C_1.Replace("WAFER_ID=1", wafercommand);
                                string[] splittedTCPCommand = TCPCommand.Trim().Split(new char[] { '=', ',' });
                                string lotID = ""; string recipeID = ""; string cassetteID = ""; string waferID = ""; string slotID = ""; string totalSlot = "";

                                if (splittedTCPCommand[0].ToUpper() == "[START]LOT_ID")
                                    lotID = splittedTCPCommand[1].Trim();
                                if (splittedTCPCommand[2]/*.ToUpper()*/ == "VISION_RECIPE_ID")
                                    recipeID = splittedTCPCommand[3].Trim();
                                if (splittedTCPCommand[4].ToUpper() == "CASSETTE_ID")
                                    cassetteID = splittedTCPCommand[5].Trim();
                                if (splittedTCPCommand[6].ToUpper() == "WAFER_ID")
                                    waferID = splittedTCPCommand[7].Trim();
                                if (splittedTCPCommand[8].ToUpper() == "SLOT_ID")
                                    slotID = splittedTCPCommand[9].Trim();
                                if (splittedTCPCommand[10].ToUpper() == "TOTAL_SLOT")
                                    totalSlot = splittedTCPCommand[11].Trim();

                                string sWaferInfo = string.Format("{0}_{1}_{2}_{3}_{4}_{5}", lotID, recipeID, cassetteID, waferID, slotID, totalSlot);

                                lock (WaferID_Queue)//Get all incoming commands queued into "WaferID_Queue" Queue<string>
                                {
                                    if (WaferID_Queue.All(Command => Command != sWaferInfo))
                                    {
                                        WaferID_Queue.Enqueue(sWaferID);

                                        string logfilepath = @"D:\" + "TesterLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; WaferID_Queue; Enqueue; " + sWaferInfo + "\n");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                string logfilepath = @"D:\" + "TesterLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                                File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; Exception; CMD_C_1; {ex}" + "\n");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string logfilepath = @"D:\" + "TesterLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; Exception; CMD_C_1; {ex}" + "\n");
                    }
                }));
            }
            set
            {
                cMD_C_1 = value;
            }
        }


        public string Str_D_1
        {
            get { return str_D_1; }
            set
            {
                str_D_1 = value;
                OnPropertyChanged(nameof(Str_D_1));
            }
        }
        public string Str_D_2
        {
            get => str_D_2; set
            {
                str_D_2 = value;
                OnPropertyChanged(nameof(Str_D_2));
            }
        }
        public string Str_D_3
        {
            get => str_D_3; set
            {
                str_D_3 = value;
                OnPropertyChanged(nameof(Str_D_3));
            }
        }
        public string Str_D_4
        {
            get => str_D_4; set
            {
                str_D_4 = value;
                OnPropertyChanged(nameof(Str_D_4));
            }
        }

        Task tsk;
        // Create a CancellationTokenSource
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken token;

        public ICommand CMD_D_1
        {
            get
            {
                return cMD_D_1 ?? (cMD_D_1 = new RelayCommand<object>(param =>
                {
                    try
                    {
                        cts = new CancellationTokenSource(); // Create a new CancellationTokenSource
                        token = cts.Token; // Get the new token
                        StartTask(); // Start a new task with the new token
                    }
                    catch (Exception ex)
                    {
                        string logfilepath = @"D:\" + "TesterLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; Exception; CMD_D_1; {ex}" + "\n");
                    }
                }));
            }
            set
            {
                cMD_D_1 = value;
            }
        }
        public ICommand CMD_D_2
        {
            get
            {
                return cMD_D_2 ?? (cMD_D_2 = new RelayCommand<object>(param =>
                {
                    // Request task cancellation
                    Console.WriteLine("Requesting task cancellation...");
                    cts.Cancel();

                }));
            }
            set
            {
                cMD_D_2 = value;
            }
        }
        public ICommand CMD_E_1
        {
            get
            {
                return cMD_E_1 ?? (cMD_E_1 = new RelayCommand<object>(param =>
                {
                    try
                    {

                        DateTime comparisonValue = DateTime.MaxValue;

                        DateTime dateTime1 = DateTime.Now;
                        DateTime dateTime2 = DateTime.Now;

                        // Get the current DateTime
                        DateTime currentDateTime = DateTime.Now;

                        // Convert DateTime to string
                        string dateTimeString1 = "2024/05/17 12:50:59.500";//currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        string dateTimeString2 = "2024/05/17 12:50:59.373";//currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                        // Output the string representation
                        Console.WriteLine($"DateTime1 as string: {dateTimeString1}; DateTime2 as string: {dateTimeString2}");

                        // Convert string back to DateTime
                        if (DateTime.TryParse(dateTimeString1, out DateTime parsedDateTime))
                        {
                            // Output the parsed DateTime
                            Console.WriteLine($"Parsed DateTime: {parsedDateTime}");
                            dateTime1 = parsedDateTime;
                        }
                        else
                        {
                            Console.WriteLine("Invalid DateTime1 string format.");
                        }

                        // Convert string back to DateTime
                        if (DateTime.TryParse(dateTimeString2, out DateTime _parsedDateTime))
                        {
                            // Output the parsed DateTime
                            Console.WriteLine($"Parsed DateTime: {_parsedDateTime}");
                            dateTime2 = _parsedDateTime;
                        }
                        else
                        {
                            Console.WriteLine("Invalid DateTime2 string format.");
                        }


                        if (dateTime1 < comparisonValue)
                        {
                            comparisonValue = dateTime1;
                            // dateTime1 comes before dateTime2
                        }
                        else if (dateTime1 > dateTime2)
                        {
                            // dateTime1 comes after dateTime2
                        }
                        else
                        {
                            // dateTime1 and dateTime2 are equal
                        }

                        int comparisonResult = dateTime1.CompareTo(dateTime2);

                        if (comparisonResult < 0)
                        {
                            // dateTime1 comes before dateTime2
                        }
                        else if (comparisonResult > 0)
                        {
                            // dateTime1 comes after dateTime2
                        }
                        else
                        {
                            // dateTime1 and dateTime2 are equal
                        }
                    }
                    catch (Exception ex)
                    {
                        string logfilepath = @"D:\" + "TesterLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; Exception; CMD_E_1; {ex}" + "\n");
                    }
                }));
            }
            set
            {
                cMD_E_1 = value;
            }
        }

        public ICommand CMD_F_1
        {
            get
            {
                return cMD_F_1 ?? (cMD_F_1 = new RelayCommand<object>(param =>
                {
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        string logfilepath = @"D:\" + "TesterLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; Exception; CMD_F_1; {ex}" + "\n");
                    }
                }));
            }
            set
            {
                cMD_F_1 = value;
            }
        }

        public ICommand CMD_Log_1
        {
            get
            {
                return cMD_Log_1 ?? (cMD_Log_1 = new RelayCommand<object>(param =>
                {
                    try
                    {
                        RunLog.Clear();
                    }
                    catch (Exception ex)
                    {
                        string logfilepath = @"D:\" + "TesterLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; Exception; CMD_Log_1; {ex}" + "\n");
                    }
                }));
            }
            set
            {
                cMD_Log_1 = value;
            }
        }
        public ObservableCollection<string> RunLog
        {
            get { return _runLog; }
            set
            {
                _runLog = value;
            }
        }

        public string Str_G_1
        {
            get { return str_G_1; }
            set
            {
                str_G_1 = value;
                OnPropertyChanged(nameof(Str_G_1));
            }
        }
        public string Str_G_2
        {
            get => str_G_2; set
            {
                str_G_2 = value;
                OnPropertyChanged(nameof(Str_G_2));
            }
        }
        public string Str_G_3
        {
            get => str_G_3; set
            {
                str_G_3 = value;
                OnPropertyChanged(nameof(Str_G_3));
            }
        }
        public string Str_G_4
        {
            get => str_G_4; set
            {
                str_G_4 = value;
                OnPropertyChanged(nameof(Str_G_4));
            }
        }

        CancellationTokenSource cts_G_1 = new CancellationTokenSource();
        CancellationToken token_G_1;

        public ICommand CMD_G_1
        {
            get
            {
                return cMD_G_1 ?? (cMD_G_1 = new RelayCommand<object>(param =>
                {
                    try
                    {
                        cts_G_1 = new CancellationTokenSource(); // Create a new CancellationTokenSource
                        token_G_1 = cts_G_1.Token; // Get the new token
                        var IsSuccess = int.TryParse(str_G_3, out waferindex);
                        StartCopy();
                    }
                    catch (Exception ex)
                    {
                        string logfilepath = @"C:\QVS\Log\" + "TesterLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; Exception; CMD_G_1; {ex}" + "\n");
                    }
                }));
            }
            set
            {
                cMD_G_1 = value;
            }
        }

        int waferindex = 0;
        string logfilepath = @"C:\QVS\Log\" + "TesterLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
        private void StartCopy()
        {
            Task tsk = Task.Run(() =>
            {
                while (!token_G_1.IsCancellationRequested)
                {
                    // Check if the directory exists
                    if (Directory.Exists(str_G_2))
                    {
                        // Check if cancellation has been requested
                        if (token_G_1.IsCancellationRequested)
                        {
                            //RunLog.Add("------------StartCopy STOPPED------------");
                            Application.Current.Dispatcher.Invoke(() => RunLog.Add($"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | ------------All STOPPED------------"));
                            cts_G_1.Token.ThrowIfCancellationRequested();
                            File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | ------------All STOPPED------------" + "\n");
                        }

                        // Get .tif files count in the directory
                        string[] tifFiles = Directory.GetFiles(str_G_2, "*.tif");
                        int tifFileCount = tifFiles.Length;

                        int delay = 0;
                        var result = int.TryParse(str_G_4, out delay);

                        // if the file count is less than 200
                        if (tifFileCount < 100)// && !IsCopying)
                        {
                            //RunLog.Add($"StartCopy Wafer {waferindex}");
                            Application.Current.Dispatcher.Invoke(() => RunLog.Add($"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | StartCopy Wafer {waferindex}"));
                            File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | StartCopy Wafer {waferindex}" + "\n");

                            // Proceed with copy operation
                            try
                            {
                                string foldername = str_G_1;
                                DirectoryInfo Dir = new DirectoryInfo(foldername);
                                FileInfo[] FI = Dir.GetFiles("*.tif");

                                foreach (var item in FI)
                                {
                                    // Check if cancellation has been requested
                                    if (token_G_1.IsCancellationRequested)
                                    {
                                        //Console.WriteLine("Task cancellation requested. FI");
                                        Application.Current.Dispatcher.Invoke(() => RunLog.Add($"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | ------------STOPPED Wafer {waferindex}; Unit / {item.Name} /------------"));
                                        cts_G_1.Token.ThrowIfCancellationRequested();
                                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | ------------STOPPED Wafer {waferindex}; Unit / {item.Name} /------------" + "\n");
                                    }

                                    string sourceFileName = item.FullName;//3_18A_PPIWAFER2_CAS01_1_1_25_[]_[]
                                                                          //string sourceFolderName = Path.GetFileName(Path.GetDirectoryName(sourceFileName));
                                    string sourcedir = Path.GetDirectoryName(sourceFileName);
                                    string destFileName = item.FullName.Replace("CAS01_1_1_25", "CAS01_" + waferindex + "_1_25").Replace(sourcedir, str_G_2);

                                    if (!Directory.Exists(System.IO.Path.GetDirectoryName(destFileName)))
                                        Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destFileName));

                                    if (!File.Exists(destFileName))
                                    {
                                        File.Copy(sourceFileName, destFileName);
                                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; {destFileName} copied" + "\n");
                                    }
                                    else
                                    {
                                        //File.Delete(destFileName);
                                        //File.Copy(sourceFileName, destFileName);
                                    }

                                    Thread.Sleep(delay);
                                }//done whole wafer
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Main: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; Exception; CMD_G_1; {ex}" + "\n");
                                break;
                            }

                            //RunLog.Add($"...DoneCopy Wafer {waferindex}");
                            Application.Current.Dispatcher.Invoke(() => RunLog.Add($"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | Wafer {waferindex} done"));
                            waferindex++;
                        }
                    }

                    // Wait for a short period before checking again (to avoid tight loop)
                    Thread.Sleep(100);
                }
            }, token_G_1);
        }

        public ICommand CMD_G_2
        {
            get
            {
                return cMD_G_2 ?? (cMD_G_2 = new RelayCommand<object>(param =>
                {
                    cts_G_1.Cancel();
                }));
            }
            set
            {
                cMD_G_2 = value;
            }
        }

        public string Str_H_1
        {
            get { return str_H_1; }
            set
            {
                str_H_1 = value;
                OnPropertyChanged(nameof(Str_H_1));
            }
        }
        public string Str_H_2
        {
            get => str_H_2; set
            {
                str_H_2 = value;
                OnPropertyChanged(nameof(Str_H_2));
            }
        }
        public string Str_H_3
        {
            get => str_H_3; set
            {
                str_H_3 = value;
                OnPropertyChanged(nameof(Str_H_3));
            }
        }
        public string Str_H_4
        {
            get => str_H_4; set
            {
                str_H_4 = value;
                OnPropertyChanged(nameof(Str_H_4));
            }
        }

        CancellationTokenSource cts_H_1 = new CancellationTokenSource();
        CancellationToken token_H_1;
        public ICommand CMD_H_1
        {
            get
            {
                return cMD_H_1 ?? (cMD_H_1 = new RelayCommand<object>(param =>
                {
                    try
                    {
                        cts_H_1 = new CancellationTokenSource(); // Create a new CancellationTokenSource
                        token_H_1 = cts_H_1.Token; // Get the new token
                        var IsSuccess = int.TryParse(str_H_3, out xmlindexbysource);
                        StartCopyXMLbySource();
                    }
                    catch (Exception ex)
                    {
                        string logfilepath = @"C:\QVS\Log\" + "TesterLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; Exception; CMD_H_1; {ex}" + "\n");
                    }
                }));
            }
            set
            {
                cMD_H_1 = value;
            }
        }

        int xmlindexbysource = 0;
        private void StartCopyXMLbySource()
        {
            Task tsk = Task.Run(() =>
            {
                while (!token_H_1.IsCancellationRequested)
                {
                    if (!Directory.Exists(str_H_2))
                        Directory.CreateDirectory(str_H_2);

                    // Check if cancellation has been requested
                    if (token_H_1.IsCancellationRequested)
                    {
                        Application.Current.Dispatcher.Invoke(() => RunLog.Add($"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | ------------All STOPPED------------"));
                        cts_H_1.Token.ThrowIfCancellationRequested();
                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | ------------All STOPPED------------" + "\n");
                    }

                    // Get .xml files count in the directory
                    string[] xmlFiles = Directory.GetFiles(str_H_2, "*.xml");
                    int xmlFileCount = xmlFiles.Length;

                    int delay = 0;
                    var result = int.TryParse(str_H_4, out delay);

                    // if the file count is less than 200
                    if (true)// xmlFileCount < 100
                    {
                        Application.Current.Dispatcher.Invoke(() => RunLog.Add($"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | StartCopyXml {xmlindexbysource}"));
                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | StartCopyXml {xmlindexbysource}" + "\n");

                        // Proceed with copy operation
                        try
                        {
                            string foldername = str_H_1;
                            DirectoryInfo Dir = new DirectoryInfo(foldername);
                            FileInfo[] FI = Dir.GetFiles("*.xml");

                            foreach (var item in FI)
                            {
                                // Check if cancellation has been requested
                                if (token_H_1.IsCancellationRequested)
                                {
                                    Application.Current.Dispatcher.Invoke(() => RunLog.Add($"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | ------------STOPPED xml {xmlindexbysource}; Unit / {item.Name} /------------"));
                                    cts_H_1.Token.ThrowIfCancellationRequested();
                                    File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | ------------STOPPED xml {xmlindexbysource}; Unit / {item.Name} /------------" + "\n");
                                }

                                string sourceFileName = item.FullName;//3_18A_PPIxml2_CAS01_1_1_25_[]_[]
                                                                      //string sourceFolderName = Path.GetFileName(Path.GetDirectoryName(sourceFileName));
                                string sourcedir = Path.GetDirectoryName(sourceFileName);
                                string destFileName = item.FullName.Replace("CAS01_0_1_25", "CAS01_" + xmlindexbysource + "_1_25").Replace(sourcedir, str_H_2);

                                if (!Directory.Exists(System.IO.Path.GetDirectoryName(destFileName)))
                                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destFileName));

                                if (!File.Exists(destFileName))
                                {
                                    File.Copy(sourceFileName, destFileName);
                                    File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; {destFileName} copied" + "\n");
                                }
                                else
                                {
                                    //File.Delete(destFileName);
                                    //File.Copy(sourceFileName, destFileName);
                                }

                                Thread.Sleep(delay);
                            }//done whole xml
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Main: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; Exception; CMD_H_1; {ex}" + "\n");
                            break;
                        }

                        Application.Current.Dispatcher.Invoke(() => RunLog.Add($"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | xml {xmlindexbysource} done"));
                        xmlindexbysource++;
                    }

                    // Wait for a short period before checking again (to avoid tight loop)
                    Thread.Sleep(10);
                }
            }, token_H_1);
        }
        public ICommand CMD_H_2
        {
            get
            {
                return cMD_H_2 ?? (cMD_H_2 = new RelayCommand<object>(param =>
                {
                    cts_H_1.Cancel();
                    xmlindexbyindex = 0;

                    if (!Directory.Exists(str_H_2))
                        Directory.CreateDirectory(str_H_2);

                    if (!Directory.Exists(str_H_1))
                        return;

                    // Get .xml files count in the directory
                    string[] xmlFiles = Directory.GetFiles(str_H_2, "*.xml");
                    int xmlFileCount = xmlFiles.Length;

                    Application.Current.Dispatcher.Invoke(() => RunLog.Add($"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | StopCopy xml | Count= {xmlFileCount}"));
                    File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | StopCopy xml | Count= {xmlFileCount}" + "\n");

                    foreach (var item in xmlFiles)
                        File.Delete(item);
                }));
            }
            set
            {
                cMD_H_2 = value;
            }
        }

        public ICommand CMD_H_3
        {
            get
            {
                return cMD_H_3 ?? (cMD_H_3 = new RelayCommand<object>(param =>
                {
                    try
                    {
                        cts_H_1 = new CancellationTokenSource(); // Use back CancellationTokenSource from '_H_1'
                        token_H_1 = cts_H_1.Token; // Use back cancllation Token from '_H_1'
                        StartCopyXMLbyIndex();
                    }
                    catch (Exception ex)
                    {
                        string logfilepath = @"C:\QVS\Log\" + "TesterLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; Exception; CMD_H_3; {ex}" + "\n");
                    }
                }));
            }
            set
            {
                cMD_H_3 = value;
            }
        }

        int xmlindexbyindex = 0;
        private void StartCopyXMLbyIndex()
        {
            Task tsk = Task.Run(() =>
            {
                if (!Directory.Exists(str_H_1) || !Directory.Exists(str_H_2))
                    return;

                // Get .xml files count in the directory
                string[] xmlFiles = Directory.GetFiles(str_H_2, "*.xml");
                int xmlFileCount = xmlFiles.Length;

                foreach (var item in xmlFiles)
                    File.Delete(item);

                string foldername = str_H_1;
                DirectoryInfo Dir = new DirectoryInfo(foldername);
                FileInfo[] FI = Dir.GetFiles("*.xml");
                string sourceFileName = FI[0].FullName;
                int dividend = 0;
                int divisor = 944;
                int quotient = 0;
                int remainder = 0;

                Application.Current.Dispatcher.Invoke(() => RunLog.Add($"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | StartCopyXml by index"));
                File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | StartCopyXml by index" + "\n");

                while (!token_H_1.IsCancellationRequested)
                {
                    try
                    {
                        if (!Directory.Exists(str_H_2))
                            Directory.CreateDirectory(str_H_2);

                        if (token_H_1.IsCancellationRequested)
                        {
                            Application.Current.Dispatcher.Invoke(() => RunLog.Add($"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | ------------STOPPED# Wafer {quotient} | Unit {remainder} | {dividend}------------"));
                            cts_H_1.Token.ThrowIfCancellationRequested();
                            File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("dd-MM-yyyy, HH:mm:ss.fff")} | ------------STOPPED# Wafer {quotient} | Unit {remainder} | {dividend}------------" + "\n");
                        }

                        int delay = 0;
                        var result = int.TryParse(str_H_4, out delay);

                        // if the file count is less than n
                        if (true)
                        {
                            xmlindexbyindex++;

                            dividend = xmlindexbyindex;
                            quotient = dividend / divisor;//wafer index
                            remainder = dividend % divisor;//unit index of current wafer
                            if (dividend % divisor == 0)
                            {
                                remainder = divisor;
                                quotient--;
                            }
                            else
                                remainder = dividend % divisor;
                            string newfilename = "30018A_PPIDUMMY_CAS01_" + quotient + 1 + "_1_25_" + remainder + ".xml";
                            string destFileName = str_H_2 + @"\" + newfilename;// 30018A_PPIDUMMY_CAS01_" + quotient + "_1_25_" + remainder + ".xml";

                            if (!Directory.Exists(System.IO.Path.GetDirectoryName(destFileName)))
                                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destFileName));

                            if (!File.Exists(destFileName))
                            {
                                File.Copy(sourceFileName, destFileName);
                                File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; [{newfilename}] | Wafer {quotient + 1} | Unit {remainder} | {dividend}" + "\n");
                            }

                            Thread.Sleep(delay);

                        }

                        //// Wait for a short period before checking again (to avoid tight loop)
                        //Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Main: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        File.AppendAllText(logfilepath, $"{DateTime.Now.ToString("HH:mm:ss.fff")}; Exception; CMD_H_1; {ex}" + "\n");
                        break;
                    }
                }
            }, token_H_1);
        }
    }
}
