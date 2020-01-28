using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using static Core.CustomAttributes.ColumnGridViewAttribute;


namespace Elevator.TagHelpers
{
    [HtmlTargetElement("MGridView", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class MGridView : TagHelper
    {


        #region Element Attributes
        [HtmlAttributeName("name")]
        public string Name { get; set; }

        /// <summary>
        /// پرس‌ و جویی که باید از آن اطلاعات واکشی شود
        /// </summary>
        [HtmlAttributeName("Query")]
        public IQueryable<dynamic> Query { get; set; }


        /// <summary>
        /// تعداد آیتم‌های موجود در هر صفحه
        /// </summary>
        [HtmlAttributeName("pagesize")]
        public int pagesize { get; set; }


        /// <summary>
        /// شماره صفحه مورد نظر
        /// </summary>
        [HtmlAttributeName("pagenumber")]
        public int pagenumber { get; set; }

        [HtmlAttributeName("classtype")]
        public object ClassType { get; set; }

        /// <summary>
        /// اینکه ردیف داشته باشد یا نه
        /// </summary>
        [HtmlAttributeName("HasRowNumber")]
        public bool HasRowNumber { get; set; }



        private readonly IHtmlHelper html;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        #endregion


        public MGridView(IHtmlHelper htmlHelper)
        {
            html = htmlHelper;
        }


        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {

            //=========================================================================
                List<ColumnGridView> columns = new List<ColumnGridView>();
                GridViewSetting gridViewSetting = new GridViewSetting();
            //=========================================================================

            gridViewSetting.HasRowNumber = this.HasRowNumber;

            //تعداد آیتمی که باید نمایش داده شود
            var result = Query.Skip(pagenumber).Take(pagesize).ToList();
            

            #region setting for columns

            //گرفتن آیتمها یا به عبارتی ستون ها
            foreach (PropertyInfo prop in ClassType.GetType().GetProperties())
            {

                ColumnGridView column = new ColumnGridView
                {
                    // نام فیلد مورد نظر
                    Name = prop.Name,
                    // نوع فیلد مورد نظر
                    ColumnType = GetFieldType(prop)

                };



                foreach (var attr in prop.GetCustomAttributesData())
                {
                    Dictionary<string, string> item = new Dictionary<string, string>();


                    //if (attr.AttributeType.Name == "DisplayAttribute")



                    if (attr.AttributeType.Name == "ColumnGridViewAttribute")
                    {
                        foreach (var ColumnGridViewAttributeAttr in attr.NamedArguments)
                        {
                            if (ColumnGridViewAttributeAttr.MemberName == "DisplayName")
                                // عنوان ستون که باید نمایش داده شود
                                column.Title = ColumnGridViewAttributeAttr.TypedValue.ToString();

                            // اندازه
                            if (ColumnGridViewAttributeAttr.MemberName == "Width")
                                column.Width = ColumnGridViewAttributeAttr.TypedValue.ToString();

                            // جستجو
                            if (ColumnGridViewAttributeAttr.MemberName == "Searchable")
                                column.Searchable = Convert.ToBoolean(ColumnGridViewAttributeAttr.TypedValue.Value);

                            // مرتب سازی
                            if (ColumnGridViewAttributeAttr.MemberName == "Sortable")
                                column.Sortable = Convert.ToBoolean(ColumnGridViewAttributeAttr.TypedValue.Value);

                            // مرتب سازی
                            if (ColumnGridViewAttributeAttr.MemberName == "AlignCenter")
                                column.AlignCenter = Convert.ToBoolean(ColumnGridViewAttributeAttr.TypedValue.Value);

                        }


                    }

                    item.Add(attr.AttributeType.Name, attr.NamedArguments[0].TypedValue.ToString());

                    column.Attrs.Add(item);
                }
                columns.Add(column);
            }

            #endregion


            ViewContext.ViewBag.ColumnsGridView = columns;
            ViewContext.ViewBag.ColumnsGridViewJson = JArray.FromObject(columns).ToString();//JsonConvert.SerializeObject(columns, Formatting.Indented).Replace("\\", "");
            ViewContext.ViewBag.GridViewSetting = gridViewSetting;
            ViewContext.ViewBag.Name = this.Name;

            (html as IViewContextAware).Contextualize(ViewContext);



            var content = html.Partial("~/Views/Shared/TagHelpersView/GridList.cshtml", result);
            output.Content.SetHtmlContent(content);

            return base.ProcessAsync(context, output);
        }




        private string GetFieldType(PropertyInfo prop)
        {
            string typeProp = prop.PropertyType.FullName;

            switch (typeProp)
            {
                case "System.Int32":
                case "System.Int64":
                case "System.Int16":
                    return "int";

                case "System.String":
                    return "string";



                default:
                    string a = prop.PropertyType.BaseType.FullName;
                    if (a == "System.Enum")
                        return "select";
                    else if (a == "System.Boolean")
                        return "select";
                    return "";


            }
        }





        /// <summary>
        /// تنظیمات مربوط به خود GridView
        /// </summary>
        public class GridViewSetting
        {

            /// <summary>
            /// آیا این جدول ردیف داشته باشد یا نه
            /// </summary>
            public bool HasRowNumber { get; set; }
        }

        /// <summary>
        /// تنظیمات مربوط به ستون‌های GridView
        /// </summary>
        public class ColumnGridView
        {
            /// <summary>
            /// نام ستون
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// عنوان ستون
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// اندازه ستون
            /// </summary>
            public string Width { get; set; }

            /// <summary>
            /// این فیلد قابلیت جستجو داشته باشد یا نه
            /// </summary>
            public bool Searchable { get; set; }

            /// <summary>
            /// نوع فیلد به چه صورت است
            /// </summary>
            public string ColumnType { get; set; }


            /// <summary>
            /// قابلیت مرتب سازی داشته باشد یا نه
            /// </summary>
            public bool Sortable { get; set; }

            /// <summary>
            /// به طور کلی این ستون چگگونه باشد
            /// راست، چپ، یا وسط چین
            /// </summary>
            public bool AlignCenter { get; set; }

            /// <summary>
            /// مقادیر مربوط به attr ها
            /// </summary>
            public List<Dictionary<string, string>> Attrs { get; set; } = new List<Dictionary<string, string>>();

        }


        


    }
}
