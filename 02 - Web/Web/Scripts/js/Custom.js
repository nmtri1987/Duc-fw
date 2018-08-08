/// <reference path="bootstrap.min.js" />
//appFilter dùng chung
//var appService = angular.module("AppService", []);
//appService.filter("mydate", function () {
//    return function (x) {
//        if (x == null || x == "") {
//            return null;
//        } else {
//            return new Date(parseInt(x.substr(6)));
//        }

//    };
//});

//appService.service("serviceUnity", [function () {
//    this.resetFormValidate = function (formid) {
//        $.fn.clearValidation = function () { var v = $(this).validate(); $('[name]', this).each(function () { v.successList.push(this); v.showErrors(); }); v.resetForm(); v.reset(); };
//        //used:
//        $("#" + formid + "").clearValidation();
//    },
//    this.AjaxPostData = function (pars, lUrl) {
//        var mObj = null;
//        $.ajax({
//            /*
//            beforeSend: function (xhrObj) {
//                xhrObj.setRequestHeader("Accept-Language", "vi");
//                xhrObj.setRequestHeader("Authorization", $("meta[name=AccessToken]").attr("content"));
//                xhrObj.setRequestHeader("UserID", $("meta[name=UserAccept]").attr("content"));
//                xhrObj.setRequestHeader("PageID", $("meta[name=PageID]").attr("content"));
//            },
//            */
//            type: 'POST',
//            url: GetPath(lUrl),
//            //dataType: 'JSON',
//            data: pars,
//            //processData: false,
//            //contentType: false,// not json
//            async: false,
//            success: function (data) {
//                mObj = data;
//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                mObj = null;
//                if (jqXHR.status == 403) {
//                    alert(errorThrown);
//                    location.href = $('#btnLogout').attr('href');
//                }
//            }
//        });
//        return mObj;
//    },
//    this.notificationMessage = function (type, message) {
//        Lobibox.notify(type, //AVAILABLE TYPES: "error", "info", "success", "warning"
//                        {
//                            //title:"",
//                            sound: false,
//                            msg: message
//                        });
//    }
//    this.upLoadImage = function (fileID, imgID) {
//        var seft = this;
//        if ($('input[id=' + fileID + ']')[0].files && $('input[id=' + fileID + ']')[0].files[0]) {

//            ///check file type
//            var fileName = document.getElementById("" + fileID + "").value
//            if (fileName.split(".")[1].toUpperCase() == "PNG" || fileName.split(".")[1].toUpperCase() == "JPG" || fileName.split(".")[1].toUpperCase() == "GIF") {
//                var reader = new FileReader();

//                reader.onload = function (e) {
//                    $('#' + imgID + '').attr('src', e.target.result);
//                }

//                reader.readAsDataURL($('input[id=' + fileID + ']')[0].files[0]);
//                $(".file-input-name").remove();
//                return true;
//            }
//            else {
//                seft.notificationMessage("warning", "Please Choose File Images!");
//                return false;
//            }

//            return true;

//            //end check file type
//        }
//    }
//    this.dataURLtoBlob = function (dataUrl) {
//        if (dataUrl.split(',').length > 1) {
//            //Decode the dataURL    
//            var binary = atob(dataUrl.split(',')[1]);

//            //Create 8-bit unsigned array
//            var array = [];
//            for (var i = 0; i < binary.length; i++) {
//                array.push(binary.charCodeAt(i));
//            }

//            //Return our Blob object
//            return new Blob([new Uint8Array(array)], {
//                type: 'image/png'
//            });
//        }
//        else {
//            return null;
//        }
//    }
//    this.myfuncChangeColor = function (myAcc) {
//        var intValue = parseInt(myAcc);

