using Core.Utilities;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elevator.TagHelpers
{
    [HtmlTargetElement("PersianDateTimePicker", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PersianDateTimePickerTagHelper : TagHelper
    {

        /// <summary>
        /// نام تگ مورد نظر
        /// </summary>
        [HtmlAttributeName("name")]
        public string Name { get; set; }

        /// <summary>
        /// تگ مورد نظر اجباری باشد یا خیر
        /// </summary>
        [HtmlAttributeName("required")]
        public bool Required { get; set; } = false;

        /// <summary>
        /// زمان هم بتواند کاربر انتخاب کند یا خیر
        /// </summary>
        [HtmlAttributeName("hastimepicker")]
        public bool HasTimePicker { get; set; } = false;

        /// <summary>
        /// مقدار پیش‌فرضی که کاربر می‌تواند قرار دهد
        /// </summary>
        [HtmlAttributeName("defaultvalue")]
        public DateTime DefaultValue { get; set; } = DateTime.MinValue;

        /// <summary>
        /// خروجی با چه فرمتی نمایش داده شود
        /// این مقدار بسته به اینکه زمان نیز نمایش داده شود یا خیر
        /// متغییر خواهد بود
        /// </summary>
        private string FormatOutPut = "";

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            // فرمت خروجی به شکل است و آیا زمان با آن هست یا خیر؟
            this.FormatOutPut = (this.HasTimePicker ? "'yyyy/MM/dd HH:mm:ss'" : "'yyyy/MM/dd'");

            // نام مربوط به این شی
            output.TagName = this.Name;

            output.Content.AppendHtml("<span>");

            output.Content.AppendHtml(@"

                <div class='input-group'>
                    <input type='text' name='" + this.Name + "_' id='" + this.Name + @"_' class='form-control' placeholder='لطفا انتخاب کنید...'
                        aria-label='IconPersianDateTimePicker' aria-describedby='IconPersianDateTimePicker'
                        " + (this.Required ? "required='required'" : "") + @" />
                    <input type='hidden' name='" + this.Name + @"' id='" + this.Name + @"' />
                    <div class='input-group-prepend'>
                        <i class='input-group-text cursor-pointer display-inherit fa fa-calendar-alt' id='IconPersianDateTimePicker'></i>
                    </div>
                </div>

            ");

            // اضافه کردن اسکریپت‌های مربوطه
            output.Content.AppendHtml(AddScript());

            output.Content.AppendHtml("</span>");

            return base.ProcessAsync(context, output);
        }

        /// <summary>
        /// اسکیریپت‌های مربوطه در این تابع ساخته می‌شود
        /// </summary>
        /// <returns></returns>
        private string AddScript()
        {

            string scripts = @"
            <script type='text/javascript'>
                $(function () {
                    $('#IconPersianDateTimePicker').MdPersianDateTimePicker({
                        
	                    targetTextSelector: '#" + this.Name + @"_',
                        targetDateSelector: '#" + this.Name + @"',
                        enableTimePicker: " + (this.HasTimePicker ? "true" : "false") + @",
	                    textFormat: " + this.FormatOutPut + @",
	                    dateFormat: " + this.FormatOutPut + @",
                        " + (this.DefaultValue != DateTime.MinValue ? 
                            "selectedDate:new Date('"+ this.DefaultValue.Year.ToString().Fa2En() + "/" + this.DefaultValue.Month.ToString().Fa2En() + "/" + this.DefaultValue.Day.ToString().Fa2En() + @"')," : 
                            "")+ @"
                    });
                });
            </script>
            ";

            return scripts;
        }

    }
}
