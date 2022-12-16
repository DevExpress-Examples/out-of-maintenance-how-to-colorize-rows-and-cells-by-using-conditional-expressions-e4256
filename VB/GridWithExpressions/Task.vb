Namespace GridWithExpressions

    Public Class Task

        Public Sub New(ByVal name As String, ByVal start As Date, ByVal finish As Date, ByVal isComplete As Boolean)
            Me.Name = name
            StartDate = start
            FinishDate = finish
            IsCompleted = isComplete
        End Sub

        Public Property Name As String

        Public Property FinishDate As Date

        Public Property StartDate As Date

        Public Property IsCompleted As Boolean
    End Class
End Namespace