//        if (intValue >= 0) {
//            return true;
//        } else {
//            return false;
//        }
//    }
//    this.exportData = (function (table, name, filename) {
//        var uri = 'data:application/vnd.ms-excel;base64,'
//          , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
//          , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
//          , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
//        return function (table, name, filename) {
//            if (!table.nodeType) table = document.getElementById(table)
//            var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
//            //window.location.href = uri + base64(format(template, ctx))
//            var link = document.getElementById("exportExcel");
//            link.download = filename + ".xls";
//            link.href = uri + base64(format(template, ctx));
//        }
//    })()
//}])
function refreshScript() {
    $('#cbAll').click(function () {
        $('input[name=cb]').prop('checked', $('#cbAll').prop('checked'));
    });

    $('input[name=cb]').click(function () {
        if ($('input[name=cb]:checked').length >= $('input[name=cb]').length) {
            $('#cbAll').prop('checked', true);
        }
        else {
            $('#cbAll').prop('checked', false);
        }
    });
}
function JsonFormatDate(date) {
    if (date != null && date != "") {

        
       // var dateParts = date.split(/-/);
       // alert((dateParts[2] * 10000));
       // var b = (dateParts[2] * 10000) + ($.inArray(dateParts[1].toUpperCase(), ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"]) * 100) + (dateParts[0] * 1);
        var MonthC = ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"];
        
        var dateString = date.substr(6);
        var currentTime = new Date(parseInt(dateString));
        var month = currentTime.getMonth() + 1 <= 9 ? "0" + (currentTime.getMonth() + 1) : (currentTime.getMonth() + 1);
        var day = currentTime.getDate() <= 9 ? "0" + currentTime.getDate() : currentTime.getDate();
        var year = currentTime.getFullYear();
        date = day + "-" + MonthC[month-1] + "-" + year;

        

        return date;
    } else {
        return "";
    }

}
//format mm/dd/yyyy
function FormatDate(date, isgethour) {
    if (date != null && date != "") {
        var month = date.getMonth() + 1 <= 9 ? "0" + (date.getMonth() + 1) : (date.getMonth() + 1);
        var day = date.getDate() <= 9 ? "0" + date.getDate() : date.getDate();
        var year = date.getFullYear();
        var result = month + "/" + day + "/" + year
        if (isgethour) {
            result = result + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
        }

        return result;
    } else {
        return "";
    }

}

function FormatDateReport(date, isgethour) {
    if (date != null && date != "") {
        var month = date.getMonth() + 1 <= 9 ? "0" + (date.getMonth() + 1) : (date.getMonth() + 1);
        var day = date.getDate() <= 9 ? "0" + date.getDate() : date.getDate();
        var year = date.getFullYear();
        var result = year + "-" + month + "-" + day;
        if (isgethour) {
            result = result + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
        }

        return result;
    } else {
        return "";
    }

}

function MDYFormatDateReport(date, isgethour) {
    if (date != null && date != "") {
        var month = date.getMonth() + 1 <= 9 ? "0" + (date.getMonth() + 1) : (date.getMonth() + 1);
        var day = date.getDate() <= 9 ? "0" + date.getDate() : date.getDate();
        var year = date.getFullYear();
        var result = month+"/"+ day+"/"+year ;
        if (isgethour) {
            result = result + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
        }

        return result;
    } else {
        return "";
    }

}
//format mm/dd/yyyy
function FormatDateTime(input, isgethour) {
    var date = new Date(input.split("T")[0] + " " + input.split("T")[1]);
    //var time = new Date(input.split("T")[1]);
    if (date != null && date != "") {
        var month = date.getMonth() + 1 <= 9 ? "0" + (date.getMonth() + 1) : (date.getMonth() + 1);
        var day = date.getDate() <= 9 ? "0" + date.getDate() : date.getDate();
        var year = date.getFullYear();
        var result = month + "/" + day + "/" + year
        if (isgethour) {
            result = result + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
        }

        return result;
    } else {
        return "";
    }

}
function fnNameNoSign(s) {

    while (s.indexOf("  ") >= 0) {
        s = s.replace("  ", " ")
    }
    if (s[0] == " ") {
        s = s.substring(1, s.length - 1);
    }
    if (s[s.length - 1] == " ") {
        s = s.substring(0, s.length - 1);
    }
    while (s.indexOf(" ") >= 0) {
        s = s.replace(" ", "-")
    }
    return s.toLowerCase();
}
function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};

