Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Collections.ObjectModel
Imports DevExpress.Xpf.Core

Namespace GridWithExpressions
	Partial Public Class MainWindow
		Inherits DXWindow
		Private tasks_Renamed As ObservableCollection(Of Task)
		Public Property Tasks() As ObservableCollection(Of Task)
			Get
				Return tasks_Renamed
			End Get
			Set(ByVal value As ObservableCollection(Of Task))
				tasks_Renamed = value
			End Set
		End Property

		Public Sub New()
			InitializeComponent()
			GridSource()
		End Sub

		Public Sub GridSource()
			Tasks = New ObservableCollection(Of Task)()

			Tasks.Add(New Task("Task1", New DateTime(2012, 9, 3), DateTime.Now, True))
			Tasks.Add(New Task("Task2", DateTime.Now, New DateTime(2012, 9, 7), False))
			Tasks.Add(New Task("Task3", New DateTime(2012, 8, 12), DateTime.Now, False))
			Tasks.Add(New Task("Task4", New DateTime(2012, 9, 3), DateTime.Now, False))
			Tasks.Add(New Task("Task5", New DateTime(2012, 7, 15), New DateTime(2012, 9, 23), False))
			Tasks.Add(New Task("Task6", New DateTime(2012, 4, 3), New DateTime(2012, 4, 2), True))
			Tasks.Add(New Task("Task7", New DateTime(2012, 9, 3), DateTime.Now, False))
		End Sub
	End Class
End Namespace
