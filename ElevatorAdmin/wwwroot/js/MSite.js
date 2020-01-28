
// Read a page's GET URL variables and return them as an associative array.
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}
//================================================================================

// برای تغییر ارتفاع صفحه
function resize_heigth_BodyContainer() {
    
    var btntable = $('.btn-table').height();

    var Mcontent = $('.Mcontent').height();

    var tablefirst = $('.table-first').height();

    var paginationdiv = $('.pagination-div').height();

    paginationdiv += parseFloat($('.pagination-div').css("padding-top")) + parseFloat($('.pagination-div').css("padding-bottom"));
    paginationdiv += parseFloat($('.pagination-div').css("margin-top")); + parseFloat($('.pagination-div').css("margin-bottom"));

    $(".bodycontainer").css({ "max-height": (Mcontent - btntable - tablefirst - paginationdiv)  + "px" });
    $(".bodycontainer").css({ "min-height": (Mcontent - btntable - tablefirst - paginationdiv)  + "px" });
}
resize_heigth_BodyContainer();

//================================================================================

// برای اندازه کردن هر سلول در هر سطر
function resize_td_table_to_th() {
    var th_tableFirst = $('.table-first > thead > tr:nth-child(2) > th');
    var th_tableSecond = $('.table-second > tbody > tr > td');

    for (i = 0; i < th_tableFirst.length; i++) {

        var width = parseFloat($(th_tableFirst[i]).width());
        width += parseFloat($(th_tableFirst[i]).css("padding-left")) + parseFloat($(th_tableFirst[i]).css("padding-right")); //Total Padding Width
        width += parseFloat($(th_tableFirst[i]).css("margin-left")) + parseFloat($(th_tableFirst[i]).css("margin-right")); //Total Margin Width
        width += parseFloat($(th_tableFirst[i]).css("borderLeftWidth")) + parseFloat($(th_tableFirst[i]).css("borderRightWidth")); //Total Border Width
        $(th_tableSecond[i]).css({ "width": width + 1 + "px" })
    }
}

resize_td_table_to_th();

$(window).resize(function () {
    resize_td_table_to_th();
});

$(function () {
//    $('.nicescroll-rails').css({ "top": parseInt($('.nicescroll-rails').css("top").replace("px", "")) + $(".table-first > thead > tr:nth-child(2)").height() + 7 + "px" });
})
//================================================================================


$('.dropify').dropify({
    messages: {
        'default': 'Drag and drop a file here or click',
        'replace': 'لطفا فایل خود را اینجا رها کنید',
        'remove': 'حذف',
        'error': 'خطایی رخ داده است'
    }
});
//================================================================================

/// از این اسکیریپت برای اسکورول دادن به صفحه استفاده می شود
$(function () {
    $(".Mcontent").niceScroll({
        cursorcolor: "#000",
        cursorwidth: "6px",
        background: "#DDD"
    });
});
//================================================================================

/// این اسکیریپت برای قیمت استفاده می شود 
$(function () {
    $(".seperator-input").simpleMoneyFormat();
    $(".seperator-input").on("keyup", function () {
        var name = $(this).attr("name").replace("_show", "");
        $("[name='" + name + "']").attr("value", $(this).val().replace(/,/g, ""));
    });
});
//================================================================================
function reload(href) {
    if (href != undefined)
        window.location = href;
    else
        location.reload();
}
//================================================================================
// این قسمت مربوط به جستجو در قسمت جداول هست
$(function () {
    $('.searchInput').on("keypress", function (event) {
        
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            $('.searchBtn').trigger('click');
        }
    });


    $(".searchSelect").on("change", function (event) {
        $('.searchBtn').trigger('click');
    });
});

//================================================================================
// این قسمت مربوط به زمانی است که در داخل یک جدول یک سطر انتخاب می‌شود
// و باید در ادامه این کارها انجام شود
$(function () {
    // برای زمانی که دکمه برگشت زده می‌شود
    $("input[data-role-table-checkbox]").prop("checked", false);

    $("input[data-role-table-checkbox]").click(function () {
        if ($(this).is(':checked')) {
            $("input[data-role-table-checkbox]").parent().parent().removeClass("tr-selected");
            $(this).parent().parent().addClass("tr-selected");

            $("span[data-role-table-btn-area-access]").removeClass("hidden");

            $("input[data-role-table-checkbox]").removeAttr("checked");
            $(this).prop("checked", true);

            $("[name='selectedRowInTable']").val($(this).attr('data-role-table-checkbox'));

        }
        else {
            $(this).parent().parent().removeClass("tr-selected");

            $("span[data-role-table-btn-area-access]").addClass("hidden");

            $("[name='selectedRowInTable']").val("");
        }
    });


    //$("td").on("click", function () {

    //    var tr_Parent = $(this).parent();
    //    var checkBox = $(tr_Parent).find("input[data-role-table-checkbox]");
    //    console.log($(tr_Parent).find("input[data-role-table-checkbox]").attr('data-role-table-checkbox'));
    //    if ($(tr_Parent).hasClass("tr-selected")) {
    //        $(tr_Parent).removeClass("tr-selected");
    //        $("[name='selectedRowInTable']").val("");
    //    }
    //    else {

    //        $(tr_Parent).addClass("tr-selected");
    //        $("[name='selectedRowInTable']").val($(checkBox).attr('data-role-table-checkbox'));
    //    }

    //});


});
//================================================================================

