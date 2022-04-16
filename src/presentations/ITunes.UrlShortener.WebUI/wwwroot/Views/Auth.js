const Auth = {
    Login: {
        Click: function () {
            let that = document.getElementById("login-form");
            $("#btn-login").text("Sending..");
            $.ajax({
                url: that.action,
                type: that.method,
                data: new FormData(that),
                cache: false,
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response === null) {
                        $('#response-message').text("Kullanıcı bulunamadı..Null");
                    } else if (response.state === 2 && response.data === true) {
                        window.location.href = "/admin/Shortly/Create";
                    } else {
                        $('#response-message').text(response.message);
                    }
                },
                error: function (xhr, error, status) {
                    console.log(error, status);
                    $('#response-message').text(status);
                },
                complete: function () {
                    $("#btn-login").text("Login");
                }
            });

        }
    },
};


$(document).ready(function () {
});