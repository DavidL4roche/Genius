<?php
    require_once "Helper.php";

    $helper = new Helper();

    if (isset($_GET["id"])) {
        $id = $_GET["id"];
        echo $helper->connectById($id);
    }
    else {
        echo json_encode(array(
            "result" => false,
            "msg" => "Adresse IP non valide"
        ));
    }
