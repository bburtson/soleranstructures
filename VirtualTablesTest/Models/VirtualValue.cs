using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualTablesTest.Models
{
    [Table(nameof(VirtualValue))]
    public class VirtualValue
    {
        public long Id { get; set; }
        public string Value { get; set; }

        public long VirtualColumnId { get; set; }
        public long VirtualRowId { get; set; }

        [ForeignKey(nameof(VirtualColumnId))]
        public VirtualColumn Column { get; set; }

        [ForeignKey(nameof(VirtualRowId))]
        public VirtualRow Row { get; set; }
    }
}