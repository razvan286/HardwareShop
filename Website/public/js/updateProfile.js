//when pressing the button, change the paragraphs into form input fields which will have same values as the paragraph at the beginning
//add a submit button which will make the confirmation after pressing it
//when pressing the submit button the data will be updated in the database


//1) create input fields and buttons
function modifyContent(elem, formAction, inputName) {
  var element = document.getElementById(elem);
  var text = element.textContent;

  //create form
  var formElement = document.createElement("form");
  formElement.className = "formUpdate update-fields";
  formElement.id = "email-form-update";
  formElement.setAttribute("method", "post");
  formElement.setAttribute("action", formAction);

  //create 2 list elements
  //one for input element, one for submit button
  let liInput = document.createElement("li");
  var input = document.createElement("input");
  input.type = "text";
  input.value = text;
  input.name = inputName;
  input.size = Math.max(text.length);
  input.id = "email-text";
  liInput.appendChild(input);
  formElement.appendChild(liInput);
  //add a label for correct/ incorrect email format
  let liLabel = document.createElement('li');
  var labelEmail = document.createElement('label');
  labelEmail.setAttribute('for', 'email-text');
  labelEmail.id = "label-email";
  liLabel.appendChild(labelEmail);
  formElement.appendChild(liLabel);

  //Check if the inputted email is in correct format

  function checkEmail(){
    var newEmailInput = document.getElementById("email-text");
    var email = newEmailInput.value;
    var message = "Enter a valid email";
    if(!(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email)))
    {
      document.getElementById("label-email").innerHTML = message;
      document.getElementById("label-email").style.color = "red";
      document.getElementById("submit-btn-email").setAttribute("type", "button");
      document.getElementById("submit-btn-email").style.backgroundColor = "gray";
    }
    else
    {
      document.getElementById("submit-btn-email").setAttribute("type", "submit");
      alert('Email has been successfully updated!'); 
    }  
  }

  //create submit button
  let liSubmit = document.createElement('li');
  var submitBtn = document.createElement("input");
  submitBtn.setAttribute("type", "button");
  submitBtn.setAttribute("value", "Update");
  submitBtn.setAttribute("name", "submit");
  submitBtn.id = "submit-btn-email";
  submitBtn.className = "updateBtn";
  liSubmit.appendChild(submitBtn);
  formElement.appendChild(liSubmit);

  element.parentNode.replaceChild(formElement, element);

  document.getElementById("submit-btn-email").onclick = checkEmail;
  //document.getElementById("email-text").onkeyup = checkMailWhenTyping;

}


//UPDATE PHONE NUMBER
function modifyPhoneContent(elem, formAction, inputName) {
  var element = document.getElementById(elem);
  var text = element.textContent;

  //create form
  var formElement = document.createElement("form");
  formElement.className = "formUpdate update-fields";
  formElement.id = "email-form-update";
  formElement.setAttribute("method", "post");
  formElement.setAttribute("action", formAction);

  //create 2 list elements
  //one for input element, one for submit button
  let liInput = document.createElement("li");
  var input = document.createElement("input");
  input.type = "text";
  input.value = text;
  input.name = inputName;
  input.size = Math.max(text.length);
  input.id = "phone-number";
  liInput.appendChild(input);
  formElement.appendChild(liInput);
  //add a label for correct/ incorrect email format
  let liLabel = document.createElement('li');
  var labelEmail = document.createElement('label');
  labelEmail.setAttribute('for', 'phone-number');
  labelEmail.id = "label-phone";
  liLabel.appendChild(labelEmail);
  formElement.appendChild(liLabel);

  //Check if the inputted email is in correct format

  function checkPhone(){
    var newPhoneInput = document.getElementById("phone-number");
    var phone = newPhoneInput.value;
    var message = "Enter a valid phone number";
    if(!(/^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/.test(phone)))
    {
      document.getElementById("label-phone").innerHTML = message;
      document.getElementById("label-phone").style.color = "red";
      document.getElementById("submit-btn-phone").setAttribute("type", "button");
      document.getElementById("submit-btn-phone").style.backgroundColor = "gray";
    }
    else
    {
      document.getElementById("submit-btn-phone").setAttribute("type", "submit");
      alert('Phone number has been successfully updated!'); 
    }  
  }

  //create submit button
  let liSubmit = document.createElement('li');
  var submitBtn = document.createElement("input");
  submitBtn.setAttribute("type", "button");
  submitBtn.setAttribute("value", "Update");
  submitBtn.setAttribute("name", "submit");
  submitBtn.id = "submit-btn-phone";
  submitBtn.className = "updateBtn";
  liSubmit.appendChild(submitBtn);
  formElement.appendChild(liSubmit);

  element.parentNode.replaceChild(formElement, element);

  document.getElementById("submit-btn-phone").onclick = checkPhone;
  //document.getElementById("email-text").onkeyup = checkMailWhenTyping;
}


