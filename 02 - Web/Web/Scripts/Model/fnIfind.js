var filedata = [];
var table;
var isAddErrorCol = false;
var myCol = [];
var ErrorCol = [];
$(document).ready(function () {
    $(".SelectFile").click(function () {
        var targetid = $(this).attr("targetid");
        $("#" + targetid).click();
    })
    $("[class=fileupload]").change(function () {

        if ($('input[class=fileupload]')[0].files && $('input[class=fileupload]')[0].files[0]) {
            ShowProcessing();//show processing
            var formDatata = new FormData();
            var Link = $(this).attr("link");
          
            
            var targetid = $(this).attr("targetid");
            var column = $(targetid).attr("arrid");
            var savebtn = $(targetid).attr("savebtn");
            
            var ColArray = eval($("#" + column).val());
            
            for (var i = 0; i < ColArray.length; i++) {
                var b = {
                    title: ColArray[i],
                    data: ColArray[i]
                }
                myCol.push(b);
            }
            formDatata.append("fScreenshot", $(this)[0].files[0])
            ld(true);
            $.ajax({
                type: 'POST',
                url: GetPath(Link),
                data: formDatata,
                processData: false,
                contentType: false,// not json
                async: false,
                cache: false,
                success: function (data) {
                    //angular.element('#CustomerManament').scope().fnLoadCustomerFromExcel(data);
                    //  angular.element('#CustomerManament').scope().$apply()
                    //table.destroy();
                    if ($.isEmptyObject(table) == false) {
                        table.destroy();
                        $(targetid).empty();
                    }
                    filedata = data.Data;
                    table= $(targetid).DataTable({
                        data: data.Data,
                        columns: myCol,
                        destroy: true,
                        paging: false,
                        searching: false,
                        responsive: true,
                        "scrollX": true,
                        fixedHeader: true,
                        fnDrawCallback: function (oSettings) {
                            $("#" + savebtn).unbind();
                            $("#" + savebtn).click(function (e) {
                                var clickbtn = this;
                                ld(true);
                                $(this).prop("disabled", true)
                                var actionlink = $(this).attr("link");
                                var par = {
                                    item: JSON.stringify(filedata)
                                }
                                $.ajax({
                                    async : true,
                                    type: 'POST',
                                    url: GetPath(actionlink),
                                    //dataType: 'json',
                                    data: par,
                                    success: function (data) {
                                        $(clickbtn).prop("disabled", false);
                                        if (data.StatusCode == 400) {
                                            if (!isAddErrorCol) {
                                                var b = {
                                                    title: "Error Message",
                                                    data: "ErrorMesssage"
                                                }
                                                
                                                // splice(position, numberOfItemsToRemove, item)
                                                ErrorCol = myCol;
                                                myCol.splice(0, 0, b);

                                                isAddErrorCol = true;
                                            }
                                            filedata = data.Data;
                                            //myCol = [b, myCol];
                                            table.destroy();
                                            $(targetid).empty(); // empty in case the columns change
                                            table = $(targetid).DataTable({
                                                columns: myCol,
                                                data: data.Data,
                                                destroy: true,
                                                paging: false,
                                                searching: false,
                                                responsive: true,
                                                fixedHeader: true
                                            });
                                            myCol = ErrorCol;
                                            notimessage("error", data.Message, "error");
                                        } else {
                                            notimessage("success", data.Message, "success");
                                        }
                                        
                                        ld(false);
                                    },
                                    error: function (e) {
                                        //AVAILABLE TYPES: "error", "info", "success", "warning"
                                        notimessage("error", e.Message, "Error");
                                        ld(false);
                                    }

                                });
                            });
                            ld(false);
                        }
                    });
                    notimessage("success", data.Message, "Success");
                    //notimessage("error", data.Message, "Error");
                    //console.log(data)
                },
                error: function (e) {
                    notimessage("error", data.Message, "Error");
                    //notimessage("error", "This File format error!", "Error");
                },
            });
            //$(".file-input-name").remove();
            HideProcessing();//hide processing
            return true;
        }
    });
});

