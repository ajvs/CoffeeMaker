$(function() {
    $(window).load(showOrderChart);
    function showOrderChart() {
        var ordersChart = new CanvasJS.Chart("orderChartContainer", {
            animationEnabled: true,
            theme: "light1",
            exportEnabled: true,
            title: {
                text: "Best Sellers"
            },
            axisY: {
                title: "Number of Orders"
            },
            data: [{
                type: "column",
                dataPoints: GetOrderHistory()
            }]
        });
        ordersChart.render();
    }

    function GetOrderHistory() {
        var orders;
        $.ajax({
            url: "/OrderDrink/GetOrderHistory",
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

    $("#btnResetOrders").click(ResetOrders);
    function ResetOrders() {
        var orders;
        $.ajax({
            url: "/OrderDrink/ResetOrders",
            type: "POST",
            contentType: "application/json",
            async: false,
            success: function (result) {
                if (result) {
                    alert($('#btnResetOrders').data('success'))
                    $("#tblOrderHistory td").remove();
                    showOrderChart();
                }
                else {
                    alert($('#btnResetOrders').data('error'))
                }                
            },
            error: function (result) {
                alert($('#btnResetOrders').data('error'))
            }
        });
        return orders;
    }
})