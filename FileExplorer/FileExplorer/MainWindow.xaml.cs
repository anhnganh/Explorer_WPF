
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var drive in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem();

                item.Header = drive;
                //Add the full path
                item.Tag = drive;


                item.Items.Add(null);



                //Listen 
                item.Expanded += FolderExpanded;


                FolderView.Items.Add(item);

            }

            var all = (TreeViewItem)FolderView.Items[0];
            string userName = Environment.UserName;
            JumpToNode(all, $"C:\\");
            Console.WriteLine(all.Tag);

        }
        
        private void FolderExpanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;
            if (item.Items.Count != 1 || item.Items[0] != null)
            {
                return;
            }
            item.Items.Clear();
            var fullPath = (string)item.Tag;




            #region Get Folders
            var directories = new List<string>();

            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                {
                    directories.AddRange(dirs);
                }
            }
            catch { }


            foreach (var directoryPath in directories)
            {
                var subItem = new TreeViewItem();
                subItem.Header = GetFileFolderName(directoryPath);
                subItem.Tag = directoryPath;

                subItem.Items.Add(null);
                subItem.Expanded += FolderExpanded;

                //add to parent
                item.Items.Add(subItem);

            }
            #endregion

            var files = new List<string>();

            try
            {
                var fs = Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                {
                    files.AddRange(fs);
                }
            }
            catch { }


            foreach (var filePath in files)
            {
                var subItem = new TreeViewItem();
                subItem.Header = GetFileFolderName(filePath);
                subItem.Tag = filePath;


                //add to parent
                item.Items.Add(subItem);

            }

           
        }
        private void SelectedItem(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selected = (TreeViewItem)e.NewValue;
            var fullPath = (string)selected.Tag;
            var metaData = new FileInfo(fullPath);
           

            try
            {
                var files = new List<DirectoryInfo>();

                var details = new List<FileDetails>();
                var allFiles = new DirectoryInfo(fullPath).GetFiles();
                var allFolders = new DirectoryInfo(fullPath).GetDirectories();



                for (int i = 0; i < allFiles.Length; i++)
                {
                    var fd = new FileDetails();
                    fd.FileName = allFiles[i].Name;
                    fd.FileCreation = allFiles[i].CreationTime.ToString();
                    fd.FileImage = $"pack://application:,,,/Images/file.png";
                    fd.IsFile = true;
                    details.Add(fd);
                }


                for (int i = 0; i < allFolders.Length; i++)
                {
                    var fd = new FileDetails();
                    fd.FileName = allFolders[i].Name;
                    fd.FileCreation = allFolders[i].CreationTime.ToString();
                    fd.FileImage = $"pack://application:,,,/Images/folder-open.png";
                    fd.IsFolder = true;
                    fd.Path = fullPath + allFolders[i].Name;

                    details.Add(fd);
                }

                myList.ItemsSource = details;
                Console.WriteLine(fullPath);
            }
            catch { }

        }
        public void SingleFileSelected(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(sender);
            var source = (ListBox)e.Source;
            var selected = (FileDetails)source.SelectedItem;
          
        }

      

        private void ItemMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            ListBox items = sender as ListBox;
            var selected = items.SelectedItem as FileDetails;
            var path = selected.Path;
            var allFolders = new DirectoryInfo(path).GetDirectories();
           
            var details = new List<FileDetails>();

            for (int i = 0; i < allFolders.Length; i++)
            {
                var detail = new FileDetails();
                detail.FileName = allFolders[i].Name;

                details.Add(detail);
            }
           
            myList.ItemsSource = details;
            Console.WriteLine(selected);

        }

        void JumpToNode(TreeViewItem tvi, string NodeName)
        {
            if (tvi.Tag.ToString() == NodeName)
            {
                tvi.IsExpanded = true;
                tvi.BringIntoView();
                return;
            }
            else
                tvi.IsExpanded = false;

            if (tvi.HasItems)
            {
                foreach (var item in tvi.Items)
                {
                    TreeViewItem temp = item as TreeViewItem;
                    JumpToNode(temp, NodeName);
                }
            }
        }
        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            Console.WriteLine(path);
            var normalizedPath = path.Replace('/', '\\');
            
            var lastIndex = normalizedPath.LastIndexOf('\\');

            if (lastIndex <= 0)
            {
                return path;
            }

            return path.Substring(lastIndex + 1);

        }
       

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

            FileDetails selected = myList.SelectedItem as FileDetails;
            var path = selected.Path;
            if (Directory.Exists(path))
            {
                Directory.Delete(path);
            }
        }
        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            Console.WriteLine(sourcePath);
            Console.WriteLine(targetPath);
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
             
            }
        }
        public string details ="";

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

            FileDetails selected = myList.SelectedItem as FileDetails;
            var path = selected.Path;
            details = path;
   
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            FileDetails selected = myList.SelectedItem as FileDetails;
            var path = selected.Path;
            var allFolders = new DirectoryInfo(path).GetDirectories();
            var details = new List<FileDetails>();

            for (int i = 0; i < allFolders.Length; i++)
            {
                var detail = new FileDetails();
                detail.FileName = allFolders[i].Name;

                details.Add(detail);
            }

            myList.ItemsSource = details;
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
           
            FileDetails selected = myList.SelectedItem as FileDetails;
            var path = selected.Path;
          //  string appPath = System.IO.Path.GetDirectoryName(path);
            Console.WriteLine(path);
            try
            {
                Directory.Move(@details + "\\", @path);
            }
            catch { }


        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {

            FileDetails selected = myList.SelectedItem as FileDetails;
            var path = selected.Path;
            Console.WriteLine(path);
           // string appPath = System.IO.Path.GetDirectoryName(path);
            CopyFilesRecursively(details, path);
            // File.Move
           System.IO.Directory.Move(@details + "\\", @path );

        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {

            FileDetails selected = myList.SelectedItem as FileDetails;
            var path = selected.Path;
            string a = path + "\\NewFolder";
            Console.WriteLine(a);
            if (!Directory.Exists(a))
            {
                Directory.CreateDirectory(a);
            }
            else
            {
                var i = 1;
                string b = a +"("+i++ +")";
                Console.WriteLine(b);
                Directory.CreateDirectory(b);

            }



        }
    }
}

