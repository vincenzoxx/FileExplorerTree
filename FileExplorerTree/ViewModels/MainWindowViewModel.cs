using Caliburn.Micro;
using FileExplorerTree.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static FileExplorerTree.Models.NodeChild;

namespace FileExplorerTree.ViewModels
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        #region Properties

        private DriveInfo _selectedDrive;
        public DriveInfo SelectedDrive
        {
            get
            {
                return this._selectedDrive;
            }
            set
            {
                this._selectedDrive = value;
                this.NotifyOfPropertyChange(() => this.SelectedDrive);
            }
        }

        private DriveInfo[] _myDrives;
        public DriveInfo[] MyDrives
        {
            get { return _myDrives; }
            set
            {
                _myDrives = value;
                this.NotifyOfPropertyChange(() => this.MyDrives);
            }
        }

        private ObservableCollection<NodeChild> _node;
        public ObservableCollection<NodeChild> Node
        {
            get { return this._node; }
            set
            {
                this._node = value;
                this.NotifyOfPropertyChange(() => this.Node);
            }
        }

        private NodeChild _selectedNode;
        public NodeChild SelectedNode
        {
            get { return this._selectedNode; }
            set
            {
                this._selectedNode = value;
                this.NotifyOfPropertyChange(() => this.SelectedNode);
            }
        }

        #endregion

        #region Events
        public void OnMainWindowLoadedAction()
        {
            this.MyDrives = DriveInfo.GetDrives();
            

        }
        public void OnMouseDoubleClickAction(NodeChild dataContext)
        {
            if (dataContext.TypeOfNode == TypeOfNode.File || dataContext.TypeOfNode == TypeOfNode.File)
                System.Diagnostics.Process.Start(dataContext.CurrentNode);
            else
                dataContext.GetChildren();
        }
        public void OnCheckedAction(NodeChild dataContext)
        {
            if (dataContext.TypeOfNode == TypeOfNode.File || dataContext.TypeOfNode == TypeOfNode.File)
                System.Diagnostics.Process.Start(dataContext.CurrentNode);
            else
                dataContext.GetChildren();
        }
        public void OnSelectionChangedAction()
        {
            string path = SelectedDrive.RootDirectory.ToString();
            if (!Directory.Exists(path)) return;
            this.Node = new ObservableCollection<NodeChild>();
            this.Node.Add(new NodeChild(path));
        }
        #endregion
        
    }
    
}
