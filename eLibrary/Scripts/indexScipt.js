var datasource;
$(document).ready(function () {
    $("#bookGrid").kendoGrid({
        dataSource: {
            data: datasource,
            pageSize: 20
        },
        height: 550,
        groupable: true,
        sortable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        columns: [{
            field: "BOOK_CLASS_ID",
            title: "圖書類別",
            width: 240
        }, {
            field: "BOOK_NAME",
            title: "書名"
        }, {
            field: "BOOK_BOUGHT_DATE",
            title: "購書日期",
            template: "#= kendo.toString(kendo.parseDate(BOOK_BOUGHT_DATE, 'yyyy/MM/dd'), 'yyyy/MM/dd') #"
        }, {
            field: "BOOK_STATUS",
            title: "借閱狀態",
            width: 150
        }, {
            field: "BOOK_KEEPER",
            title: "借閱人"
        }, {
            field: "BOOK_ID",
            title: " ",
            template: "<a class='k-button' href='/Book/EditBookData/#= BOOK_ID #'>編輯</a> <button class='k-button indexDeleteBtn'  data-deleteurl='/Book/IndexDeleteBookData/#= BOOK_ID #'>刪除</button>"
        }]
    });
    // 取得所有的書籍資料
    $.ajax({
        type: "POST",
        url: "/Book/SearchBook",
        dataType: "json",
        success: function (json) {
            datasource = json;
            $("#bookGrid").data("kendoGrid").setDataSource(datasource);
            $("#bookGrid").data("kendoGrid").refresh();
        },
        error: function (error) {
        }
    });

    //  取得所有的書籍種類(Book Class)
    $.ajax({
        type: "GET",
        url: "/Book/GetAllBookClassList",
        dataType: "json",
        success: function (json) {
            $("#searchClass").kendoDropDownList({
                dataTextField: "Text",
                dataValueField: "Value",
                dataSource: json,
                optionLabel: "無"
            });
        },
        error: function (error) {
        }
    });

    //  取得所有的借閱人(Book Keeper)
    $.ajax({
        type: "GET",
        url: "/Book/GetAllBookKeeperList",
        dataType: "json",
        success: function (json) {
            $("#searchUser").kendoDropDownList({
                dataTextField: "Text",
                dataValueField: "Value",
                dataSource: json,
                optionLabel: "無"
            });
        },
        error: function (error) {
        }
    });

    //  取得所有的借閱狀態(Book Status)
    $.ajax({
        type: "GET",
        url: "/Book/GetAllBookStatusList",
        dataType: "json",
        success: function (json) {
            $("#searchStatus").kendoDropDownList({
                dataTextField: "Text",
                dataValueField: "Value",
                dataSource: json,
                optionLabel: "無"
            });
        },
        error: function (error) {
        }
    });
});

// 按下搜尋按鈕就去根據輸入值透過ajax進行資料庫撈取並重整grid的dataSource
$("#searchBtn").click(function () {
    var bookName = $("#searchName").val();
    var bookClass = $("#searchClass").val();
    var userName = $("#searchUser").val();
    var bookStatus = $("#searchStatus").val();
    var postData = {
        "BOOK_NAME": bookName,
        "BOOK_CLASS_ID" : bookClass,
        "BOOK_KEEPER": userName,
        "BOOK_STATUS": bookStatus
    };
    console.log(postData);
    $.ajax({
        type: "POST",
        url: "/Book/SearchBook",
        data: postData,
        dataType: "json",
        success: function (json) {
            datasource = json;
            $("#bookGrid").data("kendoGrid").setDataSource(datasource);
            $("#bookGrid").data("kendoGrid").refresh();
        },
        error: function (error) {
        }
    });
});

// 按下清除按鈕就將搜尋欄位的值回覆預設並且將dataSource回到一開始
$("#cleanBtn").click(function () {
    $("#searchName").val("");
    $("#searchClass").data("kendoDropDownList").select(0);
    $("#searchUser").data("kendoDropDownList").select(0);
    $("#searchStatus").data("kendoDropDownList").select(0);
    $.ajax({
        type: "GET",
        url: "/Book/GetAllBookData",
        dataType: "json",
        success: function (json) {
            datasource = json;
            $("#bookGrid").data("kendoGrid").setDataSource(datasource);
            $("#bookGrid").data("kendoGrid").refresh();
        },
        error: function (error) {
        }
    });
});

// grid中的刪除按鈕被按下時會先確認該筆資料是否已被借出，沒有已借出的話再進行資料庫刪除的動作
$(document).on("click", ".indexDeleteBtn", function (e) {
    var target = e.target || e.srcElement;
    var bookID = $("#bookGrid").data("kendoGrid").dataItem($(target).closest("tr")).BOOK_ID;
    var deleteurl = $(this).data("deleteurl");
    $.ajax({
        type: "POST",
        url: "/Book/CurrentBookStatus/" + bookID,
        dataType: "json",
        success: function (json) {
            if (json === "B") {
                alert("此本書目前已借出，無法刪除!!!")
            } else {
                var confirmDelete = confirm("確定要刪除這筆資料?");
                if (confirmDelete) {
                    $.ajax({
                        type: "POST",
                        url: deleteurl,
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
