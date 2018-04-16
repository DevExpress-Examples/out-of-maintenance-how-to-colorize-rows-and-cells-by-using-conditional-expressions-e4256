using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Core;

namespace GridWithExpressions {
    public partial class MainWindow : DXWindow {
        ObservableCollection<Task> tasks;
        public ObservableCollection<Task> Tasks {
            get { return tasks; }
            set { tasks = value; }
        }

        public MainWindow() {
            InitializeComponent();
            GridSource();
        }

        public void GridSource() {
            Tasks = new ObservableCollection<Task>();

            Tasks.Add(new Task("Task1", new DateTime(2012, 9, 3), DateTime.Now, true));
            Tasks.Add(new Task("Task2", DateTime.Now, new DateTime(2012, 9, 7), false));
            Tasks.Add(new Task("Task3", new DateTime(2012, 8, 12), DateTime.Now, false));
            Tasks.Add(new Task("Task4", new DateTime(2012, 9, 3), DateTime.Now, false));
            Tasks.Add(new Task("Task5", new DateTime(2012, 7, 15), new DateTime(2012, 9, 23), false));
            Tasks.Add(new Task("Task6", new DateTime(2012, 4, 3), new DateTime(2012, 4, 2), true));
            Tasks.Add(new Task("Task7", new DateTime(2012, 9, 3), DateTime.Now, false));
        }
    }
}
