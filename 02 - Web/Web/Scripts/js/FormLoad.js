function DefaultLoad() {
    $('.DNHDefault').each(function () {
        BindForm(this);
    });

}
function BindForm(div) {
    //bind div and create table 
    var mydiv = $(div);
    var Target = $(mydiv).attr("target-id");
    var mylink = $(mydiv).attr("link");
    if (!$.isEmptyObject(mylink)) {

    }
    LoadForm(Target);
}
function LoadForm(select) {
    //load data from the link
    var mydiv = $("#" + select);
    
    if (!$.isEmptyObject(mydiv)) {
        var url = $(mydiv).attr("link");
        $(mydiv).load(url, function(){
            var Target = $(mydiv).attr("target-id");
            if (!$.isEmptyObject(Target)) {
                LoadForm(Target);
            }
            DefaultBindtable();
        });  
    }
    
}
