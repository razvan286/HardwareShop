<?php

require('../../app/core/db.class.php');

    session_start();

    if(isset($_POST['submit']))
    {
        $dbconn = new DB();
        $email = $_POST['email'];
        $username = $_SESSION['username'];
        $query = "UPDATE employee SET Email = '$email' WHERE Username = '$username'";

        $stmt = $dbconn->connect()->prepare($query);
        $stmt->execute();

        header('Location: ../../app/views/Home.php');
    }
?>