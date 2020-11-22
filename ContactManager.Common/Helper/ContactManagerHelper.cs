using ContactManager.Common.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;

namespace ContactManager.Common.Helper
{
    public static class ContactManagerHelper
    {
        /// <summary>
        /// validate request header
        /// </summary>
        public static void HeaderValidation(HttpRequest request)
        {
            string clientId = request.Headers[ContactManagerConstatants.ClientId];
            Hashtable vault = GetVault();

            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new Exception("Client id Missing");
            }

            else if(vault[ContactManagerConstatants.ClientId].ToString()!=clientId)
            {
                throw new Exception("Client id unauthorized");
            }

        }


        /// <summary>
        /// dummy vault for authentication
        /// </summary>
        private static Hashtable GetVault()
        {
            Hashtable vault = new Hashtable();
            vault.Add(ContactManagerConstatants.ClientId, "12345");
            return vault;
        }
    }
}
