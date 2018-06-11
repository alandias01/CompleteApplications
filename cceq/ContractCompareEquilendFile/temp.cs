using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ContractCompareEquilendFile
{
    public class temp
    {
        public temp()
        {
            List<string> raw = File.ReadAllLines("a.txt").ToList();
            List<string> formatted = new List<string>();
            raw.Select(x => "this." + x + "= x." + x + ";").ToList().ForEach(formatted.Add);
            File.WriteAllLines("formatted.txt", formatted);
        }
    }
}
