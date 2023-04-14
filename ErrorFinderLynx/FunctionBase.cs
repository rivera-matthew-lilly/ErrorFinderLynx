using System;
using System.Data.SQLite;

namespace FunctionBase { 
    public class FunctionBaseSet
    {
        public static Tuple<string, string, string, string, string, int> ParseInputString(string input, int primaryKey)
        {
            // Check that input string is at least 21 characters long
            if (input.Length < 21)
            {
                throw new ArgumentException("Input string must be at least 21 characters long");

            }

            string date = input.Substring(0, 10);
            string time = input.Substring(11, 10);
            string type = string.Empty;
            string methodName = string.Empty;
            string errorText = string.Empty;


            if (input.Length > 22)
            {
                if (input[22] == 'P')
                {
                    type = input.Substring(22, 7);
                    int firstCommaIndex = input.IndexOf("'");
                    int secondCommaIndex = input.IndexOf("'", firstCommaIndex + 1);
                    if (firstCommaIndex != -1 && secondCommaIndex != -1)
                    {
                        methodName = input.Substring(firstCommaIndex + 1, secondCommaIndex - firstCommaIndex - 1);
                        //Console.WriteLine(methodName);
                        errorText = input.Substring(secondCommaIndex + 19);
                        //Console.WriteLine(errorText);
                        primaryKey++;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input string format");
                    }
                }
                else if (input[22] == 'M')
                {
                    type = input.Substring(22, 6);
                    int firstCommaIndex = input.IndexOf("'");
                    int secondCommaIndex = input.IndexOf("'", firstCommaIndex + 1);
                    if (firstCommaIndex != -1 && secondCommaIndex != -1)
                    {
                        methodName = input.Substring(firstCommaIndex + 1, secondCommaIndex - firstCommaIndex - 1);
                        Console.WriteLine(methodName);
                        errorText = input.Substring(secondCommaIndex + 18);
                        Console.WriteLine(errorText);
                        primaryKey++;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input string format");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid input string format");
                }
            }




            return Tuple.Create(date, time, type, methodName, errorText, primaryKey);
        }


        internal static void InsertRow(int primaryKey, string date, string timeStamp, string type, string method, string errorText)
        {
            string databaseName = "C:\\Matthew IC Copy For Test\\Database\\SystemHealthBase.db";
            SQLiteConnection SystemHealthBase = new SQLiteConnection(@"data source = " + databaseName);
            SystemHealthBase.Open();
            string tableName = "LynxErrorLogInfo";
            string commandString = "INSERT INTO " + tableName + " VALUES ('" + primaryKey.ToString() + "', '" + date + "', '" + timeStamp + "', '" + type + "', '" + method + "', '" + errorText + "');"; //@"INSERT INTO " + tableName + " VALUES (" + date + ", " + timeStamp + ", " + instrumentName + ", " + commandName + ", " + manufacture + ", " + errorText + ");";
            SQLiteCommand command = new SQLiteCommand(commandString, SystemHealthBase);
            Console.WriteLine(commandString);
            command.ExecuteNonQuery();
            SystemHealthBase.Close();
            Console.WriteLine("Data Added Sucessfully");
        }

        public static string RemoveSingleQuote(string inputString)
        {
            string newString = string.Empty;
            if (inputString.Contains("'"))
            {
                newString = inputString.Replace("'", " ");
            }
            else
            {
                newString = inputString;
            }
            return newString;
        }

    }
 } 