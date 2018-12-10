define([], function ()
{
    return {
        closeSesion: function () {
            $("#logout-link").click(function (e) {
                e.preventDefault();
                $.post("/cerrar-sesion", function (res) {
                    if (res.status === "done") {
                        //close the window now.
                        window.location.href = "";
                    }
                });
            });
        },
        generarAlerta: function (message)
        {
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
        }
    }
});