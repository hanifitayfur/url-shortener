const Shortly = {
    Create: {
        Click: function () {
            let that = document.getElementById("create-shortly-form");
            $("#btn-create-shortly").text("Sending..");
            Shortly.RefreshShortlyUrlsComponent();
            $.ajax({
                url: that.action,
                type: that.method,
                data: new FormData(that),
                cache: false,
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response.state === 2) {
                        let url = 'http://localhost:5089/' + response.data.shortUrl;
                        let html = '<a id="creating-short-url" href=' + url + ' target="_blank" style="font-size: 19px;"> ' + url + '</a>' +
                            '<br><a onclick="Shortly.CopyToClipboard()" class="btn btn-secondary">Copy to Clipboard</a>';
                        $('#response-message').html(html);
                        Shortly.RefreshShortlyUrlsComponent();
                    } else {
                        $('#response-message').text(response.message);
                    }
                },
                error: function (xhr, error, status) {
                    Security.CheckUnauthorizeStatus(status);
                    $('#response-message').text(status);
                },
                complete: function () {
                    $("#btn-create-shortly").text("Create");
                }
            });

        }
    },

    RefreshShortlyUrlsComponent: function () {
        $('#shortly-urls-div').html("");
        $.get("/admin/Shortly/RefreshShortlyUrlsComponent", function (data, status) {
            $('#shortly-urls-div').html(data);
        });
    },

    CopyToClipboard: function () {
        debugger
        let copyText = $('#creating-short-url').text()
        let sampleTextarea = document.createElement("textarea");
        document.body.appendChild(sampleTextarea);
        sampleTextarea.value = copyText; 
        sampleTextarea.select();
        document.execCommand("copy");
        document.body.removeChild(sampleTextarea);
        alert(copyText + " url is copied")
    }
}


$(document).ready(function () {
});