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
    pkField =
        {
            data: PKID,
            title: PKID,
            render: function (data, type, row) {
                //if (type === 'display') {
                //    return data
                //    "<a href='#item" + data + "' target='edit' class='" + editclass + "' data-target='#myModal' data-toggle='modal' data='update' link='" + editlink + data + "' dis='popcnt'>" + data + "</a>";
                //}
                return "<a href='#item" + data + "' target='edit' class='" + editclass + "' data-target='#myModal' data-toggle='modal' data='update' link='" + editlink + data + "' dis='popcnt'>" + data + "</a>";
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
                        return { objdata: JSON.stringify(rowData)};
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
        "order": [[ 1, "desc" ]],
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

    return { myformatCol, myCol };
}
function filterData(select,ajaxUrl) {
   
    $("#" + select).DataTable({
        ajax: ajaxUrl,
        destroy: true,
    }).ajax.reload();
}