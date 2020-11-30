$(document).ready(function () {

    let noti = localStorage.getItem('Monitor-Notifica');
    NotificaBtnMod(noti == null || noti == "" ? "on" : noti);

    let relo = localStorage.getItem('Monitor-Reload');
    ReloadOptionSel(relo == null || relo == "" ? "5" : relo);

});

function NotificaBtnMod(accion) {

    if (accion == "on") {
        $("#IconNotification").removeClass("dripicons-volume-off").addClass("dripicons-volume-full");
    } else {
        $("#IconNotification").removeClass("dripicons-volume-full").addClass("dripicons-volume-off");
    }

    localStorage.setItem("Monitor-Notifica", accion);
}

$("#IconNotifiMod").click(function () {
    let noti = localStorage.getItem('Monitor-Notifica');

    NotificaBtnMod(noti == "on" ? "off" : "on");
});


function ReloadOptionSel(add, rem) {
    if (rem != null && rem != '') {
        $.each($("#ListReload li"), function () {
            if ($(this).data('rel') == rem) {
                $(this).find("i").removeClass("dripicons-checkmark");
            }
        });
    } else {
        $("#ListReload li").find("i").each(function () {
            $(this).removeClass("dripicons-checkmark");
        });
    }

    $.each($("#ListReload li"), function () {
        if ($(this).data('rel') == add) {
            $(this).find("i").addClass("dripicons-checkmark");
        }
    });

    localStorage.setItem('Monitor-Reload', add);
}


$(".ItemReload").click(function () {
    let noti = localStorage.getItem('Monitor-Reload');

    let notinew = $(this).closest('li').data("rel");

    if (noti != notinew) {
        ReloadOptionSel(notinew, noti);
    }

});






function CheckedCheckbox(objet, decicion) {
    if (decicion) {
        $(objet).attr("checked", "checked");
        $(objet).prop("checked", true);
    } else {
        $(objet).prop("checked", false);
    }
}


function AjaxSubmit(_url, _btnSubmit, _titleError) {
    this.url = _url;
    this.btnSubmit = _btnSubmit;
    this.btnCancel;
    this.partial;
    this.titleError = _titleError;
    this.divBlock;
    this.inputClear;
    this.tablereload;
}

AjaxSubmit.prototype.AjaxPop = function (_dt) {
    _btnCancel = this.btnCancel;
    _partial = this.partial;
    _titleError = this.titleError;
    _btnSubmit = this.btnSubmit;
    _inputClear = this.inputClear;

    $(this.btnSubmit).prop("disabled", true);

    $.ajax({
        url: this.url,
        data: _dt,
        type: "post",
        cache: false,
        success: function (result) {
            if (result != null) {
                $(_btnCancel).click();
                $(_partial).html(result);
                NotificaSave();
                if (_inputClear != null) {
                    _inputClear.forEach(function (input) {
                        $(input).val("");

                    });
                };
            };
        },
        error: function (result) {
            MensajeError(_titleError, result.responseJSON.mnsj);
            if (result.responseJSON.redir) {
                setTimeout(function () {
                    window.location.replace(result.responseJSON.redirectToUrl);
                }, 3000);
            }
        },
        complete: function () {
            $(_btnSubmit).prop("disabled", false);
        }
    });
}

AjaxSubmit.prototype.AjaxPopOpen = function (_dt) {
    _partial = this.partial;
    _titleError = this.titleError;

    $.ajax({
        url: this.url,
        data: _dt,
        type: "post",
        cache: false,
        success: function (result) {
            if (result != null) {
                $(_partial).html(result);
            };
        },
        error: function (result) {
            MensajeError(_titleError, result.responseJSON.mnsj);
            if (result.responseJSON.redir) {
                setTimeout(function () {
                    window.location.replace(result.responseJSON.redirectToUrl);
                }, 3000);
            }
        }
    });
}

AjaxSubmit.prototype.AjaxMonitor = function (_dt) {
    _partial = this.partial;
    _titleError = this.titleError;
    _btnCancel = this.btnCancel;

    $.ajax({
        url: this.url,
        type: "post",
        data: _dt,
        cache: false,
        success: function (result) {
            if (result != null) {
                $(_partial).html(result);
                RevisionAlarma();

                if (_btnCancel != null)
                    $(_btnCancel).click();
            };
        },
        error: function (result) {
            MensajeError(_titleError, result.responseJSON.mnsj);
            if (result.responseJSON.redir) {
                setTimeout(function () {
                    window.location.replace(result.responseJSON.redirectToUrl);
                }, 3000);
            }
        }
    });
}


AjaxSubmit.prototype.ObtenerDatos = function (_dt) {
    _partial = this.partial;
    _titleError = this.titleError;
    _btnSubmit = this.btnSubmit;
    _tablereload = this.tablereload;
    console.log(_tablereload);

    $(this.btnSubmit).prop("disabled", true);

    $.ajax({
        url: this.url,
        data: _dt,
        type: "post",
        cache: false,
        success: function (result) {
            if (result != null) {
                $(_partial).html(result);
                if(_tablereload != null)
                {
                   $(_tablereload).DataTable();
                   console.log(_tablereload);
                }
            };
        },
        error: function (result) {
            MensajeError(_titleError, result.responseJSON.mnsj);
            if (result.responseJSON.redir) {
                setTimeout(function () {
                    window.location.replace(result.responseJSON.redirectToUrl);
                }, 3000);
            }
        },
        complete: function () {
            $(_btnSubmit).prop("disabled", false);
        }
    });
}



function NotificaSave() {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: "Registro almacenado con exito.",
        showConfirmButton: false,
        timer: 1500
    })
}

function MensajeError(tit, tex) {
    swal.fire({
        icon: 'error',
        title: tit,
        text: tex,
        timer: 50000,
        confirmButtonText: 'Aceptar',
        timerProgressBar: true
    });
}