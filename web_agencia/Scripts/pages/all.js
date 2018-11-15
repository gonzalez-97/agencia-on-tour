(function ($, d, w) {
    'use strict';
    w.thisPage = function () {
        return {
            init: function () {

            },
            closeSesion: function ()
            {
                $("#logout-link").click(function (e) {
                    e.preventDefault();
                    $.post("/cerrar-sesion", function (res) {
                        if (res.status === "done") {
                            //close the window now.
                            window.location.href = "";
                        }
                    });
                });
            }
        };
    };
})(jQuery, document, window);