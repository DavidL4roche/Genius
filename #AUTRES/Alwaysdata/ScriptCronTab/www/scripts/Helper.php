<?php

DEFINE("HOST", "mysql-seriousgameiut.alwaysdata.net");
DEFINE("DBNAME", "seriousgameiut_bd");
DEFINE("USERNAME", "171322");
DEFINE("PASS", "azerty12345");

class Helper {

    // Fonction de connexion à la BD
    function ConnectBDD() {
        return new PDO('mysql:host='.HOST.';dbname='.DBNAME, USERNAME, PASS);
    }

    // Création d'utilisateur dans la base
    function CreateUser($pseudo = null, $mail = null, $pass = null) {
        // Connexion à la BD
        $bdd = $this->ConnectBDD();

        // Vérification données
        $mySqlData = array(
            "pseudo" => (isset($pseudo) && $pseudo != "") ? strtolower($pseudo) : null,
            "mail" => (isset($mail) && $mail != "") ? strtolower($mail) : null,
            "pass" => (isset($pass) && $pass != "") ? strtolower($pass) : null
        );

        $sql = "SELECT * FROM p_character WHERE PCName = '".$mySqlData["pseudo"]."' UNION SELECT * FROM p_character WHERE mail = '" . $mySqlData["mail"] . "';";
        $result = $bdd->prepare($sql);
        $result->execute();
        $d = $result->fetchAll(PDO::FETCH_ASSOC);

        if (count($d) == 0) {
            $sql = "INSERT INTO p_character(PCName, mail, Password) VALUES(";
            foreach ($mySqlData as $k => $v) {
                if ($v != null) {
                    $sql .= "'".$v."',";
                }
            }
            $sql = substr($sql, 0, strlen($sql)-1);
            $sql .= ");";

            $result = $bdd->prepare($sql);
            $result->execute();
            $lastID = $bdd->lastInsertId();

            if ($lastID > 0) {
                return json_encode(array(
                    "result" => true,
                    "msg" => "Utilisateur crée avec succès",
                    "id" => $lastID
                ));
            }
        }

        else {
            return json_encode(array(
                "result" => false,
                "msg" => "L'utilisateur n'as pas été inscrit"
            ));
        }

    }

    // Transforme un tableau PHP en chaîne JSON
    function ParseJson($array = array()) {
        $r = array();

        foreach ($array as $k => $v) {
            $t = array(
                "id" => intval($v["IDPCharacter"]),
                "pseudo" => (string)utf8_encode($v["PCName"]),
                "lastConnection" => (string)utf8_encode($v["LastConnection"])
            );
            array_push($r, $t);
        }

        $finalArray["utilisateur"] = $r;
        return json_encode($finalArray);
    }

    // Vérification connexion d'un utilisateur
    function CheckConnexion($pseudo = null, $pass = null) {
        if ($pseudo != null && $pass != null) {
            $bdd = $this->ConnectBDD();

            $sql = "SELECT * FROM p_character WHERE PCName = '" . $pseudo . "' AND Password= '" . $pass . "'";

            $result = $bdd->prepare($sql);
            $result->execute();

            $d = $result->fetchAll(PDO::FETCH_ASSOC);

            return $this->ParseJson($d);
        }
        else {
            return null;
        }
    }
}