<?php

    require_once "Helper.php";

    $helper = new Helper();

    if (isset($_GET["stat"]) && isset($_GET["value"]) && isset($_GET["id"])) {
        $stat = $_GET["stat"];
        $value = $_GET["value"];
        $id = $_GET["id"];
        echo $helper->SetPlayerStat($stat, $value, $id);
    }