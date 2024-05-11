<?php
    require '../../database.php';

    $login = $_POST['login'];
    $password = $_POST['password'];

    if(!isset($login) || !isset($password)){
        echo 'Data struct error';
        exit;
    }

    $user = R::findOne('users', 'login = ?', array($login));

    if(!isset($user)){
        echo 'Login error';
        exit;
    }

    if($user['password'] != $password){
        echo 'Password error';
        exit;
    }

    echo 'ok|'.$user['id'];
?>