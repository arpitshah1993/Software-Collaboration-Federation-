using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Prototype1
{
    class Program
    {
        private struct TestData
        {
            public string Name;
            public ITest testDriver;
        }

     
        static void Main(string[] args)
        {
            string testDrive = "TestDriver/TestDriver1.dll";
            string path = "../TestDriver/";
            List<TestData> testDriver = new List<TestData>();
            string metadetaFile = System.IO.Path.GetFullPath(path+"metadata.xml");
            System.IO.FileStream metadataXML = new System.IO.FileStream(metadetaFile, System.IO.FileMode.Open);
            Console.WriteLine("Get Input as Test Suit: Test Driver1");
            Console.WriteLine("Start executing test request");
            Console.WriteLine("Fetch metadat file of TestDriver1.dll:");
            XDocument metadataFile = XDocument.Load(metadataXML);
            Console.WriteLine(metadataFile);
            Console.WriteLine("\n\nFind dependent Test Code for this Test suit");
            

           
            XElement[] dependentList = metadataFile.Descendants("dependency").ToArray();
            List<string> dependList = new List<string>();
            foreach (var file in dependentList)
            {
                dependList.Add(file.Value);
                Console.WriteLine(file.Value);
            }
            dependList.Add(testDrive);
            path = "../";
            Console.Write("\nLoading files from temoprary directory in AppDomain: {0}\n", AppDomain.CurrentDomain.FriendlyName);
            foreach (string file in dependList)
            {
                string tempPath = Path.Combine(path, file);
                Console.Write("\nLoading: \"{0}\" in AppDomain: {1}", tempPath, AppDomain.CurrentDomain.FriendlyName);

                try
                {
                    Assembly assem = Assembly.LoadFrom(Path.GetFullPath(tempPath));
                    Type[] types = assem.GetExportedTypes();

                    foreach (Type t in types)
                    {
                        if (t.IsClass && typeof(ITest).IsAssignableFrom(t))  // searching whether this type derive from ITest 
                        {
                            ITest tdr = (ITest)Activator.CreateInstance(t);    // create instance of test driver

                            // save type name and reference to created type on managed heap
                            TestData td = new TestData();
                            td.Name = t.Name;
                            td.testDriver = tdr;
                            Console.Write("\nGot {1} derives from ITest interface in AppDomain: {0}", AppDomain.CurrentDomain.FriendlyName, t.Name);
                            testDriver.Add(td);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("\nException: {0} caught in AppDomain: {1}", ex.Message, AppDomain.CurrentDomain.FriendlyName);
                }
            }

            if (testDriver.Count == 0)
                return;
            foreach (TestData td in testDriver)
            {
                //strating watch to count elapsed time of execution
                Stopwatch watch = new Stopwatch();
                try
                {
                    Console.Write("\nRunning test  driver {0} by calling ITest::bool test() method in AppDomain: {1}", td.Name, AppDomain.CurrentDomain.FriendlyName);
                    watch.Reset();
                    //starting watch to measure elapsed time
                    watch.Start();
                    if (td.testDriver.test())
                    {
                        watch.Stop();
                        Console.Write("\nTest Result: Passed in AppDomain: {0}\n", AppDomain.CurrentDomain.FriendlyName);
                    }
                    else
                    {
                        Console.Write("\nTest Result: Failed in AppDomain: {0}\n", AppDomain.CurrentDomain.FriendlyName);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("\nException caught in AppDomain: {1} domain: {0}}", ex.Message, AppDomain.CurrentDomain.FriendlyName);
                }
                finally
                {
                    watch.Stop();
                    Console.WriteLine(string.Format("Elapsed Time to execute tests:{0}", watch.Elapsed.TotalMilliseconds));
                }
            }
            Console.Write("\n");
            Console.ReadLine();

        }
    }
}
