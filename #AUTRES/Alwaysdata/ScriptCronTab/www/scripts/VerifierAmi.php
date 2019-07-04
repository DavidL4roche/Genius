<?php

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["id"]) && isset($_GET["idAmi"])) {
    $id = $_GET["id"];
    $idAmi = $_GET["idAmi"];
    echo $helper->VerifierAmi($id, $idAmi);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champs id et idAmi non renseignés"
    ));
}