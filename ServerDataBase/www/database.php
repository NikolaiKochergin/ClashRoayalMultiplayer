<?php
    require 'RedbeanPHP/rb-mysql.php';

    R::setup('mysql:host=localhost;dbname=testdb', '<databaselogin>', '<databasepassword>');

    if(R::testConnection() == false){
        echo 'Connect Error';
        exit;
    }
?>