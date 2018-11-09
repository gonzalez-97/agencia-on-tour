(function ($, d, w) {
    'use strict';
    w.thisPage = function () {
        return {
            init: function () {
                this.cargarCursosSelect();
                this.cargarSegurosSelect();
                this.cargarServiciosSelect();
                this.cargarPrimasSelect();
            },
            loadCursoAjax: function () {
                return $.get("/cursos/all");
            },
            loadServiciosAjax: function ()
            {
                return $.get("/servicios/all");
            },
            loadSeguroAjax: function () {
                return $.get("/seguros/all");
            },
            loadPrimas_SegurosAjax: function () {
                return $.get("/primas-seguros/all");
            },
            cargarCursosSelect: function () {
                $.when(this.loadCursoAjax()).then(function (data, textStatus, jqXHR) {
                    var cursoSelect = $('#curso-alumno');
                    $(cursoSelect).empty();

                    $.each(data, function (i, item) {
                        $(cursoSelect).append($('<option>').attr('value', item.Id).html(item.Nombre));
                    });
                });
            },
            cargarServiciosSelect: function () {
                $.when(this.loadServiciosAjax()).then(function (data, textStatus, jqXHR) {
                    var servicioSelect = $('.servicios-contrato').first();
                    $(servicioSelect).empty();

                    $.each(data, function (i, item) {
                        $(servicioSelect).append($('<option>').attr('value', item.Id).html(item.Nombre));
                    });
                });
            },
            cargarSegurosSelect: function () {
                $.when(this.loadSeguroAjax()).then(function (data, textStatus, jqXHR) {
                    var seguroSelect = $('.seguros-contrato').first();
                    $(seguroSelect).empty();

                    $.each(data, function (i, item) {
                        $(seguroSelect).append($('<option>').attr('value', item.Id).html(item.Nombre));
                    });
                });
            },
            cargarPrimasSelect: function () {
                $.when(this.loadPrimas_SegurosAjax()).then(function (data, textStatus, jqXHR) {
                    var seguroSelect = $('.tipo-seguro').first();
                    $(seguroSelect).empty();

                    $.each(data, function (i, item) {
                        $(seguroSelect).append($('<option>')
                            .attr('value', item.Id_Tipo)
                            .attr('data-valor', item.Valor_Prima_Individual)
                            .attr('data-aumento', item.Porc_Aumento_Dia)
                            .html(item.Nombre_Tipo));
                    });
                });
            },
            addServicio: function ()
            {
                $(".div-servicios").first().clone().appendTo("#contenedor-servicios");
            },
            addSeguro: function () {
                $(".div-seguros").first().clone().appendTo("#contenedor-seguros");
            },
            obtenerCurso: function () {
                var curso = { Id: $('#curso-alumno').val() };
                return curso;
            },
            obtenerAlumno: function () {
                var alumno = {
                    Rut: $('#rut-alumno').val(),
                    DigitoV: this.calculaDigitoVerificador($('#rut-alumno').val()),
                    Nombre: $('#nombre-alumno').val(),
                    APaterno: $('#apaterno-alumno').val(),
                    AMaterno: $('#amaterno-alumno').val(),
                    Curso: this.obtenerCurso()
                };
                return alumno;
            },
            guardarAlumnoAjax: function (alumno) {
                return $.ajax({
                    url: "/alumno/crear-ajax",
                    type: "POST",
                    contentType: "application/json",
                    data: JSON.stringify(alumno)
                });
            },
            botonFinalizar: function () {
                this.cargandoEnFinalizar();
                var $this = this;

                if (!$this.validarObjeto(this.obtenerAlumno()))
                    return $this.normalTextEnFinalizar();

                $.when($this.guardarAlumnoAjax(this.obtenerAlumno())).then(function (data, textStatus, jqXHR) {
                    var alerta = data;
                    if (data === true) alerta = 'Se ha guardado el alumno existosamente';
                    if (data === false) alerta = 'Error al guardar los datos del alumno';
                    $this.generarAlerta(alerta);
                    return $this.normalTextEnFinalizar();
                });

            },
            validarObjeto: function (objeto) {
                var salida = true;
                for (var key in objeto) {
                    if (objeto[key] === "" || objeto[key] === null || objeto[key] === undefined) {
                        this.generarAlerta('El campo ' + key + " no puede ser vacio");
                        salida = false;
                    }
                }
                return salida;
            },
            generarAlerta: function (message) {
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
            calculaDigitoVerificador: function (rut) {
                // type check
                if (!rut || !rut.length || typeof rut !== 'string') { return -1; }
                // serie numerica
                var secuencia = [2, 3, 4, 5, 6, 7, 2, 3];
                var sum = 0;
                //
                for (var i = rut.length - 1; i >= 0; i--) {
                    var d = rut.charAt(i);
                    sum += new Number(d) * secuencia[rut.length - (i + 1)];
                }
                // sum mod 11
                var rest = 11 - sum % 11;
                // si es 11, retorna 0, sino si es 10 retorna K,
                // en caso contrario retorna el numero
                return rest === 11 ? 0 : rest === 10 ? "K" : rest;
            },
            normalTextEnFinalizar: function () {
                $('#registrar-input').html('Finalizar');
                $('#registrar-input').removeClass('disabled');
            },
            cargandoEnFinalizar: function () {
                $('#registrar-input').html('<i class="fas fa-spinner fa-spin fa-fw"></i> Guardando');
                $('#registrar-input').addClass('disabled');
                return;
            }
        };
    };
})(jQuery, document, window);
