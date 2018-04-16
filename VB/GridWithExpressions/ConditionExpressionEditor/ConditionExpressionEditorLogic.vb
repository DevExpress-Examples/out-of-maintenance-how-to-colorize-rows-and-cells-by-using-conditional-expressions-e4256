Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Data.ExpressionEditor
Imports DevExpress.Data

Namespace GridWithExpressions
	Friend Class ConditionExpressionEditorLogic
		Inherits ExpressionEditorLogicEx
		Public Sub New(ByVal editor As IExpressionEditor, ByVal columnInfo As IDataColumnInfo)
			MyBase.New(editor, columnInfo)

		End Sub

		Public Sub SetExpression(ByVal expression As String)
			ExpressionMemoEdit.Text = expression
		End Sub
	End Class
End Namespace
