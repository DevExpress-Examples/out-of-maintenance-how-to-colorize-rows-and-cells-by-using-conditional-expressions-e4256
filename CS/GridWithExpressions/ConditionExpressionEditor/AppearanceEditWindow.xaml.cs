using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Grid;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core;
using System.Windows.Data;
using DevExpress.Xpf.Editors.Themes;
using System.Windows.Controls;

namespace GridWithExpressions {

    public partial class AppearanceEditWindow : DXWindow {
        ConditionExpressionEditorControl expressionEditor;
        public ObservableCollection<StyleOption> OptionsCollection { get; set; }
        ObservableCollection<StyleOption> PrimaryStyleCollection { get; set; }
        GridColumn EditableColumn { get; set; }
        ExpressionConverter Converter { get; set; }

        public AppearanceEditWindow(GridColumn column, ObservableCollection<StyleOption> opsCollection) {
            Loaded += AppearanceEditWindow_Loaded;
            EditableColumn = column;
            InternalCollectionInit(opsCollection);
            InitializeComponent();
            InternalEditorsInit();
            ApplyButtonsThemes();
        }

        void AppearanceEditWindow_Loaded(object sender, RoutedEventArgs e) {
            expressionEditor.SetExpression(((StyleOption)expressionsListBox.SelectedItem).ExpressionString);
        }

        void InternalCollectionInit(ObservableCollection<StyleOption> opsCollection) {
            PrimaryStyleCollection = opsCollection;
            OptionsCollection = new ObservableCollection<StyleOption>();
            for (int i = 0; i < opsCollection.Count; i++) {
                OptionsCollection.Add(opsCollection[i].Clone());                
            }
        }

        private void ApplyButtonsThemes() {
            TrackBarEditThemeKeyExtension addButtonTemplateRes = new TrackBarEditThemeKeyExtension() { ResourceKey = TrackBarEditThemeKeys.RightStepButtonTemplate, ThemeName = ThemeManager.GetThemeName(this) };
            addButton.Template = (ControlTemplate)FindResource(addButtonTemplateRes);
            TrackBarEditThemeKeyExtension deleteButtonTemplateRes = new TrackBarEditThemeKeyExtension() { ResourceKey = TrackBarEditThemeKeys.LeftStepButtonTemplate, ThemeName = ThemeManager.GetThemeName(this) };
            deleteButton.Template = (ControlTemplate)FindResource(deleteButtonTemplateRes);

        }
        void InternalEditorsInit() {
            expressionsListBox.DataContext = OptionsCollection;
            expressionEditor = new ConditionExpressionEditorControl(new ExpressionColumnInfo(EditableColumn));
            expressionEditor.LostFocus += expressionEditor_LostFocus;
            expressionEditor.GotFocus += expressionEditor_GotFocus;
            Binding enabledBinding = new Binding("SelectedItem") {ElementName = "expressionsListBox", Converter = new InternalStateConverter() };
            expressionEditor.SetBinding(IsEnabledProperty, enabledBinding);
            expressionGroup.Children.Add(expressionEditor);
            Converter = new ExpressionConverter();
            expressionsListBox.SelectedIndex = 0;
        }

        void expressionEditor_GotFocus(object sender, RoutedEventArgs e) {
            if (IsSelectedItem() && ((StyleOption)expressionsListBox.SelectedItem).ExpressionString.Equals("New Condition")) 
                    expressionEditor.SetExpression(String.Empty);
        }

        void expressionEditor_LostFocus(object sender, RoutedEventArgs e) {
            ((StyleOption)expressionsListBox.SelectedItem).ExpressionString = expressionEditor.Expression;
        }

        void SaveButtonClick(object sender, RoutedEventArgs e) {
            if (OptionsCollection.Count != 0 && IsSelectedItem()) {
                ((StyleOption)expressionsListBox.SelectedItem).FieldName = EditableColumn.FieldName;
                ((StyleOption)expressionsListBox.SelectedItem).ExpressionString = expressionEditor.Expression;
            }
            PrimaryStyleCollection.Clear();                       
            for (int i = 0; i < OptionsCollection.Count; i++) {           
                PrimaryStyleCollection.Add(OptionsCollection[i]);              
            }
            Close();
        }

        void AddOptionClick(object sender, RoutedEventArgs e) {
            StyleOption opt = new StyleOption() { ExpressionString = "New Condition" };
            OptionsCollection.Add(opt);
            expressionsListBox.SelectedIndex = OptionsCollection.Count-1;
        }

        void RemoveOptionClick(object sender, RoutedEventArgs e) {
            if (OptionsCollection.Count != 0) {
                expressionsListBox.SelectedIndex = 0;
                OptionsCollection.RemoveAt(expressionsListBox.SelectedIndex);
            }
        }

        void OptionEditValueChanged(object sender, EditValueChangedEventArgs e) {
            if (IsSelectedItem()) {
                expressionEditor.SetExpression(((StyleOption)expressionsListBox.SelectedItem).ExpressionString);
            }
        }

        void CancelButtonClick(object sender, RoutedEventArgs e) {
            Close();
        }

        bool IsSelectedItem() {
            if (expressionsListBox.SelectedItem != null)
                return true;
            return false;
        }
    }
}
