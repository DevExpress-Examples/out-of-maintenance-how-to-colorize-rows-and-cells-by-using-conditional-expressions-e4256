Imports DevExpress.Xpf.Core
Imports System
Imports System.Collections.ObjectModel

Namespace GridWithExpressions
    Partial Public Class MainWindow
        Inherits DXWindow

        Public Sub New()
            InitializeComponent()
            DataContext = GridSource()
        End Sub

        Public Function GridSource() As ObservableCollection(Of Task)
            Dim tasks As New ObservableCollection(Of Task)()
            tasks.Add(New Task("Task1", New Date(2012, 9, 3), Date.Now, True))
            tasks.Add(New Task("Task2", Date.Now, New Date(2012, 9, 7), False))
            tasks.Add(New Task("Task3", New Date(2012, 8, 12), Date.Now, False))
            tasks.Add(New Task("Task4", New Date(2012, 9, 3), Date.Now, False))
            tasks.Add(New Task("Task5", New Date(2012, 7, 15), New Date(2012, 9, 23), False))
            tasks.Add(New Task("Task6", New Date(2012, 4, 3), New Date(2012, 4, 2), True))
            tasks.Add(New Task("Task7", New Date(2012, 9, 3), Date.Now, False))
            Return tasks
        End Function
    End Class
End Namespace
