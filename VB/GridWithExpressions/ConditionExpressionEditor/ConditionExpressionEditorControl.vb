Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Xpf.Editors.ExpressionEditor
Imports DevExpress.Data

Namespace GridWithExpressions
	Friend Class ConditionExpressionEditorControl
		Inherits ExpressionEditorControl
		Public Property EditorLogic() As ConditionExpressionEditorLogic
			Get
				Return CType(fEditorLogic, ConditionExpressionEditorLogic)
			End Get
			Set(ByVal value As ConditionExpressionEditorLogic)
				value = CType(fEditorLogic, ConditionExpressionEditorLogic)
			End Set
		End Property
		Public Sub New()

		End Sub
		Public Sub New(ByVal columnInfo As IDataColumnInfo)
			MyBase.New(columnInfo)

		End Sub
		Public Overrides Sub OnApplyTemplate()
			MyBase.OnApplyTemplate()
			fEditorLogic = New ConditionExpressionEditorLogic(Me, ColumnInfo)
			fEditorLogic.Initialize()
			fEditorLogic.OnLoad()
		End Sub

		Public Sub SetExpression(ByVal expression As String)
			EditorLogic.SetExpression(expression)
		End Sub
	End Class
End Namespace
