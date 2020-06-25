<?php

   spl_autoload_register('prjAutoloader');

    function prjAutoloader ($className)
    {
        include $_SERVER['DOCUMENT_ROOT'] . '/Webshop/app/model/'.$className.'.class.php';
    } 
?>
