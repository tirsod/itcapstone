
var itemList = new Array();

  $(document).ready(function(){

    var urlParams = new URLSearchParams(window.location.search);
    var _userId = getCookie("itcapstone");

    function getBaseUrl(endpoint){
        //return 'https://capstoneoutfitters.azurewebsites.net/'+endpoint;
        return 'https://localhost:7242/'+endpoint;
    }

    function calculateTotal(){
        var price = 0;

        for (var index in itemList){

            var i = itemList[index];
            console.log(i);

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

    function removeCartItem(cartItemId){
        $(`#item${cartItemId}`).remove();
        calculateTotal();
    }
        
    function changeQuantity(cartItemId){
        calculateTotal();
    }
        

    console.log("customer "+_userId);

    axios.post(getBaseUrl('Cart'), {
        customer: _userId
    })
        .then(function (response) {

            console.log(response);

            for (a = 0; a < response.data.length; a++){
                var cart = response.data[a];
                var itemSize = response.data[a].Size.toUpperCase();
                var itemQuantity = response.data[a].Quantity;

                axios.get(getBaseUrl('Products/id'), {

                    params:{id: cart.ProductID}

                }).then( function(response) {

                    var i = response.data;
                    var id = cart.CartItemID;

                    console.log("before: "+itemList);
                    itemList.push(id);
                    console.log("after: "+itemList);

                    $("#product_box").append(`

                    <tr id=item${id}> 
                    <td><button id="trash${id}"><i class="fa-solid fa-trash-can"></i></button></td>
                    <td>
                        <img id="product-img" src="img/${i.Image}.jpg" alt="${i.Title}" onclick="window.location.href='item.html?id=${i.ProductID}'>
                        <div class="description">
                            <a href='item.html?id=${i.ProductID}'> <h5>${i.Title}</h5> </a>
                        </div>
                    </td>
                    <td>${itemSize}</td>
                    <td id="price${id}"> $${i.Price}</td>
                    <td><input type="number" id="quantity${id}" min="1" max="99" value="${itemQuantity}"></td>
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

                    $(`#trash${id}`).click(function(){
                        removeCartItem(id);
                        console.log("removed");
                    });

                    $(`#quantity${id}`).on("change", function(){
                        changeQuantity(id);
                        console.log("changed");
                    });

                    calculateTotal();

                });
            }
        });
});


