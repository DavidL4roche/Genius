<?php

require_once "../Helper.php";

$helper = new Helper();

if (isset($_GET["id"]) && isset($_GET["idObjet"]) && isset($_GET["valeur"])) {
    $id = $_GET["id"];
    $idObjet = $_GET["idObjet"];
    $valeur = $_GET["valeur"];
    echo $helper->TransfertObjetsEnBase($id, $idObjet, $valeur);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champ id non renseigné"
    ));
}