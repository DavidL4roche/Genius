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

    // Vérification connexion d'un utilisateur
    function CheckConnexion($pseudo = null, $pass = null) {

    }
}