using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class CsvHelper
{
    public static List<Dictionary<string, string>> fromCsv(string csvFile)
    {
        List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
        var myCSV = Resources.Load<TextAsset>(csvFile);
        string[] allLines = myCSV.text.Replace("\r","").Split('\n');
        string[] headers = allLines[0].Split(',');        
        
        for (int i = 1; i < allLines.Length; i++)
        {
            Dictionary<string, string> item = new Dictionary<string, string>();
            string[] contents = allLines[i].Split(',');
            for(int j = 0; j < contents.Length; j++)
            {
                item[headers[j]] = contents[j];
            }
            list.Add(item);
        }
        return list;
    }
}
