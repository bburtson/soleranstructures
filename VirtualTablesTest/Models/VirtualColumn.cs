using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualTablesTest.Models
{
    [Table(nameof(VirtualColumn))]
    public class VirtualColumn
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public VirtualDataType VirtualDataType { get; set; }

        public long VirtualTableId { get; set; }

        /// <summary>
        /// The VirtualTable in which this column lives
        /// </summary>
        [ForeignKey("VirtualTableId")]
        public VirtualTable Table { get; set; }

    }
}