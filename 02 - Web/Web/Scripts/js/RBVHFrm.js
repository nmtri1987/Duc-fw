
// check if browser support HTML5 local storage
function localStorageSupport() {
    return (('localStorage' in window) && window['localStorage'] !== null)
}
//bind div and class Name
//var js = form_to_json("myform", "filter");
function form_to_json(selector, cname) {
    var objlist = '#' + selector + ' .' + cname;
    
    var obj = {};
    $.each($(objlist).serializeArray(), function (i, field) {

        obj[field.name] = field.value;
    });
    return obj;
}
function FtoJson(selector) {
    var ary = $(selector).serializeArray();
    var obj = {};
    $.each(ary, function (i, fd) {
        obj[field.name] = field.value;
    });

    return obj;
}

//DNH
function unBind() {
    $('.i-checks').unbind();
    //$('.chosen-select').unbind();
    $('.date').unbind();
    $('.mybtn').unbind();
    $('.mysitemap').unbind();
    $('.filterbtn').unbind();
    $('.btnSearch').unbind();
    $('.btnExportExcel').unbind();
    $(".singInfo").unbind("click");
  
}
function BindSiteMap(obj,e) {

    var link = $(obj).attr('link');
    var cl = $(obj).attr('clink');
    
    if (link.toLowerCase().indexOf("list") != -1 || link.toLowerCase().indexOf("singfrm") != -1) {
        link = link.substring(0, link.lastIndexOf("/") + 1);
    }
    var title = $(obj).attr('title');
    if ($.isEmptyObject(cl)==true) {
        ChangeUrl(title, link);
    }
    //e.preventDefault();
    //LoadAjaxTabView(this, e, false);
    fnGetTag(obj, e, false);

}
//DNH
function BindDefault(isAdd) {
    
    
    //if (isAdd == true) return false;
    //if object is null or not add + update Tab
   // if ($.isEmptyObject(isAdd)) {
    if (!isAdd) {
        
            unBind();
            DefaultBindtable();
            // }
            DefaultLoad();
            $('.mysitemap').click(function (e) {
                BindSiteMap(this, e);

            });
            $('.btnExportExcel').click(function (e) {
                e.stopPropagation(); // Do not propagate the event.
                var js = form_to_json("myform", "frmfilter");
                var url = $(this).attr('data-url') + "?searchprm=" + JSON.stringify(js);

                $.fileDownload(GetPath(url))
                .done(function () { alert('File download a success!'); })
                .fail(function () { alert('File download failed!'); });
                return false; // Do not submit the form.
            });
            $('.btnSearch').click(function (e) {
                //    e.preventDefault();
                
                var js = form_to_json("myform", "frmfilter");
                var url = $(this).attr('data-url') + "?searchprm=" + JSON.stringify(js);
                
                var b = {
                    "url": url,
                    "type": 'POST',
                    "data": js
                }
                
                //url += "?EmpCD=" + $("#EmpCD").val() + "&fromdate=" + $("#fromdate").val() + "&todate=" + $("#todate").val() + "&CompanyID=" + $("#CompanyCD").val();
                //$("#dtbProvider").attr("data-url", url);
                //filterData("dtbEPPosition", b);
                $("#dtbEPPosition").DataTable().ajax.url(url).load();
                //alert($("#dtbEPPosition").DataTable().ajax.data());

            });

            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
            });
            $('.date').datepicker({
                todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                format: "dd/mm/yyyy",
            });
            $('.filterbtn').click(function (e) {
                e.preventDefault();
                $(".filter").slideToggle("slow");
            });

            $('.mybtn').click(function (e) {
                e.preventDefault();
                LoadAjaxTabView(this, e, false);
            });
            $(".singInfo").click(function (e) {
                BindSiteMap(this, e);
            });
            $(".chosen-select").select2();
    } else {
        DefaultBindSearchtable();
    }
    closeAfterClick();
        
  
}
jQuery.download = function (url, data, method) {
    //url and data options required
    if (url && data) {
        //data can be string of parameters or array/object
        data = typeof data == 'string' ? data : jQuery.param(data);
        //split params into form inputs
        var inputs = '';
        jQuery.each(data.split('&'), function () {
            var pair = this.split('=');
            inputs += '<input type="hidden" name="' + pair[0] +
                '" value="' + pair[1] + '" />';
        });
        //send request
        jQuery('<form action="' + url +
                '" method="' + (method || 'post') + '">' + inputs + '</form>')
            .appendTo('body').submit().remove();
    };
};


