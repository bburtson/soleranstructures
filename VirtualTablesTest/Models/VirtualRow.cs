using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualTablesTest.Models
{
    [Table(nameof(VirtualRow))]
    public class VirtualRow
    {
        public long Id { get; set; }

        public long VirtualTableId { get; set; }

        /// <summary>
        /// The table in which this row lives
        /// </summary>
        [ForeignKey("VirtualTableId")]
        public VirtualTable Table { get; set; }

        public List<VirtualValue> Values { get; set; }


    }
}