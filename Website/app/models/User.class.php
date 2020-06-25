<?php

class User
{
    public function SendInquiry()
    {
        //sending 'email' to the database 
        $dbconn = new DB();
        $msg = "";
        if (isset($_POST['Submit'])) {
            $subject = $_POST['subject'];
            $txt = $_POST['emailContent'];
            $date = date('d/m/Y');
            if ($subject == "" || $txt == "") {
                $msg = "Not all of the values are entered!";
            } else {
                $employee_id = $_SESSION['employeeId'];

                $emailContent = "Subject: " . $subject . " Body: " .  $txt;

                $query = "INSERT INTO email (EmployeeID, Email, Date) VALUES ( '$employee_id', '$emailContent', '$date')";
                $stm = $dbconn->connect()->prepare($query);
                $stm->execute();
                header('Location: ./views/Home.php');
            }
        }
    }
    
    
}
