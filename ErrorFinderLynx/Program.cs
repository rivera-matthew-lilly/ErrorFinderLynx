using System;
using System.IO;
using FunctionBase;

namespace ErrorFinderLynx
{
    class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = @"C:\Matthew IC Copy For Test\Lynx\Output";
            string searchWord1 = "error";
            string searchWord2 = "Method";
            int primaryKey = 10000;
            //do not inculde dates before 07/11/2019
            //02/16/2023 14:47:05.2 Process 'Production Methods\Fill Blocks with Media (SVJ).met' MethodCompleted, A 'Aspirate(VVP96)' command was stopped due to a motion error
            //02/20/2023 08:19:22.0 Method 'Biologica DNA Miniprep Mixing V2,0 (MJR)' had an error in line 6, 'Load Tips(VVP96)'[Command 'Load Tips(VVP96)': Command 'Load Tips(VVP96)' can not have an existing tip type
            //9 char {0-8}, 10 char {10-20}, if index 22 == P 7 char {22-28} elif index 22 == M 6 char {22-27},
            foreach (string filePath in Directory.GetFiles(directoryPath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        int lineNumber = 0;
                        while ((line = reader.ReadLine()) != null)
                        {
                            lineNumber++;
                            if (line.Contains(searchWord1) && line.Contains(searchWord2))
                            {
                                var parsedLine = FunctionBaseSet.ParseInputString(line, primaryKey);
                                Console.WriteLine("---------------------------------------------------------------------");
                                Console.WriteLine(line);
                                //Console.WriteLine("Date: " + parsedLine.Item1);
                                //Console.WriteLine("Time: " + parsedLine.Item2);
                                //Console.WriteLine("Type: " + parsedLine.Item3);
                                //Console.WriteLine("Method: " + parsedLine.Item4);
                                //Console.WriteLine("Error: " + parsedLine.Item5);
                                //Console.WriteLine("Primary Key: " + parsedLine.Item6);

                                string date = FunctionBaseSet.RemoveSingleQuote(parsedLine.Item1);
                                //Console.WriteLine(date);
                                string time = FunctionBaseSet.RemoveSingleQuote(parsedLine.Item2);
                                string type = FunctionBaseSet.RemoveSingleQuote(parsedLine.Item3);
                                string method = FunctionBaseSet.RemoveSingleQuote(parsedLine.Item4);
                                string error = FunctionBaseSet.RemoveSingleQuote(parsedLine.Item5);
                                int pKey = parsedLine.Item6;
                                primaryKey = parsedLine.Item6;
                                
                                FunctionBaseSet.InsertRow(pKey, date, time, type, method, error);

                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error reading file {0}: {1}", Path.GetFileName(filePath), e.Message);
                }
            }
        }
    }

}
