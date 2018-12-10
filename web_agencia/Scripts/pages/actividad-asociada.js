require(["all"], function (all) {

    var actividad_asociada =
    {
        init: function ()
        {
            all.closeSesion();
            this.cargarActividadesSelect();
            this.cargarCursosSelect();

            $('#registrar-input').click(function (e) {
                actividad_asociada.botonFinalizar();
            });

        },
        cargarActividadesSelect: function () {
            $.when(this.loadActividadAjax()).then(function (data, textStatus, jqXHR) {
                var actividadSelect = $('#actividad_select');
                $(actividadSelect).empty();

                $.each(data, function (i, item) {
                    $(actividadSelect).append($('<option>').attr('value', item.Id).html(item.Nombre));
                });
            });
        },
        cargarCursosSelect: function () {
            $.when(this.loadCursoAjax()).then(function (data, textStatus, jqXHR) {
                var cursoSelect = $('#curso_select');
                $(cursoSelect).empty();

                $.each(data, function (i, item) {
                    $(cursoSelect).append($('<option>').attr('value', item.Id).html(item.Nombre));
                });
            });
        },
        loadCursoAjax: function () {
            return $.get("/actividad-asociada/cursos-ajax");
        },
        loadActividadAjax: function () {
            return $.get("/actividad-asociada/actividades-ajax");
        },
        guardarActividadAjax: function (actividad) {
            return $.ajax({
                url: "/actividad-asociada/guardar-ajax",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(actividad)
            });
        },
        obtenerActividadAsociada: function ()
        {
            var actividadCurso = {
                Id_Actividad: parseInt($('#actividad_select').val()) || 0 ,
                Id_Curso: parseInt($('#curso_select').val()) || 0,
                Total_Recaudado: parseInt($('#total_recaudado').val()) || 0 
            };
            return actividadCurso;
        },
        validarActividadAsociada: function (actividadCurso)
        {
            var salida = true;
            if (actividadCurso.Id_Actividad === 0) {
                all.generarAlerta('Id de actividad invalido');
                salida = false;
            }

            if (actividadCurso.Id_Curso === 0) {
                all.generarAlerta('Id de curso invalido');
                salida = false;
            }

            if (actividadCurso.Total_Recaudado === 0) {
                all.generarAlerta('Valor invalido, campo requerido');
                salida = false;
            }
            return salida;

        },
        botonFinalizar: function () {
            this.cargandoEnFinalizar();

            var actividad = this.obtenerActividadAsociada();

            if (!this.validarActividadAsociada(actividad))
                return actividad_asociada.normalTextEnFinalizar();

            $.when(this.guardarActividadAjax(actividad)).then(function (data, textStatus, jqXHR) {
                var alerta = data;
                if (data === true) alerta = 'Se ha guardado la actividad existosamente';
                if (data === false) alerta = 'Error al guardar actividad del curso';
                all.generarAlerta(alerta);

                return actividad_asociada.normalTextEnFinalizar();
            });

        },
        normalTextEnFinalizar: function () {
            $('#registrar-input').html('Crear Actividad');
            $('#registrar-input').removeClass('disabled');
        },
        cargandoEnFinalizar: function () {
            $('#registrar-input').html('<i class="fas fa-spinner fa-spin fa-fw"></i> Guardando');
            $('#registrar-input').addClass('disabled');
            return;
        }
    };


    actividad_asociada.init();

});