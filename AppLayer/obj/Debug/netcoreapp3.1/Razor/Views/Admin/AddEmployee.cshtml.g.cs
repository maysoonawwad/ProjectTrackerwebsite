#pragma checksum "C:\Users\Lenovo\source\repos\FinalProject2\AppLayer\Views\Admin\AddEmployee.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "13f37f473ecf0b5f9bdde7d625e1efa9a95140d1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_AddEmployee), @"mvc.1.0.view", @"/Views/Admin/AddEmployee.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"13f37f473ecf0b5f9bdde7d625e1efa9a95140d1", @"/Views/Admin/AddEmployee.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a1562e0d345a952cceb940c0aa8076b62ff33ef", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_AddEmployee : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
            WriteLiteral(@"
<h2 class=""mb-3 topdiv "">Employees</h2>
<div class=""row mt-4"">
    <div class=""col-sm-6 col-md-4"">
        <div class=""card"">
            <div class=""card-body"">
                <h5 class=""card-title"">Project manager</h5>
                <p class=""card-text"">Project manager is the employee responsible for adding projects</p>
                <a href=""/Admin/AddPMView"" class=""btn btn-outline-primary"">Add</a>
                <a href=""/ProjectManager/ProjectManagers"" class=""btn btn-outline-primary"">Show project managers</a>

            </div>
        </div>
    </div>
    <div class=""col-sm-6 col-md-4"">
        <div class=""card"">
            <div class=""card-body"">
                <h5 class=""card-title"">Team leader</h5>
                <p class=""card-text"">Team leader is the employee responsible for adding project sprints and Duties</p>
                <a href=""/Admin/AddTLView"" class=""btn btn-outline-primary"">Add</a>
                <a href=""/TeamLeader/TeamLeaders"" class=""btn btn-outline-p");
            WriteLiteral(@"rimary""> Show team leaders </a>

            </div>
        </div>
    </div>
    <div class=""col-sm-6 col-md-4"">
        <div class=""card"">
            <div class=""card-body"">
                <h5 class=""card-title"">Team member</h5>
                <p class=""card-text"">Member is the employee responsible for adding solutions to the project</p>
                <a href=""/Admin/AddTMView"" class=""btn btn-outline-primary"">Add</a>
                <a href=""/TeamMember/TeamMembers"" class=""btn btn-outline-primary"">Show team members </a>
            </div>
        </div>
    </div>
</div>");
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
