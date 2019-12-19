using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yazilimtestdunyasi.Base;

namespace yazilimtestdunyasi.Common.FileOperations
{
    class FileOperations
    {
        public static string dosyadanOku()
        {
            string dosya_yolu = $@"{BaseTestValue.FileReadPath}\LoginBilgileri.txt";
            //Okuma işlem yapacağımız dosyanın yolunu belirtiyoruz.
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosyanın açılacağını,
            //3.parametre dosyaya erişimin veri okumak için olacağını gösterir.
            StreamReader sw = new StreamReader(fs);
            //Okuma işlemi için bir StreamReader nesnesi oluşturduk.
            string yazi = sw.ReadLine();
            //Satır satır okuma işlemini gerçekleştirdik 
            sw.Close();
            fs.Close();
            //İşimiz bitince kullandığımız nesneleri iade ettik.
            return yazi; //okuduklarımızı geri döndürdük.
        }
    }
}
