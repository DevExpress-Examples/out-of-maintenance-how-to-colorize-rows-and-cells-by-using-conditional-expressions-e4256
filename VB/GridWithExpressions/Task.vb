Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace GridWithExpressions
	Public Class Task

		Public Sub New(ByVal name As String, ByVal start As DateTime, ByVal finish As DateTime, ByVal isComplete As Boolean)
			Me.Name = name
			StartDate = start
			FinishDate = finish
			IsCompleted = isComplete
		End Sub

		Public Property Name() As String
		Public Property FinishDate() As DateTime
		Public Property StartDate() As DateTime
		Public Property IsCompleted() As Boolean
	End Class
End Namespace
