using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yazilimtestdunyasi.Base
{
    public static class BaseTestValue
    {
        public const int TimeoutWaitSeconds = 20;
        public const string AmazonHomePageURL = "https://www.amazon.com.tr/";
        public const string HomePageURL = "https://www.mcbu.edu.tr/";

        public const string GridURL = "http://localhost:4444/wd/hub";
        public const string ProgramPath = @"C:\Users\bilal\OneDrive\Masaüstü\yazilimtestdunyasi";
        public const string ScreenShotPath= ProgramPath + @"\yazilimtestdunyasi\Resources\ScreenShots";
        public const string FileReadPath= ProgramPath + @"\yazilimtestdunyasi\Resources\FileRead";
        public const string LogPath = ProgramPath + @"\yazilimtestdunyasi\Resources\Logs";
    }
}
