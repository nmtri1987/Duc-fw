function ChangeUrl(page, url) {
    if (typeof (history.pushState) != "undefined") {
        var obj = { Page: page, Url: url };
        history.pushState(obj, obj.Page, obj.Url);
    } else {
        alert("Browser does not support HTML5.");
    }
}
var editor;
var _from = "";
var _To = "";
var dt;
function IfindDrag(select, url) {
    $("." + select).sortable({
        connectWith: ".connectList",
        cursor: "move",
        update: function (event, ui) {
            var taskid = $(ui.item).attr('id');
            if (ui.sender === null) {
                _from = this.id;
            } else {
                _To = this.id;
                var pars = {
                    From: _from,
                    To: _To,
                    PMActivityCD: taskid
                };
                //  var url = '/PMTimeActivity/UpdateStatus';
                AjaxPostData(pars, url);
            }


        }
    }).disableSelection();
}
function showTab(ob) {

    var mainTab = $(ob);
    var tabclick = "." + ob + ">ul>li";
    var tablink;
    var showdiv = "." + ob + " .tab-pane";

    $(tabclick).click(function (e) {
        $(tabclick).removeClass("active");
        $(this).addClass("active");
        tablink = $(this).find("a").attr("tartgetID");
        $(showdiv).removeClass("active");
        $(tablink).addClass("active");
    });
}
function LoadAjaxTabView(ob, e, ifm) {
    e.preventDefault();
    var tabname = $(ob).attr("data");
    var url = $(ob).attr("link");
    var dis = $(ob).attr("dis");
    var showLink = $(ob).attr("show-link");
    if (typeof (showLink) != "undefined") {
        if (showLink) {
            ChangeUrl("", url);
        }
    }
    
    fnReloadPopUp(url, tabname, ifm, dis, false);
}
function LoadView(ob, ifm) {

    var tabname = $(ob).attr("data");
    var url = $(ob).attr("link");
    var dis = $(ob).attr("dis");
    var showLink = $(ob).attr("show-link");
    if (typeof (showLink) != "undefined") {
        if (showLink) {
            
            ChangeUrl("", url);
        }
    }
    
    
    $("#" + dis).html("<div class='modal-header'><i class='fa fa-laptop modal-icon'></i> <h4 class='modal-title'>Loading...</h4></div>")


    if (ifm) {
        $("#" + dis).html("<iframe frameborder='0' scrolling='no' marginheight='0' marginwidth='0' class='col-lg-12 col-md-12 col-sm-12' ' src='" + url + '&Tabname=' + tabname + "'></iframe>");
    } else {
        $("#" + dis).load(url + '&Tabname=' + tabname);
    }
}
$(document).ready(function () {

    $('.mybtn').click(function (e) {
        e.preventDefault();
        LoadAjaxTabView(this, e, false);
    });
    //$(".select-control").select2();//dropdown select2 jquery
    $('.cmdevent').click(function (e) {
        alert("Change password");
    });
    $(".ifdcard").flip({
        trigger: "hover"
    });
    ShowHideButtonFrm(true);//disablebutton
});
function closePopup() {
    $("#myModal .close").click();
}

function onBegin() {
    $(":submit").prop('disabled', true);
    $('#searchError').hide(0);
    $('#searchResults').hide(0);
    $('#loadingDisplay').show(0);
    $('#loadProgressBar').css('width', '50%').attr("aria-valuenow", 50);
}
function onComplete() {
    $('#loadProgressBar').css('width', '100%').attr("aria-valuenow", 100);
    $(":submit").prop('disabled', false);
    $("#loadingDisplay").delay(500).fadeOut(20).queue(function (next) {
        $('#loadProgressBar').delay(1200).css('width', '0%').attr("aria-valuenow", 0);
        next();
    });
    if($.isEmptyObject(dt)==false){
        dt.ajax.reload();
    }
    
    // BindDefault();
}
function onSuccess() {
    $('#searchResults').delay(800).slideDown(500);

    //BindDefault();
}
//Get Data Submit form Ajax 
var GetDataSubmit = function (data) {
    closePopup();
    if (data.StatusCode == 200) {
        notimessage("success", data.Message, "Success");
    } else {
        notimessage("error", data.Message, "Error");
    }

}

function onFailure() {
    $('#searchError').show(0);
}
/*
$(document).ready(function () {
    $('input[type="submit"]').attr('disabled', true);
    $('input[type="text"]').on('keyup', function () {
        if ($(this).val() != '') {
            $('input[type="submit"]').attr('disabled', false);
        } else {
            $('input[type="submit"]').attr('disabled', true);
        }
    });
});
*/

