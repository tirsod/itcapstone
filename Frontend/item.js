
  $(document).ready(function(){

    var urlParams = new URLSearchParams(window.location.search);
    var _id = urlParams.get("id");
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
        });
});


