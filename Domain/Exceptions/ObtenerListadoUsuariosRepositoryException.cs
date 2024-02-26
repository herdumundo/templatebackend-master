using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;

namespace Domain.Exceptions
{
    public class ObtenerListadoUsuariosRepositoryException : ApiException
    {
        public ObtenerListadoUsuariosRepositoryException(string message) : base(message)
        {

        }
    }
}
