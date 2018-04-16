Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Media
Imports System.Windows
Imports DevExpress.Xpf.Grid
Imports System.Windows.Data
Imports System.ComponentModel

Namespace GridWithExpressions
	Public Class StyleOption
		Implements INotifyPropertyChanged
		Private _backgroundBrush As SolidColorBrush
		Private _foregroundBrush As SolidColorBrush
		Private fontStyle As FontStyle
		Private fontFamily As FontFamily
		Private fontSize As Integer

		Public Property BackgroundBrush() As SolidColorBrush
			Get
				Return _backgroundBrush
			End Get
			Set(ByVal value As SolidColorBrush)
				If _backgroundBrush Is Nothing Then
					_backgroundBrush = value
				Else
					_backgroundBrush.Color = (CType(value, SolidColorBrush)).Color
				End If
				OnPropertyChanged("BackgroundBrush")
			End Set
		End Property
		Public Property ForegroundBrush() As SolidColorBrush
			Get
				Return _foregroundBrush
			End Get
			Set(ByVal value As SolidColorBrush)
				If _foregroundBrush Is Nothing Then
					_foregroundBrush = value
				Else
					_foregroundBrush.Color = (CType(value, SolidColorBrush)).Color
				End If
				OnPropertyChanged("ForegroundBrush")
			End Set
		End Property
		Public Property CurrentFontStyle() As FontStyle
			Get
				Return fontStyle
			End Get
			Set(ByVal value As FontStyle)
				fontStyle = value
				OnPropertyChanged("CurrentFontStyle")
			End Set
		End Property
		Public Property CurrentFontFamily() As FontFamily
			Get
				Return fontFamily
			End Get
			Set(ByVal value As FontFamily)
				fontFamily = value
				OnPropertyChanged("CurrentFontFamily")
			End Set
		End Property
		Public Property CurrentFontSize() As Integer
			Get
				Return fontSize
			End Get
			Set(ByVal value As Integer)
				fontSize = value
				OnPropertyChanged("CurrentFontSize")
			End Set
		End Property
		Private privateExpressionString As String
		Public Property ExpressionString() As String
			Get
				Return privateExpressionString
			End Get
			Set(ByVal value As String)
				privateExpressionString = value
			End Set
		End Property
		Private privateFieldName As String
		Public Property FieldName() As String
			Get
				Return privateFieldName
			End Get
			Set(ByVal value As String)
				privateFieldName = value
			End Set
		End Property
		Private privateApplyToRow As Boolean
		Public Property ApplyToRow() As Boolean
			Get
				Return privateApplyToRow
			End Get
			Set(ByVal value As Boolean)
				privateApplyToRow = value
			End Set
		End Property
		Private privateStyleTrigger As DataTrigger
		Public Property StyleTrigger() As DataTrigger
			Get
				Return privateStyleTrigger
			End Get
			Set(ByVal value As DataTrigger)
				privateStyleTrigger = value
			End Set
		End Property

		Public Function Clone() As StyleOption
			Dim newOpt As New StyleOption()

			newOpt.BackgroundBrush = BackgroundBrush
			newOpt.ForegroundBrush = ForegroundBrush
			newOpt.CurrentFontStyle = CurrentFontStyle
			newOpt.CurrentFontFamily = CurrentFontFamily
			newOpt.CurrentFontSize = CurrentFontSize
			newOpt.ExpressionString = ExpressionString
			newOpt.FieldName = FieldName
			newOpt.ApplyToRow = ApplyToRow

			Return newOpt
		End Function

		Public Sub New()
			_backgroundBrush = New SolidColorBrush(Colors.White)
			_foregroundBrush = New SolidColorBrush(Colors.Black)
			fontFamily = New FontFamily("Times New Roman")
			fontStyle = FontStyles.Normal
			fontSize = 14
			ApplyToRow = False
		End Sub

		#Region "INotifyMembers"
		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

		Protected Sub OnPropertyChanged(ByVal propertyName As String)
			Dim handler As PropertyChangedEventHandler = PropertyChangedEvent

			If handler IsNot Nothing Then
				handler(Me, New PropertyChangedEventArgs(propertyName))
			End If
		End Sub
		#End Region
		Public Function IsInitialized() As Boolean
            Return BackgroundBrush IsNot Nothing AndAlso ForegroundBrush IsNot Nothing AndAlso CurrentFontFamily IsNot Nothing AndAlso Not Object.ReferenceEquals(CurrentFontStyle, Nothing) AndAlso (Not String.IsNullOrEmpty(ExpressionString)) AndAlso Not String.IsNullOrEmpty(FieldName)
		End Function

		Public Sub GenerateSetters()
			Dim triggerBinding As New Binding() With {.Converter = New ExpressionConverter(), .ConverterParameter = ExpressionString}
			StyleTrigger = New DataTrigger() With {.Value = True, .Binding = triggerBinding}

			If (Not ApplyToRow) Then

				triggerBinding.Path = New PropertyPath("RowData.Row", Nothing)

				StyleTrigger.Setters.Add(New Setter(CellContentPresenter.ForegroundProperty, New Binding("ForegroundBrush") With {.Source = Me}))
				StyleTrigger.Setters.Add(New Setter(CellContentPresenter.BackgroundProperty, New Binding("BackgroundBrush") With {.Source = Me}))
				StyleTrigger.Setters.Add(New Setter(CellContentPresenter.FontFamilyProperty, New Binding("CurrentFontFamily") With {.Source = Me}))
				StyleTrigger.Setters.Add(New Setter(CellContentPresenter.FontStyleProperty, New Binding("CurrentFontStyle") With {.Source = Me}))
				StyleTrigger.Setters.Add(New Setter(CellContentPresenter.FontSizeProperty, New Binding("CurrentFontSize") With {.Source = Me}))
			Else
				triggerBinding.Path = New PropertyPath("Row", Nothing)

				StyleTrigger.Setters.Add(New Setter(GridRowContent.ForegroundProperty, New Binding("ForegroundBrush") With {.Source = Me}))
				StyleTrigger.Setters.Add(New Setter(GridRowContent.BackgroundProperty, New Binding("BackgroundBrush") With {.Source = Me}))
				StyleTrigger.Setters.Add(New Setter(GridRowContent.FontFamilyProperty, New Binding("CurrentFontFamily") With {.Source = Me}))
				StyleTrigger.Setters.Add(New Setter(GridRowContent.FontStyleProperty, New Binding("CurrentFontStyle") With {.Source = Me}))
				StyleTrigger.Setters.Add(New Setter(GridRowContent.FontSizeProperty, New Binding("CurrentFontSize") With {.Source = Me}))
			End If
		End Sub

	End Class
End Namespace
