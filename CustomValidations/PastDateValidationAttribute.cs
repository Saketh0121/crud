using System.ComponentModel.DataAnnotations;

namespace Sunny_Kasuvojula_A3__11_1_S4.CustomValidations
{
    public class PastDateValidationAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date < DateTime.Now;
            }
            return false;
        }
    }
}
