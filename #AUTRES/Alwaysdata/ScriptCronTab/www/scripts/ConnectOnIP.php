<?php

    require_once "Helper.php";

    $helper = new Helper();

    if (isset($_GET["connect"]) && isset($_GET["playerId"])) {
        $connect = $_GET["connect"];
        $playerId = $_GET["playerId"];
        echo $helper->connectOnIP($connect, $playerId);
    }
    else {
        echo json_encode(array(
            "result" => false,
            "msg" => "Champs connect et playerId non renseignés"
        ));
    }