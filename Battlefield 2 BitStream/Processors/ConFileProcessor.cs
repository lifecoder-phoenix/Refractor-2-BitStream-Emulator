﻿using Battlefield_BitStream_Common.Processors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFS;

namespace Battlefield_2_BitStream.Processors
{
    public class ConFileProcessor : IConFileProcessor//this is a big fat headache
    {
        private Dictionary<string, Func<object, object, int>> registeredMethods { get; set; }
        private bool InIf = false;
        public ConFileProcessor()
        {
            registeredMethods = new Dictionary<string, Func<object, object, int>>();
        }
        public void ExecuteConFile(string file)
        {
            string[] commands;
            if (!File.Exists(file))
            {
                if (file.StartsWith("/"))
                    file = file.Substring(1);
                var vfile = VFileSystemManager.GetFile(file);
                if (vfile == null)
                    return;//return for now, so we can actually load
                commands = vfile.ReadAllLines();
            }
            else
            {
                commands = File.ReadAllLines(file);
            }
            foreach(var command in commands)
            {
                var data = command.Split(' ');
                if (string.IsNullOrEmpty(data[0]) || string.IsNullOrWhiteSpace(data[0]))
                    continue;
                if (data[0].StartsWith("rem"))
                    continue;
                if(data[0] == "if")
                {
                    InIf = true;
                    continue;
                }
                if(InIf && (data[0] == "else" || data[0] == "endIf"))
                {
                    InIf = false;
                    continue;
                }
                if (InIf)
                    continue;
                if (data[0] == "endIf")
                    continue;
                if (!registeredMethods.ContainsKey(data[0]))
                {
                    Console.WriteLine("[CONPROCESSOR] Unknown con function: " + command);
                    continue;
                }
                var variable1 = data[1];
                if (variable1.StartsWith(Convert.ToString('"')))
                {
                    for(int i = 2; i < data.Length; i++)
                    {
                        variable1 += data[i];
                        if (data[i].EndsWith(Convert.ToString('"')))
                            break;
                    }
                }
                string variable2 = null;
                if(data.Length > 2)
                {
                    variable2 = data[2];
                    if (variable2.StartsWith(Convert.ToString('"')))
                    {
                        for (int i = 2; i < data.Length; i++)
                        {
                            variable2 += data[i];
                            if (data[i].EndsWith(Convert.ToString('"')))
                                break;
                        }
                    }
                }
                registeredMethods[data[0]](variable1, variable2);
            }
        }

        public void ExecuteConFile(VFile file)
        {
            var commands = file.ReadAllLines();
            foreach (var command in commands)
            {
                var data = command.Split(' ');
                if (string.IsNullOrEmpty(data[0]) || string.IsNullOrWhiteSpace(data[0]))
                    continue;
                if (data[0].StartsWith("rem"))
                    continue;
                if (data[0] == "if")
                {
                    InIf = true;
                    continue;
                }
                if (InIf && (data[0] == "else" || data[0] == "endIf"))
                {
                    InIf = false;
                    continue;
                }
                if (InIf)
                    continue;
                if (data[0] == "endIf")
                    continue;
                if (!registeredMethods.ContainsKey(data[0]))
                {
                    Console.WriteLine("[CONPROCESSOR] Unknown con function: " + command);
                    continue;
                }
                var variable1 = data[1];
                if (variable1.StartsWith(Convert.ToString('"')))
                {
                    for (int i = 2; i < data.Length; i++)
                    {
                        variable1 += data[i];
                        if (data[i].EndsWith(Convert.ToString('"')))
                            break;
                    }
                }
                string variable2 = null;
                if (data.Length > 2)
                {
                    variable2 = data[2];
                    if (variable2.StartsWith(Convert.ToString('"')))
                    {
                        for (int i = 2; i < data.Length; i++)
                        {
                            variable2 += data[i];
                            if (data[i].EndsWith(Convert.ToString('"')))
                                break;
                        }
                    }
                }
                registeredMethods[data[0]](variable1, variable2);
            }
        }

        public void RegisterConMethod(string name, Func<object, object, int> method)
        {
            if (registeredMethods.ContainsKey(name))
                return;
            registeredMethods.Add(name, method);
        }
    }
}