$(document).ready(function () {
  //  activeMenuPageLoad("");
    $(window).resize(function () {
        ///add class then resize browser reponsive
        var withWindow = $(window).width();
        var menuId = $("[id=myNavbar]");
        if (withWindow <= 768) {
            if (!menuId.hasClass("navbar-collapse")) {
                menuId.addClass("navbar-collapse").addClass("collapse")
            }
        } else {
            menuId.removeClass("navbar-collapse").removeClass("collapse")
        }
        ///end add class then resize browser
    });
    /*datetime picker*/
    $('.navigation ul ul li a.level-2').each(function () {
        var href = location.pathname.toLowerCase();
        var arr = href.split('/')
        if (arr.indexOf($(this).attr('title').toLowerCase()) != -1) {
            $(this).parents("ul:first").parents("li:first").find('a.dropdown-collapse:first').click();
            if ($(this).next().html() == undefined) {
                $(this).css("color", "#999");///active level 2
            } else {
                ///active level 3
                $(this).click();
                var temp = $(this).next().children().find("a.level-3");
                temp.each(function () {
                    if (arr.indexOf($(this).attr('title').toLowerCase()) != -1) {
                        if ($(this).next().html() == undefined) {
                            $(this).css("color", "#999");
                        }
                        else {
                            /// active level 4
                            $(this).click();
                            var temp2 = $(this).next().children().find("a.level-4");
                            temp2.each(function () {
                                if (arr.indexOf($(this).attr('title').toLowerCase()) != -1) {
                                    $(this).css("color", "#999");
                                }
                            })
                        }
                    }
                })

            }

        }

    });
    $(".header-sort").click(function () {
        window.location.href = $(this).children("a").attr("href");

    })
    $("table th.sortable").click(function () {
        $("table th.sortable .fa").remove();
        $("table th.sortable").prepend("<i class='fa fa-sort' style='float:left;margin-right:5px'></i>");
        $(this).find("i").removeClass("fa-sort").removeClass("fa-sort-up").removeClass(",fa-sort-down");
        if ($(this).hasClass("sort-asc")) {
            $(this).find("i").addClass("fa-sort-down");
        } else {
            $(this).find("i").addClass("fa-sort-up");
            // $(this).append("<i class='fa fa-sort-down'></i>");
        }
    })
    showiconsort()
})
function showiconsort() {
    $("table th.sortable").prepend("<i class='fa fa-sort' style='float:left;margin-right:5px'></i>");

}
function notimessage(type, msg, title) {
    Lobibox.notify(type, //AVAILABLE TYPES: "error", "info", "success", "warning"
                     {
                         //title:title,
                         sound: false,
                         msg: msg
                     });
}

function ShowProcessing() {
    var strhtml = '<div class="cssload-container"><div class="cssload-double-torus"></div></div>';
    var str = $(".cssload-container").html();
    if (str == null || str == "") {
        $(".table-processing").append(strhtml);
    }
    $(".cssload-container").show();
    $(".table-processing table").css({ "opacity": "0.2" })
}
function HideProcessing() {
    setTimeout(function () {
        $(".cssload-container").hide();
        $(".table-processing table").css({ "opacity": "1" })
    }, 500)

}
function AjaxGetData(lUrl) {
    var mObj = null;
    $.ajax({
        type: 'GET',
        url: GetPath(lUrl),
        async: false,
        success: function (data) {
            mObj = data;

        },
        error: function (jqXHR, textStatus, errorThrown) {
            mObj = null;

        }
    });
    return mObj;
}
function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

