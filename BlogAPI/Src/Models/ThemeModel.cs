using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogAPI.Src.Models
{
    [Table("tb_themes")]
    public class Theme
    {
        #region Attributes

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }

        [JsonIgnore, InverseProperty("Theme")]
        public List<Post> RelatedPosts { get; set; }

        #endregion
    }
}
