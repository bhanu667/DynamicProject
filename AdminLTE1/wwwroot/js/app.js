// Document is ready
$(document).ready(function () {

    // Validate Username
    $('#pagecheck').hide();
    let pagenameError = true;
    $('#pagename').keyup(function () {
        validateUsername();
    });

    function validateUsername() {
        let pagenameValue = $('#pagename').val();
        if (pagenameValue.length == '') {
            $('#pagecheck').show();
            usernameError = false;
            return false;
        }
        else if ((pagenameValue.length < 3) ||
            (pagenameValue.length > 10)) {
            $('#pagecheck').show();
            $('#pagecheck').html
                ("**length of pagename must be between 3 and 10");
            pagenameError = false;
            return false;
        }
        else {
            $('#pagecheck').hide();
        }
    }

    // Validate Pageurl
    $('#urlcheck').hide();
    let pageurlError = true;
    $('#pageurl').keyup(function () {
        validateUsername();
    });

    function validateUsername() {
        let pageurlValue = $('#pageurl').val();
        if (pageurlValue.length == '') {
            $('#urlcheck').show();
            usernameError = false;
            return false;
        }
        else if ((pageurlValue.length < 3) ||
            (pageurlValue.length > 10)) {
            $('#urlcheck').show();
            $('#urlcheck').html
                ("**length of pagename must be between 3 and 10");
            pageurlError = false;
            return false;
        }
        else {
            $('#urlcheck').hide();
        }
    }

    // Validate Pageurl
    $('#urlcheck').hide();
    let pageurlError = true;
    $('#pageurl').keyup(function () {
        validateUsername();
    });

    function validateUsername() {
        let pageurlValue = $('#pageurl').val();
        if (pageurlValue.length == '') {
            $('#urlcheck').show();
            usernameError = false;
            return false;
        }
        else if ((pageurlValue.length < 3) ||
            (pageurlValue.length > 10)) {
            $('#urlcheck').show();
            $('#urlcheck').html
                ("**length of pagename must be between 3 and 10");
            pageurlError = false;
            return false;
        }
        else {
            $('#urlcheck').hide();
        }
    }

    
    // Submitt button
    $('#submitbtn').click(function () {
        validateUsername();
        validatePassword();
        validateConfirmPasswrd();
        validateEmail();
        if ((usernameError == true) &&
            (passwordError == true) &&
            (confirmPasswordError == true) &&
            (emailError == true)) {
            return true;
        } else {
            return false;
        }
    });
});
