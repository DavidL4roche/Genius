<?php
    require_once "Helper.php";

    $helper = new Helper();

    if (isset($_GET["mail"]) && $_GET["mail"] != "") {
        $mail = $_GET["mail"];
        echo $helper->reinitiatePassword($mail);
    }
    else {
        echo json_encode(array(
            "result" => false,
            "msg" => "Veuillez saisir un mail."
        ));
    }