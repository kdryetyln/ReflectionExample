using System;
using System.IO;
using System.Reflection;

namespace ReflectionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            _filePath = Directory.GetParent(_filePath).FullName;
            _filePath = Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName;
            _filePath = Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName;

            string dllPath = _filePath + "//ReflectionExample//ExampleforDLL//bin//Debug//netcoreapp2.2";

            ListAllMethods(dllPath, "BaseClass", "System.ComponentModel.CategoryAttribute");
        }

        public static void ListAllMethods(string dllPath, string baseClassName, string attributeName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(dllPath);
            FileInfo[] fileInfoList = directoryInfo.GetFiles("*dll");

            foreach (FileInfo file in fileInfoList)
            {
                Console.WriteLine("-------------------------------" + file.Name + "-----------------------------------------------");
                try
                {
                    Assembly assemblyObject = Assembly.LoadFrom(file.FullName);
                    Type[] types = assemblyObject.GetTypes();

                    foreach (Type type in types)
                    {

                        if (type.BaseType != null && type.BaseType.Name == baseClassName)
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine("Implementing from " + baseClassName + " : / " + type.Name);
                            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                            foreach (MethodInfo info in methods)
                            {
                                Attribute[] attributes = Attribute.GetCustomAttributes(info);
                                foreach (var attribute in attributes)
                                {
                                    if (attribute.ToString() == attributeName)
                                    {
                                        Console.WriteLine(type.Name + " --- " + info.Name);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex is ReflectionTypeLoadException)
                    {
                        var exp = ex as ReflectionTypeLoadException;
                        var loaderException = exp.LoaderExceptions;
                    }
                    //Console.WriteLine(ex.Message);
                }

            }

        }

    }
}
