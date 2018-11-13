(function ($, d, w) {
    'use strict';
    w.thisPage = function () {
        return {
            init: function () {
                this.cargarCursosSelect();
                this.loadDatePicker();
                //this.cargarSegurosSelect();
                //this.cargarServiciosSelect();
                //this.cargarPrimasSelect();
            },
            loadDatePicker: function () {
                $('.datepicker').datetimepicker({
                    format: 'DD/MM/YYYY',
                    showTodayButton: true,
                    defaultDate: moment(),
                    locale: 'es',
                    icons: {
                        time: "fa fa-clock-o",
                        date: "fa fa-calendar",
                        up: "fa fa-chevron-up",
                        down: "fa fa-chevron-down",
                        previous: 'fa fa-chevron-left',
                        next: 'fa fa-chevron-right',
                        today: 'fa fa-screenshot',
                        clear: 'fa fa-trash',
                        close: 'fa fa-remove'
                    }
                });
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
            cargarPrimasSelect: function (primaSelect) {
                var $thisObject = this;
                $.when(this.loadPrimas_SegurosAjax()).then(function (data, textStatus, jqXHR) {
                    $(primaSelect).empty();
                    $.each(data, function (i, item) {
                        $(primaSelect).append($('<option>')
                            .attr('value', item.Id_Tipo)
                            .attr('data-valor', item.Valor_Prima_Individual)
                            .html(item.Nombre_Tipo));
                    });

                    //Volvemos a recalcular el contrato
                    $thisObject.calcularValorContrato();
                    //Cuando cambie el select recalculamos
                    $thisObject.addEventCalculoContrato(primaSelect);

                });
            },
            cargarSeguro_PrimaSelect: function (seguroSelect, primaSelect)
            {
                var $thisObject = this;
                $.when(this.loadSeguroAjax(), this.loadPrimas_SegurosAjax()).then(function (dataSeguro, dataPrima) {

                    var seguros = dataSeguro[0];
                    $(seguroSelect).empty();
                    $.each(seguros, function (i, item) {
                        $(seguroSelect).append($('<option>')
                            .attr('value', item.Id)
                            .html(item.Nombre));
                    });
                    //Cuando cambie el select recalculamos
                    $thisObject.addEventCalculoContrato(seguroSelect);

                    var primas = dataPrima[0];
                    $(primaSelect).empty();
                    $.each(primas, function (i, item) {
                        $(primaSelect).append($('<option>')
                            .attr('value', item.Id_Tipo)
                            .attr('data-valor', item.Valor_Prima_Individual)
                            .html(item.Nombre_Tipo));
                    });
                    //Cuando cambie el select recalculamos
                    $thisObject.addEventCalculoContrato(primaSelect);

                    //Volvemos a recalcular el contrato
                    $thisObject.calcularValorContrato();
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
            addEventOnlyNumberMin: function (e)
            {
                $(e).change(function () {
                    var min = parseInt($(this).attr('min'));
                    if ($(this).val() < min) 
                        $(this).val(min);

                    if (!parseInt($(this).val())) 
                        $(this).val(min);
                });
            },
            addEventUploadFile: function (e)
            {
                $(e).change(function () {
                    var formData = new FormData();
                    formData.append('file', $(this)[0].files[0]);
                    $.ajax({
                        type: 'post',
                        url: '/contrato/subir-archivo-temp',
                        data: formData,
                        success: function (response) {
                            if (response !== '') {
                                console.log('El archivo temporal ' + response + 'se ha subido');
                                $(e).attr('data-temp', response);
                            }
                        },
                        processData: false,
                        contentType: false,
                        error: function () {
                            console.log("Error al guardar el archivo temporal!");
                        }
                    });
                });
            },
            addEventButtonDelete: function (button)
            {
                $(button).click(function () {
                    $(button).parent().parent().remove();
                });
            },
            addEventButtonDelWithCalcularContrato: function (button)
            {
                var $thisObject = this;
                $(button).click(function () {
                    $(button).parent().parent().remove();
                    $thisObject.calcularValorContrato();
                });
            },
            calcularValorContrato: function ()
            {
                var totalContrato = 0;
                totalContrato += this.calcularTotalServicios();
                totalContrato += this.calcularTotalDestinos();
                totalContrato += this.calcularTotalSeguros();
                $('#valor-contrato').val(totalContrato);
            },
            calcularTotalSeguros: function ()
            {
                var salida = 0;
                var $thisObject = this;
                $.each($('.div-seguros'), function (i, item) {
                    var select = $(item).find('.tipo-seguro').first();
                    var dias = parseInt($(item).find('.input-dias').first().val()) || 1;
                    var totalPorSeguro = $thisObject.calcularTotalSegurosSingle(select, dias);
                    $(item).find('.total-seguro').first().val(totalPorSeguro);
                    salida += totalPorSeguro;
                });

                return salida;
            },
            calcularTotalSegurosSingle: function (selectPrima, dias)
            {
                var salida = 0;
                var diasFinal = parseInt(dias) || 1;
                var valorPrima = parseInt($(selectPrima).children("option:selected").data('valor')) || 1;
                salida = valorPrima * diasFinal;
                return salida;
            },
            calcularTotalServicios: function ()
            {
                var salida = 0;
                $.each($('.div-servicios'), function (i, item)
                {
                    var select = $(item).find('.servicios-contrato').first();
                    var value = parseInt($(select).children("option:selected").data('valor')) || 1;
                    salida += value;
                });

                return salida;
            },
            calcularTotalDestinos: function () {
                var salida = 0;
                $.each($('.div-destinos'), function (i, item) {
                    var select = $(item).find('.destinos-contrato').first();
                    var value = parseInt($(select).children("option:selected").data('valor')) || 1;
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
                this.addEventButtonDelWithCalcularContrato($botonCerrar);
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
                this.addEventButtonDelWithCalcularContrato($botonCerrar);
                var $col1 = $('<div>').addClass('col-sm-1').append($botonCerrar);
                $col1.appendTo($row);

                $row.appendTo($contenedorDestinos);
            },
            addDocumento: function ()
            {
                var $contenedorDocumentos = $('#contenedor-documentos');

                var $row = $('<div>').addClass('row div-documentos');
                var $inputFile = $('<input type="file">').addClass('form-control input-archivo').css({ position: 'initial', opacity: 'initial' });
                this.addEventUploadFile($inputFile);
                var $formGroup = $('<div>').addClass('form-group').append($inputFile);
                var $col10 = $('<div>').addClass('col-sm-10').append($formGroup);
                $col10.appendTo($row);

                var $icono = $('<i>').addClass('material-icons').html('close');
                var $botonCerrar = $('<button type="button">').addClass('btn btn-outline-danger btn-round btn-sm btn-fab').append($icono);
                this.addEventButtonDelete($botonCerrar);
                var $col1 = $('<div>').addClass('col-sm-1').append($botonCerrar);
                $col1.appendTo($row);

                $row.appendTo($contenedorDocumentos);
            },
            addSeguro: function () {

                var $contenedorSeguros = $('#contenedor-seguros');
                var $row = $('<div>').addClass('row div-seguros');

                var $selectPrimaSeguro = $('<select>').addClass('form-control tipo-seguro');
                var $selectSeguro = $('<select>').addClass('form-control seguros-contrato');
                this.cargarPrimasSelect($selectPrimaSeguro);
                this.cargarSegurosSelect($selectSeguro);
                //para que al cambiar saquemos el total del contrato...
                this.addEventCalculoContrato($selectPrimaSeguro);
                

                //this.cargarSegurosSelect($selectSeguro);
                var $formGroupSeguro = $('<div>').addClass('form-group').append($selectSeguro);
                var $colSeguro = $('<div>').addClass('col-sm-3').append($formGroupSeguro);
                $colSeguro.appendTo($row);

                
                //this.cargarPrimasSelect($selectPrimaSeguro);
                var $formGroupPrimas = $('<div>').addClass('form-group').append($selectPrimaSeguro);
                var $colPrima = $('<div>').addClass('col-sm-4').append($formGroupPrimas);
                $colPrima.appendTo($row);

                var $inputDias = $('<input type="number" min="1">').addClass('form-control input-dias').val(1);
                this.addEventOnlyNumberMin($inputDias);
                this.addEventCalculoContrato($inputDias);

                var $formGroupDias = $('<div>').addClass('form-group').append($inputDias);
                var $colDias = $('<div>').addClass('col-sm-2').append($formGroupDias);
                $colDias.appendTo($row);

                var $inputValor = $('<input type="text">').addClass('form-control disabled total-seguro');
                var $formGroupValor = $('<div>').addClass('form-group').append($inputValor);
                var $colValor = $('<div>').addClass('col-sm-2').append($formGroupValor);
                $colValor.appendTo($row);

                var $icono = $('<i>').addClass('material-icons').html('close');
                var $botonCerrar = $('<button type="button">').addClass('btn btn-outline-danger btn-round btn-sm btn-fab').append($icono);
                this.addEventButtonDelWithCalcularContrato($botonCerrar);
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
            obtenerListaSeguros: function () {
                var array = new Array();
                var $thisObject = this;
                $.each($('.div-seguros'), function (i, item) {
                    var selectSeguro = $(item).find('.seguros-contrato').first();
                    var selectPrima = $(item).find('.tipo-seguro').first();
                    var dias = parseInt($(item).find('.input-dias').first().val()) || 1;
                    var total = $thisObject.calcularTotalSegurosSingle(selectPrima, dias);
                    var elemento = {
                        Valor: total, Total_Dias: dias, Tipo_Seguro: parseInt($(selectPrima).val()),
                        Seguro: { Id: parseInt($(selectSeguro).val()) }
                    };
                    array.push(elemento);
                });
                return array;
            },
            obtenerListaArchivos: function ()
            {
                var array = new Array();
                $.each($('.div-documentos'), function (i, item) {
                    var inputArchivo = $(item).find('.input-archivo').first();
                    if ($(inputArchivo).data('temp')) {
                        var archivo = { Nombre: $(inputArchivo).data('temp') };
                        array.push(archivo);
                    }
                });
                return array;
            },
            obtenerContrato: function () {
                var contrato = {
                    Nombre: $('#nombre-contrato').val(),
                    Descripcion: $('#descripcion-contrato').val(),
                    Fecha_Viaje: $('#fecha-viaje').data('DateTimePicker').date()._d,
                    Valor: $('#valor-contrato').val(),
                    Curso: this.obtenerCurso(),
                    ListaServiciosAsociados: this.obtenerListaServicios(),
                    ListaDestinosAsociados: this.obtenerListaDestinos(),
                    ListaSeguroAsociados: this.obtenerListaSeguros(),
                    ListaArchivos : this.obtenerListaArchivos()
                };
                return contrato;
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

                var contrato = this.obtenerContrato();

                if (!$this.validarObjeto(contrato))
                    return $this.normalTextEnFinalizar();

                $.when($this.guardaContratoAjax(contrato)).then(function (data, textStatus, jqXHR) {
                    var alerta = data;
                    if (data === true) alerta = 'Se ha guardado el contrato existosamente';
                    if (data === false) alerta = 'Error al guardar los datos del contrato';
                    $this.generarAlerta(alerta);
                    return $this.normalTextEnFinalizar();
                });

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
