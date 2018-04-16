using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.ExpressionEditor;
using DevExpress.Data;

namespace GridWithExpressions {
    class ConditionExpressionEditorLogic : ExpressionEditorLogicEx {
        public ConditionExpressionEditorLogic(IExpressionEditor editor, IDataColumnInfo columnInfo)
            : base(editor, columnInfo) {

        }

        public void SetExpression(string expression) {
            ExpressionMemoEdit.Text = expression;
        }
    }
}
