
var tokenKey = 'accessToken';
var Api = 'Api';
function setToken(data) {
    //add token to browser cache
    sessionStorage.setItem(tokenKey, data.access_token);
    sessionStorage.setItem(Api, data.Api);
    if (!$.isEmptyObject(data.access_token)) {
        window.location = data.returnUrl;
    }
}
function callApi(apiurl) {

    //get token 
    var token = sessionStorage.getItem(tokenKey);
    var api = sessionStorage.getItem(Api);
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }

    $.ajax({
        type: 'GET',
        //url: '/api/values',
        url: api+apiurl,
        headers: headers
    }).done(function (data) {
        self.result(data);
    }).fail(showError);
}

function GetApi(selector) {
    var myselect = "#" + selector;
    var allowCheckBox = true;
    var AllowSearch = true;
    var api = sessionStorage.getItem(Api);
    var ajaxlink = $("#" + selector).attr("data-url");
    var token = sessionStorage.getItem(tokenKey);
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }
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
                    var idnum = ColArray[i].idfield
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
                            return "<button type='button' class='btn btn-primary btn-block button' data-param='" + dataParam + "' data-url='" + url + "' name='btnsave' value='save'>Pay</button>";
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
            "type": "POST",
            headers: headers

        },
        "bFilter": AllowSearch,
        "search": {
            "search": txtsearch
        },
        "columns": myCol,
        "columnDefs": myformatCol,
        "lengthMenu": [[25, 50], [25, 50]],
        fnDrawCallback: function (oSettings) {
            $("." + editclass).unbind("click")
            $("." + editclass).click(function (e) {
                e.preventDefault();
                LoadAjaxTabView(this, e, false);
            });
            $(".refresh-button").unbind("click");
            $(".refresh-button").click(function () {
                dt.ajax.reload();
            });
           
        }
    });
  

}