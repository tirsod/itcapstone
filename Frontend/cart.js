var itemList = new Array();
var _userId = getCookie("itcapstone");
  $(document).ready(function(){

    var urlParams = new URLSearchParams(window.location.search);
    
    if(_userId <= 0)
    {
        window.location = "login.html";
    }

    axios.post(getBaseUrl('Cart'), {
        customer: _userId
    })
        .then(function (response) {

            for (a = 0; a < response.data.CartItems.length; a++){
                var cart = response.data.CartItems[a];
            if (cart.Status == "inCart")
            {
                var itemSize = cart.Size.toUpperCase();
                var itemQuantity = cart.Quantity;

                var i = cart.Product;
                var id = cart.CartItemID;
                console.log("id set to: "+id);

                itemList.push(id);

                    $("#product_box").append(`

                    <tr id="item${id}"> 
                    <td><button id="trash${id}" onClick="removeCartItem(${id})" ><i class="fa-solid fa-trash-can"></i></button></td>
                    <td>
                        <img id="product-img" src="img/${i.Image}.jpg" alt="${i.Title}" onclick="window.location.href='item.html?id=${i.ProductID}'>
                        <div class="description">
                            <a href='item.html?id=${i.ProductID}'> <h5>${i.Title}</h5> </a>
                        </div>
                    </td>
                    <td>${itemSize}</td>
                    <td id="price${id}"> $${i.Price}</td>
                    <td><input type="number" id="quantity${id}" min="1" max="99" value="${itemQuantity}" onChange="changeQuantity(${id})"></td>
                    <td id="subtotal${id}">$${i.Price * itemQuantity}</td>
                    </tr>

                    <div class="product" onclick="window.location.href='item.html?id=${i.ProductID}';">
                        <img src="img/${i.Image}.jpg" width = 50px height = 50px alt="${i.Title}">
                        <div class="description" onclick="window.location.href='item.html?id=${i.ProductID}';">
                            <h5>${i.Title}</h5>
                            <h4>$${i.Price}</h4>
                        </div>
                    </div>    
                    `);
                }

                    calculateTotal();
            }
        });

    
    $("#checkoutButton").click( function(){
        checkout();
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
        console.log(response);
        window.location.href = `confirmation.html?confirmation=${response.data}`;
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