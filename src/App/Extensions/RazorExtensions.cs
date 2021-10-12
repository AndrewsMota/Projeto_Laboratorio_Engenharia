using Microsoft.AspNetCore.Mvc.Razor;
using System;

namespace App.Extensions
{
    public static class RazorExtensions
    {
        public static string FormataCpf(this RazorPage page, string documento)
        {
            if(documento != null)
            {
                return Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00");
            }

            return null;
        }

        public static string FormataCnpj(this RazorPage page, string documento)
        {
            if (documento != null)
            {
                return Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00");
            }

            return null;
        }

        public static string FormataCep(this RazorPage page, string cep)
        {
            if (cep != null)
            {
                return Convert.ToUInt64(cep).ToString(@"00000\-000");
            }

            return null;
        }

        public static string FormataTelefone(this RazorPage page, string telefone)
        {
            if (telefone != null)
            {
                return Convert.ToUInt64(telefone).ToString(@"(00) 00000\-0000");
            }

            return null;
        }
    }
}
