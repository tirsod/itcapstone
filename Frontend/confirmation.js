var itemList = new Array();
var _userId = getCookie("itcapstone");


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