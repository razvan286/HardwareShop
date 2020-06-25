<!DOCTYPE html>
<html lang="en">

<head>
    <link rel="stylesheet" type="text/css" href="../../public/css/main.css">
    <link rel="stylesheet" type="text/css" href="../../public/css/normalize.css">
    <link rel="stylesheet" type="text/css" href="../../public/css/ionicons-master/docs/css/ionicons.min.css">
    <link href="https://fonts.googleapis.com/css?family=Lato:100,300,300i,400&display=swap" rel="stylesheet">
    <script src="../../public/js/password.js"></script>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1,user-scalable=no">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="HandheldFriendly" content="true">
    <title>Media Bazaar</title>
</head>

<body class="body-login-page">

    <div class="container">

        <div class="flex-container">
            <div class="company-logo">

            </div>
        </div>

        <form action="../../public/php/updateFirstLoginPassword.php" method="POST">
            <ul class="list">
                <li>
                    <h2 id="login-heading">Choose your password</h2>
                </li>
                <li><input type="password" name="new-password" placeholder="New password" id="password" required> </li>
                <li><input type="password" name="confpassword" placeholder="Confirm new password" id="confirmPassword" required></li>
                <li><label id="label-pass"></label></li>
                <li><input type="submit" onclick="checkPass()" id="confirm-btn" name="submit" value="Confirm"></li>
            </ul>
        </form>
    </div>
</body>

</html>