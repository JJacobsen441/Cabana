using Cabana.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace Cabana.Statics
{
    public class CheckHelper
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return false;

                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckSearch(VmSearch model)
        {
            if (model.IsNull())
                return false;

            if (model.search_string.IsNull())
                return false;

            model.search_string = HttpUtility.UrlDecode(model.search_string);
            model.search_string = model.search_string.Trim();
            model.search_string = model.search_string.ToLower();

            bool ok_a = !string.IsNullOrEmpty(model.search_string);

            bool ok_b = model.search_string.Length <= 20;

            /*
             * use fx 'no_tag' for no tags
             * */
            bool ok_c;
            model.search_string = StringHelper.Sanitize(model.search_string, false, false, new List<string>() { "b", "strong" }, CharacterHelper.All(false), out ok_c);

            bool ok_d = !model.genres && !model.votes ? true : model.genres ^ model.votes;

            return ok_a && ok_b && ok_c && ok_d;
        }

        public static bool CheckName(ref string name, bool tolower, int len, bool allow_upper, List<string> tags, char[] characters)
        {
            if (name.IsNull())
                return false;

            name = HttpUtility.UrlDecode(name);
            name = name.Trim();
            if(tolower)
                name = name.ToLower();

            bool ok_a = !string.IsNullOrEmpty(name);

            bool ok_b = name.Length <= len;

            /*
             * use fx 'no_tag' for no tags
             * */
            bool ok_c;
            name = StringHelper.Sanitize(name, false, allow_upper, tags, characters, out ok_c);

            return ok_a && ok_b && ok_c;
        }
    }
}