function showIcon(i) {
    var defIcon = "fa";
    if (i == 1) {
        defIcon += " fa-database";
    } else if (i == 2) {
        defIcon += " fa-line-chart";

    } else if (i == 3) {
        defIcon += "  fa-codepen";

    } else {
        defIcon += " fa-laptop";
    }
    return defIcon
}
//show the menu on left side
function LoadLeftMenu(id) {
    var str = "<div class='ifmenu'><ul class='nav nav-pills nav-stacked metismenu' id='side-menu'>";//nav navbar-nav
    var noteID = $.trim(id);
    var result = AjaxGetData("/SiteMap/GetAll");
    if ($.type(result) != "array") {
        window.location.href = window.location.href;
        return;
    }

    var levelChild = [];
    result.sort(function (a, b) {
        return parseInt(a.Position) - parseInt(b.Position);
    });
    $.each(result, function (i, value) {
        if (result[i].ParentID == noteID) {
            levelChild.push(result[i]);
        }
    })
    $.each(levelChild, function (i, value) {
        var levelChildChild = [];
        var rootMap = noteID + ">" + levelChild[i].NodeID;
        var url = "";
        if ($.trim(levelChild[i].Url) == "/" || $.trim(levelChild[i].Url) == "") {
            url = "#"
        } else {
            url = GetPath(levelChild[i].Url)
        }
        $.each(result, function (j, val) {
            if (result[j].ParentID == levelChild[i].NodeID) {
                levelChildChild.push(result[j]);
            }
        })
        if (levelChildChild.length > 0) {
            if (i == 0) {
                str += " <li class='dropdown active tab-child-li' id='menuID_" + levelChild[i].NodeID + "'>";
            } else {
                str += " <li class='dropdown tab-child-li' id='menuID_" + levelChild[i].NodeID + "' >";
            }
            str += "<a class='menu-left dropdown-toggle ' tartgetID='#div_" + levelChild[i].NodeID + "'   href='#' alt='" + levelChild[i].Title + "' title='" + levelChild[i].Title + "'><i class='" + showIcon(i) + "'></i> </a>";
            str += "</li>";

        }
        else {
            str += "<li id='menuID_" + levelChild[i].NodeID + "' title='" + levelChild[i].Description + "'><a  href='" + url + "' data-map='" + rootMap + "'><i class='" + showIcon(i) + "'></i></a></li>";
        }

    })

    str += "</ul>";
    str += "<div class='panel-body'>";
    str += "<div class='tab-content'>";
    $.each(levelChild, function (i, value) {
        var levelChildChild = [];
        var rootMap = noteID + ">" + levelChild[i].NodeID;
        var url = "";
        if ($.trim(levelChild[i].Url) == "/" || $.trim(levelChild[i].Url) == "") {
            url = "#"
        } else {
            url = GetPath(levelChild[i].Url)
        }
        $.each(result, function (j, val) {
            if (result[j].ParentID == levelChild[i].NodeID) {
                levelChildChild.push(result[j]);
            }
        })
        if (levelChildChild.length > 0) {


            if (i == 0) {
                str += "<div class='tab-pane tab-child-menu active' id='div_" + levelChild[i].NodeID + "'>";
            } else {
                str += "<div class='tab-pane tab-child-menu' id='div_" + levelChild[i].NodeID + "' >";
            }
            str += loadMenuChild(levelChildChild, result, rootMap);
            str += "</div>";


        }
    });
    str += "</div>";
    str += "</div>";
    str += "</div>";
    $("[id=ifind-side-menu]").html("").prepend(str);

    showTab("ifmenu");
    $('.mysitemap').click(function (e) {
        var link = $(this).attr('link');
        if (link.toLowerCase().indexOf("list") != -1 || link.toLowerCase().indexOf("singfrm") != -1) {
            link = link.substring(0, link.lastIndexOf("/") + 1);
        }
        var title = $(this).attr('title');
        ChangeUrl(title, link);
        e.preventDefault();
        LoadAjaxTabView(this, e, false);
        try {
            var idsite = $.trim($(this).attr("data-map")).split('>');
            activeMenuPageLoad(idsite[idsite.length - 1])
        } catch (ex) { }

    });
}
function loadMenuChild(childList, allList, rootmap) {
    var str = "";
    str += "<ul class='nav nav-pills nav-stacked collapse' style='display:block;padding-left: 15px;font-size: 12px;'>";

    $.each(childList, function (i, value) {
        var levelChildChild = [];
        var url = "";
        if ($.trim(childList[i].Url) == "/" || $.trim(childList[i].Url) == "") {
            url = "#"
        } else {
            url = GetPath(childList[i].Url)
        }
        $.each(allList, function (j, val) {
            if (allList[j].ParentID == childList[i].NodeID) {
                levelChildChild.push(allList[j]);
            }
        })
        rootmap += ">" + childList[i].NodeID;
        if (levelChildChild.length > 0) {
            str += " <li class='dropdown' id='menuID_" + childList[i].NodeID + "'>";
            str += "<a class='menu-left dropdown-toggle'   href='#'><i class='glyphicon glyphicon-triangle-bottom'></i><span class='nav-label'>" + childList[i].Title + "</span></a>";
            str += loadMenuChild(levelChildChild, allList, rootmap);
            str += "</li>";
        }
        else {
            if (url == "/" || url == "#" || url == "") {
                str += "<li id='menuID_" + childList[i].NodeID + "'><a class='mysitemap'  data='update'  dis='' link='' data-map='" + rootmap + "' href='#' title='" + childList[i].Description + "'><i class='glyphicon glyphicon-triangle-right'></i><span class='nav-label'>" + childList[i].Title + "</span></a></li>";
            }
            else {
                str += "<li id='menuID_" + childList[i].NodeID + "'><a class='mysitemap'  data='update'  dis='mainctn' link='" + url + "' data-map='" + rootmap + "' href='#' title='" + childList[i].Description + "'><i class='glyphicon glyphicon-triangle-right'></i><span class='nav-label'>" + childList[i].Title + "</span></a></li>";
            }

        }
    })

    str += "</ul>";
    return str;
}
function activeMenuPageLoad(ids) {
    var href = $("meta[name=ControlerName]").attr("content")
    var result = AjaxGetData("/SiteMap/GetAll");
    var idSite = "";
    var rootMap = "";
    var rootmapbytitle = "";
    if (ids != "") {
        idSite = ids
    } else {
        $.each(result, function (i, val) {
            var url = $.trim(result[i].Url).toLowerCase();
            if (url.split('/').indexOf($.trim(href).toLowerCase()) != -1) {
                idSite = result[i].NodeID;
                return;
            }
        })
    }

    rootMap = idSite + createMaproot(idSite, result)
    //create root
    rootmapbytitle = rootmapbytitle + createMaprootByTitle(idSite, result)
    //show view
    $("[id=sitemap-top]").html(rootmapbytitle)
    if (rootMap != "") {
        var listID = rootMap.split('>');
        if (listID.length > 0) {
            $("ul li").removeClass("active");
            $("a[href='#menu_" + listID[listID.length - 1] + "']").click();
            $("[id=menuID_" + listID[listID.length - 2] + "] a").click();

            $("[id=menuID_" + listID[listID.length - 2] + "]").addClass("active");
            $(".tab-child-menu").removeClass("active");
            $(".tab-child-li").removeClass("active");
            $.each(listID, function (i, value) {

                $("[id=menuID_" + listID[i] + "]").addClass("active");
                $("[id=div_" + listID[i] + "]").addClass("active");


            })

        }


    }
}
function createMaproot(id, data) {

    var rootMap = "";
    for (var i = 0; i < data.length; i++) {
        if ($.trim(data[i].NodeID) == $.trim(id)) {
            if ($.trim(data[i].ParentID) != "00000000-0000-0000-0000-000000000000") {
                rootMap += ">" + data[i].ParentID;
                id = data[i].ParentID;
                i = 0;
            }
        }
    }
    return rootMap;
}
function createMaprootByTitle(id, data) {

    var rootMap = "";

    for (var i = 0; i < data.length; i++) {
        if ($.trim(data[i].NodeID) == $.trim(id)) {



            if ($.trim(data[i].ParentID) != "00000000-0000-0000-0000-000000000000") {
                rootMap += data[i].Title;
                id = data[i].ParentID;
                rootMap += ">";
                i = -1;

            }
            else {
                rootMap += data[i].Title;

            }


        }

    }
    var temp = rootMap.split('>');
    var str = "";

    for (var j = temp.length - 1; j >= 0; j--) {

        if (j == 0) {
            str += '<li class="active">';
            str += '<strong>' + temp[j] + '</strong>';
            str += '</li>';
        } else {
            str += ' <li>';
            str += temp[j];
            str += ' </li>';
        }

    }
    return str;
}

