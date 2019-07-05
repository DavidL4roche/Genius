<?php

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["id"]) && isset($_GET["idObjet"])) {
    $id = $_GET["id"];
    $idObjet = $_GET["idObjet"];
    echo $helper->ObjetAchete($id, $idObjet);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champs id et idArt non renseignés"
    ));
}