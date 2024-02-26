using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class UsuarioBasedeDatosPoderJudicial
    {
        [Key]     
        public int Codigo_Usuario { get; set; }
        public int Codigo_Tipo_Usuario { get; set; }
        public int Codigo_Persona { get; set; }
        public string Username { get; set; }
        public string Password_Usuario { get; set; }
        public string Usuario_Base_Datos { get; set; }
        public string Password_Base_Datos { get; set; }       
        public int Codigo_Legajo { get; set; }
        public string Codigo_Despacho { get; set; }
        public bool Activo { get; set; }
    }

    public class UsuarioBBDDPoderJudicialValidator : AbstractValidator<UsuarioBasedeDatosPoderJudicial>
    {
        public UsuarioBBDDPoderJudicialValidator()
        {
            //RuleFor(x => x.Codigo_Usuario)
            //    .NotEmpty().WithMessage("El código de usuario es obligatorio.")
            //    .GreaterThan(0).WithMessage("El código de usuario debe ser mayor que cero.")
            //    .Must(c => c.ToString().All(char.IsDigit))
            //    .WithMessage("El codigo usuario no puede contener letras")
            //    .GreaterThan(0).WithMessage("El código de usuario debe ser mayor que cero.");

            RuleFor(x => x.Codigo_Tipo_Usuario)
                .NotEmpty().WithMessage("El código de tipo de usuario es obligatorio.")                
                .Must(c => c.ToString().All(char.IsDigit))
                .WithMessage("El codigo tipo usuario no puede contener letras")
                .GreaterThan(0).WithMessage("El código tipo usuario debe ser mayor que cero.");

            RuleFor(x => x.Codigo_Persona)
                .NotEmpty().WithMessage("El código persona es obligatorio.")               
                .Must(c => c.ToString().All(char.IsDigit))
                .WithMessage("El codigo persona no puede contener letras")
                .GreaterThan(0).WithMessage("El código persona debe ser mayor que cero.");

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

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre de usuario debe tener como máximo 50 caracteres.");

            RuleFor(x => x.Codigo_Legajo)
                .NotEmpty().WithMessage("El código de legajo es obligatorio.")
                .GreaterThan(0).WithMessage("El código de legajo debe ser mayor que cero.");

            RuleFor(x => x.Codigo_Despacho)
                .NotEmpty().WithMessage("El código de despacho es obligatorio.")
                .MaximumLength(50).WithMessage("El código de despacho debe tener como máximo 50 caracteres.");

            RuleFor(x => x.Activo).NotNull().WithMessage("El estado activo debe ser proporcionado.");

        }
    }
}
