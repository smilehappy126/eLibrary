
$(document).ready(function () {
    $("#boughtDatePicker").kendoDatePicker({
        format: "yyyy/MM/dd HH:mm:ss",
        dateInput: true
    });
    var validator = $("#newBookForm").kendoValidator().data("kendoValidator");

    //  取得所有的書籍種類(Book Class)
    $.ajax({
        type: "GET",
        url: "/Book/GetAllBookClassList",
        dataType: "json",
        success: function (json) {
            $("#bookClassPicker").kendoDropDownList({
                dataTextField: "Text",
                dataValueField: "Value",
                dataSource: json
            });
        },
        error: function (error) {
        }
    });

});
