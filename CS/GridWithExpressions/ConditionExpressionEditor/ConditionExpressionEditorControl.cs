using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpf.Editors.ExpressionEditor;
using DevExpress.Data;

namespace GridWithExpressions {
    class ConditionExpressionEditorControl : ExpressionEditorControl {
        public ConditionExpressionEditorLogic EditorLogic { get { return (ConditionExpressionEditorLogic)fEditorLogic; } set { value = (ConditionExpressionEditorLogic)fEditorLogic; } }
        public ConditionExpressionEditorControl() {
            
        }
        public ConditionExpressionEditorControl(IDataColumnInfo columnInfo)
            : base(columnInfo) {

        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            fEditorLogic = new ConditionExpressionEditorLogic(this, ColumnInfo);
            fEditorLogic.Initialize();
            fEditorLogic.OnLoad();
        }

        public void SetExpression(string expression) {
            EditorLogic.SetExpression(expression);
        }
    }
}
