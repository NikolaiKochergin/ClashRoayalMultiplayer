<?php
    require '../../database.php';
    require '../System/configConstants.php';

    $userID = $_POST['userID'];
    $selectedIDsJson = $_POST['selectedIDs'];

    if(!isset($userID) || !isset($selectedIDsJson)){
        echo 'Data struct error';
        exit;
    }

    $selectedIDs = json_decode($selectedIDsJson, true)['IDs'];

    if(!isset($selectedIDs)){
        echo 'Array not found: ' . $selectedIDsJson;
        exit;
    }

    $user = R::load('users', $userID);
    $links = $user -> withCondition('cards_users.cards_id IN ('. R::genSlots($selectedIDs) .')', $selectedIDs) -> ownCardsUsers;

    if(count($links) > DECK_SIZE){
        echo 'Count error ' . count($links);
        exit;
    }
    foreach($links as $link){
        $link -> selected = true;
    }

    R::store($user);

    $links = $user -> 
    withCondition('cards_users.cards_id NOT IN ('. R::genSlots($selectedIDs) .') AND cards_users.selected = ?', [...$selectedIDs, true]) -> ownCardsUsers;

    foreach($links as $link){
        $link -> selected = false;
    }

    R::store($user);

    echo 'ok';
?>