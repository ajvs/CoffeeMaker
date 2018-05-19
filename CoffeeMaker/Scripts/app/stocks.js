$(function () {

    $(window).load(showStockChart);
    function showStockChart() {
        var stockChart = new CanvasJS.Chart("stockChartContainer", {
            animationEnabled: true,
            theme: "light2",
            exportEnabled: true,
            title: {
                text: "Stocks Available"
            },
            data: [{
                type: "column",
                dataPoints: GetStocks()
            }]
        });
        stockChart.render();
    }

    function GetStocks() {
        var orders;
        $.ajax({
            url: "/Stocks/GetStocks",
            type: "POST",
            contentType: "application/json",
            async: false,
            success: function (result) {
                orders = JSON.parse(result);
            },
            error: function (result) {
                console.log("failed");
            }
        });
        return orders;
    }
    $("#btnResetStocks").click(ResetStocks);
    function ResetStocks() {
        var orders;
        $.ajax({
            url: "/Stocks/ResetStocks",
            type: "POST",
            contentType: "application/json",
            async: false,
            success: function (result) {
                if(result)
                {
                    alert($('#btnResetStocks').data('success'))
                }
                else {
                    alert($('#btnResetStocks').data('error'))
                }
                showStockChart();
            },
            error: function (result) {
                alert($('#btnResetStocks').data('error'))
            }
        });
        return orders;
    }
})