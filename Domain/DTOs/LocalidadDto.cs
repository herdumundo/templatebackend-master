using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class LocalidadDto
    {
        public int Codigo_Circunscripcion { get; set; }
        public String Nombre { get; set; }
    }

    public class LocalidadesDtoValidator : AbstractValidator<LocalidadDto>
    {
        public LocalidadesDtoValidator()
        {
            RuleFor(c => c.Codigo_Circunscripcion)
                .NotEmpty().WithMessage("El Codigo de circunscripción es obligatorio")
                .Must(c => c.ToString().All(char.IsDigit))
                .WithMessage("El número de circunscripción no puede contener letras");

            RuleFor(c => c.Nombre)
                .NotEmpty().WithMessage("El nombre de Circunscripción es obligatorio")
                .Must(c => !string.IsNullOrEmpty(c) && !c.Any(char.IsDigit))
                .WithMessage("El nombre de circunscripción no puede contener números");            
        }
    }
}
