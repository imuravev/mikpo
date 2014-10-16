using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mikpo_3.Classes
{
    public class IniFile
{ 
     public string path;

    [DllImport("kernel32")] 
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    [DllImport("kernel32")] 
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath); 

    public IniFile(string INIPath) 
    { 
         path = INIPath; 
    } 

    public void IniWriteValue(string Section, string Key, string Value) 
    { 
           if(!Directory.Exists(Path.GetDirectoryName(path))) 
                 Directory.CreateDirectory(Path.GetDirectoryName(path)); 
           if(!File.Exists(path)) 
                  using (File.Create(path)) { }; 

           WritePrivateProfileString(Section, Key, Value, this.path); 
    } 

   public string IniReadValue(string Section, string Key) 
   { 
         StringBuilder temp = new StringBuilder(255); 
         int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path); 
         return temp.ToString(); 
   }

   public int IniReadIntValue(string Section, string Key)
   {
       StringBuilder temp = new StringBuilder(255);
       int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
       return Convert.ToInt32(temp.ToString());
   }

   public double IniReadDoubleValue(string Section, string Key)
   {
       StringBuilder temp = new StringBuilder(255);
       int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
       return Convert.ToDouble(temp.ToString());
   }

   public bool IniReadBoolValue(string Section, string Key)
   {
       StringBuilder temp = new StringBuilder(255);
       int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
       return Convert.ToBoolean(temp.ToString());
   } 

}
}
