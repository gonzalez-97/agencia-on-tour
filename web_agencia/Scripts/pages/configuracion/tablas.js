$(document).ready(function () {

    var StaticContent =  "<a href='#' class='btn btn-link btn-info btn-just-icon like'><i class='material-icons'>favorite</i></a>";
    StaticContent += "<a href='#' class='btn btn-link btn-warning btn-just-icon edit'><i class='material-icons'>dvr</i></a>";
    StaticContent += "<a href='#' class='btn btn-link btn-danger btn-just-icon remove'><i class='material-icons'>close</i></a>";

    $('#datatables').DataTable({
        pagingType: "full_numbers",
        language: { url: "//localhost:62066/Scripts/Spanish.json" },
        ajax: {
            dataType: 'jsonp',
            url: "http://localhost:62478/api/sys_tabla"
        },
        columns: [
            { "data": "Id_Tabla" },
            { "data": "Id_Usuario_Creador" },
            { "data": "Nombre_Tabla" },
            { "data": "Nombre_Vista" },
            {
                "data": null,
                "defaultContent": StaticContent,
                "className": "disabled-sorting text-right"
            }
        ],
        lengthMenu: [
            [10, 25, 50, -1],
            [10, 25, 50, "All"]
        ],
        ordering : true,
        columnDefs: [{
            orderable: false,
            targets: "disabled-sorting"
        }],
        responsive: true
    });

    var table = $('#datatable').DataTable();

    // Edit record
    table.on('click', '.edit', function () {
        $tr = $(this).closest('tr');
        var data = table.row($tr).data();
        alert('You press on Row: ' + data[0] + ' ' + data[1] + ' ' + data[2] + '\'s row.');
    });

    // Delete a record
    table.on('click', '.remove', function (e) {
        $tr = $(this).closest('tr');
        table.row($tr).remove().draw();
        e.preventDefault();
    });

    //Like record
    table.on('click', '.like', function () {
        alert('You clicked on Like button');
    });

    $('#button-create-table').click(function () {

        var $spanload = $('<span>').addClass('btn-label')
            .append($('<i>').addClass('fas fa-circle-notch fa-spin'))
            .append(" Validando")
            .append($('<div>').addClass('ripple-container'));


        $('#button-create-table').data('original-html', $('#button-create-table').html());
        $('#button-create-table').html($spanload);

        //Si es valido se valida el campo primario de la tabla...
        if ($('#new-table').valid())
            ValidarCrearTabla();
    });

    setFormValidation('#new-table');

});

function setFormValidation(id) {
    $(id).validate({
        highlight: function (element) {
            $(element).closest('.form-group').removeClass('has-success').addClass('has-danger');
            $(element).closest('.form-check').removeClass('has-success').addClass('has-danger');
        },
        success: function (element) {
            $(element).closest('.form-group').removeClass('has-danger').addClass('has-success');
            $(element).closest('.form-check').removeClass('has-danger').addClass('has-success');
        },
        errorPlacement: function (error, element) {
            $(element).append(error);
        }
    });
}

function ValidarCrearTabla()
{
    $.when(ValidarCampoPrimario($('#column-primary')), ValidarNombreTabla($('#table-sql'))).done(function (data1, data2) {
        // a1 and a2 are arguments resolved for the page1 and page2 ajax requests, respectively.
        // Each argument is an array with the following structure: [ data, statusText, jqXHR ]

        if (data1 == $('#column-primary').val() && data2 == $('#table-sql').val())
        {
            EnviarFormularioCrearTabla();
        }
        else
        {
            $('#button-create-table').html($('#button-create-table').data('original-html'));
        }
    });
}

function ValidarCampoPrimario(input_campo)
{
    return $.ajax({
        type: "GET",
        crossDomain: true,
        dataType: 'jsonp',
        url: "http://localhost:62478/api/Sys_Campo/sugerido", // script to validate in server side
        data: { nombre_campo: $(input_campo).val() },
        success: function (data) {
            if (data !== $(input_campo).val())
            {
                $('label[for="column-primary"]').html('Nombre ya utilizado sugerencia: ' + data);
                $(input_campo).closest('.form-group').removeClass('has-success').addClass('has-danger');
            }
        }
    });
}

function ValidarNombreTabla(input_tabla)
{
    return $.ajax({
        type: "GET",
        url: "http://localhost:62478/api/Sys_Tabla/sugerido", // script to validate in server side
        data: { nombre_tabla: $(input_tabla).val() },
        success: function (data) {
            if (data !== $(input_tabla).val())
            {
                $('label[for="table-sql"]').html('Nombre ya utilizado sugerencia: ' + data);
                $(input_tabla).closest('.form-group').removeClass('has-success').addClass('has-danger');
            }
        }
    });
}

function EnviarFormularioCrearTabla()
{
    $('#button-create-table').html($('#button-create-table').data('original-html'));
    $('#new-table').get(0).submit();
}
