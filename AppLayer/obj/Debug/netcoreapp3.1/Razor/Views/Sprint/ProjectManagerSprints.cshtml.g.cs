#pragma checksum "C:\Users\Lenovo\source\repos\FinalProject2\AppLayer\Views\Sprint\ProjectManagerSprints.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "726c580193ae0cdbaa38217fe6e905221539d757"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sprint_ProjectManagerSprints), @"mvc.1.0.view", @"/Views/Sprint/ProjectManagerSprints.cshtml")]
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
#line 1 "C:\Users\Lenovo\source\repos\FinalProject2\AppLayer\Views\_ViewImports.cshtml"
using AppLayer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Lenovo\source\repos\FinalProject2\AppLayer\Views\_ViewImports.cshtml"
using AppLayer.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"726c580193ae0cdbaa38217fe6e905221539d757", @"/Views/Sprint/ProjectManagerSprints.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a1562e0d345a952cceb940c0aa8076b62ff33ef", @"/Views/_ViewImports.cshtml")]
    public class Views_Sprint_ProjectManagerSprints : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"

<h2 class=""topdiv"">Sprints</h2>

<table class=""table  table-bordered mt-4"">
    <thead>
        <tr>
            <th>
                Project title
            </th>

            <th>
                Sprint start deate
            </th>
            <th>
                Sprint end date
            </th>
            <th>
                Sprint status
            </th>




        </tr>

    </thead>
    <tbody>
");
#nullable restore
#line 34 "C:\Users\Lenovo\source\repos\FinalProject2\AppLayer\Views\Sprint\ProjectManagerSprints.cshtml"
         foreach (var sprint in ViewBag.Sprints)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 37 "C:\Users\Lenovo\source\repos\FinalProject2\AppLayer\Views\Sprint\ProjectManagerSprints.cshtml"
           Write(sprint.ProjectTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 38 "C:\Users\Lenovo\source\repos\FinalProject2\AppLayer\Views\Sprint\ProjectManagerSprints.cshtml"
           Write(sprint.StartDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 39 "C:\Users\Lenovo\source\repos\FinalProject2\AppLayer\Views\Sprint\ProjectManagerSprints.cshtml"
           Write(sprint.EndDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n\r\n");
#nullable restore
#line 42 "C:\Users\Lenovo\source\repos\FinalProject2\AppLayer\Views\Sprint\ProjectManagerSprints.cshtml"
             foreach (var status in ViewBag.Statuses)
            {
                if (status.StatusId == sprint.StatusId)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>");
#nullable restore
#line 46 "C:\Users\Lenovo\source\repos\FinalProject2\AppLayer\Views\Sprint\ProjectManagerSprints.cshtml"
                   Write(status.StatusName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 47 "C:\Users\Lenovo\source\repos\FinalProject2\AppLayer\Views\Sprint\ProjectManagerSprints.cshtml"

                }
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n        </tr>\r\n");
#nullable restore
#line 53 "C:\Users\Lenovo\source\repos\FinalProject2\AppLayer\Views\Sprint\ProjectManagerSprints.cshtml"


        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n\r\n\r\n\r\n\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
