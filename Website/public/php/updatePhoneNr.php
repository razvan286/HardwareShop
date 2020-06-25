<?php

    require('../../app/core/db.class.php');

    session_start();

    if(isset($_POST['submit']))
    {
        $dbconn = new DB();
       $phone = $_POST['phone'];
       $username = $_SESSION['username'];
       $query = "UPDATE employee SET PhoneNumber = '$phone' WHERE Username = '$username'";

       $stmt = $dbconn->connect()->prepare($query);
       $stmt->execute();

       header('Location: ../../app/views/Home.php');
    }
?>