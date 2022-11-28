var itemList = new Array();
var _userId = getCookie("itcapstone");

function getBaseUrl(endpoint){
    //return 'https://capstoneoutfitters.azurewebsites.net/'+endpoint;
    return 'https://localhost:7242/'+endpoint;
}

  $(document).ready(function(){

    var urlParams = new URLSearchParams(window.location.search);
    
    axios.get(getBaseUrl('Profile'), {
        params: {
            id: _userId
          }
    })
    .then(function (response) {
        $("#username").html(response.data.Name);
        $("#email").html(response.data.Email);
    });

    axios.post(getBaseUrl('Cart'), {
        customer: _userId
    })
        .then(function (response) {

            for (a = 0; a < response.data.CartItems.length; a++){
                var cart = response.data.CartItems[a];
                console.log(cart.Product.Price);
                if(cart.Status == "ordered"){

                var itemSize = cart.Size.toUpperCase();
                var itemQuantity = cart.Quantity;

                var i = cart.Product;
                var id = cart.CartItemID;
                console.log("id set to: "+id);

                var orderTotal = itemQuantity*cart.Product.Price;

                itemList.push(id);

                    $("#product_box").append(`
                     <tr>
                        <td>${id}</td>
                        <td>${itemQuantity}</td>
                        <td>${itemSize}</td>
                        <td>$${orderTotal}</td>
                        <td><img src="img/${i.Image}.jpg" alt="Men_Tee_Binary"></td>
                    </tr>
                    `);
                }
            }
        });
});

function removeCartItem(cartItemId){
    $(`#item${cartItemId}`).remove();

    axios.get(getBaseUrl('Cart/remove'), {
        params: {
            id: cartItemId
          }
    })
    .then(function (response) {

    });

    calculateTotal();
}
    
function changeQuantity(cartItemId){

    var q = $(`#quantity${cartItemId}`).val();

    axios.get(getBaseUrl('Cart/quantity'), {
        params: {
            id: cartItemId,
            quantity: q
          }
    })
    .then(function (response) {

    });

    calculateTotal();
}

function checkout(){

    axios.get(getBaseUrl('Cart/checkout'), {
        params: {
            id: _userId
          }
    })
    .then(function (response) {
        window.location.href = "confirmation.html";
    });

    calculateTotal();
}

function calculateTotal(){
    var price = 0;

    for (var index in itemList){

        var i = itemList[index];

        if ( $(`#subtotal${i}`).length ){ //Null check
            
            var pt = $(`#price${i}`).html().replace('$', '');
            var qt = $(`#quantity${i}`).val();
            var ht = pt*qt;

            price += parseInt(ht);

            $(`#subtotal${i}`).html(`$${pt*qt}`);
        }

        
    }

    $("#total").html(`$${price}`);
}