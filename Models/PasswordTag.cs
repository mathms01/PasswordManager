using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordsManager.Models
{
    public class PasswordTag
    {
        public int PasswordId { get; set; }

        public int TagId { get; set; }

        [ForeignKey(nameof(PasswordId))]
        public Password Password { get; set; }

        [ForeignKey(nameof(TagId))]
        public Tag Tag { get; set; }

    }
}
