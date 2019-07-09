<?php

require_once "../Helper.php";

$helper = new Helper();

if (isset($_GET["id"]) && isset($_GET["idRess"]) && isset($_GET["valeur"])) {
    $id = $_GET["id"];
    $idRess = $_GET["idRess"];
    $valeur = $_GET["valeur"];
    echo $helper->TransfertRessourcesEnBase($id, $idRess, $valeur);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champ id non renseigné"
    ));
}