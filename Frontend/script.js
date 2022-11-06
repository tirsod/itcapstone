$(document).ready(function(){
    function getBaseUrl(endpoint){
        return 'https://capstoneoutfitters.azurewebsites.net'+endpoint;
    }
    $('#login_form').submit(function(){
        $('#login_alert').hide();
        axios.post(getBaseUrl('Login'),
        {
            username: $('#username').val(),
            password: $('#password').val()
        }).then(function (response) {
            console.log(response.data);
            if(!response.data.status){
                $('#login_alert').show();
            } else {
                setCookie("itcapstone", response.data.cookieId, 1);
                window.location = "index.html";
            }
        });
        return false;
    });

    axios.get(getBaseUrl('Shop'), {
        params: {
            id: getCookie('itcapstone') == null ? 0 : 1
          }
    })
        .then(function (response) {
            if(response.data.Status){
                $('#profile_button').show();
                $('#profile_button a').html(response.data.Nickname);
            } else {
                $('#login_button').show();
            }
        });
});

function setCookie(name,value,days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days*24*60*60*1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "")  + expires + "; path=/";
}
function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for(var i=0;i < ca.length;i++) {
        var c = ca[i];
        while (c.charAt(0)==' ') c = c.substring(1,c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
    }
    return null;
}
function eraseCookie(name) {   
    document.cookie = name +'=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}

