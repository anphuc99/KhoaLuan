using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.script.Lib
{
    public class Lang
    {
        public static string toText(string key)
        {
            string curLang = PlayerPrefs.GetString("Lang");
            List<Dictionary<string, string>> list = CsvHelper.fromCsv("Lang/Lang");
            foreach (Dictionary<string, string> pair in list)
            {
                try
                {
                    if (pair["key"] == key)
                    {
                        return pair[curLang];
                        break;
                    }

                }
                catch
                {
                }
            }

            return key;
        }
    }
}
