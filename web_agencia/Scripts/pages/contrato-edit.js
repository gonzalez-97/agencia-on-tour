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
                contrato.botonFinalizar();
            });

        },
        cargarContrato: function (id) {
            var contrato = { Id: parseInt(this.getIdContratoFromURL()) };
            var $this = this;
            $.when(this.loadContratoAjax(contrato)).then(function (json, textStatus, jqXHR) {
                //Nombre...
                console.log(json);
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

                //Servicios...
                $.each(json.ListaServiciosAsociados, function (i, item) {
                    $this.addOldServicio(item);
                });

                //Destinos...
                $.each(json.ListaDestinosAsociados, function (i, item) {
                    $this.addOldDestino(item);
                });

            });
        },
        addOldServicio: function (servicio_asignado)
        {
            var $contenedorServicios = $('#contenedor-servicios');

            var $row = $('<div>').addClass('row div-servicios');
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

            var $row = $('<div>').addClass('row div-destinos');
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
        }
    };


    editar.init();

});