using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows;
using DevExpress.Xpf.Grid;
using System.Windows.Data;
using System.ComponentModel;

namespace GridWithExpressions {
    public class StyleOption : INotifyPropertyChanged {
        SolidColorBrush backgroundBrush;
        SolidColorBrush foregroundBrush;
        FontStyle fontStyle;
        FontFamily fontFamily;
        int fontSize;

        public SolidColorBrush BackgroundBrush {
            get {
                return backgroundBrush;
            }
            set {
                if (backgroundBrush == null)
                    backgroundBrush = value;
                else
                    backgroundBrush.Color = ((SolidColorBrush)value).Color; 
                OnPropertyChanged("BackgroundBrush"); 
            }
        }
        public SolidColorBrush ForegroundBrush {
            get {
                return foregroundBrush;
            }
            set {
                if (foregroundBrush == null)
                    foregroundBrush = value;
                else
                    foregroundBrush.Color = ((SolidColorBrush)value).Color; 
                OnPropertyChanged("ForegroundBrush"); 
            }
        }
        public FontStyle CurrentFontStyle {
            get {
                return fontStyle;
            }
            set { fontStyle = value; OnPropertyChanged("CurrentFontStyle"); }
        }
        public FontFamily CurrentFontFamily {
            get {
                return fontFamily;
            }
            set { fontFamily = value; OnPropertyChanged("CurrentFontFamily"); }
        }
        public int CurrentFontSize {
            get {
                return fontSize;
            }
            set {
                fontSize = value; OnPropertyChanged("CurrentFontSize");
            }
        }
        public String ExpressionString { get; set; }
        public String FieldName { get; set; }
        public bool ApplyToRow { get; set; }
        public DataTrigger StyleTrigger { get; set; }

        public StyleOption Clone() {
            StyleOption newOpt = new StyleOption();

            newOpt.BackgroundBrush = BackgroundBrush;
            newOpt.ForegroundBrush = ForegroundBrush;
            newOpt.CurrentFontStyle = CurrentFontStyle;
            newOpt.CurrentFontFamily = CurrentFontFamily;
            newOpt.CurrentFontSize = CurrentFontSize;
            newOpt.ExpressionString = ExpressionString;
            newOpt.FieldName = FieldName;
            newOpt.ApplyToRow = ApplyToRow;

            return newOpt;
        }

        public StyleOption() {
            backgroundBrush = new SolidColorBrush(Colors.White);
            foregroundBrush = new SolidColorBrush(Colors.Black);
            fontFamily = new FontFamily("Times New Roman");
            fontStyle = FontStyles.Normal;
            fontSize = 14;
            ApplyToRow = false;
        }

        #region INotifyMembers
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        public bool IsInitialized() {
            return BackgroundBrush != null && ForegroundBrush != null && CurrentFontFamily != null && CurrentFontStyle != null && !String.IsNullOrEmpty(ExpressionString) && !String.IsNullOrEmpty(FieldName);
        }

        public void GenerateSetters() {
            Binding triggerBinding = new Binding() { Converter = new ExpressionConverter(), ConverterParameter = ExpressionString };
            StyleTrigger = new DataTrigger() { Value = true, Binding = triggerBinding };

            if (!ApplyToRow) {

                triggerBinding.Path = new PropertyPath("RowData.Row", null);

                StyleTrigger.Setters.Add(new Setter(CellContentPresenter.ForegroundProperty, new Binding("ForegroundBrush") { Source = this }));
                StyleTrigger.Setters.Add(new Setter(CellContentPresenter.BackgroundProperty, new Binding("BackgroundBrush") { Source = this }));
                StyleTrigger.Setters.Add(new Setter(CellContentPresenter.FontFamilyProperty, new Binding("CurrentFontFamily") { Source = this }));
                StyleTrigger.Setters.Add(new Setter(CellContentPresenter.FontStyleProperty, new Binding("CurrentFontStyle") { Source = this }));
                StyleTrigger.Setters.Add(new Setter(CellContentPresenter.FontSizeProperty, new Binding("CurrentFontSize") { Source = this }));
            }
            else {
                triggerBinding.Path = new PropertyPath("Row", null);

                StyleTrigger.Setters.Add(new Setter(GridRowContent.ForegroundProperty, new Binding("ForegroundBrush") { Source = this }));
                StyleTrigger.Setters.Add(new Setter(GridRowContent.BackgroundProperty, new Binding("BackgroundBrush") { Source = this }));
                StyleTrigger.Setters.Add(new Setter(GridRowContent.FontFamilyProperty, new Binding("CurrentFontFamily") { Source = this }));
                StyleTrigger.Setters.Add(new Setter(GridRowContent.FontStyleProperty, new Binding("CurrentFontStyle") { Source = this }));
                StyleTrigger.Setters.Add(new Setter(GridRowContent.FontSizeProperty, new Binding("CurrentFontSize") { Source = this }));
            }
        }

    }
}
