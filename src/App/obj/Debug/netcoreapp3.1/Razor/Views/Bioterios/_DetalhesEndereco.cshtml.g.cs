#pragma checksum "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cbe2f8af740471abd986000963e5e473a9892445"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Bioterios__DetalhesEndereco), @"mvc.1.0.view", @"/Views/Bioterios/_DetalhesEndereco.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\_ViewImports.cshtml"
using App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\_ViewImports.cshtml"
using App.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\_ViewImports.cshtml"
using Business.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\_ViewImports.cshtml"
using App.Extensions;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cbe2f8af740471abd986000963e5e473a9892445", @"/Views/Bioterios/_DetalhesEndereco.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ea9944d4057a7cd06cdc42f54f4ac0b57c44482f", @"/Views/_ViewImports.cshtml")]
    public class Views_Bioterios__DetalhesEndereco : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Business.Models.Bioterio>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div style=\"padding-top: 20px\">\r\n    <div>\r\n        <hr />\r\n        <h4>Endereço</h4>\r\n    </div>\r\n\r\n");
#nullable restore
#line 9 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml"
     if (Model != null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <table class=\"table table-hover\">\r\n            <thead class=\"thead-dark\">\r\n                <tr>\r\n                    <th>\r\n                        ");
#nullable restore
#line 15 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml"
                   Write(Html.DisplayNameFor(model => model.Endereco.Logradouro));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 18 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml"
                   Write(Html.DisplayNameFor(model => model.Endereco.Numero));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 21 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml"
                   Write(Html.DisplayNameFor(model => model.Endereco.Complemento));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 24 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml"
                   Write(Html.DisplayNameFor(model => model.Endereco.Bairro));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 27 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml"
                   Write(Html.DisplayNameFor(model => model.Endereco.Cep));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 30 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml"
                   Write(Html.DisplayNameFor(model => model.Endereco.Cidade));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 33 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml"
                   Write(Html.DisplayNameFor(model => model.Endereco.Estado));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th></th>\r\n                </tr>\r\n            </thead>\r\n\r\n            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 41 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml"
               Write(Html.DisplayFor(model => model.Endereco.Logradouro));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 44 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml"
               Write(Html.DisplayFor(model => model.Endereco.Numero));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 47 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml"
               Write(Html.DisplayFor(model => model.Endereco.Complemento));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 50 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml"
               Write(Html.DisplayFor(model => model.Endereco.Bairro));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 53 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml"
               Write(this.FormataCep(Model.Endereco.Cep));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 56 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml"
               Write(Html.DisplayFor(model => model.Endereco.Cidade));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 59 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml"
               Write(Html.DisplayFor(model => model.Endereco.Estado));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n        </table>\r\n");
#nullable restore
#line 63 "C:\Users\paulo\Documents\Faculdade\UNIT\5° Período\Laboratório de Engenharia de Software\Projeto_Laboratorio_Engenharia\src\App\Views\Bioterios\_DetalhesEndereco.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n\r\n<script>\r\n    SetModal();\r\n</script>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Business.Models.Bioterio> Html { get; private set; }
    }
}
#pragma warning restore 1591