function ImageUpload(link, display, data) {

    $.ajax({
        type: "POST",
        url: link,
        contentType: false,
        processData: false,
        data: data,
        beforeSend: onBegin,
        complete: onComplete,
        success: function (result) {
            $('#' + display).attr('src', result.link);
            var hiddenImg = $('#' + display).attr('data-hiddenimg');
            $('#' + hiddenImg).val(result.filename);
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });



}
function FileUpload(link, data) {
    var res = "";
    $.ajax({
        type: "POST",
        url: link,
        contentType: false,
        processData: false,
        data: data,
        async: false,
        success: function (result) {
            res = result;
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }

    });
    return res;
}
//show popup true /f
function ld(open) {
    if (open == true) {
        $("#popcnt").html("<div class='modal-header'><i class='fa fa-laptop modal-icon'></i> <h4 class='modal-title'>In Progress...</h4></div>");
        $("#myModal").modal("show");
    } else {
        $("#popcnt").html("");
        $("#myModal").modal("hide");
    }
}

function tableshow(selector, ajaxlink, hdarr, PKID) {
    var ColArray = eval($("#" + hdarr).val());
    var editlink = ajaxlink.replace("GetGata", "") + "Update?" + PKID + "=";
    ///ALT/OG/EPEarningType/Update?TypeCD=HL
    // alert(editlink);
    var myCol = [];
    var editclass = "btn" + selector;
    var pkField =
        {
            data: PKID,
            render: function (data, type, row) {
                if (type === 'display') {
                    return '<input type="checkbox" class="editor-active i-checks" value="' + data + '" name="' + PKID + '" >';
                }
                return data;
            },
            className: "dt-body-center ifind"
        };
    myCol.push(pkField);
    pkField =
        {
            data: PKID,
            title: 'Name',
            render: function (data, type, row) {
                if (type === 'display') {
                    return "<a href='#item" + data + "' target='edit' class='" + editclass + "' data-target='#myModal' data-toggle='modal' data='update' link='" + editlink + data + "' dis='popcnt'>" + data + "</a>";
                }
                return data;
            },
            className: "dt-body-center ifind"
        };
    myCol.push(pkField);
    for (var i = 0; i < ColArray.length; i++) {
        // var b = "{'title': '" + ColArray[i] + "', 'data': '" + ColArray[i] + "'}";
        var b = {
            title: ColArray[i],
            data: ColArray[i],
            className: "ifind"
        }
        myCol.push(b);
    }

    //editor = new $.fn.dataTable.Editor( {
    //    ajax: "../php/staff.php",
    //    table: "#" + selector,
    //    fields: myCol
    //});

    //$('#' + selector).on('click', 'tbody td:not(:first-child)', function (e) {
    //    editor.inline(this);
    //});
    var dt = $('#' + selector).DataTable({
        "serverSide": true,
        "processing": true,
        "ajax": ajaxlink,
        "search": {
            "search": ""
        },
        "columns": myCol,
        "lengthMenu": [[25, 50], [25, 50]],
        fnDrawCallback: function (oSettings) {
            $("." + editclass).click(function (e) {
                e.preventDefault();
                LoadAjaxTabView(this, e, false);
            });
        }
    });

}





function AddrowColapseTable(selector) {
    var countCol = $('#' + selector + ' thead th').length;
    $('#' + selector + ' tbody tr').each(function () {
       
        var id = $(this).find("button.btn-expand").attr("id")
        $(this).after("<tr class='collapse' id='ListDetailOne_" + id + "'><td colspan='" + countCol + "'><div id='ListDetail_" + id + "'></div></td></tr>")

        try {
            var searchkey = ($('#' + selector).attr("search-advanced")).split(',')
            var PKID = $("#" + selector).attr("key");
            var url = $(this).find("button.btn-expand").attr('link-default');
            $.each(searchkey, function (i, value) {
                if (searchkey[i] != PKID) {
                    if (i == 0) {
                        url += "?"
                    }
                    url += searchkey[i] + "=" + $("#" + searchkey[i]).val();
                    url += "&"
                    
                }

            })
            
            $(this).find("button.btn-expand").attr('link', url)
        } catch (ex) {

        }
    })

    $('.btnExpend' + selector + '').click(function (e) {
        e.preventDefault();
        var data = $("#" + selector).DataTable().row($(this).parents("tr")).data()
        var url = $(this).attr("link");
        url += "CompanyID=" + data.CompanyID;
        $(this).attr('link', url)
        if ($(this).attr("aria-expanded") == "false") {
            LoadAjaxTabView(this, e, false);
        }

    });

}
function ConvertDatetimeJsonDataTable(selector) {
    $("#" + selector + " tbody td").each(function () {
        var value = $(this).text();

        if (value.indexOf("Date") != -1) {
            try {
                $(this).text(JsonFormatDate(value));
            }
            catch (ex) {
                console.log(ex.message)
            }
        }


    });
}
function formatDattimeJsonDataTable(value) {
    try {
        return JsonFormatDate(value);
    }
    catch (ex) {
        return value;
    }

}
function formatNumberDataTable(value) {

    if (!isNaN(parseFloat(value))) {
        try {
            return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }
        catch (ex) {
            return value;
        }
    }
    return value;
}
function LoadAutoComplete(selector) {
    var url = $(selector).attr("data-url");
    var formID = $(selector).attr("data-form-id");
    var minlength = $(selector).attr("data-length");
    if (minlength == undefined && minlength == "") {
        minlength = 3
    }

    var arr = [];
    var columns = [];
    var count = 0;
    var selectItem = 0;
    var result = [];
    var i = 0;
    $("[id=" + formID + "] .form-control").each(function () {

        var name = $(this).attr("name");
        arr.push(name)
        //console.log(name)
        if (count < 4) {
            if (name.toLocaleLowerCase().indexOf("id") == -1 && !$(this).hasClass("autohiden")) {
                columns.push({ name: name, minWidth: 1 + 'px', valueField: i })
                count++;
            }

        }
        i++;
    })

    //update min width column 
    var withmin = ($(selector).width()) / count;
    $.each(columns, function (i, value) {
        columns[i].minWidth = withmin + "px"
    })
    //console.log("1")
    //end update min width column 
    $(selector).mcautocomplete({
        showHeader: true,
        columns: columns,
        source: function (request, response) {
            var pars = {
                Keyword: request.term,
                OrderBy: "",
                OrderDirection: 'ASC',
                Page: 1,
                PageSize: 100
            };
            result = AjaxPostData(pars, url)
            var array = [];

            $.each(result, function (i, value) {
                var arraychild = [];
                $.each(arr, function (j, value) {
                    try {
                        if ($.trim(arr[j]).indexOf(".") != -1) {
                            var temp = $.trim(arr[j]).split('.');
                            var dataObj = result[i][temp[0]];
                            arraychild.push(dataObj[temp[1]])
                        } else {
                            arraychild.push(result[i][arr[j]])
                        }

                    } catch (e) {
                        console.log(e.message)
                    }

                })
                array.push(arraychild);
            })
            if (result == null || result.length == 0) {
                var arraychild = [];
                arraychild.push("No Record")
                arraychild.push("")
                arraychild.push("")
                arraychild.push("")
                array.push(arraychild);
            }
            ShowHideButtonFrm(false);//disablebutton
            response(array);
        },
        minLength: minlength,
        select: function (event, ui) {
            selectItem = 1;
            if (ui.item && ui.item[1] != "") {
                this.value = (ui.item ? ui.item[0] : '');
                $.each(arr, function (j, value) {
                    try {
                        var type = $("[name='" + arr[j] + "']")
                        switch (type.attr("type")) {
                            case "checkbox":
                                try {
                                    type.prop("checked", ui.item[j]);
                                } catch (e) {
                                    console.log(e.message)
                                }
                                break;
                            default:
                                try {
                                    type.val($.trim(ui.item[j]))
                                } catch (e) {
                                    console.log(e.message)
                                }
                                break;

                        }
                    } catch (e) {
                        console.log(e.message)
                    }



                })
            }

            return false;
        }
    })
    //console.log("2")

    $(selector).blur(function () {

        var data = $(selector).val();
        var pars = {
            keyword: data,
            orderBy: "",
            orderDirection: 'ASC',
            page: 1,
            pageSize: 100
        };
        var results = AjaxPostData(pars, url)
        var result = [];
        var values = $(selector).val();
        var name = $(selector).attr("name");
        $.each(results, function (j, value) {
            if ($.trim(results[j][name]).toLowerCase() == values.toLowerCase()) {
                result.push(results[j])
                return;
            }
        })
        if (result.length == 1) {
            $.each(arr, function (j, value) {
                try {
                    var type = $("[name='" + arr[j] + "']")
                    switch (type.attr("type")) {
                        case "checkbox":
                            try {
                                if (type.hasClass("i-checks")) {
                                    if (result[0][arr[j]]) {
                                        type.iCheck('check')
                                    } else {
                                        type.iCheck('uncheck')
                                    }
                                } else {
                                     type.prop("checked", result[0][arr[j]]);
                                }
                               

                            } catch (e) {
                                console.log(e.message)
                            }
                            break;
                        default:
                            try {
                                if ($.trim(arr[j]).indexOf(".") != -1) {
                                    var temp = $.trim(arr[j]).split('.');
                                    var dataObj = result[0][temp[0]];
                                    // arraychild.push(dataObj[temp[1]])
                                    type.val($.trim(dataObj[temp[1]]))
                                } else {
                                    type.val($.trim(result[0][arr[j]]))
                                    // arraychild.push(result[i][arr[j]])
                                }
                                // type.val($.trim(result[0][arr[j]]))
                            } catch (e) {
                                console.log(e.message)
                            }
                            break;

                    }
                    ShowHideButtonFrm(false);//disablebutton
                } catch (e) {
                    console.log(e.message)
                }

            })
        } else {

            try {
                $("[data-action=add]").click()
                $(selector).val(data)
                if ($.trim(data) != "") {
                    try { $("[data-action=save]").prop("disabled", false) } catch (e) { }
                }


            } catch (e) {

            }

            //$(selector).val("");//If have data --> Load form , Else --> a) Key --->Create New b)Autonumebring --> Show <new> change later
        }
        $(selector).unbind("blur")
        setTimeout(function () {
            $(".ui-autocomplete").css("display", "none")
        }, 500)

    })

}

function RefreshForm(selector) {
    var arr = [];
    var url = $(selector).attr("data-url");
    var formID = $(selector).attr("data-form-id");
    var action = $(selector).attr("data-action");
    var key = $(selector).attr("data-key-id");
    var id = $("[id=" + key + "]").val();
    var valueSearch = $(".input-auto").val();
    switch (action) {
        case "refresh":
            if (id == null || id == "" || id == "New" || id == "0") {
                url = url + "/" + 0 + "?action=" + action;
            } else {
                url = url + "/" + id + "?action=" + action;
            }

            break;
        case "add":

            url = url + "/" + 0 + "?action=" + action;
            break;
    }
    $("[id=" + formID + "] .form-control").each(function () {
        var name = $(this).attr("name");
        arr.push(name)
    })
    var result = AjaxGetData(url)
    $.each(arr, function (j, value) {
        var type = $("[name='" + arr[j] + "']")
        switch (type.attr("type")) {
            case "checkbox":
                try {
                    if (result[arr[j]]) {
                        type.prop("checked", true);
                    } else {
                        type.prop("checked", false);
                    }
                } catch (e) {
                    console.log(e.message)
                }
                break;
            default:
                try {
                    if ($.trim(arr[j]).indexOf(".") != -1) {
                        var temp = $.trim(arr[j]).split('.');
                        var dataObj = result[temp[0]];
                        // arraychild.push(dataObj[temp[1]])
                        type.val($.trim(dataObj[temp[1]]))
                    } else {
                        type.val($.trim(result[arr[j]]))
                        // arraychild.push(result[i][arr[j]])
                    }
                } catch (e) {
                    console.log(e.message)
                }
                break;
        }
    })
    console.log(valueSearch)
    if (valueSearch != "" && valueSearch != null && action != "add") {
        $(".input-auto").val(valueSearch)
        ShowHideButtonFrm(false);//disablebutton
    } else {
        ShowHideButtonFrm(true);//disablebutton
    }
}

function DeleteData(selector) {
    var arr = [];
    var url = $(selector).attr("data-url");
    var formID = $(selector).attr("data-form-id");
    var action = $(selector).attr("data-action");
    var key = $(selector).attr("data-key-id");
    var id = $("[id=" + key + "]").val();
    if (id == null || id == "") return;
    var pars = {
        id: id,
        Action: "delete"
    }
    $("[id=" + formID + "] .form-control").each(function () {
        var name = $(this).attr("name");
        arr.push(name)
    })
    AjaxPostData(pars, url)
    try {
        fnNotification(1, "Delete Success")
        $("[data-action=add]").click()
    } catch (e) {

    }
}

function ChangePassword(selector) {
    var url = $(selector).attr("data-url");
    var formID = $(selector).attr("data-form-id");
    var action = $(selector).attr("data-action");
    var key = $(selector).attr("data-key-id");
    var temp = key.split(',');
    var userName = $("[id=" + temp[0] + "]").val()
    var password = $("[id=" + temp[1] + "]").val()
    url = url + "?userName=" + userName + "&password=" + password;
    var result = AjaxGetData(url);
    if (result) {
        fnNotification(1, "Change Pass Success!")
    }
}
function ShowHideButtonFrm(status) {
    try {
        $("[data-action=save]").prop("disabled", status)
        var key = $("[data-action=delete]").attr("data-key-id");
        var id = $("[id=" + key + "]").val()
        if ($.trim(id) == "" || $.trim(id) == "New" || $.trim(id) == "0") {
            $("[data-action=delete]").prop("disabled", true)
            $("[data-action=changepass]").prop("disabled", true)

        } else {
            $("[data-action=delete]").prop("disabled", false)
            $("[data-action=changepass]").prop("disabled", false)
        }

    } catch (e) { }

}

function fnNotification(status, mes) {
    switch (status) {
        case 1:
            $(".message-notify").parent().addClass("alert-success")

            break
        case 2:
            $(".message-notify").parent().addClass("alert-danger")
            break;
    }
    $(".message-notify").text(mes);
    $(".alert").slideToggle(1000)
    setTimeout(function () {
        $(".alert").slideToggle(200)
    }, 4000)

}

function LoadTabView(tabid, isfrm) {
    var url = $("#" + tabid).attr("data-url");
    var querystr = $("#" + tabid).attr("data-query");
    var targetid = $("#" + tabid).attr("data-targetid");
    if (querystr != "") {
        var temp = querystr.split(',');
        if (temp.length > 0) {
            url += "?";
            $.each(temp, function (i, value) {
                url += temp[i] + "=" + $("#" + temp[i]).val();
                if (temp.length > i) {
                    url += "&"
                }
            })
        }
    }

    if (isfrm) {
        $("#" + targetid).html("<iframe frameborder='0' scrolling='no' marginheight='0' marginwidth='0' class='col-lg-12 col-md-12 col-sm-12' ' src='" + url + "'></iframe>")
    } else {
        $("#" + targetid).load(url);
    }
}


function StartJSSingleForm() {//chỉnh
    $("[id=tabs]").tabs();
    var dataKey = $(".single-form").attr("data-key");
    var listTab = [];
    $("[id=tabs] ul.ul-tabs li a.multi-tab-head").each(function () {
        var id = $(this).attr("id")
        if (id != "" && id != undefined && id.indexOf("ui") == -1) {
            listTab.push(id)
        }

    })
    LoadMultiTab(listTab)
    $("#" + dataKey).on("change", function () {
        LoadMultiTab(listTab)
    })
    $(".btnLoadtab").on("click", function () {
        LoadMultiTab(listTab)
    })
}

function LoadMultiTab(listTab) {
    $.each(listTab, function (i, value) {
        LoadTabView($.trim(listTab[i]), false)
    })
}

function ShowModalMessage(selector, btnfunc, message) {
    var txtmessage = $("#" + selector).attr("data-message");
    var txtbutton = $("#" + selector).attr("data-button");
    $("#" + selector + " #" + txtmessage + "").text(message);
    $("#" + selector + " #" + txtbutton + "").attr("onclick", btnfunc)
    $("#" + selector + "").modal({
        backdrop: "static"
    })
    $("#" + selector + "").modal('show')
}
/*table*/
function fnEditabledblClick(Selector) {
    $(Selector).children("input").show();
    $(Selector).children("label").hide();
    $(Selector).children("input").focus();
    $(Selector).children("input").select();
}
function fnEditableblur(Selector) {
    $(Selector).hide();
    $(Selector).prev("label").text($(Selector).val())
    $(Selector).prev("label").show();
}

function fnAddRowTable(selector, columnNumber, arr) {
    var hidden = $("#" + selector).attr("data-hidden-field");
    var str = '<tr onclick="fnActiveRowTable(this,\'' + hidden + '\')">';
    for (var i = 0; i < columnNumber; i++) {
        var index = fnGetArrfromObject("index", arr).indexOf(i);
        if (index != -1) {
            var type = arr[index].Type;
            switch (type) {
                case "text":
                    if (arr[index].hasHidden) {
                        str += '<td ondblclick="fnEditabledblClick(this)"><input type="hidden"/><label class="nonadd-text"></label><input type="text" onblur="fnEditableblur(this)" class="form-control add-text"/></td>';

                    } else {
                        str += '<td ondblclick="fnEditabledblClick(this)"><label class="nonadd-text"></label><input type="text" onblur="fnEditableblur(this)" class="form-control add-text"/></td>';
                    }
                    break;
                default:
                    str += '<td class="text-center"><input type="checkbox" /></td>';
                    break;
            }
        } else {
            str += '<td></td>';
        }
    }
    str += '</tr>';
    $("#" + selector + " tbody .dataTables_empty").parent().remove()
    $("#" + selector + " tbody").append(str);
}

function fnGetArrfromObject(key, arr) {
    var result = [];
    $.each(arr, function (i, value) {
        result.push(arr[i][key])
    })
    return result;
}
function fnActiveRowTable(selector, hidden) {
    //#BEEBFF
    if (!$(selector).hasClass("active")) {
        $(selector).parent().find("tr").removeClass("active")
        $(selector).addClass("active");
        $("#" + hidden).val($(selector).index());
    }

}
function fnReloadPopUp(url, tabname, ifm, dis, isShow) {
    $("#" + dis).html("<div class='modal-header'><i class='fa fa-laptop modal-icon'></i> <h4 class='modal-title'>Loading...</h4></div>");
   
    if (ifm) {
        $("#" + dis).html("<iframe frameborder='0' scrolling='no' marginheight='0' marginwidth='0' class='col-lg-12 col-md-12 col-sm-12' ' src='" + url + '&Tabname=' + tabname + "'></iframe>");
    } else {

        if (url.indexOf("?") != -1) {
            $("#" + dis).load(url + '&Tabname=' + tabname + "&ajax=1&dt=" + Math.random(), function (responseTxt, statusTxt, xhr) {
                if (statusTxt == "success") {
                    BindDefault();
                }
            });
        } else {
            $("#" + dis).load(url + '?Tabname=' + tabname + "&ajax=1&dt=" + Math.random(), function (responseTxt, statusTxt, xhr) {
                if (statusTxt == "success") {
                    BindDefault();
                }
            });
        }
    }
    
    if (isShow) {
        $("#myModal").modal("show");
    }
}
//DNH
function unBind() {
    $('.i-checks').unbind();
    //$('.chosen-select').unbind();
    $('.date').unbind();
    $('.mybtn').unbind();
}
//DNH
function BindDefault() {
    unBind();
    $(".chosen-select").select2();
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
    $('.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true
    });
    $(".ifdcard").flip({
        trigger: "hover"
    });
    $('.mybtn').click(function (e) {
        e.preventDefault();
        LoadAjaxTabView(this, e, false);
    });
}
function fnLoadPopUpIfNeed(idItem, controllerName) {
    switch (controllerName) {
        case "PMTimeActivity":
            var url = GetPath("/PMTimeActivity/Update?PMActivityCD=" + idItem + "")
            fnReloadPopUp(url, "update", false, "popcnt", true)
            break;
    }
}

