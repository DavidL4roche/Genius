<?php

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["nomAmi"])) {
    $nomAmi = $_GET["nomAmi"];
    echo $helper->GetAmi($nomAmi);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champs nomAmi non renseignés"
    ));
}