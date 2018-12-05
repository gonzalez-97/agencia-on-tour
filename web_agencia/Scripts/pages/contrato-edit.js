require(["all", "contrato"], function (all, contrato) {

    var editar =
    {
        init: function ()
        {
            all.closeSesion();
            contrato.loadDatePicker();
            this.cargarContrato(this.getIdContratoFromURL());

            $('#add-servicio').click(function (e) {
                contrato.addServicio();
            });

            $('#add-seguro').click(function (e) {
                contrato.addSeguro();
            });

            $('#add-destino').click(function (e) {
                contrato.addDestino();
            });

            $('#add-documento').click(function (e) {
                contrato.addDocumento();
            });

            $('#registrar-input').click(function (e) {
                editar.botonFinalizar();
            });

        },
        cargarContrato: function (id) {
            var contrato = { Id: parseInt(this.getIdContratoFromURL()) };
            var $this = this;
            $.when(this.loadContratoAjax(contrato)).then(function (json, textStatus, jqXHR) {

                console.log(json);
                //Id Contrato
                $('#id-contrato').val(json.Id);
                //Nombre...
                $('#nombre-contrato').val(json.Nombre);
                $('#curso-alumno').empty();
                //Curso...
                $('#curso-alumno').append($('<option>').attr('value', json.Curso.Id).html(json.Curso.Nombre));
                //Descripcion...
                $('#descripcion-contrato').val(json.Descripcion);
                //Fecha ...
                var fecha_viaje = new Date(json.Fecha_Viaje.match(/\d+/)[0] * 1);
                $('#fecha-viaje').data("DateTimePicker").date(fecha_viaje);

                //Valor...
                $('#valor-contrato').val(json.Valor);

                //Seguros...
                $.each(json.ListaSeguroAsociados, function (i, item) {
                    $this.addOldSeguro(item);
                });

                //Servicios...
                $.each(json.ListaServiciosAsociados, function (i, item) {
                    $this.addOldServicio(item);
                });

                //Destinos...
                $.each(json.ListaDestinosAsociados, function (i, item) {
                    $this.addOldDestino(item);
                });

                //Archivos...
                $.each(json.ListaArchivos, function (i, item) {
                    $this.addOldDocumento(json.Id,item);
                })

            });
        },
        addOldSeguro: function (seguro_asignado)
        {
            var $contenedorSeguros = $('#contenedor-seguros');
            var $row = $('<div>').addClass('row div-seguros').attr('data-id', seguro_asignado.Id);

            var $selectTipoSeguro = $('<select>').addClass('form-control tipo-seguro');
            $selectTipoSeguro.append($('<option>').html('Cargando...'));
            var $selectSeguro = $('<select>').addClass('form-control seguros-contrato');
            $selectSeguro.append($('<option>').html('Cargando...'));
            var $inputValor = $('<input type="text">').addClass('form-control disabled total-seguro');

            $.when(contrato.loadTiposSegurosAjax(), contrato.loadSegurosAjax(), contrato.loadUF_MiIndicador()).then(function (dataTipo, dataSeguro, dataUf) {

                var tipos = dataTipo[0];
                $selectTipoSeguro.empty();
                $.each(tipos, function (i, item) {
                    $selectTipoSeguro.append($('<option>')
                        .attr('value', item.Id)
                        .attr('data-tipo-seguro', item.Tipo_Aseguradora)
                        .html(item.Nombre));
                });

                $selectTipoSeguro.val(seguro_asignado.Tipo_Seguro.Id);


                //Se muestran los seguros segun el tipo...
                var seguros_filtrados = dataSeguro[0].filter(function (seg) {
                    return seg.Id_Tipo_Seguro === seguro_asignado.Tipo_Seguro.Tipo_Aseguradora;
                });

                //valor uf hoy...
                var valor_uf = dataUf[0].uf.valor;

                $selectSeguro.empty();
                $.each(seguros_filtrados, function (i, item) {
                    $selectSeguro.append($('<option>')
                        .attr('value', item.Id)
                        .attr('data-valor', Math.round(item.Valor_Uf * valor_uf))
                        .html(item.Nombre));
                });

                $selectSeguro.val(seguro_asignado.Seguro);

                $inputValor.val(seguro_asignado.Valor);
            });

            contrato.addEventChangeTipoSeguro($selectTipoSeguro, $selectSeguro, $inputValor);
            contrato.addEventChangeSeguro($selectSeguro, $inputValor);

            //this.cargarSegurosSelect($selectSeguro);
            var $formGroupTipo = $('<div>').addClass('form-group').append($selectTipoSeguro);
            var $colTipo = $('<div>').addClass('col-sm-3').append($formGroupTipo);
            $colTipo.appendTo($row);


            //this.cargarPrimasSelect($selectPrimaSeguro);
            var $formGroupSeguro = $('<div>').addClass('form-group').append($selectSeguro);
            var $colSeguro = $('<div>').addClass('col-sm-4').append($formGroupSeguro);
            $colSeguro.appendTo($row);

            var $inputDias = $('<input type="number" min="1">').addClass('form-control input-dias').val(seguro_asignado.Total_Dias);
            contrato.addEventOnlyNumberMin($inputDias);
            contrato.addEventCalculoContrato($inputDias);

            var $formGroupDias = $('<div>').addClass('form-group').append($inputDias);
            var $colDias = $('<div>').addClass('col-sm-2').append($formGroupDias);
            $colDias.appendTo($row);

            var $formGroupValor = $('<div>').addClass('form-group').append($inputValor);
            var $colValor = $('<div>').addClass('col-sm-2').append($formGroupValor);
            $colValor.appendTo($row);

            var $icono = $('<i>').addClass('material-icons').html('close');
            var $botonCerrar = $('<button type="button">').addClass('btn btn-outline-danger btn-round btn-sm btn-fab').append($icono);
            contrato.addEventButtonDelWithCalcularContrato($botonCerrar);
            var $colCerrar = $('<div>').addClass('col-sm-1').append($botonCerrar);
            $colCerrar.appendTo($row);

            $row.appendTo($contenedorSeguros);
        },
        addOldServicio: function (servicio_asignado)
        {
            var $contenedorServicios = $('#contenedor-servicios');

            var $row = $('<div>').addClass('row div-servicios').attr('data-id', servicio_asignado.Id);
            var $selectServicio = $('<select>').addClass('form-control servicios-contrato');

            $.when(contrato.loadServiciosAjax()).then(function (data, textStatus, jqXHR) {
                $selectServicio.empty();
                $.each(data, function (i, item) {
                    $selectServicio.append($('<option>')
                        .attr('value', item.Id)
                        .attr('data-valor', item.Valor)
                        .html(item.Nombre));
                });
                $selectServicio.val(servicio_asignado.Servicio.Id);
                //contrato.calcularValorContrato();
                contrato.addEventCalculoContrato($selectServicio);
            });

            var $formGroup = $('<div>').addClass('form-group').append($selectServicio);
            var $col10 = $('<div>').addClass('col-sm-10').append($formGroup);
            $col10.appendTo($row);

            var $icono = $('<i>').addClass('material-icons').html('close');
            var $botonCerrar = $('<button type="button">').addClass('btn btn-outline-danger btn-round btn-sm btn-fab').append($icono);
            contrato.addEventButtonDelWithCalcularContrato($botonCerrar);
            var $col1 = $('<div>').addClass('col-sm-1').append($botonCerrar);
            $col1.appendTo($row);

            $row.appendTo($contenedorServicios);
        },
        addOldDestino: function (destino_asignado) {
            var $contenedorDestinos = $('#contenedor-destinos');

            var $row = $('<div>').addClass('row div-destinos').attr('data-id', destino_asignado.Id);
            var $selectDestino = $('<select>').addClass('form-control destinos-contrato');

            $.when(contrato.loadDestinosAjax()).then(function (data, textStatus, jqXHR) {
                $selectDestino.empty();
                $.each(data, function (i, item) {
                    $selectDestino.append($('<option>')
                        .attr('value', item.Id)
                        .attr('data-valor', item.Valor)
                        .html(item.Nombre));
                });
                $selectDestino.val(destino_asignado.Destino.Id);
                //contrato.calcularValorContrato();
                contrato.addEventCalculoContrato($selectDestino);
            });

            var $formGroup = $('<div>').addClass('form-group').append($selectDestino);
            var $col10 = $('<div>').addClass('col-sm-10').append($formGroup);
            $col10.appendTo($row);

            var $icono = $('<i>').addClass('material-icons').html('close');
            var $botonCerrar = $('<button type="button">').addClass('btn btn-outline-danger btn-round btn-sm btn-fab').append($icono);
            contrato.addEventButtonDelWithCalcularContrato($botonCerrar);
            var $col1 = $('<div>').addClass('col-sm-1').append($botonCerrar);
            $col1.appendTo($row);

            $row.appendTo($contenedorDestinos);
        },
        addOldDocumento: function (IdContrato, documento)
        {
            var $contenedorDocumentos = $('#contenedor-documentos');

            var $row = $('<div>').addClass('row div-documentos').attr('data-id', documento.Id);
            var $inputFile = $('<a download >').addClass('btn btn-primary btn-round btn-sm input-archivo')
                .attr('data-temp', documento.Nombre)
                .attr('href', '/Content/contrato/' + IdContrato + "/" + documento.Nombre)
                .html('<i class="fas fa-file-download fa-fw"></i>Descargar Documento');

            var $formGroup = $('<div>').addClass('form-group').append($inputFile);
            var $col10 = $('<div>').addClass('col-sm-10').append($formGroup);
            $col10.appendTo($row);

            var $icono = $('<i>').addClass('material-icons').html('close');
            var $botonCerrar = $('<button type="button">').addClass('btn btn-outline-danger btn-round btn-sm btn-fab').append($icono);
            contrato.addEventButtonDelete($botonCerrar);
            var $col1 = $('<div>').addClass('col-sm-1').append($botonCerrar);
            $col1.appendTo($row);

            $row.appendTo($contenedorDocumentos);
        },
        getIdContratoFromURL: function ()
        {
                var sPageURL = window.location.href;
                var indexOfLastSlash = sPageURL.lastIndexOf("/");

                if (indexOfLastSlash > 0 && sPageURL.length - 1 != indexOfLastSlash)
                    return sPageURL.substring(indexOfLastSlash + 1);
                else
                    return 0;
        },
        loadContratoAjax: function (contrato) {
            return $.ajax({
                url: "/contrato/cargar-ajax",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(contrato)
            });
        },
        editaContratoAjax: function (contrato) {
            return $.ajax({
                url: "/contrato/editar-ajax",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(contrato)
            });
        },
        botonFinalizar: function ()
        {
            contrato.cargandoEnFinalizar();

            var contratoEdit = contrato.obtenerContrato();
            //Se añade el Id del contrato en modo edicion...
            contratoEdit["Id"] = parseInt($('#id-contrato').val());

            console.log(contratoEdit);

            if (!contrato.validarObjeto(contratoEdit))
                return contrato.normalTextEnFinalizar();

            $.when(this.editaContratoAjax(contratoEdit)).then(function (data, textStatus, jqXHR) {
                var alerta = data;
                if (data === true) alerta = 'Se ha actualizado el contrato existosamente';
                if (data === false) alerta = 'Error al actualizar los datos del contrato';
                contrato.generarAlerta(alerta);

                //if (data === true) return contrato.reloadPageAfter3sec();

                return contrato.normalTextEnFinalizar();
            });

        },
    };


    editar.init();

});