function fnLoadAttach(requestid, controllerName) {
    switch (controllerName) {
        case "PMProject":
            var url = "/PMProject/GetAttachProject?PMProjectCD=" + requestid;
            var result = AjaxGetData(url);
            if (result.length > 0) {
                var str = "";
                $.each(result, function (i, value) {
                    var path = GetPath("/PMProject/DownLoadFile?ProjectCD=" + requestid + "&AttachID=" + result[i].AttachID + "&FileType=" + result[i].Filetype + "&Tilte=" + result[i].Title)
                    str += "<li>"
                    str += "<a href='" + path + "'><i class='fa fa-file'></i> "
                    str += result[i].Title + "." + result[i].Filetype
                    str += "</a>"
                    str += "</li>"
                })
                $("#projectfile").html('').prepend(str);
            }
            break;
    }
}

function fnLoadAutocompleteOneColumn(selector) {
    var url = $(selector).attr("data-url");
    var formID = $(selector).attr("data-form-id");
    var minlength = $(selector).attr("data-length");
    var id = $(selector).attr("id");
    var targetID = $(selector).attr("data-tagetID");
    var columns = []
    columns.push("" + id)
    $(selector).autocomplete({
        showHeader: false,
        columns: columns,
        source: function (request, response) {
            var pars = {
                Keyword: request.term,
                OrderBy: "",
                OrderDirection: 'ASC',
                Page: 1,
                PageSize: 100
            };
            result = AjaxPostData(pars, url)
            var array = [];
            if (result == null || result.length == 0) {
                array.push("No Record");
            } else {
                $.each(result, function (i, value) {
                    var arraychild = [];
                    console.log(result)
                    array.push(result[i][id]);
                })
            }
            response(array);
        },
        minLength: minlength,
        select: function (event, ui) {
            this.value = (ui.item ? ui.item.value : '');
            if (this.value == "No Record") {
                this.value = "";
            }
            var url = GetPath(window.location.pathname + "/SingFrm/?" + id + "=" + this.value);
            fnReloadPopUp(url, "data", false, targetID, false);

            return false;
        }
    })
    $(selector).blur(function () {
        var value = $(this).val();
        var url = GetPath(window.location.pathname + "/SingFrm/?" + id + "=" + this.value);
        fnReloadPopUp(url, "data", false, targetID, false);
    })
}

