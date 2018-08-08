
(function ($) {
    $.fn.BindJson = function (fielddata, cssClassPrefix) {
        var selectorPrefix = " ." + cssClassPrefix;
        for (key in fielddata) {
            var ctl = $(selectorPrefix + key, this);
            if (ctl.length != 0) {
                BindFormByType(ctl, ctl[0], fielddata[key]);
                //var isTextField = (ctl[0].tagName == "DIV" || ctl[0].tagName == "SPAN" || ctl[0].tagName == "LABEL");
                //if (isTextField) ctl.text(fielddata[key]);
                //if (ctl[0].type == 'checkbox') ctl.attr('checked', (fielddata[key] == true));
                //else ctl.val(fielddata[key]);
            }
        }
    };

    $.fn.BindJson = function (fielddata) {
        for (key in fielddata) {
            var ctl = $("#" + key, this);
            if (ctl.length != 0) {
                BindFormByType(ctl, ctl[0].type, fielddata[key]);
                //var isTextField = (ctl[0].tagName == "DIV" || ctl[0].tagName == "SPAN" || ctl[0].tagName == "LABEL");
                //if (isTextField) ctl.text(fielddata[key]);
                //if (ctl[0].type == 'checkbox') ctl.attr('checked', (fielddata[key] == true));
                //else ctl.val(fielddata[key]);
            }
        }
    };

})(jQuery);
function BindFormByType(ctl, Ftype, Fval) {
    
    switch (Ftype) {
        case "checkbox":
            ctl.attr('checked', (Fval == true));
            break;
        case "select-one":
            ctl.val(Fval).trigger('change.select2');;
            break;
        default:
            if (ctl.attr("type") == 'datetime') {
                var dt = new Date(Fval);
                ctl.val(formattedDate(dt));
            } else {
                ctl.val(Fval);
            }
            break;
    }
}
(function ($) {
    $.fn.ClearDataFields = function (clearClass) {
        $(":input", this).each(function () {
            var type = this.type,
                tag = this.tagName.toLowerCase();
            if (type == 'text' || type == 'password' || type == 'tel' || type == 'date' || type == 'email' || tag == 'textarea' || type == 'hidden' || type == 'number') {
                this.value = "";
            } else if (type == 'checkbox' || type == 'radio') {
                this.checked = false;
            } else if (tag == 'select') {
                this.selectedIndex = 0;
            }
        });
        if (clearClass) {
            $("." + clearClass, this).text("");
        }
    };
})(jQuery);

function formattedDate(d) {
    let month = String(d.getMonth() + 1);
    let day = String(d.getDate());
    const year = String(d.getFullYear());

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return month+"/"+day+"/"+year;
}

function fnLoadAutocompleteOneColumn(selector) {
    var durl = $(selector).attr("data-url");
    var url = $(selector).attr("search-url");

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
          
            BindFrm(formID,durl, this.value);

            return false;
        }
    })
    $(selector).blur(function () {
        BindFrm(formID,durl, $(this).val());
    })
}
function BindFrm(formID,durl, value) {
    var mydata = AjaxPostData({ id: value }, durl);
    if (!$.isEmptyObject(mydata)) {
        $(formID).BindJson(mydata);
    }
}