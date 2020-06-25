<?php

    require('../../app/core/db.class.php');

    session_start();

    if(isset($_POST['submit']))
    {
        $dbconn = new DB();
        $password = $_POST['new-password'];
        //change the session password 
        $_SESSION['password'] = $password;
        $username = $_SESSION['username'];
        $query = "UPDATE employee SET Password = '$password' WHERE Username = '$username'";

        $stmt = $dbconn->connect()->prepare($query);
        $stmt->execute();

        header('Location: ../../app/views/Home.php');
    }
