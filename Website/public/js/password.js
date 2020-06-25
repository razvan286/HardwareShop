function checkPasswords()
{
    var newPassword = document.getElementById("password");
    var confirmPassword = document.getElementById("confirmPassword");
    var password1 = newPassword.value;
    var password2 = confirmPassword.value;

    if(password1 == password2)
    {
        return true;
    }
    return false;
}

function checkPass()
{
    var message = "Passwords do not match!";
    var ok = checkPasswords();
    if(ok == true)
    {
        document.getElementById('confirm-btn').setAttribute("type", "submit");
        alert('Password has been successfully updated!');
    }
    else
    {
        document.getElementById("label-pass").innerHTML = message;
        document.getElementById("label-pass").style.color = "red";
        document.getElementById("confirm-btn").setAttribute("type", "button");
        document.getElementById("confirm-btn").style.backgroundColor = "gray";
    }
}

