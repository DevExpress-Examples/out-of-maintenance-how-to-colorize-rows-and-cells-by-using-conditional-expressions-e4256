Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Data
Imports DevExpress.Xpf.Grid

Namespace GridWithExpressions
	Friend Class ExpressionColumnInfo
		Implements IDataColumnInfo
		Private columnInfo As IDataColumnInfo
		Private columnsList As List(Of IDataColumnInfo)

		Public Sub New(ByVal column As GridColumn)
			columnInfo = CType(column, IDataColumnInfo)
			FillColumnsList()
		End Sub
		Private Sub FillColumnsList()
			columnsList = New List(Of IDataColumnInfo)()
			For Each col As GridColumn In columnInfo.Columns
				columnsList.Add(col)
			Next col
			columnsList.Add(columnInfo)
		End Sub

		Public ReadOnly Property Caption() As String Implements IDataColumnInfo.Caption
			Get
				Return columnInfo.Caption
			End Get
		End Property

        Public ReadOnly Property Columns() As List(Of IDataColumnInfo) Implements IDataColumnInfo.Columns
            Get
                Return columnsList
            End Get
        End Property

		Public ReadOnly Property Controller() As DataControllerBase Implements IDataColumnInfo.Controller
			Get
				Return columnInfo.Controller
			End Get
		End Property

		Public ReadOnly Property FieldName() As String Implements IDataColumnInfo.FieldName
			Get
				Return columnInfo.FieldName
			End Get
		End Property

		Public ReadOnly Property FieldType() As Type Implements IDataColumnInfo.FieldType
			Get
				Return columnInfo.FieldType
			End Get
		End Property

		Public ReadOnly Property Name() As String Implements IDataColumnInfo.Name
			Get
				Return columnInfo.Name
			End Get
		End Property

		Public ReadOnly Property UnboundExpression() As String Implements IDataColumnInfo.UnboundExpression
			Get
				Return String.Empty
			End Get
		End Property
	End Class
End Namespace
