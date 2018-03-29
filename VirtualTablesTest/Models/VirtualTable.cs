using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualTablesTest.Models
{
    [Table(nameof(VirtualTable))]
    public class VirtualTable
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<VirtualColumn> Columns { get; set; }
        public List<VirtualRow> Rows { get; set; }
    }


}
