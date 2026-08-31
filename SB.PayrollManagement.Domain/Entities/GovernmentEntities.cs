using System.ComponentModel.DataAnnotations;

namespace SB.PayrollManagement.Domain.Entities
{
    public class GovernmentEntities
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string StatePower { get; set; }

        public string Sector { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
