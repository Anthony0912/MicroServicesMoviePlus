using FluentValidation;
using Service.api.Movie.Entities;

namespace Service.api.Movie.Validator
{
    public class MovieValidator : AbstractValidator<EMovie>
    {
        public MovieValidator()
        {
            RuleFor(v => v.Title).NotEmpty().WithMessage("El campo título es requerido");
            RuleFor(v => v.ReleaseDate).NotEmpty().WithMessage("El campo fecha de lanzamiento es requerida");
            RuleFor(v => v.Director).NotEmpty().WithMessage("El campo director es requerido");
            RuleFor(v => v.Genre).NotEmpty().WithMessage("El campo genero es requerido");
            RuleFor(v => v.Rating).NotEmpty().WithMessage("El campo clasificación es requerido");
            RuleFor(v => v.Duration).NotEmpty().WithMessage("El campo duración es requerido");
            RuleFor(v => v.Language).NotEmpty().WithMessage("El campo lenguaje es requerido");
            RuleFor(v => v.Country).NotEmpty().WithMessage("El campo país es requerido");
            RuleFor(v => v.Budget).NotEmpty().WithMessage("El campo presupuesto es requerido");
            RuleFor(v => v.BoxOffice).NotEmpty().WithMessage("El campo taquilla es requerido");
            RuleFor(v => v.ProductionCompany).NotEmpty().WithMessage("El campo empresa productora es requerido");
            RuleFor(v => v.Cast).NotEmpty().WithMessage("El campo casting es requerido");
            RuleFor(v => v.Plot).NotEmpty().WithMessage("El campo trama es requerido");
            RuleFor(v => v.PosterUrl).NotEmpty().WithMessage("El campo url del póster es requerido");
            RuleFor(v => v.TrailerUrl).NotEmpty().WithMessage("El campo url del tráiler es requerido");
            RuleFor(v => v.Awards).NotEmpty().WithMessage("El campo premios es requerido");
            RuleFor(v => v.Keywords).NotEmpty().WithMessage("El campo palabras claves es requerido");
            RuleFor(v => v.ImdbRating).NotEmpty().WithMessage("El campo clasificación imdb es requerido");
            RuleFor(v => v.RottenTomatoesRating).NotEmpty().WithMessage("El campo clasificación Rotten Tomatoes es requerido");
            RuleFor(v => v.MetacriticRating).NotEmpty().WithMessage("El campo clasificación de crítica es requerido");
        }
    }
}
