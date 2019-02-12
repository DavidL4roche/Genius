<?php

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["mail"])) {
    $mail = $_GET["mail"];
    $helper->sendMail($mail);
}