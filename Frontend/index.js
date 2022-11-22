// Old Script.js

  $(document).ready(function(){
    function getBaseUrl(endpoint){
        //return 'https://capstoneoutfitters.azurewebsites.net/'+endpoint;
        console.log("cookie: "+getCookie('itcapstone'));
        return 'https://localhost:7242/'+endpoint;
        
    }

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

                $("#product_box").append(`
                <div class="product" onclick="window.location.href='item.html?id=${i.id}';">
                    <img src="img/${i.Image}.jpg" alt="${i.title}">
                    <div class="description" onclick="window.location.href='item.html?id=${i.id}';">
                        <h5>${i.title}</h5>
                        <h4>${i.Price}</h4>
                    </div>
                </div>    
                `);
            }


            /*
            console.log("islogged");
            console.log(response.data);
            if(response.data.Status){
                //$("#profile_button").hide();
                $('#profile_button').show();

                console.log(response.data.Nickname);
                console.log("pain");
                $('#profile_button').html(response.data.Nickname);

                $('#login_button').hide();
                $('#logout_button').show();
            } else {
                $('#login_button').show();
                $('#logout_button').hide();
            }
            */
        });

    /*
    $("#linkCreateAccount").click( function( e ){
        e.preventDefault();
        //loginForm.classList.add("form--hidden");
        $('#login_form').addClass("form--hidden");
        $("#signup_form").removeClass("form--hidden");
        //createAccountForm.classList.remove("form--hidden");
    });
    $("#linkLogin").click( function( e ){
        e.preventDefault();
        //loginForm.classList.add("form--hidden");
        $('#signup_form').addClass("form--hidden");
        $("#login_form").removeClass("form--hidden");
        //createAccountForm.classList.remove("form--hidden");
    });

    $('#login_form').submit(function(){
        $('#login_alert').hide();
        axios.post(getBaseUrl('Login'),
        {
            username: $('#username').val(),
            password: $('#password').val()
        }).then(function (response) {
            console.log(response.data);
            if(!response.data.status){
                $('#loginalert').show();
            } else {
                setCookie("itcapstone", response.data.cookieId, 1);
                console.log(response.data.cookieId);
                window.location = "index.html";
            }
        });
        return false;
    });

    
    $('#logout_button').click( function(){
        eraseCookie("itcapstone");
    });
	
	$('#signup_form').submit(function(){
		
        $('#signupalert').hide();
		$('#signupsuccess').hide();

        axios.post(getBaseUrl('Signup'),
        {
			
            username: $('#newusername').val(),
            password: $('#newpassword').val(),
			email: $('#newemail').val()
			
        }).then(function (response) {
			
			$('#signupalert').show();
            
            console.log(response.data);
			
            if(!response.data.status){
                
				switch (response.data.code){
					case "usernameRequired":
						$('#signupalert').html('The username field is required.');
						break;
					case "usernameInUse":
						$('#signupalert').html('That username is already in use.');
						break;
					case "passwordRequired":
						$('#signupalert').html('The password field is required.');
						break;
					case "passwordTooShort":
						$('#signupalert').html('Your password should be over 8 characters in length.');
						break;
				}
				
            } else {
                $('#signupalert').hide();
                $('#signupsuccess').show();
				$('#signupsuccess').html('Your account has been successfully created.');
            }
			
        });
        return false;
    });
    */
});


