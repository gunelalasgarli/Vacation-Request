#pragma checksum "C:\Users\ASUS\Desktop\some tasks\Newrepo\Setclapp Task\back\VacationUI\Areas\Admin\Views\Dashboard\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "879e01aef3daa9cc3f8e6b4b3e6bdcbd73d98f3f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Dashboard_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/Dashboard/Index.cshtml")]
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
#line 1 "C:\Users\ASUS\Desktop\some tasks\Newrepo\Setclapp Task\back\VacationUI\Areas\Admin\Views\_ViewImports.cshtml"
using VacationUI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ASUS\Desktop\some tasks\Newrepo\Setclapp Task\back\VacationUI\Areas\Admin\Views\_ViewImports.cshtml"
using Domain.Entities.Common;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ASUS\Desktop\some tasks\Newrepo\Setclapp Task\back\VacationUI\Areas\Admin\Views\_ViewImports.cshtml"
using Domain.Entities.HomeModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\ASUS\Desktop\some tasks\Newrepo\Setclapp Task\back\VacationUI\Areas\Admin\Views\_ViewImports.cshtml"
using Domain.Entities.HomeModel.Enums;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\ASUS\Desktop\some tasks\Newrepo\Setclapp Task\back\VacationUI\Areas\Admin\Views\_ViewImports.cshtml"
using VacationUI.ViewModels.AccountViewModel;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"879e01aef3daa9cc3f8e6b4b3e6bdcbd73d98f3f", @"/Areas/Admin/Views/Dashboard/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99b40dd0f991918d1d44a13dbdc6a64a060e967e", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Dashboard_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<VacationRequest>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\ASUS\Desktop\some tasks\Newrepo\Setclapp Task\back\VacationUI\Areas\Admin\Views\Dashboard\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Areas/Admin/Views/Shared/_AdminLayout.cshtml";
    int messagecount = 0;
    int accepted = 0;
    int rejected = 0;
    int pending = 0;
    int neww = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 12 "C:\Users\ASUS\Desktop\some tasks\Newrepo\Setclapp Task\back\VacationUI\Areas\Admin\Views\Dashboard\Index.cshtml"
   foreach (var item in Model)
    {
        messagecount++;

        if (item.StatusNum == 2)
        {
            accepted++;
        }
        else if (item.StatusNum == 3)
        {
            rejected++;
        }
        else if (item.StatusNum == 1)
        {
            pending++;
        }
        else { neww++; }
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-xl-4 col-md-4 mb-4"">
        <div class=""card border-left-warning shadow h-100 py-2"">
            <div class=""card-body"">
                <div class=""row no-gutters align-items-center"">
                    <div class=""col mr-2"">
                        <div class=""text-xs font-weight-bold text-warning text-uppercase mb-1"">
                            Messages
                        </div>
                        <div class=""h5 mb-0 font-weight-bold text-gray-800"">
                            ");
#nullable restore
#line 41 "C:\Users\ASUS\Desktop\some tasks\Newrepo\Setclapp Task\back\VacationUI\Areas\Admin\Views\Dashboard\Index.cshtml"
                       Write(messagecount);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" Request available
                        </div>
                    </div>
                    <div class=""col-auto"">
                        <i class=""fas fa-comments fa-2x text-gray-300""></i>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""col-xl-4 col-md-4 mb-4"">
        <div class=""card border-left-warning shadow h-100 py-2"">
            <div class=""card-body"">
                <div class=""row no-gutters align-items-center"">
                    <div class=""col mr-2"">
                        <div class=""text-xs font-weight-bold text-warning text-uppercase mb-1"">
                            Messages
                        </div>
                        <div class=""h5 mb-0 font-weight-bold text-gray-800"">
                            ");
#nullable restore
#line 60 "C:\Users\ASUS\Desktop\some tasks\Newrepo\Setclapp Task\back\VacationUI\Areas\Admin\Views\Dashboard\Index.cshtml"
                       Write(accepted);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" Request accepted
                        </div>
                    </div>
                    <div class=""col-auto"">
                        <i class=""fas fa-comments fa-2x text-gray-300""></i>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""col-xl-4 col-md-4 mb-4"">
        <div class=""card border-left-warning shadow h-100 py-2"">
            <div class=""card-body"">
                <div class=""row no-gutters align-items-center"">
                    <div class=""col mr-2"">
                        <div class=""text-xs font-weight-bold text-warning text-uppercase mb-1"">
                            Messages
                        </div>
                        <div class=""h5 mb-0 font-weight-bold text-gray-800"">
                            ");
#nullable restore
#line 79 "C:\Users\ASUS\Desktop\some tasks\Newrepo\Setclapp Task\back\VacationUI\Areas\Admin\Views\Dashboard\Index.cshtml"
                       Write(rejected);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" Request rejected
                        </div>
                    </div>
                    <div class=""col-auto"">
                        <i class=""fas fa-comments fa-2x text-gray-300""></i>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""col-xl-4 col-md-4 mb-4"">
        <div class=""card border-left-warning shadow h-100 py-2"">
            <div class=""card-body"">
                <div class=""row no-gutters align-items-center"">
                    <div class=""col mr-2"">
                        <div class=""text-xs font-weight-bold text-warning text-uppercase mb-1"">
                            Messages
                        </div>
                        <div class=""h5 mb-0 font-weight-bold text-gray-800"">
                            ");
#nullable restore
#line 98 "C:\Users\ASUS\Desktop\some tasks\Newrepo\Setclapp Task\back\VacationUI\Areas\Admin\Views\Dashboard\Index.cshtml"
                       Write(pending);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" Request Pending
                        </div>
                    </div>
                    <div class=""col-auto"">
                        <i class=""fas fa-comments fa-2x text-gray-300""></i>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""col-xl-4 col-md-4 mb-4"">
        <div class=""card border-left-warning shadow h-100 py-2"">
            <div class=""card-body"">
                <div class=""row no-gutters align-items-center"">
                    <div class=""col mr-2"">
                        <div class=""text-xs font-weight-bold text-warning text-uppercase mb-1"">
                            Messages
                        </div>
                        <div class=""h5 mb-0 font-weight-bold text-gray-800"">
                            ");
#nullable restore
#line 117 "C:\Users\ASUS\Desktop\some tasks\Newrepo\Setclapp Task\back\VacationUI\Areas\Admin\Views\Dashboard\Index.cshtml"
                       Write(neww);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" Request New
                        </div>
                    </div>
                    <div class=""col-auto"">
                        <i class=""fas fa-comments fa-2x text-gray-300""></i>
                    </div>
                </div>
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<VacationRequest>> Html { get; private set; }
    }
}
#pragma warning restore 1591
