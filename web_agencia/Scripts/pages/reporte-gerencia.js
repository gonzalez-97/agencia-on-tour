define([], function () {

    var reporte =
    {
        init: function () {
            this.graficoColegioGeneral();
            this.graficoCursoGeneral();
            this.initGraficoActividades();
        },
        graficoColegioGeneral: function ()
        {
            $.when(this.loadColegioResumenAjax()).then(function (data, textStatus, jqXHR) {
                // Themes begin
                am4core.useTheme(am4themes_animated);
                // Themes end

                // Create chart instance
                var chart = am4core.create("grafico-general-colegio", am4charts.PieChart);

                // Add data
                chart.data = data;

                // Add and configure Series
                var pieSeries = chart.series.push(new am4charts.PieSeries());
                pieSeries.dataFields.value = "Total";
                pieSeries.dataFields.category = "Colegio";
                pieSeries.slices.template.stroke = am4core.color("#fff");
                pieSeries.slices.template.strokeWidth = 2;
                pieSeries.slices.template.strokeOpacity = 1;

                // This creates initial animation
                pieSeries.hiddenState.properties.opacity = 1;
                pieSeries.hiddenState.properties.endAngle = -90;
                pieSeries.hiddenState.properties.startAngle = -90;
            });
        },
        graficoCursoGeneral: function ()
        {
            $.when(this.loadCursoResumenAjax()).then(function (data, textStatus, jqXHR) {
                var cursos = data.map(function (c) {
                    return {
                        Nombre: c.Nombre.trim() + " - " + c.Colegio.Nombre.trim(),
                        Total: c.TotalReunido,
                        Deuda: c.TotalPagar
                    };
                });

                console.log(cursos);

                // Themes begin
                am4core.useTheme(am4themes_animated);
                // Themes end

                // Create chart instance
                var chart = am4core.create("grafico-general-cursos", am4charts.XYChart);

                // Add data
                chart.data = cursos;

                // Create axes
                var categoryAxis = chart.yAxes.push(new am4charts.CategoryAxis());
                categoryAxis.dataFields.category = "Nombre";
                categoryAxis.numberFormatter.numberFormat = "#";
                categoryAxis.renderer.inversed = true;
                categoryAxis.renderer.grid.template.location = 0;
                categoryAxis.renderer.cellStartLocation = 0.1;
                categoryAxis.renderer.cellEndLocation = 0.9;

                var valueAxis = chart.xAxes.push(new am4charts.ValueAxis());
                valueAxis.renderer.opposite = true;

                // Create series
                function createSeries(field, name) {
                    var series = chart.series.push(new am4charts.ColumnSeries());
                    series.dataFields.valueX = field;
                    series.dataFields.categoryY = "Nombre";
                    series.name = name;
                    series.columns.template.tooltipText = "{name}: [bold]{valueX}[/]";
                    series.columns.template.height = am4core.percent(100);
                    series.sequencedInterpolation = true;

                    var valueLabel = series.bullets.push(new am4charts.LabelBullet());
                    valueLabel.label.text = "{valueX}";
                    valueLabel.label.horizontalCenter = "left";
                    valueLabel.label.dx = 10;
                    valueLabel.label.hideOversized = false;
                    valueLabel.label.truncate = false;

                    var categoryLabel = series.bullets.push(new am4charts.LabelBullet());
                    categoryLabel.label.text = "{name}";
                    categoryLabel.label.horizontalCenter = "right";
                    categoryLabel.label.dx = -10;
                    categoryLabel.label.fill = am4core.color("#fff");
                    categoryLabel.label.hideOversized = false;
                    categoryLabel.label.truncate = false;
                }

                createSeries("Deuda", "Deuda");
                createSeries("Total", "Reunido");

            });
        },
        initGraficoActividades: function () {
            $.when(this.loadPagosActividades()).then(function (data, textStatus, jqXHR) {

                var cursos = data.map(function (c) {
                    return {
                        Id: c.Curso.Id,
                        Nombre: c.Curso.Nombre.trim() + " - " + c.Curso.Colegio.Nombre.trim()
                    };
                });

                var cursoUnicos = [...(new Set(cursos.map(function (curso) { return curso.Id })))].map(function (cursoId) {
                    return cursos.find(function (curso) {
                        return curso.Id === cursoId;
                    });
                })


                var filtroCursos = $('#filtro-cursos');
                $(filtroCursos).empty();
                $.each(cursoUnicos, function (e, value) {
                    $(filtroCursos).append($('<option>').attr('value', value.Id).html(value.Nombre));
                });

                var datosFiltrados = data.filter(function (c) {
                    return c.Curso.Id === parseInt($(filtroCursos).val());
                });

                var curso = data.map(function (c) {
                    return c.Curso;
                }).find(function (c) {
                    return c.Id === parseInt($(filtroCursos).val());
                });

                var jsonGraphs = datosFiltrados.map(function (c) {
                    return {
                        Nombre: c.Actividad.Nombre + "(N°" + c.Id + ")",
                        Total: c.Total_Recaudado
                    };
                });

                reporte.graficoActividades(jsonGraphs);

                $(filtroCursos).change(function () {
                    var datosFiltrados = data.filter(function (c) {
                        return c.Curso.Id === parseInt($(filtroCursos).val());
                    });

                    var curso = data.map(function (c) {
                        return c.Curso;
                    }).find(function (c)
                    {
                        return c.Id === parseInt($(filtroCursos).val());
                    });

                    var jsonGraphs = datosFiltrados.map(function (c) {
                        return {
                            Nombre: c.Actividad.Nombre + "(N°" + c.Id + ")",
                            Total: c.Total_Recaudado
                        };
                    });

                    reporte.graficoActividades(jsonGraphs);
                });
                

            });
        },
        graficoActividades: function (json)
        {

            // Themes begin
            am4core.useTheme(am4themes_animated);
            // Themes end

            var chart = am4core.create("grafico-actividades-cursos", am4charts.PieChart);
            chart.hiddenState.properties.opacity = 0; // this creates initial fade-in

            chart.data = json;
            chart.radius = am4core.percent(70);
            chart.innerRadius = am4core.percent(40);
            chart.startAngle = 180;
            chart.endAngle = 360;

            var series = chart.series.push(new am4charts.PieSeries());
            series.dataFields.value = "Total";
            series.dataFields.category = "Nombre";

            series.slices.template.cornerRadius = 10;
            series.slices.template.innerCornerRadius = 7;
            series.slices.template.draggable = true;
            series.slices.template.inert = true;
            series.alignLabels = false;

            series.hiddenState.properties.startAngle = 90;
            series.hiddenState.properties.endAngle = 90;

            chart.legend = new am4charts.Legend();

            console.log(json);
        },
        loadColegioResumenAjax: function () {
            return $.get("/gerencia/avance-por-colegio");
        },
        loadCursoResumenAjax: function () {
            return $.get("/gerencia/avance-por-curso");
        },
        loadPagosActividades: function (){
            return $.get("/gerencia/avance-por-actividades");
        }
    };


    reporte.init();

});