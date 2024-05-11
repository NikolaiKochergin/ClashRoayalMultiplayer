<?php
    require '../../database.php';

    $user = R::load('users', 1);

    $rating = R::dispense('rating');
    $rating -> win = 0;
    $rating -> loss = 0;

    $user -> rating = $rating;

    R::store($user);

    echo 'complete';
?>