function fnfinFormat(str) {
    var month = str.substring(4, 6)
    var year = str.substring(0, 4)
    if (parseInt(month) <= 9) {
        month = "0" + parseInt(month);
    }
    return month + "-" + year;
}

function TableShow(selector) {
    var myselect = "#" + selector;
    var allowCheckBox = true;
    var AllowSearch = true;
    var ajaxlink = $("#" + selector).attr("data-url");
    var hdarr = $("#" + selector).attr("arr-id");
    var PKID = $("#" + selector).attr("key");
    var PKIDTitle = $("#" + selector).attr("key-title");
    var txtsearch = $("#" + selector).attr("default-search");
    var linkEditExten = "";
    try { linkEditExten = $("#" + selector).attr("link-edit") } catch (ex) { linkEditExten = ""; }

    var ColArray = eval($("#" + hdarr).val());
    var editlink = ajaxlink.substring(0, ajaxlink.lastIndexOf("/") + 1) + "Update?" + PKID + "=";
    if (linkEditExten != "" && linkEditExten != undefined) {
        editlink = linkEditExten + "&" + PKID + "=";
    }

    var search = $("#" + selector).attr("allow-search");
    var checkbox = $("#" + selector).attr("allow-chk");
    if (search == 0) {
        AllowSearch = false;
    }
    if (checkbox == 0) {
        allowCheckBox = false;
    }
    ///ALT/OG/EPEarningType/Update?TypeCD=HL
    // alert(editlink);
    var myCol = [];
    var myformatCol = [];
    var editclass = "btn" + selector;
    if (allowCheckBox) {
        var pkField =
            {
                data: PKID,
                render: function (data, type, row) {
                    if (type === 'display') {
                        return '<input type="checkbox" class="editor-active i-checks" value="' + data + '" name="' + PKID + '" >';
                    }
                    return data;
                },
                className: "dt-body-center"
            };
        myCol.push(pkField);
    }

    var titleKey = PKID;
    if (PKIDTitle != undefined) {
        titleKey = PKIDTitle
    }
    pkField =
        {
            data: PKID,
            title: titleKey,
            render: function (data, type, row) {
                if (type === 'display') {
                    if (linkEditExten != "" && linkEditExten != undefined) {
                        return "<a href='#item" + data + "' target='edit' class='" + editclass + "' data='update' link='" + editlink + $.trim(data) + "' dis='popcnt'>" + data + "</a>";
                    }
                    return "<a href='#item" + data + "' target='edit' class='" + editclass + "' data-target='#myModal' data-toggle='modal' data='update' link='" + editlink + $.trim(data) + "' dis='popcnt'>" + data + "</a>";
                }
                return data;
            },
            className: "dt-body-center"
        };
    myCol.push(pkField);
    for (var i = 0; i < ColArray.length; i++) {
        if (ColArray[i].name == undefined) {
            var b = {
                title: ColArray[i],
                data: ColArray[i],
                className: "ifind"
            }
            myCol.push(b);
        } else {
            var colType = ColArray[i].type;
            var isSortable = true;
            var titleTable = ColArray[i].name;
            if (ColArray[i].sort != undefined) {
                isSortable = ColArray[i].sort
            }
            
            if (ColArray[i].title != undefined) {
                titleTable = ColArray[i].title
            }
            var b = {
                title: titleTable,
                data: ColArray[i].name,
                orderable: isSortable,
            }
            myCol.push(b);
            switch (colType) {
                case "number":
                    var temp = {
                        "targets": myCol.length - 1,
                        "data": ColArray[i].name,
                        "render": function (data, type, full, meta) {
                            if (data == null) {
                                return 0;
                            }
                            return addCommas(data);
                        },
                        "className": "text-right"
                    }
                    myformatCol.push(temp);
                    break;
                case "date":
                    var temp = {
                        "targets": myCol.length - 1,
                        "data": ColArray[i].name,
                        "render": function (data, type, full, meta) {
                            return formatDattimeJsonDataTable(data);
                        }
                    }
                    myformatCol.push(temp);
                    break;
                case "bool":
                    var temp = {
                        "targets": myCol.length - 1,
                        "data": ColArray[i].name,
                        "render": function (data, type, full, meta) {
                            if (data == true) {
                                return "<input type='checkbox' class='i-checks' checked='checked' disabled=''/>";
                            }
                            return "<input type='checkbox' class='i-checks' disabled=''/>";
                        },
                        "className": "text-center"
                    }
                    myformatCol.push(temp);
                    break;
                case "numberbox":
                    var idnum=ColArray[i].idfield
                    var temp = {
                        "targets": myCol.length - 1,
                        "data": ColArray[i].name,
                        "render": function (data, type, full, meta) {
                            data = -(parseFloat(data));
                            return "<input type='textbox' class='form-control' id='" + idnum + "' value='" + data + "'/>";
                        },
                        "className": "text-center"
                    }
                    myformatCol.push(temp);
                    break;
                case "datebox":
                    var id = ColArray[i].idfield
                    var temp = {
                        "targets": myCol.length - 1,
                        //"data": ColArray[i].name,
                        "render": function (data, type, full, meta) {
                            var date = FormatDate(new Date(), false)
                            return "<div class='input-group date'  style='width:150px'><input type='textbox' class='form-control' id='" + id + "' value='" + date + "'/><span class='input-group-addon'><i class='fa fa-calendar'></i></span></div>";
                        },
                        "className": "text-center"
                    }
                    myformatCol.push(temp);
                    break;
                case "button":
                    var dataParam = ColArray[i].boxfield
                    var url = ColArray[i].url;
                    var temp = {
                        "targets": myCol.length - 1,
                        //"data": ColArray[i].name,
                        "render": function (data, type, full, meta) {
                            return "<button type='button' class='btn btn-primary btn-block button' data-param='" + dataParam + "' data-url='"+url+"' name='btnsave' value='save'>Pay</button>";
                        },
                        "className": "text-center"
                    }
                    myformatCol.push(temp);
                    break;

            }
        }

    }
    dt = $('#' + selector).DataTable({
        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": ajaxlink,
            "type": "POST"
        },
        "bFilter": AllowSearch,
        "search": {
            "search": txtsearch
        },
        "columns": myCol,
        "columnDefs": myformatCol,
        "lengthMenu": [[25, 50], [25, 50]],
        fnDrawCallback: function (oSettings) {
            //ConvertDatetimeJsonDataTable(selector)
            //formatNumberDataTable(selector)
            //$('.ifind').editable('http://www.example.com/save.php', {
            //    indicator: 'Saving...',
            //    tooltip: 'Click to edit...'
            //});
            $('.date').datepicker({
                //todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
            });
            
            $("." + editclass).unbind("click")
            $("." + editclass).click(function (e) {
                e.preventDefault();
                LoadAjaxTabView(this, e, false);
            });
            $(".refresh-button").unbind("click");
            $(".refresh-button").click(function () {
                dt.ajax.reload();
            });
            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
            });
            var lineUpdate = $(myselect).attr("link-update");
            if ($.isEmptyObject(lineUpdate) == false) {
                $('.ifind').editable(lineUpdate, {
                    indicator: 'Saving...',
                    tooltip: 'Click to edit...',
                    onblur: "submit",
                    submitdata: function (svalue, settings) {
                        //var value = $(this).find("input").val();
                        var aray = $(this).find("form").serializeArray();
                        var visIdx = $(this).index();
                        var idx = table.row($(this).parent()).index();
                        table.cell(idx, visIdx).data(aray[0].value)
                        var rowData = table.row($(this).parent()).data();     
                        return { objdata: JSON.stringify(rowData)};
                    },
                    callback: function (value, settings) {
                        var data = JSON.parse(value);
                        if (data.StatusCode == 200) {
                            notimessage("success", data.Message, "success");
                            table.row($(this).parent()).data(data.Data);
                            //fix display menu
                          //  table.ajax.reload();
                            
                        } else if (data.StatusCode == 400) {
                            notimessage("error", "Has something wrong, Please contact admin for information!", "Error");
                        }
                    }
                });
            }
        }
    });
    $('#' + selector + ' tbody .button').unbind("click")
    $('#' + selector + ' tbody').on('click', '.button', function () {
        //button in row click envent
        var data = dt.row($(this).parents('tr')).data();
        var param = $(this).attr("data-param").split(',');
        var id = $(this).parents("tr")
        var url = $(this).attr("data-url");
        var str = '{';
        $.each(param, function (i, value) {
            if (data[param[i]] != undefined) {
                str += '"' + param[i] + '":"' + data[param[i]] + '"';
            } else {
                var temp = id.find("input#" + param[i])
                if (temp != undefined) {
                    str += '"' + param[i] + '":"' + temp.val() + '"';
                }

            }

            if (i < param.length - 1) {
                str += ',';
            }
        })
        str += '}';
        var pars = JSON.parse(str)
        var result = AjaxPostData(pars, url)
        if (result.StatusCode == 200) {
            dt.ajax.reload();
        }

    });

}

