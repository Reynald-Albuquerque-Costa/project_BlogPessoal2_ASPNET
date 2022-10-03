using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogAPI.Src.Models
{
    [Table("tb_users")]
    public class User
    {
        #region Attributes

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }

        [JsonIgnore, InverseProperty("Creator")]
        public List<Post> Mypost { get; set; }

        #endregion
    }
}
