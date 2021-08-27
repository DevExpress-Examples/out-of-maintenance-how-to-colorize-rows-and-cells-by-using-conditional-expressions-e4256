<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128648972/12.1.7%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4256)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [AppearanceEditWindow.xaml](./CS/GridWithExpressions/ConditionExpressionEditor/AppearanceEditWindow.xaml) (VB: [AppearanceEditWindow.xaml](./VB/GridWithExpressions/ConditionExpressionEditor/AppearanceEditWindow.xaml))
* [AppearanceEditWindow.xaml.cs](./CS/GridWithExpressions/ConditionExpressionEditor/AppearanceEditWindow.xaml.cs) (VB: [AppearanceEditWindow.xaml.vb](./VB/GridWithExpressions/ConditionExpressionEditor/AppearanceEditWindow.xaml.vb))
* [ConditionExpressionEditorControl.cs](./CS/GridWithExpressions/ConditionExpressionEditor/ConditionExpressionEditorControl.cs) (VB: [ConditionExpressionEditorControl.vb](./VB/GridWithExpressions/ConditionExpressionEditor/ConditionExpressionEditorControl.vb))
* [ConditionExpressionEditorLogic.cs](./CS/GridWithExpressions/ConditionExpressionEditor/ConditionExpressionEditorLogic.cs) (VB: [ConditionExpressionEditorLogic.vb](./VB/GridWithExpressions/ConditionExpressionEditor/ConditionExpressionEditorLogic.vb))
* [ExpressionColumnBehavior.cs](./CS/GridWithExpressions/ConditionExpressionEditor/ExpressionColumnBehavior.cs) (VB: [ExpressionColumnBehavior.vb](./VB/GridWithExpressions/ConditionExpressionEditor/ExpressionColumnBehavior.vb))
* [ExpressionColumnInfo.cs](./CS/GridWithExpressions/ConditionExpressionEditor/ExpressionColumnInfo.cs) (VB: [ExpressionColumnInfo.vb](./VB/GridWithExpressions/ConditionExpressionEditor/ExpressionColumnInfo.vb))
* [ExpressionConverters.cs](./CS/GridWithExpressions/ConditionExpressionEditor/ExpressionConverters.cs) (VB: [ExpressionConverters.vb](./VB/GridWithExpressions/ConditionExpressionEditor/ExpressionConverters.vb))
* [StyleOption.cs](./CS/GridWithExpressions/ConditionExpressionEditor/StyleOption.cs) (VB: [StyleOption.vb](./VB/GridWithExpressions/ConditionExpressionEditor/StyleOption.vb))
* **[MainWindow.xaml](./CS/GridWithExpressions/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/GridWithExpressions/MainWindow.xaml))**
* [MainWindow.xaml.cs](./CS/GridWithExpressions/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/GridWithExpressions/MainWindow.xaml.vb))
* [Task.cs](./CS/GridWithExpressions/Task.cs) (VB: [Task.vb](./VB/GridWithExpressions/Task.vb))
<!-- default file list end -->
# How to: Colorize rows and cells by using conditional expressions


<p>This example illustrates how to use some conditional expressions to colorize the GridControl's cells and rows.</p>


<h3>Description</h3>

<p>This functionality was implemented by an attached behavior for TableView which encapsulates all the functionality. In addition, this approach allows you to add new style options in XAML or in a code-behind file of your application.</p>
<p>We also added a new "Format Condition Editor" item to the column popup menu.<br> <img data-image="b7ee221a-0dd0-4878-8423-e12669307fa4"><br> &nbsp;<br> This item will show you a dialog window for adding new styles and deleting and editing existing styles.</p>
<p><img data-image="761ea06b-2214-4cc1-9ac0-b374e39bf964"></p>

<br/>


