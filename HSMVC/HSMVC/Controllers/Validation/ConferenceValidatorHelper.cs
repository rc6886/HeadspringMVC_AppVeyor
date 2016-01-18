using System;
using HSMVC.DataAccess;
using StructureMap;

namespace HSMVC.Controllers.Validation
{
    public class ConferenceValidatorHelper
    {
        public static string RequiredMessage(string propertyName)
        {
            return string.Format("{0} is a required field.", propertyName);
        }

        public static string NotAValidDateMessage(string propertyName)
        {
            return string.Format("{0} is not a valid date.", propertyName);
        }

        public static bool IsAValidDate(DateTime? date)
        {
            return !date.Equals(default(DateTime));
        }

        public static bool DoesConferenceNameAlreadyExist(string nameToCheck)
        {
            if (string.IsNullOrEmpty(nameToCheck))
                return false;

            var conference = ObjectFactory.GetInstance<IConferenceRepository>()
                .FindByName(nameToCheck);

            return conference != null;
        }
    }
}