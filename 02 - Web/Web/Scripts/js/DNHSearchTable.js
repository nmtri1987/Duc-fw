function DefaultBindSearchtable() {
    $('.DNHSearchTable').each(function () {
        //bind div and create table 
        var mydiv = $(this);
        var mytable = $(mydiv).attr("target_table");
        var PageName = $(mydiv).attr("PageName");
        var CusGetlink = $(mydiv).attr("CusLink");
        var DisableEdit = $(mydiv).attr("disedit");
        var mytemplate = DNHSearchTemplate(mytable, PageName, CusGetlink, DisableEdit, this);
        $(mydiv).html(mytemplate);

        //var currentElement = $(this);
        BindSearchTable(this,mytable);
    });

}
function DNHSearchTemplate(obj, pageName, CusGetlink, DisableEdit, parent) {
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
    var targetID = $(parent).attr("TargetID");
    var ParentID = $(parent).attr("id");
    var tableID = "tblSearch_" + targetID + "_" + obj;
    var b = "<div><div class='panel-heading modal-header'>Select - <button type='button' id='cls_" + ParentID + "'  class='pull-right close' data-target='" + ParentID + "'>×</button></div>";
    b += " <div class='table-responsive'><table parentid='" + ParentID + "' ofn='" + ofn + "' id='" + tableID + "'  fcol='" + frcol + "' link='" + link + "' np='" + np + "' class='table table-striped table-bordered' data-url='" + link + Getlink + "' header-link='" + link + "headerLink' TargetID='" + targetID + "' default-search='' allow-search='1' allow-chk='1' style='width:100%'></table></div>";
    b+="</div>"
    return b;
}
function BindSearchTable(parent,selector) {
    var colItem = "#" + selector + "colctx";
    var MainItem = selector;
    var targetID = $(parent).attr("TargetID");
    selector = "#tblSearch_" + targetID + "_" + selector;
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
                DrawSearchTable(parent,MainItem, myCol);
            }
        });
    } else {
        DrawSearchTable(parent,MainItem, myCol);
    }

}

function DrawSearchTable(parent, myselect, myCol) {

    var targetID = $(parent).attr("TargetID");
    var mainDiv = "#gsearch_" + targetID;
    myselect = "#tblSearch_" + targetID + "_" + myselect;
    targetID = "#" + targetID;

    var myMain = myselect;
    //myselect = "#tbl" + myselect;
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
    //var frcol = $(myselect).attr("fcol");

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

    var ParentID = "#" + $(myselect).attr("parentid");
    var closebtn = "#cls_" + $(myselect).attr("parentid");
    var isShowSearch = 0;
    var BindForm = $(ParentID).attr("BindForm");
    var FieldMapping = $(ParentID).attr("FieldMapping");
    var mytable = $(myselect).DataTable({
        "serverSide": true,
        "processing": true,
        "lengthChange": false,
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

        "lengthMenu": [[15], [15]],
        "columns": myCol,
        "scrollX": true,
        "order": [[1, "desc"]],
        fnDrawCallback: function (oSettings) {
            try { $("#" + selector + "TotalRecord").val(oSettings._iRecordsTotal) } catch (ex) { }

        },
        "fnInitComplete": function (oSettings, json) {

            $(mainDiv).find(targetID).on('keyup', function () {
                if (isShowSearch == 0) {
                    //  $(ParentID).slideToggle('slow');
                  //  HideAllList(mainDiv);
                    //$(mainDiv).find(".input-group-addon").click();
                    $(mainDiv).find(".DNHSearchTable").addClass("in");
                    isShowSearch = 1;
                }
                mytable.search(this.value).draw();
            });
            $(closebtn).click(function () {
               // HideAllList(mainDiv);
                $(mainDiv).find(".DNHSearchTable").addClass("in");
                isShowSearch = 0;
                //$(ParentID).slideToggle('slow');
            });
            $(myselect).find(".i-checks").unbind("click");
            $(myselect).find(".i-checks").iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
            });
            $("#popcnt").click(function () {
                 HideAllList(mainDiv);
            });
            $(myselect).on('click', 'tr', function () {
                $(closebtn).click();
                var visIdx = $(this).index();
                var rowData = mytable.row(visIdx).data();
                if (!$.isEmptyObject(BindForm)) {
                    //$(mainDiv).find(targetID).val(JSON.stringify(oSettings));
                    $("#" + BindForm).jsForm({
                        data: rowData,
                        prefix: false
                    });
                    $("#" + BindForm).jsForm("fill", rowData);
                } else {
                    if (!$.isEmptyObject(rowData)) {
                        setvalue($(mainDiv).find(targetID), rowData, FieldMapping);
                    }
                }
            });
        }
    });


    if (!$.isEmptyObject(ofn)) {
        eval(ofn);
    }
}
function HideAllList(mainDiv) {
    $(".DNHSearchTable").removeClass("in");
  //  $(mainDiv).find(".input-group-addon").click();
}
function setvalue(ob, rowData, FieldMapping) {
    $(ob).val(rowData[FieldMapping]);
}