<?php

    require('../../app/core/db.class.php');

    session_start();

    if(isset($_POST['submit']))
    {
        $dbconn = new DB();
        $bio = $_POST['new-bio'];

        $username = $_SESSION['username'];
        $query = "UPDATE employee SET PersonalInfo = '$bio' WHERE Username = '$username'";

        $stmt = $dbconn->connect()->prepare($query);
        $stmt->execute();

        header('Location: ../../app/views/Home.php');
    }
?>