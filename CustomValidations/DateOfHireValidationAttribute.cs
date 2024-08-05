using System.ComponentModel.DataAnnotations;

namespace Sunny_Kasuvojula_A3__11_1_S4.CustomValidations
{
    public class DateOfHireValidationAttribute :ValidationAttribute
    {
        private readonly DateTime _mentionedDate = new DateTime(1989, 1, 1);

        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date >= _mentionedDate;
            }
            return false;
        }

    }
}