/*Calculate Enddate*/
function addDays(StartDate, EndDate, ddlPeriod,typePeriod) {

    if (ddlPeriod != null) {


        var index = ddlPeriod.selectedIndex;

        if (ddlPeriod.options[ddlPeriod.selectedIndex].text != 'In-Definite') {
           
            if (ddlPeriod.value.length == 0 || ddlPeriod.value == '0' || index <= 0) {
                ddlPeriod.style.borderColor = "Red";
                ddlPeriod.title = "This field is mandatory";
                alert('Please select Period');
                ddlPeriod.focus();

                //StartDate.value = '';
            }
            else if (StartDate.value == '' || StartDate.value == 'DD-MM-YYYY') {
                StartDate.style.borderColor = "Red";
                StartDate.title = "This field is mandatory";
               // alert('Please select StartDate');
                StartDate.focus();


            }
            else if (ddlPeriod.options[ddlPeriod.selectedIndex].text == 'In-Definite') {

                EndDate.value = '31-12-9999';
            }
            else {

//                if (typePeriod == 'Month') {
//                    val = ddlPeriod.options[ddlPeriod.selectedIndex].value * 30;

//                }
//                else if (typePeriod == 'Year') {

//                    val = (ddlPeriod.options[ddlPeriod.selectedIndex].value) / 12 * 365;
//                }

//                else {
//                    val = ddlPeriod.options[ddlPeriod.selectedIndex].value;

//                }


                if (typePeriod == 'Month' || typePeriod == 'Year') {

                    valmonth = ddlPeriod.options[ddlPeriod.selectedIndex].value;
                }

                else {
                    valmonth = ddlPeriod.options[ddlPeriod.selectedIndex].value / 30;
                }

                

               // noofdays = parseInt(val); //alert(noofdays);
                noofmonths = parseInt(valmonth); 
                var datepicker = StartDate.value;
                var dmy = datepicker.split("-");
                
                var dt = parseInt(dmy[0], 10);
                var mon = parseInt(dmy[1], 10) - 1;
                var yr = parseInt(dmy[2], 10);

                var joindate = new Date(yr,mon ,dt );
               
                var thisMonth = joindate.getMonth();
                var thisDate = joindate.getDate();

                //joindate.setMonth(thisMonth + valmonth);
                //joindate.setDate(thisDate - 1);                
                var enddate = new Date(new Date(joindate).setMonth(joindate.getMonth() + noofmonths));
                //enddate = new Date(new Date(enddate).setDate(joindate.getDate() -1));
                enddate.setDate(enddate.getDate() - 1);
              //  alert(joindate);
                //  alert(enddate);

                EndDate.value = ("0" + enddate.getDate()).slice(-2) + "-" + ("0" + (enddate.getMonth() + 1)).slice(-2) + "-" + enddate.getFullYear()
            }
        }
        else {
           
            EndDate.value = '31-12-9999';
        }
    }
}
