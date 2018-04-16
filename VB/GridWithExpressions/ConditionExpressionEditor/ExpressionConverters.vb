Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Data
Imports System.Globalization
Imports DevExpress.Data.Filtering.Helpers
Imports System.ComponentModel

Namespace GridWithExpressions
	Public Class ExpressionConverter
		Implements IValueConverter
		Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
			Dim eval As New ExpressionEvaluator(TypeDescriptor.GetProperties(CType(value, Task)), parameter.ToString())
			Dim result As Boolean = eval.Fit(CType(value, Task))
			Return result
		End Function

		Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
			Return value
		End Function
	End Class

	Public Class InternalStateConverter
		Implements IValueConverter
		Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
			If value IsNot Nothing Then
				If value.GetType() Is GetType(StyleOption) Then
					Return True
				ElseIf value.GetType() Is GetType(Integer) Then
					Dim count As Integer = System.Convert.ToInt32(value)
					Return If(count = 0, False, True)
				End If
			End If
			Return False
		End Function

		Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
			Return value
		End Function
	End Class

	'public class FontSizeConverter : IValueConverter {
	'    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
	'        if (value is double)
	'            return value;
	'        if (value is string)
	'            return double.Parse(value as string);
	'        return DependencyProperty.UnsetValue;
	'    }

	'    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
	'        if (value == null)
	'            return value;
	'        return value.ToString();
	'    }
	'}
End Namespace
