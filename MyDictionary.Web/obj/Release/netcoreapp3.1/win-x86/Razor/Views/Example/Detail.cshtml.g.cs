#pragma checksum "C:\Users\Luc\source\repos\MyDictionary\MyDictionary.Web\Views\Example\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "aa59d22eeb6099ead656091d09c10753fd38e685"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Example_Detail), @"mvc.1.0.view", @"/Views/Example/Detail.cshtml")]
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
#line 1 "C:\Users\Luc\source\repos\MyDictionary\MyDictionary.Web\Views\_ViewImports.cshtml"
using MyDictionary.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Luc\source\repos\MyDictionary\MyDictionary.Web\Views\_ViewImports.cshtml"
using MyDictionary.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aa59d22eeb6099ead656091d09c10753fd38e685", @"/Views/Example/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5fae33a8c1797b3a12610fa32e10e31fb6fafb1f", @"/Views/_ViewImports.cshtml")]
    public class Views_Example_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ExampleDetailViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Luc\source\repos\MyDictionary\MyDictionary.Web\Views\Example\Detail.cshtml"
  
    ViewData["Title"] = "Detail";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Detail</h1>\r\n<div class=\"container-fluid\">\r\n    <div class=\"row\">\r\n        <div class=\"col-lg-12\">\r\n            <pre style=\"white-space: pre-wrap;\">");
#nullable restore
#line 10 "C:\Users\Luc\source\repos\MyDictionary\MyDictionary.Web\Views\Example\Detail.cshtml"
                                           Write(Model.Example.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</pre>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ExampleDetailViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
