$(function() {
    $(".btnPlaceOrder").click(placeOrder);

    function placeOrder() {
        var orderInput = {
            CustomerName: $("#txtboxCustomerName").val(),
            DrinkOrderedGuid: $("#ddDrinkName option:selected").val(),
            Quantity: $("#txtboxQuantity").val(),
            DrinkOrdered: $("#ddDrinkName option:selected").text()
        }

        $.ajax({
            url: "/OrderDrink/PlaceOrder",
            data: JSON.stringify({
                order: orderInput
            }),
            type: "POST",
            contentType: "application/json",
            success: function (result) {
                if(result.success){
                    $("#txtboxQuantity").val('');
                    $("#txtboxCustomerName").val('');
                }
                alert(result.message);
            },
            error: function (result) {
                alert(result.message);
            }
        })
    };

    $("#btnCreateCoffee").click(createCoffee);

    function createCoffee() {
        var recipeInput = {
            DrinkId: $("#ddDrinkName option:selected").val(),
            StockId: $("#ddStockName option:selected").val(),
            Quantity: $("#txtboxQuantity").val()
        }

        $.ajax({
            url: "/AddToMenu/SubmitCoffee",
            data: JSON.stringify({
                recipe: recipeInput
            }),
            type: "POST",
            contentType: "application/json",
            success: function (result) {
                console.log("success");

            },
            error: function (result) {
                console.log("failed");
            }
        })
    }; 

})