Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace GridWithExpressions
	Public Class Task

        Public Sub New(ByVal settedName As String, ByVal start As DateTime, ByVal finish As DateTime, ByVal isComplete As Boolean)
            name = settedName
            StartDate = start
            FinishDate = finish
            IsCompleted = isComplete
        End Sub

		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		Private privateFinishDate As DateTime
		Public Property FinishDate() As DateTime
			Get
				Return privateFinishDate
			End Get
			Set(ByVal value As DateTime)
				privateFinishDate = value
			End Set
		End Property
		Private privateStartDate As DateTime
		Public Property StartDate() As DateTime
			Get
				Return privateStartDate
			End Get
			Set(ByVal value As DateTime)
				privateStartDate = value
			End Set
		End Property
		Private privateIsCompleted As Boolean
		Public Property IsCompleted() As Boolean
			Get
				Return privateIsCompleted
			End Get
			Set(ByVal value As Boolean)
				privateIsCompleted = value
			End Set
		End Property
	End Class
End Namespace
