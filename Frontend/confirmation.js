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

    $("#confirmation").html( urlParams.get("confirmation") );

});