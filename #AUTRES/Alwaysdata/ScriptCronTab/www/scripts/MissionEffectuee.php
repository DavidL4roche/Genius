<?php

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["idJoueur"]) && isset($_GET["idMission"])) {
    $idJoueur = $_GET["idJoueur"];
    $idMission = $_GET["idMission"];
    echo $helper->MissionEffectuee($idJoueur, $idMission);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champs id et idArt non renseignés"
    ));
}