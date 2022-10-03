using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace BlogAPI.Src.Models
{
    [Table("tb_posts")]
    public class Post
    {
        #region Attributes

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        [ForeignKey("fk_user")]
        public User Creator { get; set; }
        [ForeignKey("fk_theme")]
        public Theme Theme { get; set; }

        #endregion
    }
}
