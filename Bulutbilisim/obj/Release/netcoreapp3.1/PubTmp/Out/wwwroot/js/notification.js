function notificationFunction(notificationObj, modal = true) {

    //https://codeseven.github.io/toastr/demo.html
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    if (typeof notificationObj.action != "undefined" && notificationObj.action != null && notificationObj.action != "") {
        toastr.options.onHidden = function (a) { location.href = notificationObj.action; }
    }


    if (notificationObj.mesaj != "" && notificationObj.durum != "") {

        toastr[notificationObj.durum](notificationObj.mesaj, notificationObj.baslik);
        if (notificationObj.durum == "success") {
            if (window.activemodal != undefined) {
                $(activemodal).modal('hide');
            }
            //if (notificationObj.donuslinki.length > 0) {
            //    console.log(notificationObj.donuslinki);
            //}
        }

    }

}

function OnSuccessForm(data) {
    console.log(data);
    debugger;
    notificationFunction(data);
    if (data.durum == "success") {
        setTimeout(function () {
            location.reload(true);
        }, 2000);
    }

    /*location.reload(true);*/
}
function OnErrorForm(data) {
    console.log(data)
    notificationFunction(data)
}

const toastrType = ["success", "error", "warning", "info"];



function miniNotificationFunction(baslik, mesaj, durum) {

    if (baslik == null || baslik == undefined || baslik == "") {
        baslik = "Bilgilendirme";
    }

    if (durum == null || durum == undefined || durum == "" || !toastrType.includes(durum)) {
        durum = "success";
    }

    if (mesaj == null || mesaj == undefined || mesaj == "") {
        mesaj = "Bilgilendirme Mesajı";
    }

    //https://codeseven.github.io/toastr/demo.html
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }


    toastr[durum](mesaj, baslik);
}

