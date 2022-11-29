
  $(document).ready(function(){

    var urlParams = new URLSearchParams(window.location.search);
    var _id = urlParams.get("id");
    var item;
    console.log(_id);

    axios.get(getBaseUrl('Products/id'), {
        params: {
            id: _id
          }
    })
        .then(function (response) {
            $("#itemTitle").html(response.data.Title);
            $("#itemDesc").html(response.data.Description);
            $("#itemImage").attr("src", "img/"+response.data.Image+".jpg");
            $("#itemPrice").html("$"+response.data.Price);

            item = response.data;
        });

    axios.get(getBaseUrl('Products'), {
        params: {
            featured: 1
            }
    })
         .then(function (response) {
            console.log("hi");
    
             console.log(response.data[1].Price);
            for (a = 0; a < response.data.length; a++){
                  var i = response.data[a];
    
                 console.log(i.Image);
    
                    $("#related").append(`
                    <div class="product" onclick="window.location.href='item.html?id=${i.ProductID}';">
                        <img src="img/${i.Image}.jpg" alt="${i.Title}">
                        <div class="description" onclick="window.location.href='item.html?id=${i.ProductID}';">
                            <h5>${i.Title}</h5>
                            <h4>$${i.Price}</h4>
                        </div>
                    </div>    
                    `);
                }
            });

    $("#itemAdd").click( function(){

        var chosenSize = $('#itemSize').val(); 
        var chosenQuantity = $('#itemQuantity').val();

        if (getCookie("itcapstone") == 0){
            window.location = "login.html";
        }
        else{

            if (chosenSize != "none"){

                axios.post(getBaseUrl('Products/add'),
                {
                    customerid: getCookie("itcapstone"),
                    productid: _id,
                    size: chosenSize,
                    quantity: chosenQuantity
                }).then(function (response) {
                    console.log(response.data);
                });

            }
        }
    });
});


