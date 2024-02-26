using Domain.DTOs;
using FluentValidation;

namespace Domain.Entities
{
    public class UsuarioBBDDPoderJudicialDTO
    {
        public int Codigo_Usuario { get; set; }
        public string primer_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string Username { get; set; }
        public string Password_Usuario { get; set; }
        public string Usuario_Base_Datos { get; set; }
        public string Password_Base_Datos { get; set; }       
        public bool Activo { get; set; }
    }

    public class UsuarioBBDDPoderJudicialDTOValidator : AbstractValidator<UsuarioBBDDPoderJudicialDTO>
    {
        public UsuarioBBDDPoderJudicialDTOValidator()
        {
            RuleFor(x => x.Codigo_Usuario)
            .NotEmpty().WithMessage("El código de usuario es obligatorio.")
            .GreaterThan(0).WithMessage("El código de usuario debe ser mayor que cero.");

            RuleFor(x => x.primer_nombre)
                .NotEmpty().WithMessage("El primer nombre es obligatorio.")
                .MaximumLength(50).WithMessage("El primer nombre debe tener como máximo 50 caracteres.");

            RuleFor(x => x.primer_apellido)
                .NotEmpty().WithMessage("El primer apellido es obligatorio.")
                .MaximumLength(50).WithMessage("El primer apellido debe tener como máximo 50 caracteres.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre de usuario debe tener como máximo 50 caracteres.");

            RuleFor(x => x.Password_Usuario)
                .NotEmpty().WithMessage("La contraseña de usuario es obligatoria.")
                .MinimumLength(8).WithMessage("La contraseña de usuario debe tener al menos 8 caracteres.")
                .MaximumLength(50).WithMessage("La contraseña de usuario debe tener como máximo 50 caracteres.");

            RuleFor(x => x.Usuario_Base_Datos)
                .NotEmpty().WithMessage("El usuario de la base de datos es obligatorio.")
                .MaximumLength(50).WithMessage("El usuario de la base de datos debe tener como máximo 50 caracteres.");

            RuleFor(x => x.Password_Base_Datos)
                .NotEmpty().WithMessage("La contraseña de la base de datos es obligatoria.")
                .MinimumLength(8).WithMessage("La contraseña de la base de datos debe tener al menos 8 caracteres.")
                .MaximumLength(50).WithMessage("La contraseña de la base de datos debe tener como máximo 50 caracteres.");

            RuleFor(x => x.Activo).NotNull().WithMessage("El estado activo debe ser proporcionado.");

        }
    }

}
