<?php
    require '../../database.php';

    $cardNames = array('Archer', 'Warrior', 'Golem', 'Archer_Elder', 'Warrior_Elder', 'Golem_Elder', 'Archer_GrandElder', 'Warrior_GrandElder', 'Golem_GrandElder');

    foreach($cardNames as $name){
        $card = R::dispense('cards');
        $card -> name = $name;
        R::store($card);
    }    
?>