//update personal BIO
function updateBio(elem){
  var element = document.getElementById(elem);
  var text = element.textContent;

  console.log(text);
  debugger;
  //create form
  var formElement = document.createElement("form");
  formElement.className = "formUpdate update-fields";
  formElement.setAttribute("method", "post");
  formElement.setAttribute("action", "../../public/php/updateBio.php");

  //create 2 list elements, 1 text area and 1 button
  let liInput = document.createElement("li");
  var textArea = document.createElement('textarea');
  textArea.id = "textArea-bio";
  textArea.type = "text";
  textArea.value = text;
  textArea.name = "new-bio";
  textArea.size = Math.max(text.length);
  liInput.appendChild(textArea);
  formElement.appendChild(liInput);
  //create a label for length of text
  let liLabel = document.createElement('li');
  var labelBio = document.createElement('label');
  labelBio.setAttribute('for', 'textArea-bio');
  labelBio.id = "label-bio";
  liLabel.appendChild(labelBio);
  formElement.appendChild(liLabel);
  //check if the inserted text is less than 1000 chars
  function checkBio(){
    var newBio = document.getElementById("textArea-bio");
    var bio = newBio.value;
    var message = "Inserted text length has to be less than 1000 characters!";
    if(bio.length > 1000)
    {
      document.getElementById("label-bio").innerHTML = message;
      document.getElementById("label-bio").style.color = "red";
      document.getElementById("submit-btn-bio").setAttribute("type", "button");
      document.getElementById("submit-btn-bio").style.backgroundColor = "gray";
    }
    else
    {
      document.getElementById("submit-btn-bio").setAttribute("type", "submit");
      alert("Personal bio has been successfully updated!");
    }
  }
  //create submit button
  let liSubmit = document.createElement('li');
  var submitBtn = document.createElement("input");
  submitBtn.setAttribute("type", "button");
  submitBtn.setAttribute("value", "Update");
  submitBtn.setAttribute("name", "submit");
  submitBtn.id = "submit-btn-bio";
  submitBtn.className = "updateBtn";
  liSubmit.appendChild(submitBtn);
  formElement.appendChild(liSubmit);

  element.parentNode.replaceChild(formElement, element);
  document.getElementById("submit-btn-bio").onclick = checkBio;
}

//--- PASSWORD UPDATE FIELDS ---
function insertAfter(newNode, referenceNode) {
  referenceNode.parentNode.replaceChild(newNode, referenceNode.nextSibling);
}

//var strenght = "";


function updatePassword(elem){
  var element = document.getElementById(elem);
  var text = element.value;
  //var text = element.value;

  //create form elements
  //list

  //2) label - New password
  //3) new password textBox
  //4) label - Confirm new password
  //5) confirm new password textBox
  //6) Update button

  //create form
  var formElement = document.createElement("form");
  formElement.className = "formUpdate updateFields";
  formElement.setAttribute("method", "post");
  formElement.setAttribute("action", "../../public/php/updatePassword.php");

  //create list
  //1)

  //2)
  let liNewPassLabel = document.createElement('li');
  var lblNewPass = document.createElement('label');
  lblNewPass.id = "password-update-label";
  lblNewPass.textContent = "New password";
  liNewPassLabel.appendChild(lblNewPass);
  formElement.appendChild(liNewPassLabel); 

  //3)
  let liInputNewPass = document.createElement("li");
  //input new pass
  var inputNewPass = document.createElement('input');
  inputNewPass.type = "password";
  inputNewPass.className = "input-pass";
  inputNewPass.name = "new-password";
  inputNewPass.id = "new-password";
  inputNewPass.className = "input-new-pass-field";
  liInputNewPass.appendChild(inputNewPass);
  //eye icon
  var showIconNewPass = document.createElement('i');
  showIconNewPass.className = "fa fa-eye";
  showIconNewPass.id = "show-icon-new-pass";
  liInputNewPass.appendChild(showIconNewPass);
  //label for password strenght
  var labelPasswordStrength = document.createElement('label');
  labelPasswordStrength.setAttribute('for', 'new-password');
  labelPasswordStrength.id = "password-strength";
  liInputNewPass.appendChild(labelPasswordStrength);
  formElement.appendChild(liInputNewPass);
  //add label for valid password
  let liLabel = document.createElement('li');
  var labelPass = document.createElement('label');
  labelPass.setAttribute('for', 'password-strength');
  labelPass.id = "label-pass";
  liLabel.appendChild(labelPass);
  formElement.appendChild(liLabel);
  //append these 2 to the formElement
  

  //4)
  /* let liConfirmNewPassLabel = document.createElement('li');
  var lblConfirmNewPass = document.createElement('label');
  lblConfirmNewPass.id = "password-update-label";
  lblConfirmNewPass.textContent = "Confirm new password";
  liConfirmNewPassLabel.appendChild(lblConfirmNewPass);
  formElement.appendChild(liConfirmNewPassLabel);
  
  //5)
  let liInputConfirmNewPass = document.createElement("li");
  var inputConfirmNewPass = document.createElement('input');
  inputConfirmNewPass.type = "password";
  inputConfirmNewPass.className = "input-pass";
  inputConfirmNewPass.id = "input-confirm-new-pass-field";
  //eye icon
  var showIconConfirmPass = document.createElement('i');
  showIconConfirmPass.className = "fa fa-eye";
  showIconConfirmPass.id = "show-icon-confirm-pass";  
  liInputConfirmNewPass.appendChild(inputConfirmNewPass);
  liInputConfirmNewPass.appendChild(showIconConfirmPass);
  formElement.appendChild(liInputConfirmNewPass);
*/

function checkPass(){
  var message = "Password is too weak!";
  var strength = checkPasswordStrength();
  if(strength == true)
  {
    document.getElementById("submit-btn-password").setAttribute("type", "submit");
    alert('Password has been successfully updated!');
  }
  else
  {
    document.getElementById("label-pass").innerHTML = message;
    document.getElementById("label-pass").style.color = "red";
    document.getElementById("submit-btn-password").setAttribute("type", "button");
    document.getElementById("submit-btn-password").style.backgroundColor = "gray";
  }
}
  
  //6)
  let liSubmit = document.createElement('li');
  var submitBtn = document.createElement("input");
  submitBtn.setAttribute("type", "button");
  submitBtn.setAttribute("value", "Update");
  submitBtn.setAttribute("name", "submit");
  submitBtn.id = "submit-btn-password";
  submitBtn.className = "updateBtn";
  liSubmit.appendChild(submitBtn);
  formElement.appendChild(liSubmit);

 var referenceElem = document.getElementById("label-show-pass");
 insertAfter(formElement, referenceElem);

 

 //call the function for strenght
 document.getElementById("new-password").onkeyup = checkPasswordStrength;
 showNewPasswords('new-password', 'show-icon-new-pass');
 document.getElementById("submit-btn-password").onclick = checkPass;
 //showNewPasswords('input-confirm-new-pass-field', 'show-icon-confirm-pass');

}