// این قسمت مربوط به بخش دکمه های بالا جداول است
// وقتی روی دکمه ایی زد، شماره آیتم انتخاب شده به انتحای آدرسی که در دکمه قرار دارد
// اضافه میکند و سپس به سمت آن صفحه می‌رود
$(function () {
    $(".data-role-table-btn").click(function () {

        var ladda = Ladda.create(this);
        ladda.start();
        setTimeout(function () {
            $(".loading").removeClass("hidden");
        }, 500);

        var attr = $(this).attr('ismodal');

        var href = $(this).attr("data-role-href");

        var queryString = $(this).attr("data-role-querystring");

        if ($("[name='selectedRowInTable']").val() != "") {
            if (queryString == undefined)
                href += "/" + $("[name='selectedRowInTable']").val();
            else
                href += "?" + "Id=" + $("[name='selectedRowInTable']").val() + "&" + queryString;
        }
        else {


            if (queryString != undefined) {
                href += "?" + queryString;
            }
        }


        // باز شدن در یک مودال
        if ((typeof attr !== typeof undefined && attr !== false)) {
            CustomOpenModal(href, $(this).attr('modal-title'));
        }
        // باز شدن به صورت عادی و در همان صفحه
        else {
            reload(href);
        }
        setTimeout(function () {
            $(".loading").addClass("hidden");
        }, 500);
        ladda.stop();

    });
});

//================================================================================
/// در این سامانه 
// هروقت یک فیلد از نوع 
//checkbox
// کلیک شد بسته به نوع کلیک (انتخاب کرده باشد یا مه)
// مقداری برای این فیلد قرار میدهد
$(document).on("click", "[type='checkbox']", function (e) {

    //درصفحه مربوط به دسترسی‌ها نباید این روش اعمال شود
    // چک میکنیم اگر همچین کلاسی داشته باشند 
    // یعنی در آن صفحه هستیم
    if (!$(this).hasClass("checkbox-NotValue")) {
        if (this.checked) {
            $(this).attr("value", "true");
        } else {
            $(this).attr("value", "false");
        }
    }
});

//================================================================================

//  ajax
// به صورت معمولی
function AjaxCall(url, params, type, successCallback) {

    setTimeout(function () {
        // نمایش لودینگ
        $('.loading').removeClass("hidden");
    }, 500);

    timeOut = 6000;

    if (type == null)
        type = "POST";

    type = (type.toUpperCase() != "GET" &&
        type.toUpperCase() != "POST" &&
        type.toUpperCase() != "PUT" &&
        type.toUpperCase() != "DELETE") ? "POST" : type;


    $.ajax({
        type: type,
        url: url,
        async: false,
        data: params,
        success: successCallback,
        error: function (xhr, textStatus, errorThrown) {
            console.log(xhr.status);
            console.log(textStatus);
            console.log(errorThrown);

            // درصورتی که کوکی شخص پریده باشد باید به صفحه لاگین هدایت شود
            // درصورتی که شماره status برابر 401 باشد باید به صفحه لاگین هدایت شود
            if (xhr.status == 401)
                window.location.href = "/";

            // درصورتی که کاربر به این اکشن دسترسی نداشته باشد
            if (xhr.status == 403) {
                $("[name='NoAccess']").removeClass("hidden");
                $('#target_').html('');
            }
        }
    });

    setTimeout(function () {
        // پنهان کردن لودینگ
        $('.loading').addClass("hidden");
    }, 500);
}

