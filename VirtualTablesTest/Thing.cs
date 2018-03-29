using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualTablesTest
{
    [Table("Thing")]
    public class Thing
    {
        public long ThingId { get; set; }
        public string Name { get; set; }
    }
}