using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Interactivity;
using DevExpress.Xpf.Grid;
using System.Windows;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid.Themes;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace GridWithExpressions {
    class ExpressionColumnBehavior : Behavior<TableView> {

        #region StylesCollection
        
        public static readonly DependencyProperty StylesCollectionProperty = DependencyProperty.Register("StylesCollection",
                                                                                 typeof(ObservableCollection<StyleOption>),
                                                                                 typeof(ExpressionColumnBehavior));

        public ObservableCollection<StyleOption> StylesCollection {
            get {
                return (ObservableCollection<StyleOption>)GetValue(StylesCollectionProperty);
            }
            set {
                SetValue(StylesCollectionProperty, value);
            }
        }

        public ExpressionColumnBehavior() {
            StylesCollection = new ObservableCollection<StyleOption>();
            StylesCollection.CollectionChanged += StylesCollection_CollectionChanged;
            IsLoading = true;
        }

        void StylesCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (!IsLoading) {
                if (e.Action == NotifyCollectionChangedAction.Add) {
                    CollectionModify();
                }
                else if (e.Action == NotifyCollectionChangedAction.Reset) {
                    RowStyleBuilding();
                    View.RowStyle = RowStyle;
                    CellStyleBuilding();
                    for (int i = 0; i < ((GridControl)View.Parent).Columns.Count; i++) {
                        if (((GridControl)View.Parent).Columns[i].CellStyle != null)
                            ((GridControl)View.Parent).Columns[i].CellStyle = CellStyle;
                    }
                }
            }
        }

        private void CollectionModify() {
            for (int i = 0; i < StylesCollection.Count; i++) {
                if (StylesCollection[i].IsInitialized()) {
                    StylesCollection[i].GenerateSetters();

                    if (!StylesCollection[i].ApplyToRow) {
                        CellTriggersAdding(StylesCollection[i]);
                        ((GridControl)View.Parent).Columns[StylesCollection[i].FieldName].CellStyle = CellStyle;
                    }
                    else {
                        RowTriggersAdding(StylesCollection[i]);
                        View.RowStyle = RowStyle;
                    }
                }
            }
        }

        #endregion
        TableView View { get; set; }
        Style RowStyle { get; set; }
        Style CellStyle { get; set; }
        AppearanceEditWindow ExpressionWindow { get; set; }
        BarButtonItem ExprButton { get; set; }
        bool IsLoading { get; set; }
              
        protected override void OnAttached() {
            base.OnAttached();
            View = AssociatedObject;
            View.Loaded += View_Loaded;
            EditorBarButtonBuilding();
            AssociatedObject.ColumnMenuCustomizations.Add(ExprButton);
            RowStyleBuilding();
        }

        void View_Loaded(object sender, RoutedEventArgs e) {
            IsLoading = false;
            CollectionModify();
        }

        protected override void OnDetaching() {
            base.OnDetaching();
            ExprButton.ItemClick -= BarButtonItem_ItemClick;
        }

        private void EditorBarButtonBuilding() {
            ExprButton = new BarButtonItem() { Name = "barButtonItem1", Content = "Format Conditions Editor" };
            ExprButton.ItemClick += BarButtonItem_ItemClick;
        }

        private void RowStyleBuilding() {
            GridRowThemeKeyExtension gridRowThemeKeyExtension = new GridRowThemeKeyExtension() { ResourceKey = GridRowThemeKeys.RowStyle };
            RowStyle = new Style(typeof(GridRowContent)) { BasedOn = AssociatedObject.FindResource(gridRowThemeKeyExtension) as Style };
        }

        private void CellStyleBuilding() {
            GridRowThemeKeyExtension gridCellThemeKeyExtension = new GridRowThemeKeyExtension() { ResourceKey = GridRowThemeKeys.CellStyle };
            CellStyle = new Style(typeof(CellContentPresenter)) { BasedOn = AssociatedObject.FindResource(gridCellThemeKeyExtension) as Style };
        }
        private void CellTriggersAdding(StyleOption option) {
            if (((GridControl)View.Parent).Columns[option.FieldName].CellStyle == null) {
                CellStyleBuilding();
                CellStyle.Triggers.Add(option.StyleTrigger);
            }
            else {
                Style NewCellStyle = new Style(typeof(CellContentPresenter)) { BasedOn = CellStyle.BasedOn };
                foreach (DataTrigger tg in CellStyle.Triggers) {
                    NewCellStyle.Triggers.Add(tg);
                }
                NewCellStyle.Triggers.Add(option.StyleTrigger);
                CellStyle = NewCellStyle;
            }
        }

        private void RowTriggersAdding(StyleOption option) {
            Style NewRowStyle = new Style(typeof(GridRowContent)) { BasedOn = RowStyle.BasedOn };
                foreach (DataTrigger tg in RowStyle.Triggers) {
                    NewRowStyle.Triggers.Add(tg);
                }
            NewRowStyle.Triggers.Add(option.StyleTrigger);
            RowStyle = NewRowStyle;
        }

        void BarButtonItem_ItemClick(object sender, ItemClickEventArgs e) {
            ExpressionWindow = new AppearanceEditWindow(((GridColumnMenuInfo)ExprButton.DataContext).Column, StylesCollection);
            ExpressionWindow.Show();
        }
    }
}
