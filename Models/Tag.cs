using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordsManager.Models
{
    public class Tag
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required]
        public string Label { get; set; }

        [InverseProperty(nameof(PasswordTag.Tag))]
        public List<PasswordTag> Passwords { get; set; }
    }
}
