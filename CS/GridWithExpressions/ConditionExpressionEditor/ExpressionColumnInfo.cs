using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data;
using DevExpress.Xpf.Grid;

namespace GridWithExpressions {
    class ExpressionColumnInfo :IDataColumnInfo {
        IDataColumnInfo columnInfo;
        List<IDataColumnInfo> columns;

        public ExpressionColumnInfo(GridColumn column) {
            columnInfo = (IDataColumnInfo)column;
            FillColumnsList();
        }
        void FillColumnsList() {
            columns = new List<IDataColumnInfo>();
            foreach (GridColumn col in columnInfo.Columns) {
                columns.Add(col);
            }
            columns.Add(columnInfo);
        }

        public string Caption {
            get { return columnInfo.Caption; }
        }

        public List<IDataColumnInfo> Columns {
            get { return columns; }
        }

        public DataControllerBase Controller {
            get { return columnInfo.Controller; }
        }

        public string FieldName {
            get { return columnInfo.FieldName; }
        }

        public Type FieldType {
            get { return columnInfo.FieldType; }
        }

        public string Name {
            get { return columnInfo.Name; }
        }

        public string UnboundExpression {
            get { return String.Empty; }
        }
    }
}
