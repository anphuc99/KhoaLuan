using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Event 
{
    private static int eventID = 0;
    public delegate void events(Dictionary<string, object> Dictionary);
    private static Dictionary<Events, Dictionary<int, events>> store = new Dictionary<Events, Dictionary<int, events>>();
    public static int register(Events name, events events)
    {
        int ID = eventID;
        Dictionary<int, events> dict;
        if (store.ContainsKey(name))
        {
            dict = store[name];
        }
        else
        {
            dict = new Dictionary<int, events>();
            store.Add(name, dict);
        }
        dict.Add(ID, events);
        eventID++;
        return ID;
    }

    public static void emit(Events name, Dictionary<string, object> value)
    {
        if (store.ContainsKey(name))
        {
            Dictionary<int, events> keyValuePairs = store[name];
            Stack<int> remove = new Stack<int>();
            foreach (var kv in keyValuePairs)
            {
                try
                {
                    kv.Value(value);
                }
                catch(Exception e)
                {
                    if (e.Message.Contains("has been destroyed but you are still trying to access it"))
                    {
                        remove.Push(kv.Key);
                    }
                    else
                    {
                        Debug.LogError(e.ToString());
                    }
                }
            }

            foreach (int kv in remove)
            {
                unRegister(name, kv);
            }
        }
    }

    public static void unRegister(Events name, int key)
    {
        if (store.ContainsKey(name))
        {
            Dictionary<int, events> keyValuePairs = store[name];
            keyValuePairs.Remove(key);
        }
    }
}
