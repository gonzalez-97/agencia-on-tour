var onTour = {
    isDebug: false,
    isAspForms: false,
    load: function () {
        console.log("se inicia la carga...");

        if (typeof Sys != 'undefined') {
            onTour.isAspForms = true;
            onToru.isDebug = Sys.Debug.isDebug;
            console.log("Windows forms modo debug...");
        }

        onTour.minValidaciones();
        onTour.minContador();
        onTour.onDebug();
    },
    register: function () {
        onTour.load();
        try {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(onTour.load);
        } catch (e) {
            consolelog("No se ha cargado ajax webforms. [" + e.message + "]");
        }
    },
    logger: {

        log: function (msg) { if (window.console) console.warn(msg); },

        debug: function (msg) { if (onTour.isDebug && window.console) console.debug(msg); }

    },
    base64decode: function (s) { return Base64.decode(s); },

    base64encode: function (s) { return Base64.encode(s); },

    esEntero: function (n) { return +n === parseInt(n); },

    padding: function (value, padding) {
        var str = "" + value;
        var pad = padding;
        return pad.substring(0, pad.length - str.length) + str;
    },

    onDebug: function () {
        if (!onTour.isDebug) return;
        $("body").append(onTour.base64decode("PGRpdiBpZD0ibW9kb0RlYnVnSW5mbyIgc3R5bGU9InotaW5kZXg6OTk5OTk7cG9zaXRpb246Zml4ZWQ7cmlnaHQ6NXB4O2JvdHRvbTo1cHg7b3BhY2l0eTogMC43NTtwYWRkaW5nOiAxNXB4OyAgbWFyZ2luLWJvdHRvbTogMjBweDsgIGJvcmRlcjogMXB4IHNvbGlkIHRyYW5zcGFyZW50OyAgYm9yZGVyLXJhZGl1czogNHB4OyBjb2xvcjogI2E5NDQ0MjsgIGJhY2tncm91bmQtY29sb3I6ICNmMmRlZGU7ICBib3JkZXItY29sb3I6ICNlYmNjZDE7Zm9udC1mYW1pbHk6ICZxdW90O0hlbHZldGljYSBOZXVlJnF1b3Q7LEhlbHZldGljYSxBcmlhbCxzYW5zLXNlcmlmOyI+PHN0cm9uZz5Nb2RvIERlYnVnPC9zdHJvbmc+PC9kaXY+"));
    }
};


onTour.minValidaciones = function () {

    $(".NumerosEnteros").addClass("validarRegex").data("validacion", "[^0-9]+").data("valparam", "g");
    $(".DigitoVerificador").addClass("validarRegex").data("validacion", "[^0-9kK]").data("valparam", "g");
    $(".DigitoVerificadorE").addClass("validarRegex").data("validacion", "[^0-9kKeE]").data("valparam", "g");
    $(".Letras").addClass("validarRegex").data("validacion", "[^a-zA-ZnÑñ áéíóúÁÉÍÓÚ]").data("valparam", "g");
    $(".Letras2").addClass("validarRegex").data("validacion", "[^a-zA-ZnÑñ áéíóúÁÉÍÓÚ0-9]").data("valparam", "g");
    $(".CaracteresValidos").addClass("validarRegex").data("validacion", "[^a-zA-ZnÑñ áéíóúÁÉÍÓÚ'0-9.,]").data("valparam", "g");
    $(".CaracteresCorreoElectronico").addClass("validarRegex").data("validacion", "[^a-zA-ZnÑñ áéíóúÁÉÍÓÚ'0-9_@.,-]").data("valparam", "g");
    $(".CaracteresValidosFecha").addClass("validarRegex").data("validacion", "[^0-9-]").data("valparam", "g");
    $(".CaracteresValidosObservaciones").addClass("validarRegex").data("validacion", "[^a-zA-ZnÑñ áéíóúÁÉÍÓÚ'0-9.,;:()-]").data("valparam", "g");
    $(".ExpresionFechaValida").addClass("validarRegex").data("validacion", "[^0-9-]").data("valparam", "g");
    $(".ExpresionRutValido").addClass("validarRegex").data("validacion", "[^0-9kK.-]").data("valparam", "g");
    $(".ExpresionRutValido2").addClass("validarRegex").data("validacion", "[^0-9kK-]").data("valparam", "g");
    $(".NumerosDecimales").addClass("validarRegex").data("validacion", "[^0-9,.]").data("valparam", "g");
    $(".NumerosGuion").addClass("validarRegex").data("validacion", "[^0-9-]").data("valparam", "g");

    $(".validarRegex").each(function () {
        var ELEMENTO = $(this);
        if (onTour.esEntero(ELEMENTO.attr("maxlength"))) {
            ELEMENTO.data("length", ELEMENTO.attr("maxlength"));
        }

        if (ELEMENTO.is("[maxlength]")) ELEMENTO.removeAttr("maxlength");

    }).bind("drop paste keydown", function (e) {
        var ELEMENTO = $(this);

        if (e.type == "keydown" && (e.keyCode >= 37 && e.keyCode <= 40)) {
            return true;
        }

        setTimeout(function () {

            var RESULTADO = ELEMENTO.val().replace(new RegExp(ELEMENTO.data("validacion"), ELEMENTO.data("valparam")), "");

            if (typeof ELEMENTO.data("length") != "undefined" && Number(ELEMENTO.data("length")) > 0) {
                RESULTADO = RESULTADO.substr(0, ELEMENTO.data("length"));
            }

            ELEMENTO.val(RESULTADO);

            if (ELEMENTO.is(".ValChange")) ELEMENTO.trigger("change");

        }, 0)
    });
};
