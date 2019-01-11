<?php
    require_once "Helper.php";

    $helper = new Helper();

    if (isset($_GET["mail"]) && isset($_GET["pass"])) {
        $mail = $_GET["mail"];
        $pass = $_GET["pass"];
        echo $helper->CheckConnexionByMail($mail, $pass);
    }