using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.api.Movie.Entities
{
    public class EMovie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string Title { get; set; } = "";

        [Column(TypeName = "varchar(50)")]
        public string ReleaseDate { get; set; } = "";

        [Column(TypeName = "varchar(100)")]
        public string Director { get; set; } = "";

        [Column(TypeName = "varchar(50)")]
        public string Genre { get; set; } = "";

        [Column(TypeName = "varchar(50)")]
        public string Rating { get; set; } = "";

        [Column(TypeName = "varchar(50)")]
        public string Duration { get; set; } = "";

        [Column(TypeName = "varchar(50)")]
        public string Language { get; set; } = "";

        [Column(TypeName = "varchar(50)")]
        public string Country { get; set; } = "";

        [Column(TypeName = "int")]
        public int Budget { get; set; } = 0;

        [Column(TypeName = "int")]
        public int BoxOffice { get; set; } = 0;

        [Column(TypeName = "varchar(50)")]
        public string ProductionCompany { get; set; } = "";

        [Column(TypeName = "varchar(50)")]
        public string Cast { get; set; } = "";

        [Column(TypeName = "varchar(600)")]
        public string Plot { get; set; } = "";

        [Column(TypeName = "varchar(MAX)")]
        public string PosterUrl { get; set; } = "";

        [Column(TypeName = "varchar(MAX)")]
        public string TrailerUrl { get; set; } = "";

        [Column(TypeName = "varchar(50)")]
        public string Awards { get; set; } = "";

        [Column(TypeName = "varchar(150)")]
        public string Keywords { get; set; } = "";

        [Column(TypeName = "varchar(50)")]
        public string ImdbRating { get; set; } = "";

        [Column(TypeName = "varchar(50)")]
        public string RottenTomatoesRating { get; set; } = "";

        [Column(TypeName = "varchar(50)")]
        public string MetacriticRating { get; set; } = "";
    }
}
