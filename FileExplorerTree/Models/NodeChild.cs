using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace FileExplorerTree.Models
{
    public enum TypeOfNode
    {
        Directory,
        File,
        Image,
        Unknown
    }
    public class NodeChild : INotifyPropertyChanged
    {
        private static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

        private void OnPropertyChanged(string prop)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        
        private string _currentNode;
        public string CurrentNode
        {
            get { return this._currentNode; }
            set
            {
                this._currentNode = value;
                this.OnPropertyChanged("CurrentNode");
            }
        }

        private TypeOfNode _typeOfNode;
        public TypeOfNode TypeOfNode
        {
            get { return this._typeOfNode; }
            set
            {
                this._typeOfNode = value;
                this.OnPropertyChanged("TypeOfNode");
            }
        }
        private bool _isExpanded;
        public bool IsExpanded
        {
            get { return this._isExpanded; }
            set
            {
                this._isExpanded = value;
                this.OnPropertyChanged("IsExpanded");
                if(value) this.GetChildren();
            }
        }
        

        private string _currentName;
        public string CurrentName
        {
            get { return this._currentName; }
            set
            {
                this._currentName = value;
                this.OnPropertyChanged("CurrentName");
            }
        }

        private BitmapImage _currentBitmap;
        public BitmapImage CurrentBitmap
        {
            get { return this._currentBitmap; }
            set
            {
                this._currentBitmap = value;
                this.OnPropertyChanged("CurrentBitmap");
            }
        }

        private long _fileSize;
        public long FileSize
        {
            get { return this._fileSize; }
            set
            {
                this._fileSize = value;
                this.OnPropertyChanged("FileSize");
            }
        }

        private ObservableCollection<NodeChild> _children;
        public ObservableCollection<NodeChild> Children
        {
            get { return this._children; }
            set
            {
                this._children = value;
                this.OnPropertyChanged("Children");
            }
        }
        public NodeChild(string path)
        {
            this.CurrentNode = path;
            this.CurrentName = string.IsNullOrEmpty(path.Split('\\')?.Last()) ? path : path.Split('\\')?.Last() ?? path;
            bool isDirectory = Directory.Exists(this.CurrentNode);
            if (isDirectory)
                CreateDirectory();
            else
                CreateFile();
        }
        private void CreateDirectory()
        {
            this.TypeOfNode = TypeOfNode.Directory;
            try
            {
                this.CurrentBitmap = Utilities.BitmapToImageSource(DefaultIcons.FolderLarge.ToBitmap());
            }
            catch
            {

            }
        }
        private void CreateFile()
        {
            try
            {
                this.CurrentBitmap = Utilities.BitmapToImageSource(System.Drawing.Icon.ExtractAssociatedIcon(this.CurrentNode).ToBitmap());
            }
            catch
            {

            }
            FileInfo info = new FileInfo(this.CurrentNode);
            try
            {
                if (ImageExtensions.Contains(info.Extension.ToUpperInvariant()))
                    this.TypeOfNode = TypeOfNode.Image;
                else
                    this.TypeOfNode = TypeOfNode.File;
            }
            catch
            {
                this.TypeOfNode = TypeOfNode.Unknown;
            }
            
            this.FileSize = info.Length;
        }
        
        
        public async void GetChildren()
        {

            if (Children == null)
            {
                Children = new ObservableCollection<NodeChild>();
            }
            this.CleanList();
            if (!Directory.Exists(this.CurrentNode)) return;
            try
            {
                Directory.GetFiles(this.CurrentNode).Where(s => Children.All(cd => cd.CurrentNode != s)).Select(s => new NodeChild(s)).ToList().ForEach(s => Children.Add(s));
                Directory.GetDirectories(this.CurrentNode).Where(s => Children.All(cd => cd.CurrentNode != s)).Select(s => new NodeChild(s)).ToList().ForEach(s => Children.Add(s));
            }
            catch (Exception ex)
            {
                this.IsExpanded = false;
                MessageBox.Show(ex.Message);
            }

            this.OnPropertyChanged("Children");
        }
        private void CleanList()
        {
            var missingFiles = Children.Select((cd, index) => new { cd, index }).Where(cd => cd.cd.TypeOfNode == TypeOfNode.Directory ? !Directory.Exists(cd.cd.CurrentNode) : !File.Exists(cd.cd.CurrentNode));
            if (missingFiles.Any())
            {
                missingFiles.ToList().ForEach(tm => Children.RemoveAt(tm.index));
                this.OnPropertyChanged("Children");
            }
        }

    }
}
