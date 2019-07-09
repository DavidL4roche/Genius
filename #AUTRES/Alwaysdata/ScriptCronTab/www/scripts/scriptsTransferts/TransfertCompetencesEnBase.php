<?php

require_once "../Helper.php";

$helper = new Helper();

if (isset($_GET["id"]) && isset($_GET["idComp"]) && isset($_GET["valeur"])) {
    $id = $_GET["id"];
    $idComp = $_GET["idComp"];
    $valeur = $_GET["valeur"];
    echo $helper->TransfertCompetencesEnBase($id, $idComp, $valeur);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champ id non renseigné"
    ));
}