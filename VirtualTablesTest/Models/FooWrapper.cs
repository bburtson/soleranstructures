using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualTablesTest.Models
{
    public class FooWrapper
    {
        public VirtualTable Table { get; set; }

        public IEnumerable<VirtualColumn> Columns { get; set; }

        public VirtualRow ModelRow { get; set; }
    }
}
