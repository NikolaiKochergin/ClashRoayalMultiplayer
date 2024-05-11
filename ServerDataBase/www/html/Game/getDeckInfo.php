<?php
    require '../../database.php';

    $userID = $_POST['userID'];

    if(!isset($userID)){
        echo 'Data struct error';
        exit;
    }

    $user = R::load('users', $userID);
    $allCards = $user -> sharedCards;

    $availableCards = [];
    foreach($allCards as $card){
        $availableCards[] = $card -> export();
    }

    $availableCardsJson = json_encode($availableCards);

    $selectedCardBeans = $user -> withCondition('cards_users.selected = ?', array(true)) -> sharedCards;

    $selectedIDs = json_encode(array_column($selectedCardBeans, 'id'));

    echo 'ok|{"availableCards":' . $availableCardsJson . ', "selectedIDs": ' . $selectedIDs . '}'
?>