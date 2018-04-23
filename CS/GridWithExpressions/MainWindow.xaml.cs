using DevExpress.Xpf.Core;
using System;
using System.Collections.ObjectModel;

namespace GridWithExpressions {
    public partial class MainWindow : DXWindow {

        public MainWindow() {
            InitializeComponent();
            DataContext = GridSource();
        }

        public ObservableCollection<Task> GridSource() {
            ObservableCollection<Task> tasks = new ObservableCollection<Task>();
            tasks.Add(new Task("Task1", new DateTime(2012, 9, 3), DateTime.Now, true));
            tasks.Add(new Task("Task2", DateTime.Now, new DateTime(2012, 9, 7), false));
            tasks.Add(new Task("Task3", new DateTime(2012, 8, 12), DateTime.Now, false));
            tasks.Add(new Task("Task4", new DateTime(2012, 9, 3), DateTime.Now, false));
            tasks.Add(new Task("Task5", new DateTime(2012, 7, 15), new DateTime(2012, 9, 23), false));
            tasks.Add(new Task("Task6", new DateTime(2012, 4, 3), new DateTime(2012, 4, 2), true));
            tasks.Add(new Task("Task7", new DateTime(2012, 9, 3), DateTime.Now, false));
            return tasks;
        }
    }
}