var dt;

function TableShow(selector) {
    var myselect = "#" + selector;
    var allowCheckBox = true;
    var AllowSearch = true;
    var ajaxlink = $("#" + selector).attr("data-url");
    var hdarr = $("#" + selector).attr("arr-id");
    var PKID = $("#" + selector).attr("key");
    var PKIDTitle = $("#" + selector).attr("key-title");
    var txtsearch = $("#" + selector).attr("default-search");
    var ExportBtn = $("#" + selector).attr("exptargetid");
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
    var d = new Date();
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
                //if (type === 'display') {
                //    return data
                //    "<a href='#item" + data + "' target='edit' class='" + editclass + "' data-target='#myModal' data-toggle='modal' data='update' link='" + editlink + data + "' dis='popcnt'>" + data + "</a>";
                //}
                return "<a href='#item" + data + "' target='edit' class='singInfo'  data='update' link='" + editlink + data + "&d=" + d.getSeconds() + "' dis='mainctn'>" + data + "</a>";
            },
            className: "dt-body-center"
        };
    myCol.push(pkField);
    var objrender = BCBind(myformatCol, myCol, ColArray);
    myformatCol = objrender.myformatCol;
    myCol = objrender.myCol;
    dt = $('#' + selector).DataTable({
        "serverSide": true,
        "processing": true,
        responsive: true,
        "scrollX": true,
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
        "lengthMenu": [[25, 50, 100], [25, 50, 100]],
        "order": [[1, "desc"]],
        fixedHeader: true,
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
            $('.singInfo').unbind("click")
            $(".singInfo").click(function (e) {
                e.preventDefault();
                BindSiteMap(this, e);
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
                        var idx = dt.row($(this).parent()).index();
                        dt.cell(idx, visIdx).data(aray[0].value)
                        var rowData = dt.row($(this).parent()).data();
                        return { objdata: JSON.stringify(rowData) };
                    },
                    callback: function (value, settings) {
                        var data = JSON.parse(value);
                        if (data.StatusCode == 200) {
                            notimessage("success", data.Message, "success");
                            dt.row($(this).parent()).data(data.Data);
                            $('.i-checks').unbind();
                            $('.i-checks').iCheck({
                                checkboxClass: 'icheckbox_square-green',
                                radioClass: 'iradio_square-green',
                            });
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
    var ofn = "";
    if (!$.isEmptyObject($(myselect).attr("ofn"))) {
        ofn = $(myselect).attr("ofn");
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
    var objrender = BCBind(myformatCol, myCol, ColArray);
    myformatCol = objrender.myformatCol;
    myCol = objrender.myCol;

    if (expand != 0) {
        pkField =
       {
           data: PKID,
           render: function (data, type, row) {
               if (type === 'display') {

                   return "<div id='" + selector + "btnlist' class='btnlist'><button id='" + data + "'  target='edit' class='btn btn-sm btn-expand btn-info btnExpend" + selector + "' data-toggle='collapse' data-target='#ListDetailOne_" + data + "' data='update' link='" + linkExpand + "?" + PKID + "=" + data + "&' link-default='" + linkExpand + "?" + PKID + "=" + data + "&' dis='ListDetail_" + data + "' aria-expanded='false'><i class='fa fa-list'></i></button></div>";
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
        "scrollX": true,
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
        "lengthMenu": [[50, 100, 500], [50, 100, 500]],
        "order": [[1, "desc"]],

        /// select: true,
        fnDrawCallback: function (oSettings) {
            var api = this.api();
            var rows = api.rows({ page: 'current' }).nodes();
            var last = null;

            //api.column(2, { page: 'current' }).data().each(function (group, i) {
            //    if (last !== group) {
            //        $(rows).eq(i).before(
            //            '<tr class="group"><td colspan="5">' + group + '</td></tr>'
            //        );

            //        last = group;
            //    }
            //});

            if (expand != 0) {
                AddrowColapseTable(selector)
            }
            
            if (!$.isEmptyObject(ofn)) {
                eval(ofn);
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
                        return { objdata: JSON.stringify(rowData) };
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
//use for binding data
function BCBind(myformatCol, myCol, ColArray) {
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
            var b = {
                title: ColArray[i].name,
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
                case "dateY":
                    var temp = {
                        "targets": myCol.length - 1,
                        "data": ColArray[i].name,
                        "render": function (data, type, full, meta) {

                            return JFDate(data);
                        },

                    }
                    myformatCol.push(temp);
                    break;
                case "ipt":
                    var temp = {
                        "targets": myCol.length - 1,
                        "data": ColArray[i].name,
                        "render": function (data, type, full, meta) {
                            return "<input type='text' class='form-control  text-box single-line' value='" + data + "' disabled=''/>";
                        },

                    }
                    myformatCol.push(temp);
                    break;
                case "dipt":
                    var temp = {
                        "targets": myCol.length - 1,
                        "data": ColArray[i].name,
                        "render": function (data, type, full, meta) {
                            return "<input type='text' class='form-control  text-box single-line dec' value='" + addCommas(data) + "' disabled=''/>";
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

    return { myformatCol: myformatCol, myCol: myCol };
}
function filterData(select, ajaxUrl) {

    $("#" + select).DataTable({
        ajax: ajaxUrl,
        destroy: true,
    }).ajax.reload();
}
function formatDattimeJsonDataTable(value) {
    try {
        return JFormatDate(value);
    }
    catch (ex) {
        return value;
    }

}

function DNHTimeJsonDataTable(value) {
    try {
        return JFDate(value);
    }
    catch (ex) {
        return value;
    }

}
//dd-MMM-YYYY
function JFormatDate(date) {
    if (date != null && date != "") {
        
        // var dateParts = date.split(/-/);
        // alert((dateParts[2] * 10000));
        // var b = (dateParts[2] * 10000) + ($.inArray(dateParts[1].toUpperCase(), ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"]) * 100) + (dateParts[0] * 1);
        var MonthC = ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"];
        var yearfist = date.substr()
        //var currentTime = new Date(dateString[0],dateString[0],);
        var dateString = date.substr(6);
        
        var currentTime = new Date(parseInt(dateString));
        
        var month = currentTime.getMonth() + 1 <= 9 ? "0" + (currentTime.getMonth() + 1) : (currentTime.getMonth() + 1);
        var day = currentTime.getDate() <= 9 ? "0" + currentTime.getDate() : currentTime.getDate();
        var year = currentTime.getFullYear();
        date = day + "-" + MonthC[month - 1] + "-" + year;



        return date;
    } else {
        return "";
    }

}
function ConvertDateMMddyyyy(val) {
    var datestring = val.split('/');
    var curDate = new Date(datestring[2] + "-" + datestring[0] + "-" + datestring[1]);
    //new Date(dateString[2], dateString[0], dateString[1]);
    return curDate;
}
//date = yyyy-mm-dd as input
function JFDate(date) {
    if (date != null && date != "") {

        
        // var dateParts = date.split(/-/);
        // alert((dateParts[2] * 10000));
        // var b = (dateParts[2] * 10000) + ($.inArray(dateParts[1].toUpperCase(), ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"]) * 100) + (dateParts[0] * 1);
        var MonthC = ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"];
        
        if (date.substr(0, 10) == "1900-01-01") {
            return "";
        }
        var dateString = date.substr(0, 10).split('-');
        var currentTime = new Date(dateString[0], dateString[1], dateString[2]);
        //var currentTime = new Date(parseInt(mydate));
        
        var month = currentTime.getMonth() <= 9 ? "0" + (currentTime.getMonth()) : (currentTime.getMonth());
        var day = currentTime.getDate() <= 9 ? "0" + currentTime.getDate() : currentTime.getDate();
        var year = currentTime.getFullYear();
        date = day + "-" + MonthC[month - 1] + "-" + year;



        return date;
    } else {
        return "";
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

function AjaxPostData(pars, lUrl) {
    var mObj = null;
    $.ajax({
        type: 'POST',
        url: GetPath(lUrl),
        data: pars,
        async: false,
        success: function (data) {
            mObj = data;
            if (data.StatusCode == 200) {
                notimessage("success", data.Message, "success");
            } else if (data.StatusCode == 400) {
                notimessage("error", "Has something wrong, Please contact admin for information!", "Error");
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            mObj = null;
            notimessage("error", "Has something wrong, Please contact admin for information!", "Error");

        }
    });
    return mObj;
}
var AjaxDownloadFile = function (configurationSettings) {
    // Standard settings.
    this.settings = {
        // JQuery AJAX default attributes.
        url: "",
        type: "POST",
        headers: {
            "Content-Type": "application/json; charset=UTF-8"
        },
        data: {},
        // Custom events.
        onSuccessStart: function (response, status, xhr, self) {
        },
        onSuccessFinish: function (response, status, xhr, self, filename) {
        },
        onErrorOccured: function (response, status, xhr, self) {
        }
    };
    this.download = function () {
        var self = this;
        $.ajax({
            type: "POST",
            url: GetPath(this.settings.url),
            headers: this.settings.headers,
            data: this.settings.data,
            async: false,
            success: function (response, status, xhr) {
                // Start custom event.
                self.settings.onSuccessStart(response, status, xhr, self);

                // Check if a filename is existing on the response headers.
                var filename = "";
                var disposition = xhr.getResponseHeader("Content-Disposition");
                if (disposition && disposition.indexOf("attachment") !== -1) {
                    var filenameRegex = /filename[^;=\n]*=(([""]).*?\2|[^;\n]*)/;
                    var matches = filenameRegex.exec(disposition);
                    if (matches != null && matches[1])
                        filename = matches[1].replace(/[""]/g, "");
                }

                var type = xhr.getResponseHeader("Content-Type");
                var blob = new Blob([response], { type: type });

                if (typeof window.navigator.msSaveBlob !== "undefined") {
                    // IE workaround for "HTML7007: One or more blob URLs were revoked by closing the blob for which they were created. These URLs will no longer resolve as the data backing the URL has been freed.
                    window.navigator.msSaveBlob(blob, filename);
                } else {
                    var URL = window.URL || window.webkitURL;
                    var downloadUrl = URL.createObjectURL(blob);

                    if (filename) {
                        // Use HTML5 a[download] attribute to specify filename.
                        var a = document.createElement("a");
                        // Safari doesn"t support this yet.
                        if (typeof a.download === "undefined") {
                            window.location = downloadUrl;
                        } else {
                            a.href = downloadUrl;
                            a.download = filename;
                            document.body.appendChild(a);
                            a.click();
                        }
                    } else {
                        window.location = downloadUrl;
                    }

                    setTimeout(function () {
                        URL.revokeObjectURL(downloadUrl);
                    }, 100); // Cleanup
                }

                // Final custom event.
                self.settings.onSuccessFinish(response, status, xhr, self, filename);
            },
            error: function (response, status, xhr) {
                // Custom event to handle the error.
                self.settings.onErrorOccured(response, status, xhr, self);
            }
        });
    };
    // Constructor.
    {
        // Merge settings.
        $.extend(this.settings, configurationSettings);
        // Make the request.
        this.download();
    }
};

function fnGetTag(ob, e, ifm) {
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
    fnSiteMap(url, tabname, ifm, dis, false);
}
function fnSiteMap(url, tabname, ifm, dis, isShow) {
    

    ld(true);
    $("#" + dis).html("<div class='modal-header'><i class='fa fa-laptop modal-icon'></i> <h4 class='modal-title'>Loading...</h4></div>");

    if (ifm) {
        $("#" + dis).html("<iframe frameborder='0' scrolling='no' marginheight='0' marginwidth='0' class='col-lg-12 col-md-12 col-sm-12' ' src='" + url + '&Tabname=' + tabname + "'></iframe>");
    } else {

        if (url.indexOf("?") != -1) {
            $("#" + dis).load(url + '&Tabname=' + tabname + "&ajax=1", function (responseTxt, statusTxt, xhr) {
                if (statusTxt == "success") {

                    ld(false);
                    BindDefault();
                    
                }
            });
        } else {
            $("#" + dis).load(url + '?Tabname=' + tabname + "&ajax=1", function (responseTxt, statusTxt, xhr) {
                if (statusTxt == "success") {
                    //ld(false);
                    if ($.isEmptyObject(tabname)) {
                        ld(false);
                    }
                    BindDefault();

                }
            });
        }
    }
    $("body").addClass("menu-collapsed");
    $("body").removeClass("menu-expanded");
    if (isShow) {
        $("#myModal").modal("show");
    }
}
function closeAfterClick() {
    $(".refresh-btn").click();
}
$(document).ready(function () {
    BindDefault();
    $.fn.dataTable.ext.errMode = 'none';
 
    //$('.modal').appendTo("body");
});