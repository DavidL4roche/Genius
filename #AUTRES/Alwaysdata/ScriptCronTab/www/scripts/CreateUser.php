<?php
    require_once "Helper.php";

    $helper = new Helper();

    if (isset($_GET["pseudo"]) && isset($_GET["mail"]) && isset($_GET["pass"])) {
        $pseudo = $_GET["pseudo"];
        $mail = $_GET["mail"];
        $pass = $_GET["pass"];

        echo $helper->CreateUser($pseudo, $mail, $pass);
    }
    else {
        echo "Erreur, veuillez saisir tous les champs";
    }