<?php
    require '../../database.php';

    $cardNames = array('Archer', 'Warrior', 'Golem', 'Archer_Elder', 'Warrior_Elder', 'Golem_Elder');

    foreach($cardNames as $name){
        $card = R::dispense('cards');
        $card -> name = $name;
        R::store($card);
    }    
?>