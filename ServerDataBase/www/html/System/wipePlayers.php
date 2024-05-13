<?php
    require '../System/configConstants.php';
    require '../../database.php';

    $users = R::findAll('users');

    foreach($users as $user){
        $cards = $user -> sharedCardsList;
        foreach($cards as $card){
            unset($user -> $sharedCards[$card -> id]);
        }

        R::store($user);
    }

    R::exec('DROP TABLE IF EXISTS cards_users');
    R::exec('DROP TABLE IF EXISTS cards');
    require('createCards.php');

    $availableCards = DEFAULT_AVAILABLE_CARDS;
    $selectedID = DEFAULT_SELECTED_CARDS;
    foreach($users as $user){
        foreach($availableCards as $id){
            $card = R::load('cards', $id);
            $user -> link('cards_users', array('selected' => false)) -> cards = $card;
        }
        
        R::store($user);

        $links = $user -> withCondition('cards_users.cards_id IN ('. R::genSlots($selectedID) .')', $selectedID) -> ownCardsUsers;
        foreach($links as $link){
            $link -> selected = true;
        }
    
        R::store($user);
    }

    echo 'OK';
?>