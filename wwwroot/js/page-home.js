function GetRSS() {
    $.ajax({
        type: "GET",
        url: "/Home/GetRSS",
        beforeSend: function (xhr) {
            $('#rssContainer').attr("disabled", true); 
        }
    }).done(function (data, statusText, xhdr) {
        //console.log("Done");
        $("#rssContainer").html(data); 
    }).fail(function (xhdr, statusText, errorText) {
        //console.log("Failed");
        $("#tableContainer").text(JSON.stringify(xhdr));
    });
}