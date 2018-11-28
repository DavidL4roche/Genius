<?php

require_once "Helper.php";

$helper = new Helper();

/*
if (mail("davidlaroche@hotmail.fr","My subject","Salut toi")) {
    print "ok";
}
*/

if (isset($_GET["mail"])) {
    $mail = $_GET["mail"];
    $helper->sendMail($mail);
}