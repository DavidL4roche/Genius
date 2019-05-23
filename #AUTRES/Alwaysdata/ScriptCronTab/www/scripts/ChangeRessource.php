<?php

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["id"])) {
    $ip = $_GET["id"];
    echo $helper->updateDateCo($ip);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champ id non renseigné"
    ));
}