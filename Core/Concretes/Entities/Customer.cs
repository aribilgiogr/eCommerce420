using Core.Abstracts.Bases;
using Microsoft.AspNetCore.Identity;

namespace Core.Concretes.Entities
{
    public class Customer : BaseEntity
    {
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string? ProfilePicture { get; set; }
        public string AccountId { get; set; } = null!;
    }
}
