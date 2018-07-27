var currentKeeper = "";
var currentClass = "";
var currentStatus = "";

$(function () {
    $("#editBoughtDatePicker").kendoDatePicker({
        format: "yyyy/MM/dd HH:mm:ss",
        dateInput: true
    });
    var validator = $("#editBookForm").kendoValidator().data("kendoValidator");

    //  取得目前書籍的資料(Book Data)
    $.ajax({
        type: "POST",
        url: window.location.href,
        dataType: "json",
        success: function (json) {
            // 將從資料庫撈出來的資料放入輸入欄中
            $("#formBookName").val(json.BOOK_NAME);
            $("#formBookAuthor").val(json.BOOK_AUTHOR);
            $("#formBookPublisher").val(json.BOOK_PUBLISHER);
            $("#formBookNote").html(json.BOOK_NOTE);
            $("#editBoughtDatePicker").val(json.BOOK_BOUGHT_DATE);
            $("#editBookID").val(""+json.BOOK_ID);
            currentKeeper = json.BOOK_KEEPER_NAME;
            currentClass = json.BOOK_CLASS_NAME;
            currentStatus = json.BOOK_STATUS_NAME;
            $("#deleteBookBtn").attr("data-deleteid", json.BOOK_ID);

            //  取得所有的書籍種類(Book Class)
            $.ajax({
                type: "GET",
                url: "/Book/GetAllBookClassList",
                dataType: "json",
                success: function (json) {
                    $("#bookClassList").kendoDropDownList({
                        dataTextField: "Text",
                        dataValueField: "Value",
                        dataSource: json
                    });
                    $("#bookClassList").data("kendoDropDownList").search(currentClass);
                },
                error: function (error) {
                    alert("Some thing happened while requesting BookClassList. Please refresh the page.");
                }
            });

            //  取得所有的借閱人(Book Keeper)
            $.ajax({
                type: "GET",
                url: "/Book/GetAllBookKeeperList",
                dataType: "json",
                success: function (json) {
                    $("#bookKeeperList").kendoDropDownList({
                        dataTextField: "Text",
                        dataValueField: "Value",
                        dataSource: json,
                        optionLabel: "無"
                    });
                    $("#bookKeeperList").data("kendoDropDownList").search(currentKeeper);

                    //  取得所有的借閱狀態(Book Status)
                    $.ajax({
                        type: "GET",
                        url: "/Book/GetAllBookStatusList",
                        dataType: "json",
                        success: function (json) {
                            $("#bookStatusList").kendoDropDownList({
                                dataTextField: "Text",
                                dataValueField: "Value",
                                dataSource: json
                            });
                            $("#bookStatusList").data("kendoDropDownList").search(currentStatus);
                            if ($("#bookStatusList").data("kendoDropDownList").value() === "A" || $("#bookStatusList").data("kendoDropDownList").value() === "U") {
                                $("#bookKeeperList").data("kendoDropDownList").enable(false);
                                $("#bookKeeperList").data("kendoDropDownList").select(0);
                            } else {
                                $("#bookKeeperList").data("kendoDropDownList").enable(true);
                            }
                        },
                        error: function (error) {
                            alert("Some thing happened while requesting BookStatusList. Please refresh the page.");
                        }
                    });
                },
                error: function (error) {
                    alert("Some thing happened while requesting BookKeeperList. Please refresh the page.");
                }
            });

           
        },
        error: function (error) {
        }
    });

});

// 當書本的狀態被改變時去調整借用人的選單屬性
$("#bookStatusList").change(function () {
    if ($("#bookStatusList").data("kendoDropDownList").value() === "A" || $("#bookStatusList").data("kendoDropDownList").value() === "U") {
        $("#bookKeeperList").data("kendoDropDownList").enable(false);
        $("#bookKeeperList").data("kendoDropDownList").select(0);
    } else {
        $("#bookKeeperList").data("kendoDropDownList").enable(true);
    }
});



// 確認刪除時做出反應，警告已被借出或刪除
$("#deleteBookBtn").click(function () {
    var bookID = $("#deleteBookBtn").data("deleteid");
    $.ajax({
        type: "POST",
        url: "/Book/CurrentBookStatus/"+ bookID,
        dataType: "json",
        success: function (json) {
                    if (json === "B") {
                        alert("此本書目前已借出，無法刪除!!!")
                    } else {
                        var confirmDelete = confirm("確定要刪除這筆資料?");
                        if (confirmDelete) {
                            $.ajax({
                                 type: "POST",
                                 url: "/Book/DeleteBookData/" + bookID,
                                 dataType: "json",
                                 success: function (json) {
                                     alert("刪除失敗!");
                                     window.location.assign("/Book/Index");
                                 },
                                 error: function (error) {
                                     alert("成功刪除!!!");
                                     window.location.assign("/Book/Index");
                                 }
                             });
                        }
                    }
        },
        error: function (error) {
                  console.log(error);
        }
    });
});