//================================================================================
function AjaxCall(url, params, type) {

    //setTimeout(function () {
    // نمایش لودینگ
    $('.loading').removeClass("hidden");
    //}, 500);

    timeOut = 6000;
    var returnData;
    if (type == null)
        type = "POST";

    type = (type.toUpperCase() != "GET" &&
        type.toUpperCase() != "POST" &&
        type.toUpperCase() != "PUT" &&
        type.toUpperCase() != "DELETE") ? "POST" : type;


    $.ajax({
        type: type,
        url: url,
        async: false,
        data: params,
        success: function (data) {

            returnData = data;

        },
        error: function (xhr, textStatus, errorThrown) {
            console.log(xhr.status);
            // درصورتی که کوکی شخص پریده باشد باید به صفحه لاگین هدایت شود
            // درصورتی که شماره status برابر 401 باشد باید به صفحه لاگین هدایت شود
            if (xhr.status == 401)
                window.location.href = "/";

            // درصورتی که کاربر به این اکشن دسترسی نداشته باشد
            if (xhr.status == 403) {
                $("[name='NoAccess']").removeClass("hidden");
                $('#target_').html('');
            }
        }
    });

    setTimeout(function () {
        // پنهان کردن لودینگ
        $('.loading').addClass("hidden");
    }, 500);
    return returnData;
}

//function AjaxCallWithCondistion(url, params, type, successCallback) {

//    // نمایش لودینگ
//    $('.loading').removeClass('hide');

//    timeOut = 6000;

//    if (type == null)
//        type = "POST";

//    type = (type.toUpperCase() != "GET" &&
//        type.toUpperCase() != "POST" &&
//        type.toUpperCase() != "PUT" &&
//        type.toUpperCase() != "DELETE") ? "POST" : type;


//    $.ajax({
//        type: type,
//        url: url,
//        async: false,
//        data: params,
//        processData: false,
//        contentType: false,
//        success: successCallback,
//        error: function (xhr, textStatus, errorThrown) {
//            console.log(xhr.status);
//            // درصورتی که کوکی شخص پریده باشد باید به صفحه لاگین هدایت شود
//            // درصورتی که شماره status برابر 401 باشد باید به صفحه لاگین هدایت شود
//            if (xhr.status == 401)
//                window.location.href = "/";

//            if (xhr.status == 403) {
//                $("[name='NoAccess']").removeClass("hide");
//                $('#target_').html('');
//            }
//        }
//    });

//    //پنهان کردن لودینگ
//    $('.loading').addClass('hide');
//}

/*
 * از این تابع کلا برای زمانی که بخواهیم کلا
 * یا فرمو به سمت سرور بفرستیم و یا کلا در داخل صفحه فایل آپلود داشته باشیم استفاده می کنیم
 */
function AjaxCallWithUploadFile(url, params, type, successCallback) {
    setTimeout(function () {
        // نمایش لودینگ
        $('.loading').removeClass('hidden');
    }, 500);

    timeOut = 6000;

    if (type == null)
        type = "POST";

    type = (type.toUpperCase() != "GET" &&
        type.toUpperCase() != "POST" &&
        type.toUpperCase() != "PUT" &&
        type.toUpperCase() != "DELETE") ? "POST" : type;


    $.ajax({
        type: type,
        url: url,
        async: false,
        data: params,
        cache: false,
        contentType: false,
        processData: false,

        success: successCallback,
        error: function (xhr, textStatus, errorThrown) {
            console.log(xhr.status);
            // درصورتی که کوکی شخص پریده باشد باید به صفحه لاگین هدایت شود
            // درصورتی که شماره status برابر 401 باشد باید به صفحه لاگین هدایت شود
            if (xhr.status == 401)
                window.location.href = "/";

            if (xhr.status == 403) {
                $("[name='NoAccess']").removeClass("hidden");
                $('#target_').html('');
            }
        }
    });


    setTimeout(function () {
        // پنهان کردن لودینگ
        $('.loading').addClass('hidden');
    }, 500);
}
//================================================================================

// از این تابع باز کردن مودال و قرار دادن یک صفحه در آن استفاده می شود
// برای استفاده از این تابع باید ابتدا آدرس صفحه ای که باید لود شود داده شده
// تا با یک ajax
// صفحه مورد نظر آورده شده و نمایش داده شود
// همچنین عنوان صفحه نیز باید داده شود
function CustomOpenModal(url, modalTitle) {

    $('[name="LiftBazarModalContent"]').html(AjaxCall(url, null, "GET"));

    $('[name="LiftBazarModalTitle"]').html(modalTitle);

    $('#LiftBazarModal').modal('show');

}


$(function () {

    //$('[data-toggle="tooltip"]').tooltip({
    //    trigger: 'hover'
    //})
    //$('.btn-secondary').click(function () {
    //    $('[data-toggle="tooltip"]').tooltip('hide');
    //}); 

});

//================================================================================

$(".btn").click(function () {



});