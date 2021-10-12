using Business.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace App.Extensions
{
    [HtmlTargetElement("*", Attributes = "supress-by-role-name")]
    public class DesabilitaLinkPorRoleTagHelper : TagHelper
    {
        private readonly IUsersRepository _usersRepository;

        public DesabilitaLinkPorRoleTagHelper(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HtmlAttributeName("supress-by-role-name")]
        public string IdentityRoleName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var temAcesso = _usersRepository.ValidarAcesso(IdentityRoleName);

            if (temAcesso) return;

            output.SuppressOutput();
        }
    }
}
