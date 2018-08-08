function DefaultBindtable() {
    $('.DNHtable').each(function () {
        //bind div and create table 
        var mydiv = $(this);
        var mytable = $(mydiv).attr("target_table");
        var PageName = $(mydiv).attr("PageName");
        var CusGetlink = $(mydiv).attr("CusLink");
        var DisableEdit = $(mydiv).attr("disedit");
        var mytemplate = DNHTemplate(mytable, PageName, CusGetlink, DisableEdit,this);
        $(mydiv).html(mytemplate);

        //var currentElement = $(this);
        BindTable(mytable);
    });

}
function DNHTemplate(obj, pageName, CusGetlink, DisableEdit,parent) {
    ///next step load form in Server Template and record into client Cache
    var link = GetPath("/" + obj + "/");
    var SaveLink = link + "Create";
    if (!$.isEmptyObject($(parent).attr("SaveUrl"))) {
        SaveLink = $(parent).attr("SaveUrl");
    }
    var ofn = "";
    if (!$.isEmptyObject($(parent).attr("ofn"))) {
        ofn = $(parent).attr("ofn");
    }
    
    var Getlink = "GetGata";
    if (!$.isEmptyObject(CusGetlink)) {
        Getlink = CusGetlink;
    }
    var np = "";
    if (!$.isEmptyObject($(parent).attr("np"))) {
        np = $(parent).attr("np");
    }
    var edit = true;
    if (!$.isEmptyObject(DisableEdit)) {
        if (DisableEdit == 1) {
            edit = false;
        }
    }
    var frcol = 1;
    if (!$.isEmptyObject($(parent).attr("fcol"))) {
        frcol = $(parent).attr("fcol");
    }
    var colFilter = obj + "ColFilter";
    var b = "<section><div class='row'><div class='col-md-12'><div class='card'>";
    b += "<div class='card-header'><h4 class='card-title'>"+ pageName +"</h4><a class='heading-elements-toggle'><i class='fa fa-ellipsis-v font-medium-3'></i></a></div>";
    b += "<div class='panel panel-success list-panel' id='list-panel'>";
    //b += " <div class='panel-heading list-panel-heading' style='height:44px;'><h3 class='panel-title list-panel-title'>" + pageName + "</h3></div>";
    b += " <div class='card-body collapse in'> <div class='card-block card-dashboard'><div class='dt-events-log' id='log"+obj+"'></div>";
    b += "<div class='panel-body'>";
    b += "<div class='pull-right'>";
    b += "<button class='btn  btn-warning filterbtn' name='Action' ><i class='fa fa-filter'></i></button> ";
    b += "<a class='btn  btn-warning filcolbtn' name='Column' type='button'  data-toggle='collapse' data-target='#" + colFilter + "'><i class='fa fa-th-list'></i></a> ";
    b += "<div class='colfilter collapse' id='" + colFilter + "'><div class='panel panel-default panel-select-custom'>";
    b += "<div class='panel-heading'>Select Column";
    b += "<a href='#' class='pull-right' data-toggle='collapse' data-target='#" + colFilter + "'>&times;</a></div>";
    b += "<div class='panel-body' id=" + obj + "colctx></div></div></div>";
    b += "<a class='btn btn-success '  href='" + link + "ListUpload?Code=' dis='popcnt' target='ExcelImport'><i class='fa fa-upload'></i></a> ";
    b += "<a href='/" + obj + "/ExportExcel?Code=' class='btn btn-primary btnExportExcel' data-url='/" + obj + "/ExportExcel' title='ExportToExcel' ><i class='fa fa-download'></i></a></div>";
    if (edit) {
        b += "<button type='button' class='btn  btn-default mybtn' data-target='#myModal' data-toggle='modal' data='add' link='" + SaveLink + "'  dis='popcnt'><i class='fa fa-pencil'></i></button> ";
        b += "<button class='btn  btn-default'  type='submit' name='Action' value='Delete'><i class='fa fa-trash'></i></button> ";
    }
    b += "<button type='button' class='btn btn-info refresh-button refresh-btn' ><span class='ft-rotate-cw' aria-hidden='true'></span></button>";
    b += "<div class='filter' id='frmFilter' name='frmFilter' style='display:none'>";//think how to Add Filter into system
    b += "</div>";
    b += "<hr />";
    b += "  <div class='table-responsive'><table ofn='" + ofn + "' id='tbl" + obj + "'  fcol='" + frcol + "' link='" + link + "' np='" + np + "' class='table table-striped table-bordered' data-url='" + link + Getlink + "' header-link='" + link + "headerLink' default-search='' allow-search='1' allow-chk='1' style='width:100%'></table></div>";
    b += "</div></div></div></div></div></div></div></section>";
    return b;
}
function BindTable(selector) {
    var colItem = "#" + selector + "colctx";
    var MainItem = selector;
   
    
    selector = "#tbl" + selector;
    var headerLink = $(selector).attr("header-link");
    var Link = $(selector).attr("link");
    var namespace = "";
    if (!$.isEmptyObject($(selector).attr("np"))) {
        namespace = $(selector).attr("np");
    }
    var myCol = [];
    //if (localStorageSupport) {
    //    var jsolum = sessionStorage.getItem(headerLink);
    //    if (jsolum != null) {
    //        myCol = JSON.parse(jsolum);
    //    }
    //}
  
    if (myCol.length == 0) {
        myCol = [];
        var PKID;
        $.ajax({
            url: headerLink,
            type: "POST",
            //dataType: "json",
            //data: { id: group_id },
            success: function (result) {
                
                var b;
                
                var ColumnFilter = "";
                for (var i = 0; i < result.length; i++) {
                    //if (!$.isEmptyObject(result[i])) {


                    if ($.isEmptyObject(result[i].type)) {
                        var b = {
                            title: result[i].value,
                            data: result[i].name,
                            className: "ifind"
                        }
                    } else {
                        if (result[i].type.lastIndexOf("key") != -1) {
                            PKID = result[i].name;
                            b = {
                                data: result[i].name,
                                render: function (data, type, row) {
                                    if (type === 'display') {
                                        return '<input type="checkbox" class="editor-active i-checks" value="' + data + '" name="' + PKID + '" >';
                                    }
                                    return data;
                                },
                                className: "dt-body-center"
                            }
                            myCol.push(b);
                        }
                        b = bindCloumn(result[i], Link, namespace);
                    }
                    ColumnFilter += "<div class='" + MainItem + "col collist hidetex' data-column='" + (i + 1) + "' alt='" + result[i].name + "' title='" + result[i].name + "'><input type='checkbox' class='editor-active i-checks' disabled='' checked /> " + result[i].name + "</div>";

                    myCol.push(b);
                }
                //}
                $(colItem).html(ColumnFilter);
                if (localStorageSupport) {
                    //localStorage.setItem("test", JSON.stringify(myCol));
                    sessionStorage.setItem(headerLink, JSON.stringify(myCol));
                }
                DrawTable(MainItem, myCol);
            }
        });
    } else {
        DrawTable(MainItem, myCol);
    }

}
function DrawTable(myselect, myCol) {
    var myMain = myselect;
    myselect = "#tbl" + myselect;
    var height = "100%";
    
    var cusHeight = $(selector).attr("height");
    if (!$.isEmptyObject(cusHeight)) {
        height = cusHeight + "px";
    }
    var allowCheckBox = true;
    var AllowSearch = true;
    var AllowSort = true;

    var ajaxlink = $(myselect).attr("data-url");
    var hdarr = $(myselect).attr("arr-id");
    var PKID = $(myselect).attr("key");
    var txtsearch = $(myselect).attr("default-search");
    var frcol = $(myselect).attr("fcol");

    var ColArray = eval($("#" + hdarr).val());
    var editlink = ajaxlink.substring(0, ajaxlink.lastIndexOf("/") + 1) + "Update?" + PKID + "=";

    var search = $(myselect).attr("allow-search");
    var sort = $(myselect).attr("allow-sort");
    var checkbox = $(myselect).attr("allow-chk");
    var expand = $(myselect).attr("allow-Expand");
    var linkExpand = $(myselect).attr("link-Expand");
    var lineUpdate = $(myselect).attr("link-update");
    var selector = $(myselect).attr("id");
    var editclass = "btn" + selector;
    var ofn = $(myselect).attr("ofn");
    table = $(myselect).DataTable({
        "serverSide": true,
        "processing": true,
        "scrollY": height,
        "ajax": {
            "url": ajaxlink,
            "type": "POST",
        },
        "bSort": AllowSort,
        "bFilter": AllowSearch,
        "search": {
            "search": txtsearch
        },
        "columns": myCol,
        //fixedHeader: true,
        //fixedColumns: {
        //    //leftColumns: 2,
        //    "iLeftColumns": frcol, //Freezed first for columns
        //    "sHeightMatch": "auto",
        //    "iLeftWidth": 405
        //},
        //   "columnDefs": myformatCol,
        "lengthMenu": [[30, 50, 100, 500], [30, 50, 100, 500]],
        "scrollX": true,
        "order": [[1, "desc"]],
        fnDrawCallback: function (oSettings) {
            //if (expand != 0) {
            //    AddrowColapseTable(selector)
            //}

            $(".refresh-button").unbind("click");
            $(".refresh-button").click(function () {
                table.ajax.reload();
            });
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
            $(myselect).find(".i-checks").unbind("click");
            //$(".i-checks").unbind("click");
            $(myselect).find(".i-checks").iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
            });
            
           var pickerStart = $('.pickatime').pickatime({
               interval: 15,
               twelvehour: false,
                min: [5, 30],
                max: [23, 0],
                format: "HH:i",
            //    container: '#root-picker-outlet',
             //   editable: true,
                hiddenName: true
            });
       //     pickerStart.set('select', '08:30', { format: 'hh:i' });
            //$(".pickatime").pickatime();
            ////$('.pickatime').datetimepicker({
            ////    format: 'HH:mm',
            ////});
            $(".btnExportExcel").unbind("click");
            $('.btnExportExcel').click(function (e) {
                var js = form_to_json("myform", "frmfilter");
                var url = $(this).attr('href');

                if (!$.isEmptyObject(js)) {
                    url = $(this).attr('data-url') + "?searchprm=" + JSON.stringify(js);
                }
                
                $.fileDownload(GetPath(url))
                .done(function () { alert('File download a success!'); })
                .fail(function () { alert('File download failed!'); });
                return false; // Do not submit the form.
            });
            $(".btnSearch").unbind("click");
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
                table.ajax.url(url).load();
                //alert($("#dtbEPPosition").DataTable().ajax.data());

            });
            $(".mybtn").unbind("click");
            $(".mybtn").click(function (e) {
                e.preventDefault();
                LoadAjaxTabView(this, e, false);
            });
            $('.filterbtn').unbind();
            $('.filterbtn').click(function (e) {
                e.preventDefault();
                $(".filter").slideToggle("slow");
            });
            $(myselect).find(".singInfo").unbind("click");
            $(myselect).find(".singInfo").click(function (e) {
                BindSiteMap(this, e);
            });

            try { $("#" + selector + "TotalRecord").val(oSettings._iRecordsTotal) } catch (ex) { }

            $("." + myMain + "col").unbind("click");
            $("." + myMain + "col").on('click', function (e) {
                e.preventDefault();

                // Get the column API object
                var column = table.column($(this).attr('data-column'));
                // Toggle the visibility
                column.visible(!column.visible());

                $(this).find("input:checkbox").prop('checked', column.visible());
            });
            
           
          //  $('.selectcol').attr('style', 'text-align:center;');
            if (!$.isEmptyObject(ofn)) {
                eval(ofn);
            }
            //var oFC = new fixedColumns(table, {
            //    "iLeftColumns": 4, //Freezed first for columns
            //    "sHeightMatch": "auto",
            //    "iLeftWidth": 405
            //});
        }
    });

}
function bindCloumn(item, Link, namespace) {
    var temp;

    var editlink = "#";
    var d = new Date();
    switch (item.type) {
        case "key":
            temp = 
                {
                    title: item.value,
                    data: item.name,
                    render: function (data, type, row, meta) {
                        editlink = Link + "Update?" + item.name + "=" + data + "&d=" + d.getSeconds();;
                        return "<a href='#item" + data + "' target='edit' class='mybtn' data-target='#myModal' data-toggle='modal' data='update' link='" + editlink + "' dis='popcnt'>" + data + "</a>";
                    },
                    className: "dt-body-center"
                };
            break;
        case "key-ref":
            temp =
                {
                    title: item.value,
                    data: item.name,
                    render: function (data, type, row) {
                        var mydata = data.split("-");
                        var value;
                        if (mydata.length > 0) {
                            value = mydata[0];
                            data = mydata[1];
                        }
                        editlink = Link + "Update?ID=" + value;
                        return "<a href='#item" + data + "' target='edit' class='mybtn' data-target='#myModal' data-toggle='modal' data='update' link='" + editlink + "' dis='popcnt'>" + data + "</a>";
                    },
                    className: "dt-body-center"
                };
            break;
        case "key-new":
            temp ={
                title: item.value,
                data: item.name,
                render: function (data, type, row, meta) {
                    editlink = Link + item.alink + "?" + item.name + "=" + data + "&d=" + d.getSeconds();
                    
                    return "<a href='#item" + data + "' target='edit' class='singInfo' data='update' link='" + editlink + "' dis='mainctn'>" + data + "</a>";
                },
                className: "dt-body-center"
            };
            break;
        case "number":
            temp =
            {
                title: item.value,
                data: item.name,
                render: function (data, type, row, meta) {
                    return addCommas(data);
                },
                className: "text-right"
            };
            break;
        case "date":
            temp =
            {
                title: item.value,
                data: item.name,
                render: function (data, type, row, meta) {
                    data = formatDattimeJsonDataTable(data);
                    if (namespace != "") {
                        return "<span>" + data + "</span><input name='" + namespace + "[" + meta.row + "]." + item.name + "' type='hidden' value='" + data + "'  />";
                    } else {
                        return "<span>" + data + "</span>";
                    }
                },
                className: "text-right"
            };
            break;
        case "datef":
            temp =
            {
                title: item.value,
                data: item.name,
                render: function (data, type, row, meta) {
                    return DNHTimeJsonDataTable(data);
                },
                className: "text-right"
            };
            break;
        case "ipt":
            temp =
             {
                 title: item.value,
                 data: item.name,
                 render: function (data, type, row, meta) {
                     if (namespace != "") {
                         return "<input type='text' class='form-control  text-box single-line " + item.classN + "'' value='" + data + "' readonly='' name='" + namespace + "[" + meta.row + "]." + item.name + "'/>";
                     } else {
                         return "<input type='text' class='form-control  text-box single-line' value='" + data + "' disabled=''/>";
                     }
                     
                 },
                 className: "text-right"
             };
            break;
        case "ipted":
            temp =
             {
                 title: item.value,
                 data: item.name,
                 render: function (data, type, row, meta) {
                     if (namespace != "") {
                         return "<input class='form-control  text-box single-line " + item.classN + "'  name='" + namespace + "[" + meta.row + "]." + item.name + "' type='text' value='" + data + "'  />";
                     } else {
                         return "<input type='text' class='form-control  text-box single-line' value='" + data + "' />";
                     }

                 },
                 className: "text-right " + item.classN
             };
            break;
        case "time":
            temp =
             {
                 title: item.value,
                 data: item.name,
                 render: function (data, type, row, meta) {
                     if ($.isEmptyObject(data)) {
                         data = "";
                     }
                     if (namespace != "") {
                         return "<input type='text' class='form-control time text-box single-line' value='" + data + "' disabled=''/><input name='" + namespace + "[" + meta.row + "]." + item.name + "' type='hidden' value='" + data + "'  />";
                     } else {
                         return "<input type='text' class='form-control time text-box single-line' value='" + data + "' disabled=''/>";
                     }
                 },
                 className: "text-right"
             };
            break;
        case "timetext":
            temp =
             {
                 title: item.value,
                 data: item.name,
                 render: function (data, type, row, meta) {
                     if ($.isEmptyObject(data)) {
                         data = "";
                     }
                     if (namespace != "") {
                         return "<input name='" + namespace + "[" + meta.row + "]." + item.name + "' type='text' class='form-control pickatime   text-box single-line' value='" + data + "'  />";
                     } else {
                         return "<input type='text' name='" + item.name + "' class='form-control pickatime   text-box single-line' value='" + data + "' />";
                     }
                 },
                 className: "text-right"
             };
            break;
        case "bool":

            temp =
            {
                title: item.value,
                data: item.name,
                render: function (data, type, row, meta) {
                    if (data == true) {
                        return "<input type='checkbox' class='i-checks' checked='checked' disabled=''/>";
                    }
                    return "<input type='checkbox' class='i-checks' disabled=''/>";
                },
                "className": "text-center ifind"
            };
            break;
            
        case "alboolfnc":

            temp =
            {
                title: item.value,
                data: item.name,
                'checkboxes': {
                    'selectRow': true
                },
                render: function (data, type, row, meta) {
                    //if (data == true) {
                    //    return "<input type='checkbox' class='i-checks' checked='checked' />";
                    //}
                    if (namespace != "") {
                        return "<input name='" + namespace + "[" + meta.row + "].isSelect' type='checkbox' class='selectcol' onload='" + item.alink + "(this);' onclick='return  " + item.alink + "(this);' /><input name='" + namespace + "[" + meta.row + "]." + item.name + "' type='hidden' value='" + data + "'  />";
                    } else {
                        return "<input name='" + item.name + "' type='checkbox' value='" + data + "'  class='i-checks' />";
                    }
                   // return "<input name='hrm_atd_ScanTime[" + meta.row + "]." + item.name + "' type='checkbox' value='" + data + "'  class='i-checks' /> <input name='" + item.name + "' type='hidden' value='" + data + "' />";
                },
                "className": "text-center ifind selectcol"
            };
            break;
        case "albool":

            temp =
            {
                title: item.value,
                data: item.name,
                render: function (data, type, row, meta) {
                    if (data == true) {
                        return "<input type='checkbox' class='i-checks " + item.value + "'/>";
                    }
                    return "<input type='checkbox' class='i-checks " + item.value + "' />";
                },
                "className": "text-center ifind"
            };
            break;
        case "string":
                        temp =
                         {
                                 title: item.value,
                                 data: item.name,
                                 render: function (data, type, row, meta) {
                                     return "<span>" +data+ "</span><input name='" + namespace + "[" + meta.row + "]." + item.name + "' type='hidden' value='" + data + "'  />";
                                     },
                             className: "text"
                         };
                        break;
        
        case "hiddencol":
            temp =
             {
                 title: item.value,
                 data: item.name,
                 render: function (data, type, row, meta) {
                     return "<input name='" + namespace + "[" + meta.row + "]." + item.name + "' type='hidden' value='" + data + "'  />";
                 },
                 className: "hidden"
             };
            break;
        case "iptNoDisabled":
            temp =
             {
                 title: item.value,
                 data: item.name,
                 render: function (data, type, row,meta) {
                     return "<input type='text' name='" + namespace + "[" + meta.row + "]." + item.name + "' class='form-control  text-box single-line' value='" + data + "'/>";
                 },
                 className: "text-right"
             };
            break;

        case "":
            temp =
             {
                 title: item.value,
                 data: item.name,
                 render: function (data, type, row) {
                     if (namespace != "") {
                         return  "<span>" + data + "</span><input name='" + namespace + "[" + meta.row + "]." + item.name + "' type='hidden' value='" + data + "'  />";
                     } else {
                         return "<span>" + data + "</span>";
                     }
                 },
                 className: "text"
             };
            break

                        
    }
    return temp;
}
