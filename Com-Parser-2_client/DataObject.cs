using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Com_Parser_2_client
{
    class DataObject
    {
        private static readonly string[] Prefixes = { "pp", "pt_(?:bin|hex|dec)" };
        private static readonly string[] Postfixes = { "plot\\d+" };

        public string Name { private set; get; }
        public FieldInfo Field { get; }
        public int Plot { private set; get; }
        public string Text { private set; get; }
        public bool IsLogging { private set; get; }

        private DataObject(string name, FieldInfo field)
        {
            Name = name;
            Field = field;
            Plot = -1;
        }

        public override string ToString()
        {
            return String.Format("DataObject \"{0}\", Plot: {1}, Text: {2}, IsLogging: {3}", Name, Plot, Text, IsLogging);
        }

        public static DataObject ConvertField(FieldInfo fieldInfo)
        {
            string fieldName = fieldInfo.Name;
            DataObject dataObj = new DataObject(fieldName, fieldInfo);

            string prefixPattern = String.Format("^(?:{0})+", "(" + String.Join("_)|(", Prefixes) + "_)");
            string postfixPattern = String.Format("(?:{0})+$", "(_" + String.Join(")|(_", Postfixes) + ")");

            var prefix = Regex.Match(fieldName, prefixPattern);
            var postfix = Regex.Match(fieldName, postfixPattern);

            //int lastPrefixIndex = 0;
            //int firstPostfixIndex = fieldName.Length;

            List<Group> pre = GetGroups(prefix.Groups);
            List<Group> post = GetGroups(postfix.Groups);

            /*if (pre.Count > 0)
            {
                int maxIndex = pre[0].Index + pre[0].Length;
                foreach (var elem in pre)
                {
                    if (elem.Index + elem.Length > maxIndex)
                    {
                        maxIndex = elem.Index + elem.Length;
                    }
                }

                lastPrefixIndex = maxIndex;
            }
            if (post.Count > 0)
            {
                int minIndex = post[0].Index;
                foreach (var elem in post)
                {
                    if (elem.Index < minIndex)
                    {
                        minIndex = elem.Index;
                    }
                }

                firstPostfixIndex = minIndex;
            }*/

            dataObj.Name = fieldName;//fieldName.Substring(lastPrefixIndex, firstPostfixIndex - lastPrefixIndex);
            dataObj.IsLogging = true;

            foreach (Group s in pre)
            {
                if (s.Value == "pp_")
                {
                    Group group = post.Find(elem => Regex.IsMatch(elem.Value, "_plot\\d+"));

                    if (group == null)
                    {
                        Console.WriteLine("Не найден обязательный постфикс {0} для префикса {1}!", "_plot\\d+", s.Value);
                        continue;
                    }

                    dataObj.Plot = Convert.ToInt32(Regex.Match(group.Value, "\\d+").Value);
                }
                else if (s.Value.StartsWith("pt_"))
                {
                    string groupValue = pre.Find(elem => Regex.IsMatch(elem.Value, "pt_(?:bin|hex|dec)")).Value;

                    dataObj.Text = Regex.Match(groupValue, "bin|hex|dec").Value;
                }
                else
                {
                    Console.WriteLine("Префикс {0} не поддерживается. Пропускаем..", s);
                }
            }

            return dataObj;
        }

        private static List<Group> GetGroups(GroupCollection groups)
        {
            Group[] arr = new Group[groups.Count];
            groups.CopyTo(arr, 0);
            List<Group> g = arr.ToList();

            return g.FindAll(elem => g.IndexOf(elem) > 0 && elem.Length > 0);
        }
    }
}
