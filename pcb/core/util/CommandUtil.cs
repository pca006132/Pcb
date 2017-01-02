﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pcb.core.util
{
    public class CommandUtil
    {
        //for color black tech
        public static string colorSignText = Properties.Resources.signText;

        public static string escape(string text)
        {
            return text.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }
        public static string unescape(string text)
        {
            return text.Replace("\\\"", "\"").Replace("\\\\", "\\");
        }
        public static string colorBlackTech(string text)
        {
            string setBlock = escape(escape(text));
            setBlock = setBlock.Replace("§", "\\u00a7");
            return "setblock ~ ~ ~ standing_sign 0 replace {Text1:\"{\\\"text\\\":\\\"" + colorSignText +
                    "\\\",\\\"clickEvent\\\":{\\\"action\\\":\\\"run_command\\\",\\\"value\\\":\\\"/setblock ~ ~ ~ air\\\"}}\",Text2:\"{\\\"text\\\":\\\"\\\",\\\"clickEvent\\\":{\\\"action\\\":\\\"run_command\\\",\\\"value\\\":\\\"" + setBlock + "\\\"}}\",Text3:\"{\\\"text\\\":\\\"\\\"}\",Text4:\"{\\\"text\\\":\\\"\\\"}\"}";
        }        
        public static long[] randomUUIDPair()
        {
            Random rnd = new Random();
            long max = rnd.Next(int.MaxValue);
            max = max << 32;
            max += rnd.Next(int.MaxValue);
            long min = rnd.Next(int.MaxValue);
            min = min << 32;
            min += rnd.Next(int.MaxValue);
            return new long[] {min, max};
        }
        public static string UUIDPairToString(long most, long least)
        {
            string mostString = most.ToString("X16");
            mostString = mostString.Insert(8, "-").Insert(13, "-");

            string leastString = least.ToString("X16");
            leastString = leastString.Insert(4, "-");

            string toReturn = mostString + "-" + leastString;
            return toReturn;
        }
        public static long[] UUIDGetLeastMostFromString(string UUID)
        {
            long most = Convert.ToInt64(UUID.Substring(0, 16), 16);
            long least = Convert.ToInt64(UUID.Substring(16), 16);

            return new long[] { most, least };
        }
        public static bool needEscape(string str)
        {
            Stack<char> brackets = new Stack<char>();
            bool inString = false;
            foreach (char chr in str)
            {
                if (!inString)
                    switch (chr)
                    {
                        case '{':
                        case '[':
                            brackets.Push(chr);
                            break;
                        case '}':
                            if (brackets.Count == 0)
                                return true;
                            if (brackets.Peek() == '{')
                                brackets.Pop();
                            else
                                return true;
                            break;
                        case ']':
                            if (brackets.Count == 0)
                                return true;
                            if (brackets.Peek() == ']')
                                brackets.Pop();
                            else
                                return true;
                            break;
                        case ',':
                            if (brackets.Count == 0)
                                return true;
                            break;
                        case '"':
                            inString = true;
                            break;
                        case '\\':
                            return true;
                    }
                else
                    switch (chr)
                    {
                        case '"':
                            inString = false;
                            break;
                        case '\\':
                            return true;                            
                    }
            }
            if (brackets.Count > 0)
                return true;
            if (inString)
                return true;
            return false;
        }
    }
}
