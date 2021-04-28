using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;



namespace IDEMewtow
{
    public class Helpers
    {

        public void  PlaceholderEnter(TextBox btn, string name)
        {
            if (btn.Text == name)
            {
                btn.Text = "";
                btn.ForeColor = Color.LightGray;
            }
        }
        public void PlaceholderLeave(TextBox btn, string name)
        {
            if (btn.Text == "")
            {
                btn.Text = name;
                btn.ForeColor = Color.DimGray;
            }
        }

        public static TreeNode GenerateTreeView(string strPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(strPath);
            TreeNode ret = new TreeNode(dirInfo.Name);
            //Obtenemos los subdirectorios
            foreach (var dirSub in dirInfo.GetDirectories())
            {
                ret.Nodes.Add(GenerateTreeView(dirSub.FullName));
            }
            //Obtenemos las diferentes extensiones de archivo
            foreach (var strExtension in dirInfo.GetFiles().Select(x => x.Name).Distinct())
            {
                TreeNode nodeExt = new TreeNode(strExtension);
                ret.Nodes.Add(nodeExt);
            }
            return ret;
        }
        
        public static string OpenFile()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

           // MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);

            return filePath;

        }

        public static string ReadFile(string path)
        {
            var Content=string.Empty;
            try
            {
                if (File.Exists(path))
                    Content = File.ReadAllText(path);
                else
                    Content = "Archivo no encontrado";
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

            return Content;
        }

        public static void SaveFile(string path,string content)
        {
           // string path = @"c:\temp\MyTest.txt";

            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
              //  string createText = "Hello and Welcome" + Environment.NewLine;
                File.WriteAllText(path, content);
            }

            // This text is always added, making the file longer over time
            // if it is not deleted.
           // string appendText = "This is extra text" + Environment.NewLine;
           // File.AppendAllText(path, appendText);

            // Open the file to read from.
            string readText = File.ReadAllText(path);
            Console.WriteLine(readText);

        }

        public static void DeleteFile(string path)
        {
            
            try
            {
                if (File.Exists(path))
                     File.Delete(path);
                else
                    Console.WriteLine("Archivo no encontrado");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }



        }

        private void MessageSuccess()
        {
            const string message = "😄 Ya.. creaste la Base de Datos.!";
            const string caption = "Ok!";
            MessageBoxButtons btnSuccess = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, btnSuccess);
            // If the no button was pressed ...
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                // cancel the closure of the form.
                return;
            }
        }

    }

    public class MouseDownWindows
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        public extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
    }

    

}
