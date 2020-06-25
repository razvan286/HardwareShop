<?php
class DB
{    
    public function connect() 
    {        
        $servername = "studmysql01.fhict.local";
        $username = "dbi428501";
        $database = "dbi428501";
        $password = "1234";
        
        try 
        {
            ini_set('mysql.connect_timeout', 300);
            ini_set('default_socket_timeout', 300); 
            $conn = new PDO("mysql:host=$servername;dbname=$database", $username, $password);
            $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);     
            return $conn;
        }
        catch (PDOException $e)
        {
            $err_msg= $e->getMessage();
            include('./php/db_error.php');
            exit();
        }
    }
}
?>