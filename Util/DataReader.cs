using Recon.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recon.Util
{
    public class DataReader
    {
        StreamReader reader;
        public DataReader(string file)
        {
            if (File.Exists(file))
                reader = new StreamReader(file);
            else
                throw new Exception("Trying to access non existing file");
        }

        public mega<String> GetText()
        {
            mega<String> coolMega = new mega<String>();
            while (!reader.EndOfStream)
            {
                coolMega.Add(reader.ReadLine());
            }
            reader.Close();
            return coolMega;
        }
    }
}
