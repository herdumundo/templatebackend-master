using System.Data;

namespace Domain.Entities;

public class UsuarioSesion
{
    public string NombreUsuario { get; set; }
    public string NumeroDocumento { get; set; }
    public int CodigoUsuario { get; set; }
    public List<Rol> Roles { get; set; }
    public TimestampExpiracionUtc TimestampExpiracionUtc { get; set; }
}

public class TimestampExpiracionUtc
{
    public int Seconds { get; set; }
    public int Nanos { get; set; }
}

public class Rol
{
    public int AppId { get; set; }
    public List<Roles> Roles { get; set; }
}

public class Roles
{
    public int RolId { get; set; }
    public string DescripcionRol { get; set; }
}




