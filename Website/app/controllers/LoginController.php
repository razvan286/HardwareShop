<?php
require ('../core/db.class.php');
        session_start();

        if (isset($_POST['submit'])) {
          if (empty($_POST["username"]) || empty($_POST["password"])) {
            $message = '<label>All fields are required</label>';
            echo $message;
          } else {
            $dbconn = new DB();
            $query = "SELECT * FROM employee WHERE username = :username AND password = :password AND ReasonsForRelease is NULL";

            $statement = $dbconn->connect()->prepare($query);
            $statement->execute(
              array(
                'username' => $_POST["username"],
                'password' => $_POST["password"]
              )
            );
            $employees = $statement->fetchAll();
            $count = $statement->rowCount();
            if ($count > 0) {
              $_SESSION['loggedin'] = TRUE;
              $_SESSION['password'] = $_POST['password'];
              $_SESSION['username'] = $_POST["username"];
              foreach ($employees as $empl) {
                if ($empl['FirstLogin'] == 1) {
                  header('Location: ../views/Home.php');
                  exit;
                } else {
                  if ($empl['FirstLogin'] == 0) {
                    header('Location: ../views/ChangePass.php');
                  }
                }
              }
              //echo "Profile page";
            } else {
              $message = "Wrong data inserted";
              //or the employee is fired
              echo $message;
            }
          }
        }
