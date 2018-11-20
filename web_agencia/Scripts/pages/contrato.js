define([], function () {
    return {
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
        loadTiposSegurosAjax: function () {
            return $.get("/tipos-seguros/all");
        },
        loadSegurosAjax: function () {
            return $.get("/seguros/all");
        },
        loadDestinosAjax: function () {
            return $.get("/destinos/all");
        },
        loadUF_MiIndicador: function () {
            return $.get('https://mindicador.cl/api');
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
        cargarTipoSegurosSelect: function (tipoSelect) {
            $.when(this.loadTiposSegurosAjax()).then(function (data, textStatus, jqXHR) {
                $(tipoSelect).empty();
                $.each(data, function (i, item) {
                    $(setipoSelectguroSelect).append($('<option>')
                        .attr('value', item.Tipo_Aseguradora)
                        .html(item.Nombre));
                });
            });
        },
        cargarSegurosSelect: function (seguroSelect) {
            var $thisObject = this;
            $.when(this.loadSegurosAjax()).then(function (data, textStatus, jqXHR) {
                $(seguroSelect).empty();
                $.each(data, function (i, item) {
                    $(seguroSelect).append($('<option>')
                        .attr('value', item.Id)
                        .attr('data-valor', item.Valor_Uf)
                        .html(item.Nombre_Tipo));
                });

                //Volvemos a recalcular el contrato
                $thisObject.calcularValorContrato();
                //Cuando cambie el select recalculamos
                $thisObject.addEventCalculoContrato(seguroSelect);

            });
        },
        cargarTipo_SeguroSelect: function (tipoSelect, seguroSelect, inputValorPesos) {
            var $thisObject = this;
            $.when(this.loadTiposSegurosAjax(), this.loadSegurosAjax(), this.loadUF_MiIndicador()).then(function (dataTipo, dataSeguro, dataUf) {

                var tipos = dataTipo[0];
                $(tipoSelect).empty();
                $.each(tipos, function (i, item) {
                    $(tipoSelect).append($('<option>')
                        .attr('value', item.Id)
                        .attr('data-tipo-seguro', item.Tipo_Aseguradora)
                        .html(item.Nombre));
                });

                var tipo_defaut = parseInt($(tipoSelect).children("option:selected").data('tipo-seguro'));

                //Se muestran los seguros segun el tipo...
                var seguros_filtrados = dataSeguro[0].filter(function (seg) {
                    return seg.Id_Tipo_Seguro === tipo_defaut;
                });

                //valor uf hoy...
                var valor_uf = dataUf[0].uf.valor;

                $(seguroSelect).empty();
                $.each(seguros_filtrados, function (i, item) {
                    $(seguroSelect).append($('<option>')
                        .attr('value', item.Id)
                        .attr('data-valor', Math.round(item.Valor_Uf * valor_uf))
                        .html(item.Nombre));
                });

                //Volvemos a recalcular el contrato
                $thisObject.calcularValorContrato();

                $(inputValorPesos).val($thisObject.calcularTotalSegurosSingle(seguroSelect, 1));

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
        addEventCalculoContrato: function (e) {
            var $this = this;
            $(e).change(function () {
                $this.calcularValorContrato();
            });
        },
        addEventChangeTipoSeguro: function (selectTipo, selectSeguro, inputValorPesos)
        {
            var $this = this;
            $(selectTipo).change(function () {
                var valorSelect = parseInt($(selectTipo).children("option:selected").data('tipo-seguro'));
                $.when($this.loadSegurosAjax(), $this.loadUF_MiIndicador()).then(function (dataSeguro, dataUf) {

                    //Se muestran los seguros segun el tipo...
                    var seguros_filtrados = dataSeguro[0].filter(function (seg) {
                        return seg.Id_Tipo_Seguro === valorSelect;
                    });

                    //valor uf hoy...
                    var valor_uf = dataUf[0].uf.valor;

                    $(selectSeguro).empty();
                    $.each(seguros_filtrados, function (i, item) {
                        $(selectSeguro).append($('<option>')
                            .attr('value', item.Id)
                            .attr('data-valor', Math.round(item.Valor_Uf * valor_uf))
                            .html(item.Nombre));
                    });

                    $(inputValorPesos).val($this.calcularTotalSegurosSingle(selectSeguro, 1));

                    //Volvemos a recalcular el contrato
                    $this.calcularValorContrato();
                });

            });
        },
        addEventChangeSeguro: function (selectSeguro, inputValorPesos)
        {
            var $this = this;
            $(selectSeguro).change(function () {
                //Cambiamos el valor del total por seguro...
                $(inputValorPesos).val($this.calcularTotalSegurosSingle(this, 1));
                //Volvemos a recalcular el contrato
                $this.calcularValorContrato();
            });
        },
        addEventOnlyNumberMin: function (e) {
            $(e).change(function () {
                var min = parseInt($(this).attr('min'));
                if ($(this).val() < min)
                    $(this).val(min);

                if (!parseInt($(this).val()))
                    $(this).val(min);
            });
        },
        addEventUploadFile: function (e) {
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
        addEventButtonDelete: function (button) {
            $(button).click(function () {
                $(button).parent().parent().remove();
            });
        },
        addEventButtonDelWithCalcularContrato: function (button) {
            var $thisObject = this;
            $(button).click(function () {
                $(button).parent().parent().remove();
                $thisObject.calcularValorContrato();
            });
        },
        calcularValorContrato: function () {
            var totalContrato = 0;
            totalContrato += this.calcularTotalServicios();
            totalContrato += this.calcularTotalDestinos();
            totalContrato += this.calcularTotalSeguros();
            $('#valor-contrato').val(totalContrato);
        },
        calcularTotalSeguros: function () {
            var salida = 0;
            var $thisObject = this;
            $.each($('.div-seguros'), function (i, item) {
                var select = $(item).find('.seguros-contrato').first();
                var dias = parseInt($(item).find('.input-dias').first().val()) || 1;
                var totalPorSeguro = $thisObject.calcularTotalSegurosSingle(select, dias);
                $(item).find('.total-seguro').first().val(totalPorSeguro);
                salida += totalPorSeguro;
            });

            return salida;
        },
        calcularTotalSegurosSingle: function (selectPrima, dias) {
            var salida = 0;
            var diasFinal = parseInt(dias) || 1;
            var valorPrima = parseInt($(selectPrima).children("option:selected").data('valor')) || 1;
            salida = valorPrima * diasFinal;
            return salida;
        },
        calcularTotalServicios: function () {
            var salida = 0;
            $.each($('.div-servicios'), function (i, item) {
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
        addServicio: function () {
            var $contenedorServicios = $('#contenedor-servicios');

            var $row = $('<div>').addClass('row div-servicios');
            var $selectServicio = $('<select>').addClass('form-control servicios-contrato');
            //mascara
            $selectServicio.append($('<option>').html('Cargando...'));
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
            //mascara
            $selectDestino.append($('<option>').html('Cargando...'));
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
        addDocumento: function () {
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

            var $selectTipoSeguro = $('<select>').addClass('form-control tipo-seguro');

            $selectTipoSeguro.append($('<option>').html('Cargando...'));
            var $selectSeguro = $('<select>').addClass('form-control seguros-contrato');
            $selectSeguro.append($('<option>').html('Cargando...'));
            var $inputValor = $('<input type="text">').addClass('form-control disabled total-seguro');

            this.cargarTipo_SeguroSelect($selectTipoSeguro, $selectSeguro, $inputValor);
            this.addEventChangeTipoSeguro($selectTipoSeguro, $selectSeguro, $inputValor);
            this.addEventChangeSeguro($selectSeguro, $inputValor);

            //this.cargarSegurosSelect($selectSeguro);
            var $formGroupTipo = $('<div>').addClass('form-group').append($selectTipoSeguro);
            var $colTipo = $('<div>').addClass('col-sm-3').append($formGroupTipo);
            $colTipo.appendTo($row);


            //this.cargarPrimasSelect($selectPrimaSeguro);
            var $formGroupSeguro = $('<div>').addClass('form-group').append($selectSeguro);
            var $colSeguro = $('<div>').addClass('col-sm-4').append($formGroupSeguro);
            $colSeguro.appendTo($row);

            var $inputDias = $('<input type="number" min="1">').addClass('form-control input-dias').val(1);
            this.addEventOnlyNumberMin($inputDias);
            this.addEventCalculoContrato($inputDias);

            var $formGroupDias = $('<div>').addClass('form-group').append($inputDias);
            var $colDias = $('<div>').addClass('col-sm-2').append($formGroupDias);
            $colDias.appendTo($row);

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
        obtenerListaServicios: function () {
            var array = new Array();
            $.each($('.div-servicios'), function (i, item) {
                var select = $(item).find('.servicios-contrato').first();
                var elemento = { Servicio: { Id: parseInt($(select).val()) } };
                array.push(elemento);
            });
            return array;
        },
        obtenerListaDestinos: function () {
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
                var selectTipo = $(item).find('.tipo-seguro').first();
                var selectSeguro = $(item).find('.seguros-contrato').first();
                var dias = parseInt($(item).find('.input-dias').first().val()) || 1;
                var total = $thisObject.calcularTotalSegurosSingle(selectSeguro, dias);
                var elemento = {
                    Valor: total, Total_Dias: dias, Tipo_Seguro: { Id: parseInt($(selectTipo).val()) } ,
                    Seguro: parseInt($(selectSeguro).val()) 
                };
                array.push(elemento);
            });
            return array;
        },
        obtenerListaArchivos: function () {
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
                ListaArchivos: this.obtenerListaArchivos()
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

                if (data === true)
                    return $this.reloadPageAfter3sec();

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
        reloadPageAfter3sec: function () {
            $('#registrar-input').html('Contrato guardado!');
            setTimeout(function () {
                location.reload();
            }, 3000);
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
});
