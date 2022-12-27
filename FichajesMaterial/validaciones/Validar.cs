using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FichajesMaterial.validaciones
{
    class Validar
    {
        //Metodo para validar el email
        public static bool validateEmail(string email)
        {
            if (email == null)
            {
                return false;
            }
            if (new EmailAddressAttribute().IsValid(email))
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        //Validamos el telefono
        public static bool validarTelefono(string strNumber)
        {
            if ((strNumber.StartsWith("6") || strNumber.StartsWith("7")) && strNumber.Length == 9)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
