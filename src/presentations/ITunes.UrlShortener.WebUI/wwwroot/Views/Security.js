const Security = {
    CheckUnauthorizeStatus: function (status){
        if (status==="Unauthorized"){
            window.location.href = "/admin/auth/login";
        }
    }
};


$(document).ready(function () {
});