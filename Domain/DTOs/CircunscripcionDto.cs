using FluentValidation;

namespace Domain.DTOs
{
    public class CircunscripcionDto 
    {
        public string Numero_Circunscripcion { get; set; }
        public string Nombre_Circunscripcion { get; set; }
        public int Codigo_Subprograma { get; set; }
    }

    public class CircunscripcionValidator : AbstractValidator<CircunscripcionDto>
    {
        public CircunscripcionValidator()
        {
            RuleFor(c => c.Numero_Circunscripcion)
                .NotEmpty().WithMessage("El número de circunscripción es obligatorio")
                .Must(c => !string.IsNullOrEmpty(c) && !c.Any(char.IsLetter))
                .WithMessage("El número de circunscripción no puede contener letras");

            RuleFor(c => c.Nombre_Circunscripcion)
                .NotEmpty().WithMessage("El nombre de circunscripción es obligatorio")
                .Must(c => !string.IsNullOrEmpty(c) && !c.Any(char.IsDigit))
                .WithMessage("El nombre de circunscripción no puede contener números");

            RuleFor(c => c.Codigo_Subprograma)
                .NotEmpty().WithMessage("El código de subprograma es obligatorio")
                .Must(c => c.ToString().All(char.IsDigit))
                .WithMessage("El código de subprograma debe contener solo números");
        }
    }
}
