using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ServerClientLibrary
{
    public class Logger
    {
        private static FileStream m_FileStream = null;
        public static readonly string SavePath = Environment.CurrentDirectory + @"\" + DateTime.Now.Date + ".txt";
        public static void SaveLog(string log)
        {
            m_FileStream ??= File.Create(SavePath);

            using var writer = new StreamWriter(m_FileStream);
            writer.WriteLine(DateTime.Now.TimeOfDay + " " + log);
        }
    }
}
