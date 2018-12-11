define([], function ()
{
    return {
        init: function ()
        {
            this.closeSesion();
            this.changePassword();
        },
        closeSesion: function () {
            $("#logout-link").click(function (e) {
                e.preventDefault();
                $.post("/cerrar-sesion", function (res) {
                    if (res.status === "done") {
                        //close the window now.
                        window.location.href = "";
                    }
                });
            });
        },
        changePassword: function ()
        {
            var $this = this;
            $("#change-password-input").click(function (e) {
                $this.cambiarPassword();
            });
        },
        generarAlerta: function (message)
        {
            var type = ['', 'info', 'danger', 'success', 'warning', 'rose', 'primary'];

            var color = Math.floor((Math.random() * 6) + 1);

            $.notify({
                icon: "add_alert",
                message: message
            },
            {
                type: type[color],
                timer: 5000,
                placement: {
                   from: 'top',
                   align: 'right'
                }
            });
            return;
        },
        cambiarPassword: function ()
        {
            this.cargandoEnFinalizar();

            var cambio = this.obtenerCambioPassword();
            if (!this.validarCambio(cambio))
                return this.normalTextEnFinalizar();

            var $this = this;
            $.when(this.cambiarPasswordAjax(cambio)).then(function (data, textStatus, jqXHR) {
                var alerta = data;
                if (data === true) alerta = 'Se ha actualizado la contraseña existosamente.';
                if (data === false) alerta = 'Error al actualizar la contraseña.';

                $this.generarAlerta(alerta);


                if (data === true)
                    $this.reloadPageAfter3sec();

                return $this.normalTextEnFinalizar();
            });

        },
        validarCambio: function (cambio)
        {
            var salida = true;
            if (!cambio.password === "") {
                this.generarAlerta('El campo contraseña no puede ser vacio.');
                salida = false;
            }

            if (!cambio.password_again === "") {
                this.generarAlerta('El campo confirmar no puede ser vacio.');
                salida = false;
            }

            if (cambio.password !== cambio.password_again) {
                this.generarAlerta('Las contraseñas no coinciden.');
                salida = false;
            }

            return salida;
        },
        obtenerCambioPassword: function ()
        {
            var cambio = {
                password: $('#nueva-contraseña-usuario').val(),
                password_again: $('#repetir-contraseña-usuario').val()
            };
            return cambio;
        },
        cambiarPasswordAjax: function (cambioPass)
        {
            return $.ajax({
                url: "/usuarios/cambiar-password",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(cambioPass)
            });
        },
        reloadPageAfter3sec: function ()
        {
            setTimeout(function () {
                $.post("/cerrar-sesion", function (res) {
                    if (res.status === "done") {
                        //close the window now.
                        window.location.href = "";
                    }
                });
            }, 3000);
        },
        normalTextEnFinalizar: function () {
            $('#change-password-input').html('Cambiar contraseña');
            $('#change-password-input').removeClass('disabled');
        },
        cargandoEnFinalizar: function () {
            $('#change-password-input').html('<i class="fas fa-spinner fa-spin fa-fw"></i> Actualizando');
            $('#change-password-input').addClass('disabled');
            return;
        }
    }
});