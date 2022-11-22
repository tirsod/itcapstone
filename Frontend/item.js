
  $(document).ready(function(){

    var urlParams = new URLSearchParams(window.location.search);
    var _id = urlParams.get("id");
    var item;
    console.log(_id);

    function getBaseUrl(endpoint){
        //return 'https://capstoneoutfitters.azurewebsites.net/'+endpoint;
        return 'https://localhost:7242/'+endpoint;
    }

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