//check password strenght
function checkPasswordStrength(){
  var newPasswordInput = document.getElementById("new-password");
  var password = newPasswordInput.value;
  var specialCharacters = ".,/;'[]?!@#$%^&*()_-+=`~";
  var passwordScore = 0;

  for(var i = 0; i < password.length; i++){
    if(specialCharacters.indexOf(password.charAt(i)) > -1){
      passwordScore += 20;
    }
  }

  if(/[a-z]/.test(password)){
    passwordScore += 20;
  }
  if(/[A-Z]/.test(password)){
    passwordScore += 20;
  }
  if(/[\d]/.test(password)){
    passwordScore += 20;
  }
  if(password.length >= 8){
    passwordScore += 20;
  }

  var strength = "";
  var backgroundColor = "";

  if(passwordScore >= 100){
    strength = "Strong";
    backgroundColor = "green";
  }
  else if(passwordScore >= 80){
    strength = "Medium";
    backgroundColor = "gray";
  }
  else if(passwordScore >= 60){
    strength = "Weak";
    backgroundColor = "maroon";
  }
  else{
    strength = "Very weak";
    backgroundColor = "red";
  }
  
  document.getElementById("password-strength").innerHTML = strength;
  newPasswordInput.style.backgroundColor = backgroundColor;

  if(passwordScore >= 80)
  {
    return true;
  }
  else
  {
    return false;
  }
}


function showNewPasswords(passwordId, iconId){
  var password = document.getElementById(passwordId);
  var eye = document.getElementById(iconId);

  eye.addEventListener('click', togglePass);

  function togglePass(){

    eye.classList.toggle('active');

    (password.type == 'password') ? password.type = 'text' : password.type = 'password';
  }
}

//show the password when checkbox check changes
function showPassword(){
  var pass = document.getElementById("original-input-pass");
  var check = document.getElementById("check");
  if(check.checked)
  {
    pass.setAttribute('type', 'text');
  }
  else
  {
    pass.setAttribute('type', 'password');
  }
}

//new password checking before updating the old password

//var newPass = document.getElementsByClassName("input-new-pass-field");
//different functions have to be made for password and bio
//bio using textfield and password using input type - password

//customizing profile page content buttons 
document.getElementById("custom-contact-email").addEventListener('click', function(){ modifyContent('mail-written', '../../public/php/updateEmailInfo.php', 'email'); }, false);
document.getElementById("custom-contact-phone").addEventListener('click', function(){ modifyPhoneContent('phone-nr', '../../public/php/updatePhoneNr.php', 'phone'); }, false);
//document.getElementById("custom-username").addEventListener('click', function(){ modifyContent('username'); }, false);
document.getElementById("custom-password").addEventListener('click', function(){ updatePassword('original-input-pass'); }, false);
document.getElementById("custom-bio").addEventListener('click', function(){ updateBio('personal-bio'); }, false);
//checkbox check changed show/hide password
document.getElementById("check").addEventListener('click', showPassword);
