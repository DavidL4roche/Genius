<?php
    require_once "Helper.php";

    $helper = new Helper();

    if (isset($_GET["ip"]) && $_GET["ip"] != "") {
        $ip = $_GET["ip"];
        echo $helper->checkIP($ip);
    }
    else {
        echo json_encode(array(
            "result" => false,
            "msg" => "Adresse IP non valide"
        ));
    }