function TableReportShow(selector) {
    var allowCheckBox = true;
    var AllowSearch = true;
    var AllowSort = true;
    var myselect = "#" + selector;
    var ajaxlink = $(myselect).attr("data-url");
    var hdarr = $(myselect).attr("arr-id");
    var PKID = $(myselect).attr("key");
    var txtsearch = $(myselect).attr("default-search");

    var ColArray = eval($("#" + hdarr).val());
    var editlink = ajaxlink.substring(0, ajaxlink.lastIndexOf("/") + 1) + "Update?" + PKID + "=";

    var search = $(myselect).attr("allow-search");
    var sort = $(myselect).attr("allow-sort");
    var checkbox = $(myselect).attr("allow-chk");
    var expand = $(myselect).attr("allow-Expand");
    var linkExpand = $(myselect).attr("link-Expand");
    var lineUpdate = $(myselect).attr("link-update");
    var lengthmenu = []
    try{
        var recordData = $(myselect).attr("recordpage");
        if (recordData != "") {
            lengthmenu.push(recordData.split(','))
            lengthmenu.push(recordData.split(','))
        }
    }catch(ex){}
    if (lengthmenu.length == 0) {
        lengthmenu = [[50, 100, 500], [50, 100, 500]]
    }
    if (search == 0) {
        AllowSearch = false;
    }

    if (sort == 0) {
        AllowSort = false;
    }
    if (checkbox == 0) {
        allowCheckBox = false;
    }
    ///ALT/OG/EPEarningType/Update?TypeCD=HL
    // alert(editlink);
    var myCol = [];
    var myformatCol = [];
    var editclass = "btn" + selector;
    if (allowCheckBox) {
        var pkField =
            {
                data: PKID,
                render: function (data, type, row) {
                    if (type === 'display') {
                        return '<input type="checkbox" class="editor-active i-checks" value="' + data + '" name="' + PKID + '" >';
                    }
                    return data;
                },
                className: "dt-body-center"
            };
        myCol.push(pkField);
    }
    pkField =
        {
            data: PKID,
            title: PKID,
            render: function (data, type, row) {
                if (type === 'display') {
                    return data
                    //"<a href='#item" + data + "' target='edit' class='" + editclass + "' data-target='#myModal' data-toggle='modal' data='update' link='" + editlink + data + "' dis='popcnt'>" + data + "</a>";
                }
                return data;
            },
            className: "dt-body-center"
        };
    myCol.push(pkField);
    for (var i = 0; i < ColArray.length; i++) {
        // var b = "{'title': '" + ColArray[i] + "', 'data': '" + ColArray[i] + "'}";
        //console.log(ColArray[i].name)
        if (ColArray[i].name == undefined) {

            var b = {
                title: ColArray[i],
                data: ColArray[i],
                className: "ifind"
            }
            myCol.push(b);
        } else {
            var colType = ColArray[i].type;
            var titleTable = ColArray[i].name;

            if (ColArray[i].title != undefined) {
                titleTable = ColArray[i].title
            }
            var b = {
                title: titleTable,
                data: ColArray[i].name,
                className: "ifind"
            }

            myCol.push(b);
            switch (colType) {
                case "number":
                    var temp = {
                        "targets": myCol.length - 1,
                        "data": ColArray[i].name,
                        "render": function (data, type, full, meta) {
                            if (data == null) {
                                return 0;
                            }

                            return addCommas(data);
                        },
                        "className": "text-right"
                    }
                    myformatCol.push(temp);
                    break;
                case "date":
                    var temp = {
                        "targets": myCol.length - 1,
                        "data": ColArray[i].name,
                        "render": function (data, type, full, meta) {
                            return formatDattimeJsonDataTable(data);
                        },

                    }
                    myformatCol.push(temp);
                    break;
                case "bool":
                    var temp = {
                        "targets": myCol.length - 1,
                        "data": ColArray[i].name,
                        "render": function (data, type, full, meta) {
                            if (data == true) {
                                return "<input type='checkbox' class='i-checks' checked='checked' disabled=''/>";
                            }
                            return "<input type='checkbox' class='i-checks' disabled=''/>";
                        },
                        "className": "text-center ifind"
                    }
                    myformatCol.push(temp);
                    break;

            }
        }
    }
    if (expand != 0) {
        pkField =
       {
           data: PKID,
           render: function (data, type, row) {
               if (type === 'display') {

                   return "<button id='" + data + "'  target='edit' class='btn btn-sm btn-expand btn-info btnExpend" + selector + "' data-toggle='collapse' data-target='#ListDetailOne_" + data + "' data='update' link='" + linkExpand + "?" + PKID + "=" + data + "&' link-default='" + linkExpand + "?" + PKID + "=" + data + "&' dis='ListDetail_" + data + "' aria-expanded='false'><i class='fa fa-list'></i></button>";
               }
               return data;
           },
           className: "dt-body-center"
       };
        myCol.push(pkField);
    }
    //console.log(myCol)
    var rowdata;
    var table = $('#' + selector).DataTable({
        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": ajaxlink,
            "type": "POST"
        },
        "bSort": AllowSort,
        "bFilter": AllowSearch,
        "search": {
            "search": txtsearch
        },
        "columns": myCol,
        "columnDefs": myformatCol,
        "lengthMenu": lengthmenu,
        /// select: true,
        fnDrawCallback: function (oSettings) {
            if (expand != 0) {
                AddrowColapseTable(selector)
            }
            if ($.isEmptyObject(lineUpdate) == false) {
                $('.ifind').editable(lineUpdate, {
                    indicator: 'Saving...',
                    tooltip: 'Click to edit...',
                    onblur: "submit",
                    submitdata: function (svalue, settings) {
                        //var value = $(this).find("input").val();
                        var aray = $(this).find("form").serializeArray();
                        var visIdx = $(this).index();
                        var idx = table.row($(this).parent()).index();
                        table.cell(idx, visIdx).data(aray[0].value)
                        var rowData = table.row($(this).parent()).data();     
                        return { objdata: JSON.stringify(rowData)};
                    },
                    callback: function (value, settings) {
                        var data = JSON.parse(value);
                        if (data.StatusCode == 200) {
                            notimessage("success", data.Message, "success");
                            table.row($(this).parent()).data(data.Data);
                            //fix display menu
                          //  table.ajax.reload();
                            
                        } else if (data.StatusCode == 400) {
                            notimessage("error", "Has something wrong, Please contact admin for information!", "Error");
                        }
                    }
                });
            }
            $("." + editclass).unbind("click");
            $("." + editclass).click(function (e) {
                e.preventDefault();
                LoadAjaxTabView(this, e, false);
            });
            try { $("#" + selector + "TotalRecord").val(oSettings._iRecordsTotal) } catch (ex) { }
        }
    });
    //$(myselect + ' tbody').on('click', 'tr', function () {
    //    var rowData = table.row(this).data();
    //    rowdata = JSON.stringify(rowData);
    //});

}
function AppenddatatoDrop(data, valuekey,textkey, selectedvalue, selectID) {
    var str = "";
    str += '<option value="">---Select---</option>'
    $.each(data, function (i, value) {
        str += '<option value="' + data[i][valuekey] + '">' + data[i][textkey] + '</option>'
    })
    $("#" + selectID).html("").prepend(str);
}