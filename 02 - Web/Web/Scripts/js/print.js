function PrintPdf(data) {
    $('#loadProgressBar').css('width', '100%').attr("aria-valuenow", 100);
    $(":submit").prop('disabled', false);
    $("#loadingDisplay").delay(500).fadeOut(20).queue(function (next) {
        $('#loadProgressBar').delay(1200).css('width', '0%').attr("aria-valuenow", 0);
        next();
    });
    if (!$.isEmptyObject(data.Data)) {
        var list = data.Data;
        var item;
        for(var i=0; i<list.length;i++){
            item = list[i];
            var url = format("http://sgphrw02.apac.bosch.com/cms/FileHandler.ashx?RowId={0}&EntityID={1}&EmployeeCode={2}&filepath={3}&Mode={4}&filetype=PDF", item.ID, item.EntityId, item.EmployeeCode, item.filepath, item.Mode);
            window.open(url);
            //Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Generate",
            //       "window.open('../../../FileHandler.ashx?RowId=" + BasePage.EncyptURLString(VRowId) +
            //       "&EntityID=" + BasePage.EncyptURLString(SessionCollections.EntityId) +
            //       //"&EmployeeCode=" + BasePage.EncyptURLString(SessionCollections.EmployeeCode) +
            //       "&EmployeeCode=" + BasePage.EncyptURLString(tmpEmployeeCode) +
            //       "&filepath=" + BasePage.EncyptURLString(filepath) +
            //       "&Mode=" + BasePage.EncyptURLString("CONTRACT") + "&filetype=PDF')", true);
        }
    }
    
    
}

function format(fmtstr) {
    var args = Array.prototype.slice.call(arguments, 1);
    return fmtstr.replace(/\{(\d+)\}/g, function (match, index) {
        return args[index];
    });
}