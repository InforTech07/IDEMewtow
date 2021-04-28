using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace IDEMewtow
{
    class CreateFile
    {
       


        public static void NewFolder(string name)
        {
            string path = Environment.rootDir + name;

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    Console.WriteLine("That path exists already.");
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

                // Delete the directory.
              //  di.Delete();
               // Console.WriteLine("The directory was deleted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }
        }
        public static void NewFile(string nameproyect,string solution)
        {
          
            string rootDir = @"C:\Users\Techp\source\repos\IDEMewtow\proyect\";
            string dirproyect = nameproyect + @"\";
            string sol = solution + ".txt";
            string fullpath = Path.Combine(rootDir,dirproyect,sol);
            Console.WriteLine(fullpath);

            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(fullpath))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("programa en mewtow.");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(fullpath))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
