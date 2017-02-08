using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Clubber.Common.ValidationAttributes.ValidationAttributes
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        public PhoneNumberAttribute()
            : base()
        {
            ErrorMessage = "Invalid phone number";
        }

        public override bool IsValid(object numbers)
        {
            var numbersList = numbers as List<string>;
            var myRegex = new Regex(@"(?:(\+?\d{1,3}) )?(?:([\(]?\d+[\)]?)[ -])?(\d{1,5}[\- ]?\d{1,5})");

            var resultList = numbersList.Where(num => myRegex.IsMatch(num)).ToList();

            return (resultList.Count == numbersList.Count);
        }
    }
}
