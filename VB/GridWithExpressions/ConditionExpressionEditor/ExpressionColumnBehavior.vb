Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Interactivity
Imports DevExpress.Xpf.Grid
Imports System.Windows
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Grid.Themes
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized

Namespace GridWithExpressions
	Friend Class ExpressionColumnBehavior
		Inherits Behavior(Of TableView)

		#Region "StylesCollection"

		Public Shared ReadOnly StylesCollectionProperty As DependencyProperty = DependencyProperty.Register("StylesCollection", GetType(ObservableCollection(Of StyleOption)), GetType(ExpressionColumnBehavior))

		Public Property StylesCollection() As ObservableCollection(Of StyleOption)
			Get
				Return CType(GetValue(StylesCollectionProperty), ObservableCollection(Of StyleOption))
			End Get
			Set(ByVal value As ObservableCollection(Of StyleOption))
				SetValue(StylesCollectionProperty, value)
			End Set
		End Property

		Public Sub New()
			StylesCollection = New ObservableCollection(Of StyleOption)()
			AddHandler StylesCollection.CollectionChanged, AddressOf StylesCollection_CollectionChanged
			IsLoading = True
		End Sub

		Private Sub StylesCollection_CollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
			If (Not IsLoading) Then
				If e.Action = NotifyCollectionChangedAction.Add Then
					CollectionModify()
				ElseIf e.Action = NotifyCollectionChangedAction.Reset Then
					RowStyleBuilding()
					View.RowStyle = RowStyle
					CellStyleBuilding()
					For i As Integer = 0 To (CType(View.Parent, GridControl)).Columns.Count - 1
						If (CType(View.Parent, GridControl)).Columns(i).CellStyle IsNot Nothing Then
							CType(View.Parent, GridControl).Columns(i).CellStyle = CellStyle
						End If
					Next i
				End If
			End If
		End Sub

		Private Sub CollectionModify()
			For i As Integer = 0 To StylesCollection.Count - 1
				If StylesCollection(i).IsInitialized() Then
					StylesCollection(i).GenerateSetters()

					If (Not StylesCollection(i).ApplyToRow) Then
						CellTriggersAdding(StylesCollection(i))
						CType(View.Parent, GridControl).Columns(StylesCollection(i).FieldName).CellStyle = CellStyle
					Else
						RowTriggersAdding(StylesCollection(i))
						View.RowStyle = RowStyle
					End If
				End If
			Next i
		End Sub

		#End Region
		Private privateView As TableView
		Private Property View() As TableView
			Get
				Return privateView
			End Get
			Set(ByVal value As TableView)
				privateView = value
			End Set
		End Property
		Private privateRowStyle As Style
		Private Property RowStyle() As Style
			Get
				Return privateRowStyle
			End Get
			Set(ByVal value As Style)
				privateRowStyle = value
			End Set
		End Property
		Private privateCellStyle As Style
		Private Property CellStyle() As Style
			Get
				Return privateCellStyle
			End Get
			Set(ByVal value As Style)
				privateCellStyle = value
			End Set
		End Property
		Private privateExpressionWindow As AppearanceEditWindow
		Private Property ExpressionWindow() As AppearanceEditWindow
			Get
				Return privateExpressionWindow
			End Get
			Set(ByVal value As AppearanceEditWindow)
				privateExpressionWindow = value
			End Set
		End Property
		Private privateExprButton As BarButtonItem
		Private Property ExprButton() As BarButtonItem
			Get
				Return privateExprButton
			End Get
			Set(ByVal value As BarButtonItem)
				privateExprButton = value
			End Set
		End Property
		Private privateIsLoading As Boolean
		Private Property IsLoading() As Boolean
			Get
				Return privateIsLoading
			End Get
			Set(ByVal value As Boolean)
				privateIsLoading = value
			End Set
		End Property

		Protected Overrides Sub OnAttached()
			MyBase.OnAttached()
			View = AssociatedObject
			AddHandler View.Loaded, AddressOf View_Loaded
			EditorBarButtonBuilding()
			AssociatedObject.ColumnMenuCustomizations.Add(ExprButton)
			RowStyleBuilding()
		End Sub

		Private Sub View_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			IsLoading = False
			CollectionModify()
		End Sub

		Protected Overrides Sub OnDetaching()
			MyBase.OnDetaching()
			RemoveHandler ExprButton.ItemClick, AddressOf BarButtonItem_ItemClick
		End Sub

		Private Sub EditorBarButtonBuilding()
			ExprButton = New BarButtonItem() With {.Name = "barButtonItem1", .Content = "Format Conditions Editor"}
			AddHandler ExprButton.ItemClick, AddressOf BarButtonItem_ItemClick
		End Sub

		Private Sub RowStyleBuilding()
			Dim gridRowThemeKeyExtension As New GridRowThemeKeyExtension() With {.ResourceKey = GridRowThemeKeys.RowStyle}
			RowStyle = New Style(GetType(GridRowContent)) With {.BasedOn = TryCast(AssociatedObject.FindResource(gridRowThemeKeyExtension), Style)}
		End Sub

		Private Sub CellStyleBuilding()
			Dim gridCellThemeKeyExtension As New GridRowThemeKeyExtension() With {.ResourceKey = GridRowThemeKeys.CellStyle}
			CellStyle = New Style(GetType(CellContentPresenter)) With {.BasedOn = TryCast(AssociatedObject.FindResource(gridCellThemeKeyExtension), Style)}
		End Sub
		Private Sub CellTriggersAdding(ByVal [option] As StyleOption)
			If (CType(View.Parent, GridControl)).Columns([option].FieldName).CellStyle Is Nothing Then
				CellStyleBuilding()
				CellStyle.Triggers.Add([option].StyleTrigger)
			Else
				Dim NewCellStyle As New Style(GetType(CellContentPresenter)) With {.BasedOn = CellStyle.BasedOn}
				For Each tg As DataTrigger In CellStyle.Triggers
					NewCellStyle.Triggers.Add(tg)
				Next tg
				NewCellStyle.Triggers.Add([option].StyleTrigger)
				CellStyle = NewCellStyle
			End If
		End Sub

		Private Sub RowTriggersAdding(ByVal [option] As StyleOption)
			Dim NewRowStyle As New Style(GetType(GridRowContent)) With {.BasedOn = RowStyle.BasedOn}
				For Each tg As DataTrigger In RowStyle.Triggers
					NewRowStyle.Triggers.Add(tg)
				Next tg
			NewRowStyle.Triggers.Add([option].StyleTrigger)
			RowStyle = NewRowStyle
		End Sub

		Private Sub BarButtonItem_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
			ExpressionWindow = New AppearanceEditWindow((CType(ExprButton.DataContext, GridColumnMenuInfo)).Column, StylesCollection)
			ExpressionWindow.Show()
		End Sub
	End Class
End Namespace
