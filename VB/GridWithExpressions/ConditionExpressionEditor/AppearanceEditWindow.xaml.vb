Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports DevExpress.Xpf.Grid
Imports System.Collections.ObjectModel
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Core
Imports System.Windows.Data
Imports DevExpress.Xpf.Editors.Themes
Imports System.Windows.Controls

Namespace GridWithExpressions

	Partial Public Class AppearanceEditWindow
		Inherits DXWindow
		Private expressionEditor As ConditionExpressionEditorControl
		Private privateOptionsCollection As ObservableCollection(Of StyleOption)
		Public Property OptionsCollection() As ObservableCollection(Of StyleOption)
			Get
				Return privateOptionsCollection
			End Get
			Set(ByVal value As ObservableCollection(Of StyleOption))
				privateOptionsCollection = value
			End Set
		End Property
		Private privatePrimaryStyleCollection As ObservableCollection(Of StyleOption)
		Private Property PrimaryStyleCollection() As ObservableCollection(Of StyleOption)
			Get
				Return privatePrimaryStyleCollection
			End Get
			Set(ByVal value As ObservableCollection(Of StyleOption))
				privatePrimaryStyleCollection = value
			End Set
		End Property
		Private privateEditableColumn As GridColumn
		Private Property EditableColumn() As GridColumn
			Get
				Return privateEditableColumn
			End Get
			Set(ByVal value As GridColumn)
				privateEditableColumn = value
			End Set
		End Property
		Private privateConverter As ExpressionConverter
		Private Property Converter() As ExpressionConverter
			Get
				Return privateConverter
			End Get
			Set(ByVal value As ExpressionConverter)
				privateConverter = value
			End Set
		End Property

		Public Sub New(ByVal column As GridColumn, ByVal opsCollection As ObservableCollection(Of StyleOption))
			AddHandler Loaded, AddressOf AppearanceEditWindow_Loaded
			EditableColumn = column
			InternalCollectionInit(opsCollection)
			InitializeComponent()
			InternalEditorsInit()
			ApplyButtonsThemes()
		End Sub

		Private Sub AppearanceEditWindow_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			expressionEditor.SetExpression((CType(expressionsListBox.SelectedItem, StyleOption)).ExpressionString)
		End Sub

		Private Sub InternalCollectionInit(ByVal opsCollection As ObservableCollection(Of StyleOption))
			PrimaryStyleCollection = opsCollection
			OptionsCollection = New ObservableCollection(Of StyleOption)()
			For i As Integer = 0 To opsCollection.Count - 1
				OptionsCollection.Add(opsCollection(i).Clone())
			Next i
		End Sub

		Private Sub ApplyButtonsThemes()
			Dim addButtonTemplateRes As New TrackBarEditThemeKeyExtension() With {.ResourceKey = TrackBarEditThemeKeys.RightStepButtonTemplate, .ThemeName = ThemeManager.GetThemeName(Me)}
			addButton.Template = CType(FindResource(addButtonTemplateRes), ControlTemplate)
			Dim deleteButtonTemplateRes As New TrackBarEditThemeKeyExtension() With {.ResourceKey = TrackBarEditThemeKeys.LeftStepButtonTemplate, .ThemeName = ThemeManager.GetThemeName(Me)}
			deleteButton.Template = CType(FindResource(deleteButtonTemplateRes), ControlTemplate)

		End Sub
		Private Sub InternalEditorsInit()
			expressionsListBox.DataContext = OptionsCollection
			expressionEditor = New ConditionExpressionEditorControl(New ExpressionColumnInfo(EditableColumn))
			AddHandler expressionEditor.LostFocus, AddressOf expressionEditor_LostFocus
			AddHandler expressionEditor.GotFocus, AddressOf expressionEditor_GotFocus
			Dim enabledBinding As New Binding("SelectedItem") With {.ElementName = "expressionsListBox", .Converter = New InternalStateConverter()}
			expressionEditor.SetBinding(IsEnabledProperty, enabledBinding)
			expressionGroup.Children.Add(expressionEditor)
			Converter = New ExpressionConverter()
			expressionsListBox.SelectedIndex = 0
		End Sub

		Private Sub expressionEditor_GotFocus(ByVal sender As Object, ByVal e As RoutedEventArgs)
			If IsSelectedItem() AndAlso (CType(expressionsListBox.SelectedItem, StyleOption)).ExpressionString.Equals("New Condition") Then
					expressionEditor.SetExpression(String.Empty)
			End If
		End Sub

		Private Sub expressionEditor_LostFocus(ByVal sender As Object, ByVal e As RoutedEventArgs)
			CType(expressionsListBox.SelectedItem, StyleOption).ExpressionString = expressionEditor.Expression
		End Sub

		Private Sub SaveButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
			If OptionsCollection.Count <> 0 AndAlso IsSelectedItem() Then
				CType(expressionsListBox.SelectedItem, StyleOption).FieldName = EditableColumn.FieldName
				CType(expressionsListBox.SelectedItem, StyleOption).ExpressionString = expressionEditor.Expression
			End If
			PrimaryStyleCollection.Clear()
			For i As Integer = 0 To OptionsCollection.Count - 1
				PrimaryStyleCollection.Add(OptionsCollection(i))
			Next i
			Close()
		End Sub

		Private Sub AddOptionClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim opt As New StyleOption() With {.ExpressionString = "New Condition"}
			OptionsCollection.Add(opt)
			expressionsListBox.SelectedIndex = OptionsCollection.Count-1
		End Sub

		Private Sub RemoveOptionClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
			If OptionsCollection.Count <> 0 Then
				expressionsListBox.SelectedIndex = 0
				OptionsCollection.RemoveAt(expressionsListBox.SelectedIndex)
			End If
		End Sub

		Private Sub OptionEditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
			If IsSelectedItem() Then
				expressionEditor.SetExpression((CType(expressionsListBox.SelectedItem, StyleOption)).ExpressionString)
			End If
		End Sub

		Private Sub CancelButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Close()
		End Sub

		Private Function IsSelectedItem() As Boolean
			If expressionsListBox.SelectedItem IsNot Nothing Then
				Return True
			End If
			Return False
		End Function
	End Class
End Namespace
