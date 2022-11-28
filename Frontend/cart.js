
  $(document).ready(function(){

    var urlParams = new URLSearchParams(window.location.search);
    var _userId = getCookie("itcapstone");

    function getBaseUrl(endpoint){
        //return 'https://capstoneoutfitters.azurewebsites.net/'+endpoint;
        return 'https://localhost:7242/'+endpoint;
    }

    console.log("customer "+_userId);
    axios.post(getBaseUrl('Cart'), {
        customer: _userId
    })
        .then(function (response) {

            console.log(response);

            for (a = 0; a < response.data.length; a++){
                var cart = response.data[a];

                console.log(cart.ProductID);
                axios.get(getBaseUrl('Products/id'), {
                    params:{id: cart.ProductID}
                }).then( function(response) {

                    var i = response.data;
                    console.log(i);

                    $("#product_box").append(`
                    <div class="product" onclick="window.location.href='item.html?id=${i.ProductID}';">
                        <img src="img/${i.Image}.jpg" width = 50px height = 50px alt="${i.Title}">
                        <div class="description" onclick="window.location.href='item.html?id=${i.ProductID}';">
                            <h5>${i.Title}</h5>
                            <h4>$${i.Price}</h4>
                        </div>
                    </div>    
                    `);
                });
            }
        });
});


