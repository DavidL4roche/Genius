<?php
    require_once "Helper.php";

    $helper = new Helper();

    if (isset($_GET["pseudo"]) && isset($_GET["pass"])) {
        $pseudo = $_GET["pseudo"];
        $pass = $_GET["pass"];
        echo $helper->CheckConnexion($pseudo, $pass);
    }