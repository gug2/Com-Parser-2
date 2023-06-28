using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Com_Parser_2_client
{
    class UserScript
    {
        public string AssemblyPath { private set; get; }

        private readonly string sourcePath;

        public UserScript(string sourcePath)
        {
            this.sourcePath = sourcePath;
        }

        public bool Compile(string outputAssemblyDir = "userScripts", bool KeepInTempDir = false)
        {
            if (!File.Exists(sourcePath))
            {
                Console.WriteLine("Ошибка компиляции! Файл скрипта не найден. Путь: \"{0}\"", Path.GetFullPath(sourcePath));
                return false;
            }

            if (!Directory.Exists(outputAssemblyDir))
            {
                Directory.CreateDirectory(outputAssemblyDir);
            }

            var elem = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => Path.GetFileNameWithoutExtension(a.Location) == Path.GetFileNameWithoutExtension(sourcePath));
            if (elem != null)
            {
                Console.WriteLine("Скрипт {0} уже загружен!", elem.FullName);
                return false;
            }

            Console.WriteLine("Загрузка скрипта...");

            string[] source = PreprocessSource();

            Console.WriteLine("Компиляция...");

            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters()
            {
                // compile as dll
                GenerateExecutable = false,
                // compile as physical file
                GenerateInMemory = false,
                OutputAssembly = outputAssemblyDir + '/' + Path.GetFileNameWithoutExtension(sourcePath) + ".dll",
            };

            if (KeepInTempDir)
            {
                parameters.TempFiles = new TempFileCollection("userScriptsTempBin", true);
            }

            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, String.Join("\n", source));

            if (results.Errors.HasErrors)
            {
                Console.WriteLine("Ошибка компиляции для скрипта \"{0}\"", sourcePath);
                foreach (CompilerError error in results.Errors)
                {
                    Console.WriteLine(error.ToString());
                }
                return false;
            }

            Console.WriteLine("Скрипт скомпилирован.");
            AssemblyPath = parameters.OutputAssembly;

            return true;
        }

        private string[] PreprocessSource()
        {
            Console.WriteLine("Модификация кода...");

            List<string> lines = File.ReadAllLines(sourcePath).ToList();

            lines.Insert(0, "using System.Runtime.InteropServices;");
            int index = lines.FindIndex(line => line.Contains("public struct inData"));

            if (index != -1)
            {
                lines.Insert(index, "[StructLayout(LayoutKind.Sequential, Pack=1)]");
            }

            return lines.ToArray();
        }
    }
}
