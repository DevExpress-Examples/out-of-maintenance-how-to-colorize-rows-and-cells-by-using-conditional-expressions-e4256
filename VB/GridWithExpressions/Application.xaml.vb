Imports DevExpress.Xpf.Core
Imports System.Windows

Namespace GridWithExpressions
	''' <summary>
	''' Interaction logic for App.xaml
	''' </summary>
	Partial Public Class App
		Inherits Application

		Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
			MyBase.OnStartup(e)
			ThemeManager.ApplicationThemeName = "MetropolisDark"
		End Sub
	End Class
End Namespace
