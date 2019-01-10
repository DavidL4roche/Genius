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

    // Transforme un tableau PHP en chaîne JSON (pour un utilisateur)
    function ParseJson($array = array()) {
        $r = array();

        foreach ($array as $k => $v) {
            $t = array(
                "id" => intval($v["IDPCharacter"]),
                "pseudo" => (string)utf8_encode($v["PCName"]),
                "lastConnection" => (string)utf8_encode($v["LastConnection"]),
                "isFirstConnection" =>intval($v["isFirstConnection"])
            );
            array_push($r, $t);
        }

        $finalArray["utilisateur"] = $r;
        return json_encode($finalArray);
    }

    // Création d'utilisateur dans la base
    function CreateUser($pseudo = null, $mail = null, $pass = null) {

        if ($pseudo != null && $pass != null) {
            // Connexion à la BD
            $bdd = $this->ConnectBDD();

            // Vérification données
            $mySqlData = array(
                "pseudo" => (isset($pseudo) && $pseudo != "") ? strtolower($pseudo) : null,
                "mail" => (isset($mail) && $mail != "") ? strtolower($mail) : null,
                "pass" => (isset($pass) && $pass != "") ? password_hash($pass, PASSWORD_DEFAULT) : null // Hachage du mot de passe
            );

            $sql = "SELECT DISTINCT * FROM p_character WHERE PCName = '" . $mySqlData["pseudo"] . "' UNION SELECT * FROM p_character WHERE mail = '" . $mySqlData["mail"] . "';";
            $result = $bdd->prepare($sql);
            $result->execute();
            $d = $result->fetchAll(PDO::FETCH_ASSOC);

            if (count($d) == 0) {
                $sql = "INSERT INTO p_character(PCName, mail, Password) VALUES(";
                foreach ($mySqlData as $k => $v) {
                    if ($v != null) {
                        $sql .= "'" . $v . "',";
                    }
                }
                $sql = substr($sql, 0, strlen($sql) - 1);
                $sql .= ");";

                $result = $bdd->prepare($sql);
                $result->execute();
                $lastID = $bdd->lastInsertId();

                if ($lastID > 0) {
                    $this->sendMail($mail);

                    return json_encode(array(
                        "result" => true,
                        "msg" => "Utilisateur crée avec succès",
                        "id" => $lastID
                    ));
                }
            } else {
                return json_encode(array(
                    "result" => false,
                    "msg" => "L'utilisateur n'as pas été inscrit car son pseudo ou son mail existe déjà"
                ));
            }
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Veuillez renseigner les champs spécifiés"
            ));
        }
    }

    // Vérification connexion d'un utilisateur
    function CheckConnexion($pseudo = null, $pass = null) {
        if ($pseudo != null && $pass != null) {

            // Vérification utilisateur dans la base
            $bdd = $this->ConnectBDD();

            $sql = "SELECT * FROM p_character WHERE PCName = '" . $pseudo . "'";

            $result = $bdd->prepare($sql);
            $result->execute();

            $d = $result->fetchAll(PDO::FETCH_ASSOC);

            // L'utilisateur existe
            if (count($d) > 0) {

                // On vérifie si le mot de passe correspond
                $sql2 = "SELECT Password FROM p_character WHERE PCName = '" . $pseudo . "'";

                $result2 = $bdd->prepare($sql2);
                $result2->execute();

                $d2 = $result2->fetchAll(PDO::FETCH_ASSOC);

                // Si le mot de passe correspond bien on renvoit l'utilisateur correspondant
                if (count($d2) != 0 && password_verify($pass, $d2[0]["Password"])) {
                    return $this->ParseJson($d);
                }
                else {
                    return json_encode(array(
                        "result" => false,
                        "msg" => "Le mot de passe ne correspond pas"
                    ));
                }
            }
            else {
                return json_encode(array(
                    "result" => false,
                    "msg" => "L'utilisateur renseigné n'existe pas"
                ));
            }
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Veuillez renseigner un identifiant et un mot de passe"
            ));
        }
    }

    // Changement de stat d'un joueur
    function SetPlayerStat($stat, $value, $id) {
        if ($stat != null && $value != null && $id != null) {

            if ($stat != "IDPCharacter") {
                // Vérification stat dans la base
                $bdd = $this->ConnectBDD();

                $sql = "SELECT " . $stat . " FROM p_character";

                $result = $bdd->prepare($sql);
                $result->execute();

                $d = $result->fetchAll(PDO::FETCH_ASSOC);

                // La stat à changer existe
                if (count($d) > 0) {
                    $sql = "UPDATE p_character SET " . $stat . " = '" . $value . "' WHERE IDPCharacter = " . $id . ";";
                    print $sql;
                    $result = $bdd->prepare($sql);
                    $result->execute();

                    $sql = "SELECT " . $stat . " FROM p_character WHERE " . $stat . " = '" . $value . "' AND IDPCharacter = " . $id . ";";
                    $result = $bdd->prepare($sql);
                    $result->execute();

                    $d = $result->fetchAll(PDO::FETCH_ASSOC);
                    if (count($d) > 0) {
                        return json_encode(array(
                            "result" => true,
                            "msg" => "La donnée a bien été changée"
                        ));
                    } else {
                        return json_encode(array(
                            "result" => false,
                            "msg" => "La donnée demandée n'a pas pu être changé, erreur de saisie de donnée"
                        ));
                    }
                } else {
                    return json_encode(array(
                        "result" => false,
                        "msg" => "La donnée demandée n'existe pas"
                    ));
                }
            }
            else {
                return json_encode(array(
                    "result" => false,
                    "msg" => "Impossible de changer l'id du joueur"
                ));
            }
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Veuillez renseigner les champs demandés (stat, nouvelle stat et id joueur)"
            ));
        }
    }

    // Réninitalise le mot de passe
    function reinitiatePassword($mail) {

        // Vérification mail dans la base
        $bdd = $this->ConnectBDD();

        $sql = "SELECT * FROM p_character WHERE mail = '" . $mail . "'";

        $result = $bdd->prepare($sql);
        $result->execute();

        $d = $result->fetchAll(PDO::FETCH_ASSOC);

        // L'utilisateur existe
        if (count($d) > 0) {
            return $this->sendReinitialisationMail($mail);
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Ce mail n'existe pas."
            ));
        }
    }

    // Envoi de mails
    function sendMail($destinataire) {

        ini_set( 'display_errors', 1 );
        error_reporting( E_ALL );

        $from = "contact.genius@genius.com";
        $to = $destinataire;
        $subject = "Inscription à Genius réussie !";

        $message = '<html><body>';
        $message .= '<h1>';
        $message .= "Félicitations, vous vous êtes bien inscrit sur Genius !" . "\n" . "\n";
        $message .= '</h1>';
        $message .= "L'équipe Genius";
        $message .= '</body></html>';

        $headers = "From:" . $from;
        $headers .= "MIME-Version: 1.0\r\n";
        // $headers .= "Content-Type: text/html; charset=ISO-8859-1\r\n";

        if(mail($to,$subject,$message, $headers))
        {
            echo "L'email a été envoyé !";
        }
        else
        {
            echo "L'email n'a pas été envoyé.";
        }
    }

    // Envoi de mails
    function sendReinitialisationMail($destinataire) {

        ini_set( 'display_errors', 1 );
        error_reporting( E_ALL );

        $from = "contact.genius@genius.com";
        $to = $destinataire;
        $subject = "Demande de changement de mot de passe";

        $message = "Vous avez demandé à changer votre mot de passe, veuillez le changer en cliquant sur le lien ci dessous." . "\n" . "\n";
        $message .= "Lien" . "\n" . "\n";
        $message .= "L'équipe Genius";

        $headers = "From:" . $from;
        $headers .= "MIME-Version: 1.0\r\n";
        // $headers .= "Content-Type: text/html; charset=ISO-8859-1\r\n";

        if(mail($to,$subject,$message))
        {
            return json_encode(array(
                "result" => true,
                "msg" => "Un mail vient de vous être envoyé."
            ));
        }
        else
        {
            return json_encode(array(
                "result" => false,
                "msg" => "Le mail de changement n'a pas été envoyé. Veuillez réessayer ultérieurement."
            ));
        }
    }

    // Vérifier l'IP et renvoyer le compte correspondant
    function checkIP($ip) {

        // Vérification IP dans la base
        $bdd = $this->ConnectBDD();

        $sql = "SELECT playerId FROM association_ip_pc WHERE ip = '" . $ip . "'";

        $result = $bdd->prepare($sql);
        $result->execute();

        $d = $result->fetchAll(PDO::FETCH_ASSOC);

        // L'IP existe
        if (count($d) > 0) {
            return json_encode(array(
                "result" => true,
                "msg" => $d[0]["playerId"]
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "L'IP n'a pas de compte lié"
            ));
        }
    }

    // Ajoute l'adresse IP et le compte correspondant
    function addIP($ip, $playerId) {
        if ($ip != null && $playerId != null) {
            // Connexion à la BD
            $bdd = $this->ConnectBDD();

            $sql = "SELECT * FROM association_ip_pc WHERE ip = '" . $ip . "';";
            $result = $bdd->prepare($sql);
            $result->execute();
            $d = $result->fetchAll(PDO::FETCH_ASSOC);

            // L'IP n'existe pas dans la base
            if (count($d) == 0) {
                $sql2 = "INSERT INTO association_ip_pc(ip, playerId, isConnected) VALUES('" . $ip . "', " . $playerId . ", 0)";

                $result = $bdd->prepare($sql2);
                $result->execute();

                $result = $bdd->prepare($sql);
                $result->execute();
                $d = $result->fetchAll(PDO::FETCH_ASSOC);

                if (count($d) == 0) {
                    return json_encode(array(
                        "result" => false,
                        "msg" => "L'IP n'a pas pu être relié au compte"
                    ));
                }
                else {
                    return json_encode(array(
                        "result" => true,
                        "msg" => "L'IP" . $ip . "a été relié au compte du joueur " . $playerId
                    ));
                }
            }
            // L'IP existe dans la base
            else {
                // On met à jour l'id Player de l'IP
                $sql2 = "UPDATE association_ip_pc SET playerid = " . $playerId . " WHERE ip = '" . $ip . "';";
                print $sql2;
                $result = $bdd->prepare($sql2);
                $result->execute();

                $sql = "SELECT * FROM association_ip_pc WHERE ip = '" . $ip . "' AND playerId = " . $playerId . ";";
                $result = $bdd->prepare($sql);
                $result->execute();
                $d = $result->fetchAll(PDO::FETCH_ASSOC);

                if (count($d) == 0) {
                    return json_encode(array(
                        "result" => false,
                        "msg" => "L'idPlayer est non existant, impossible de mettre à jour l'IP"
                    ));
                }
                else {
                    return json_encode(array(
                        "result" => true,
                        "msg" => "L'id " . $playerId . "a été mis-à-jour pour l'IP " . $ip
                    ));
                }
            }
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Veuillez renseigner les champs spécifiés"
            ));
        }
    }

    // Connection par id
    function ConnectById($id){
        if ($id != null) {

            // Vérification utilisateur dans la base
            $bdd = $this->ConnectBDD();

            $sql = "SELECT * FROM p_character WHERE IDPCharacter = '" . $id . "'";

            $result = $bdd->prepare($sql);
            $result->execute();

            $d = $result->fetchAll(PDO::FETCH_ASSOC);

            // L'utilisateur existe
            if (count($d) > 0) {
                return $this->ParseJson($d);
            }
            else {
                return json_encode(array(
                    "result" => false,
                    "msg" => "L'id ne correspond à aucun compte"
                ));
            }
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer l'adresse IP"
            ));
        }
    }

    // Change l'attribut isConnected pour un id de joueur donné
    function connectOnIP($connect, $ip) {
        if ($connect != null && $ip != null) {
            if ($connect == "true" || $connect == "false") {
                // Vérification dans la base
                $bdd = $this->ConnectBDD();

                $sql = "SELECT isConnected FROM association_ip_pc WHERE ip = '" . $ip . "'";

                $result = $bdd->prepare($sql);
                $result->execute();

                $d = $result->fetchAll(PDO::FETCH_ASSOC);

                // La stat à changer existe
                if (count($d) > 0) {
                    $sql = "UPDATE association_ip_pc SET isConnected = " . ($connect == "true" ? 1 : 0 ) . " WHERE ip = '" . $ip . "';";

                    $result = $bdd->prepare($sql);
                    $result->execute();

                    return json_encode(array(
                        "result" => true,
                        "msg" => "La donnée a bien été changée"
                    ));
                }
                else {
                    return json_encode(array(
                        "result" => false,
                        "msg" => "La donnée demandée n'existe pas"
                    ));
                }
            } else {
                return json_encode(array(
                    "result" => false,
                    "msg" => "Veuillez renseigner les champs demandés"
                ));
            }
        }
    }

    // Renvoie l'attribut isConnected pour un id de joueur donné
    function getConnectOnIP($ip) {
        if ($ip != null) {
            // Vérification dans la base
            $bdd = $this->ConnectBDD();

            $sql = "SELECT isConnected FROM association_ip_pc WHERE ip = '" . $ip . "'";

            $result = $bdd->prepare($sql);
            $result->execute();

            $d = $result->fetchAll(PDO::FETCH_ASSOC);

            // isConnected existe
            if (count($d) > 0) {

                return json_encode(array(
                    "result" => true,
                    "msg" => $d[0]["isConnected"]
                ));
            }
            else {
                return json_encode(array(
                    "result" => false,
                    "msg" => "isConnected n'existe pas pour cet IP"
                ));
            }
        }
    }
}