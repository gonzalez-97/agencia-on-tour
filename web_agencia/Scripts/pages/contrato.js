(function ($, d, w) {
    'use strict';
    w.thisPage = function () {
        return {
            init: function () {
                this.cargarCursosSelect();
                //this.cargarSegurosSelect();
                //this.cargarServiciosSelect();
                this.cargarPrimasSelect();
            },
            loadCursoAjax: function () {
                return $.get("/cursos/all");
            },
            loadServiciosAjax: function () {
                return $.get("/servicios/all");
            },
            loadSeguroAjax: function () {
                return $.get("/seguros/all");
            },
            loadPrimas_SegurosAjax: function () {
                return $.get("/primas-seguros/all");
            },
            loadDestinosAjax: function (){
                return $.get("/destinos/all");
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
            cargarServiciosSelect: function (servicioSelect) {
                var $thisObject = this;
                $.when(this.loadServiciosAjax()).then(function (data, textStatus, jqXHR) {
                    $(servicioSelect).empty();
                    $.each(data, function (i, item) {
                        $(servicioSelect).append($('<option>')
                            .attr('value', item.Id)
                            .attr('data-valor', item.Valor)
                            .html(item.Nombre));
                    });
                    $thisObject.calcularValorContrato();
                    $thisObject.addEventCalculoContrato(servicioSelect);
                });
            },
            cargarSegurosSelect: function (seguroSelect) {
                $.when(this.loadSeguroAjax()).then(function (data, textStatus, jqXHR) {
                    $(seguroSelect).empty();
                    $.each(data, function (i, item) {
                        $(seguroSelect).append($('<option>')
                            .attr('value', item.Id)
                            .html(item.Nombre));
                    });


                });
            },
            cargarPrimasSelect: function (seguroSelect) {
                $.when(this.loadPrimas_SegurosAjax()).then(function (data, textStatus, jqXHR) {
                    $(seguroSelect).empty();
                    $.each(data, function (i, item) {
                        $(seguroSelect).append($('<option>')
                            .attr('value', item.Id_Tipo)
                            .attr('data-valor', item.Valor_Prima_Individual)
                            .html(item.Nombre_Tipo));
                    });
                });
            },
            cargarDestinosSelect: function (destinoSelect) {
                var $thisObject = this;
                $.when(this.loadDestinosAjax()).then(function (data, textStatus, jqXHR) {
                    $(destinoSelect).empty();
                    $.each(data, function (i, item) {
                        $(destinoSelect).append($('<option>')
                            .attr('value', item.Id)
                            .attr('data-valor', item.Valor)
                            .html(item.Nombre));
                    });
                    $thisObject.calcularValorContrato();
                    $thisObject.addEventCalculoContrato(destinoSelect);
                });
            },
            addEventCalculoContrato: function (e)
            {
                var $this = this;
                $(e).change(function () {
                    $this.calcularValorContrato();
                });
            },
            calcularValorContrato: function ()
            {
                var totalContrato = 0;
                totalContrato += this.calcularTotalServicios();
                totalContrato += this.calcularTotalDestinos();
                $('#valor-contrato').val(totalContrato);
            },
            calcularTotalServicios: function ()
            {
                var salida = 0;
                $.each($('.div-servicios'), function (i, item)
                {
                    var select = $(item).find('.servicios-contrato').first();
                    var value = parseInt($(select).children("option:selected").data('valor'));
                    salida += value;
                });

                return salida;
            },
            calcularTotalDestinos: function () {
                var salida = 0;
                $.each($('.div-destinos'), function (i, item) {
                    var select = $(item).find('.destinos-contrato').first();
                    var value = parseInt($(select).children("option:selected").data('valor'));
                    salida += value;
                });

                return salida;
            },
            addServicio: function ()
            {
                var $contenedorServicios = $('#contenedor-servicios');

                var $row = $('<div>').addClass('row div-servicios');
                var $selectServicio = $('<select>').addClass('form-control servicios-contrato');
                this.cargarServiciosSelect($selectServicio);
                var $formGroup = $('<div>').addClass('form-group').append($selectServicio);
                var $col10 = $('<div>').addClass('col-sm-10').append($formGroup);
                $col10.appendTo($row);

                var $icono = $('<i>').addClass('material-icons').html('close');
                var $botonCerrar = $('<button type="button">').addClass('btn btn-outline-danger btn-round btn-sm btn-fab').append($icono);
                var $col1 = $('<div>').addClass('col-sm-1').append($botonCerrar);
                $col1.appendTo($row);

                $row.appendTo($contenedorServicios);


            },
            addDestino: function () {
                var $contenedorDestinos = $('#contenedor-destinos');

                var $row = $('<div>').addClass('row div-destinos');
                var $selectDestino = $('<select>').addClass('form-control destinos-contrato');
                this.cargarDestinosSelect($selectDestino);
                var $formGroup = $('<div>').addClass('form-group').append($selectDestino);
                var $col10 = $('<div>').addClass('col-sm-10').append($formGroup);
                $col10.appendTo($row);

                var $icono = $('<i>').addClass('material-icons').html('close');
                var $botonCerrar = $('<button type="button">').addClass('btn btn-outline-danger btn-round btn-sm btn-fab').append($icono);
                var $col1 = $('<div>').addClass('col-sm-1').append($botonCerrar);
                $col1.appendTo($row);

                $row.appendTo($contenedorDestinos);


            },
            addSeguro: function () {

                var $contenedorSeguros = $('#contenedor-seguros');
                var $row = $('<div>').addClass('row div-seguros');

                var $selectSeguro = $('<select>').addClass('form-control seguros-contrato');
                this.cargarSegurosSelect($selectSeguro);
                var $formGroupSeguro = $('<div>').addClass('form-group').append($selectSeguro);
                var $colSeguro = $('<div>').addClass('col-sm-3').append($formGroupSeguro);
                $colSeguro.appendTo($row);

                var $selectPrimaSeguro = $('<select>').addClass('form-control tipo-seguro');
                this.cargarPrimasSelect($selectPrimaSeguro);
                var $formGroupPrimas = $('<div>').addClass('form-group').append($selectPrimaSeguro);
                var $colPrima = $('<div>').addClass('col-sm-4').append($formGroupPrimas);
                $colPrima.appendTo($row);

                var $inputDias = $('<input type="number">').addClass('form-control');
                var $formGroupDias = $('<div>').addClass('form-group').append($inputDias);
                var $colDias = $('<div>').addClass('col-sm-2').append($formGroupDias);
                $colDias.appendTo($row);

                var $inputValor = $('<input type="number">').addClass('form-control');
                var $formGroupValor = $('<div>').addClass('form-group').append($inputValor);
                var $colValor = $('<div>').addClass('col-sm-2').append($formGroupValor);
                $colValor.appendTo($row);

                var $icono = $('<i>').addClass('material-icons').html('close');
                var $botonCerrar = $('<button type="button">').addClass('btn btn-outline-danger btn-round btn-sm btn-fab').append($icono);
                var $colCerrar = $('<div>').addClass('col-sm-1').append($botonCerrar);
                $colCerrar.appendTo($row);

                $row.appendTo($contenedorSeguros);

            },
            obtenerCurso: function () {
                var curso = { Id: $('#curso-alumno').val() };
                return curso;
            },
            obtenerListaServicios: function ()
            {
                var array = new Array();
                $.each($('.div-servicios'), function (i, item) {
                    var select = $(item).find('.servicios-contrato').first();
                    var elemento = { Servicio: { Id: parseInt($(select).val()) } };
                    array.push(elemento);
                });
                return array;
            },
            obtenerListaDestinos: function ()
            {
                var array = new Array();
                $.each($('.div-destinos'), function (i, item) {
                    var select = $(item).find('.destinos-contrato').first();
                    var elemento = { Destino: { Id: parseInt($(select).val()) } };
                    array.push(elemento);
                });
                return array;
            },
            obtenerContrato: function () {
                var contrato = {
                    Nombre: $('#nombre-contrato').val(),
                    Descripcion: $('#descripcion-contrato').val(),
                    Fecha_Viaje: $('#fecha-viaje').val(),
                    Valor: $('#valor-contrato').val(),
                    Curso: this.obtenerCurso(),
                    ListaServiciosAsociados: this.obtenerListaServicios(),
                    ListaDestinosAsociados: this.obtenerListaDestinos()
                };
                return contrato;
            },
            guardaContratoAjax: function (contrato) {
                return $.ajax({
                    url: "/contrato/crear-ajax",
                    type: "POST",
                    contentType: "application/json",
                    data: JSON.stringify(contrato)
                });
            },
            botonFinalizar: function () {
                this.cargandoEnFinalizar();
                var $this = this;

                $.when($this.guardaContratoAjax(this.obtenerContrato())).then(function (data, textStatus, jqXHR) {
                    var alerta = data;
                    if (data === true) alerta = 'Se ha guardado el contrato existosamente';
                    if (data === false) alerta = 'Error al guardar los datos del contrato';
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
