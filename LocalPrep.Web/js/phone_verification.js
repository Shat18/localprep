

function phoneAuth() {
    
/*var a = document.getElementById('Number').value;*/
    var a = "9993619886";
    var b = "+91";
    var phoneNumber = b + a;
    

    //const phoneNumber = getPhoneNumberFromUserInput();
    //const appVerifier = window.recaptchaVerifier;
    alert(phoneNumber);
    firebase.auth().signInWithPhoneNumber(phoneNumber)
        .then((confirmationResult) => {
            // SMS sent. Prompt user to type the code from the message, then sign the
            // user in with confirmationResult.confirm(code).
            alert("raja");
            window.confirmationResult = confirmationResult;
            // ...
        }).catch((error) => {
            alert("AAraja");
            // Error; SMS not sent
            // ...
        });
}

function codeVerify() {
    const code = getCodeFromUserInput();
    confirmationResult.confirm(code).then((result) => {
        // User signed in successfully.
        const user = result.user;
        // ...
    }).catch((error) => {
        // User couldn't sign in (bad verification code?)
        // ...
    });
}
