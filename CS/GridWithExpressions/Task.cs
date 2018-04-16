using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridWithExpressions {
    public class Task {

        public Task(string name, DateTime start, DateTime finish, bool isComplete) {
            Name = name;
            StartDate = start;
            FinishDate = finish;
            IsCompleted = isComplete;
        }

        public string Name { get; set; }
        public DateTime FinishDate { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
