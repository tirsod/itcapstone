function toggleMobileMenu(menu) {
    menu.classList.toggle('open');
}

/*
function setFormMessage(formElement, type, message) {
    const messageElement = formElement.querySelector(".form__message");

    messageElement.textContent = message;
    messageElement.classList.remove("form__message--success", "form__message--error");
    messageElement.classList.add(`form__message--${type}`);
}

function setInputError(inputElement, message) {
    inputElement.classList.add("form__input--error");
    inputElement.parentElement.querySelector(".form__input-error-message").textContent = message;
}

function clearInputError(inputElement) {
    inputElement.classList.remove("form__input--error");
    inputElement.parentElement.querySelector(".form__input-error-message").textContent = "";
}

document.addEventListener("DOMContentLoaded", () => {
    const loginForm = document.querySelector("#login");
    const createAccountForm = document.querySelector("#createAccount");

    document.querySelector("#linkCreateAccount").addEventListener("click", e => {
        e.preventDefault();
        loginForm.classList.add("form--hidden");
        createAccountForm.classList.remove("form--hidden");
    });

    document.querySelector("#linkLogin").addEventListener("click", e => {
        e.preventDefault();
        loginForm.classList.remove("form--hidden");
        createAccountForm.classList.add("form--hidden");
    });

    loginForm.addEventListener("submit", e => {
        e.preventDefault();

        // Perform your AJAX/Fetch login

        setFormMessage(loginForm, "error", "Invalid username/password combination");
    });

    document.querySelectorAll(".form__input").forEach(inputElement => {
        inputElement.addEventListener("blur", e => {
            if (e.target.id === "signupUsername" && e.target.value.length > 0 && e.target.value.length < 3) {
                setInputError(inputElement, "Username must be at least 3 characters in length");
            }
        });

        inputElement.addEventListener("input", e => {
            clearInputError(inputElement);
        });
    });
});


window.addEventListener( "pageshow", function ( event ) {
    var historyTraversal = event.persisted || 
                           ( typeof window.performance != "undefined" && 
                                window.performance.navigation.type === 2 );
    if ( historyTraversal ) {
      // Handle page restore.
      window.location.reload();
    }
  });
  */

// Old Script.js

function getBaseUrl(endpoint){
    return 'https://capstoneoutfitters.azurewebsites.net/'+endpoint;
    //return 'https://localhost:7242/'+endpoint;
}

  $(document).ready(function(){
    

    axios.get(getBaseUrl('Shop'), {
        params: {
            id: getCookie('itcapstone')
          }
    })
        .then(function (response) {
            if(response.data.Status){
                //$("#profile_button").hide();
                $('#profile_button').show();
                $('#profile_button').html(response.data.Nickname);

                $('#login_button').hide();
                $('#logout_button').show();
            } else {
                $('#login_button').show();
                $('#logout_button').hide();
            }
        });

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
    return 0;
}
function eraseCookie(name) {   
    document.cookie = name +'=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}

