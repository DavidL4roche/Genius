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

                    // On affecte les ressources du joueur
                    $sql = "INSERT INTO association_ressource_pc VALUES (1," . $lastID . ", 2000)";
                    $result = $bdd->prepare($sql);
                    $result->execute();

                    $sql = "INSERT INTO association_ressource_pc VALUES (2," . $lastID . ", 2000)";
                    $result = $bdd->prepare($sql);
                    $result->execute();

                    $sql = "INSERT INTO association_ressource_pc VALUES (3," . $lastID . ", 100)";
                    $result = $bdd->prepare($sql);
                    $result->execute();

                    $sql = "INSERT INTO association_ressource_pc VALUES (4," . $lastID . ", 100)";
                    $result = $bdd->prepare($sql);
                    $result->execute();

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

    // Vérification connexion d'un utilisateur par mail
    function CheckConnexionByMail($mail = null, $pass = null) {
        if ($mail != null && $pass != null) {

            // Vérification utilisateur dans la base
            $bdd = $this->ConnectBDD();

            $sql = "SELECT * FROM p_character WHERE mail = '" . $mail . "'";

            $result = $bdd->prepare($sql);
            $result->execute();

            $d = $result->fetchAll(PDO::FETCH_ASSOC);

            // L'utilisateur existe
            if (count($d) > 0) {

                // On vérifie si le mot de passe correspond
                $sql2 = "SELECT Password FROM p_character WHERE mail = '" . $mail . "'";

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
                "msg" => "Veuillez renseigner un mail et un mot de passe"
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

    // Change la ressource renseignée pour un joueur donnée
    function SetRessources($idRessource, $idJoueur, $value) {
        if ($idRessource != null && $idJoueur != null && $value != null) {

            $sql = "UPDATE association_ressource_pc SET Value = '" . $value . "' WHERE IDRessource = " . $idRessource . " AND IDPCharacter = " . $idJoueur . ";";

            $bdd = $this->ConnectBDD();
            $result = $bdd->prepare($sql);
            $result->execute();
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Veuillez renseigner les champs demandés (idRessource, idPlayer et Valeur)"
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

    // Actualise l'attribut LastConnection d'un Joueur donné
    function updateDateCo($id) {
        if ($id != null) {
            // Vérification dans la base
            $bdd = $this->ConnectBDD();

            $sql = "UPDATE p_character SET LastConnection = '" . date("Y-m-d H:i:s") . "' WHERE IDPCharacter = " . $id;

            $result = $bdd->prepare($sql);
            $result->execute();
        }
    }

    // Reset l'admin (lui mets les valeurs de base pour test)
    function resetAdmin() {

        $id = 43;

        // On met à jour ses ressources
        $bdd = $this->ConnectBDD();
        $sql = "UPDATE association_ressource_pc SET Value = 10000 WHERE IDRessource IN (1,2) AND IDPCharacter = " . $id;
        $result = $bdd->prepare($sql);
        $result->execute();

        $sql = "UPDATE association_ressource_pc SET Value = 100 WHERE IDRessource IN (3,4) AND IDPCharacter = " . $id;
        $result = $bdd->prepare($sql);
        $result->execute();

        // On supprime ses diplomes (sauf un)
        $sql = "DELETE FROM diplom_pc WHERE IDDiplom > 1 AND IDPCharacter = " . $id;
        $result = $bdd->prepare($sql);
        $result->execute();

        // On supprime les missions qu'il a effectué
        $sql = "DELETE FROM present_missions_done WHERE IDPCharacter = " . $id;
        $result = $bdd->prepare($sql);
        $result->execute();

        // On supprime les tutos qu'il a effectué
        $sql = "DELETE FROM tuto_pc WHERE IDPCharacter = " . $id;
        $result = $bdd->prepare($sql);
        $result->execute();

        // On supprime les artéfacts qu'il a effectué
        $sql = "DELETE FROM artefact_used WHERE IDPCharacter = " . $id;
        $result = $bdd->prepare($sql);
        $result->execute();

        return "Le reset a fonctionné";
    }

    // Change le status d'un tuto
    function ChangeTutoStatus($idTuto, $idPlayer, $status) {
        if ($idTuto != null & $idPlayer != null & $status != null) {
            $bdd = $this->ConnectBDD();

            // On insert en base le status du tuto
            $sql = "INSERT INTO tuto_pc (IDTuto, IDPCharacter, Status) VALUES (" . $idTuto . ", " . $idPlayer . ", "
                . $status . ") ON DUPLICATE KEY UPDATE Status = " . $status;
            $result = $bdd->prepare($sql);
            $result->execute();

            return "Insertion status tuto en base réussie";
        }
        else {
            return "Veuillez renseigner les champs demandés";
        }
    }

    // Verifie le status d'un tuto pour un joueur
    function verifierStatusTuto($idTuto, $idPlayer) {

        if ($idTuto != null & $idPlayer != null) {
            $bdd = $this->ConnectBDD();

            // On insert en base le status du tuto
            $sql = "SELECT Status FROM tuto_pc WHERE IDPCharacter = " . $idPlayer . " AND IDTuto = " . $idTuto;
            $result = $bdd->prepare($sql);
            $result->execute();

            $d = $result->fetchAll(PDO::FETCH_ASSOC);

            // isConnected existe
            if (count($d) > 0) {
                return json_encode(array(
                    "result" => true,
                    "msg" => $d[0]["Status"]
                ));
            }
            else {
                // On insère en base le status 0 s'il n'existe pas
                $sql = "INSERT INTO tuto_pc (IDTuto, IDPCharacter, Status) VALUES (" . $idTuto . ", " . $idPlayer . ", "
                    . 0 . ") ON DUPLICATE KEY UPDATE Status = " . 0;
                $result = $bdd->prepare($sql);
                $result->execute();

                // On rappelle la fonction
                $sql = "SELECT Status FROM tuto_pc WHERE IDPCharacter = " . $idPlayer . " AND IDTuto = " . $idTuto;
                $result = $bdd->prepare($sql);
                $result->execute();

                $d = $result->fetchAll(PDO::FETCH_ASSOC);

                // isConnected existe
                if (count($d) > 0) {
                    return json_encode(array(
                        "result" => true,
                        "msg" => $d[0]["Status"]
                    ));
                }
                else {
                    return json_encode(array(
                        "result" => false,
                        "msg" => "Erreur de changement de status apres verification"
                    ));
                }
            }
        }
        else {
            return "Veuillez renseigner les champs demandés";
        }
    }

    // RESSOURCES

    // Recupère les lieux
    function RecupLieu() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from place");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les lieux"
            ));
        }
    }

    // Recupère les gains
    function RecupGain() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from gain");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les gains"
            ));
        }
    }

    // Recupère les pertes
    function RecupPerte() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from loss");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les gains"
            ));
        }
    }

    // Recupère les bonus
    function RecupBonus() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from bonus");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les bonus"
            ));
        }
    }

    // Recupère les objets
    function RecupObjet() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from item");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les objets"
            ));
        }
    }

    // Recupère les artefacts
    function RecupArtefact() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from artefact");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les artefacts"
            ));
        }
    }

    // Récupère les objets du magasin
    function RecupObjetMagasin($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT * from association_shop_item WHERE IDItem NOT IN(SELECT IDItem FROM item_bought WHERE IDPCharacter = $id)");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            if (count($dbdata) > 0) {

                return json_encode(array(
                    "result" => true,
                    "msg" => $dbdata
                ));
            }
            else {
                return json_encode(array(
                    "result" => false,
                    "msg" => "Impossible de récupérer les objets du magasin"
                ));
            }
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Recupère les durées
    function RecupDuree() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from duration");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les durées"
            ));
        }
    }

    // Recupère les rangs
    function RecupRang() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from rank");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les rangs"
            ));
        }
    }

    // Recupère les quartiers
    function RecupQuartier() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from district");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les quartiers"
            ));
        }
    }

    // Recupère les trophées
    function RecupTrophee() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from trophy");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les trophées"
            ));
        }
    }

    // Recupère les examens
    function RecupExam() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from exam");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les examens"
            ));
        }
    }

    // Recupère les entreprises
    function RecupEntreprise() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from company");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les entreprises"
            ));
        }
    }

    // Récupère les emplacements des entreprises
    function RecupEmpEntreprise($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT * from association_company_district WHERE IDCompany=$id");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            if (count($dbdata) > 0) {

                return json_encode(array(
                    "result" => true,
                    "msg" => $dbdata
                ));
            }
            else {
                return json_encode(array(
                    "result" => false,
                    "msg" => "Impossible de récupérer les emplacements de l'entreprise"
                ));
            }
        }
        else {
            return "Veuillez renseigner l'id de l'entreprise";
        }
    }

    // Récupère les spécialisations des entreprises
    function RecupSpeEntreprise($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT * from company_specialization WHERE IDCompany=$id");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            if (count($dbdata) > 0) {

                return json_encode(array(
                    "result" => true,
                    "msg" => $dbdata
                ));
            }
            else {
                return json_encode(array(
                    "result" => true,
                    "msg" => $dbdata
                ));
            }
        }
        else {
            return "Veuillez renseigner l'id de l'entreprise";
        }
    }

    // Recupère les missins
    function RecupMission() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from mission");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les missions"
            ));
        }
    }

    // Recupère les PNJ
    function RecupPNJ() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from np_character");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les missions"
            ));
        }
    }

    // Récupère les quartiers des pnj
    function RecupQuartierPNJ($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT * from association_district_npc WHERE IDNPCharacter=$id");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            if (count($dbdata) > 0) {

                return json_encode(array(
                    "result" => true,
                    "msg" => $dbdata
                ));
            }
            else {
                return json_encode(array(
                    "result" => false,
                    "msg" => "Impossible de récupérer les quartiers du pnj"
                ));
            }
        }
        else {
            return "Veuillez renseigner l'id du pnj";
        }
    }

    // Recupère les Divertissements
    function RecupDivert() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from entertainment");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les divertissements"
            ));
        }
    }

    // Recupère les Compétences
    function RecupComp() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from skill");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les compétences"
            ));
        }
    }

    // Recupère les Ressources
    function RecupRess() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from ressource");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les ressources"
            ));
        }
    }

    // Recupère les Ressources
    function RecupDiplome() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * from diplom");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les diplomes"
            ));
        }
    }

    // Recupère les Ressources
    function RecupTopicsAide() {
        //$bdd = $this->ConnectBDD();

        $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

        if ($dblink->connect_errno) {
            printf("Impossible de se connecter à la base de données.");
            exit();
        }

        $result = $dblink->query("SELECT * FROM topic WHERE category='aide' ORDER BY datePublication, idTopic DESC");

        $dbdata = array();

        while ($row = $result->fetch_assoc())  {
            $dbdata[]=$row;
        }

        if (count($dbdata) > 0) {

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return json_encode(array(
                "result" => false,
                "msg" => "Impossible de récupérer les topics"
            ));
        }
    }

    // Récupère les objets d'un joueur
    function MAJObjet($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT IDItem, Quantity FROM item_pc WHERE IDPCharacter=$id");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Récupère les compétences d'un joueur
    function MAJComp($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT * FROM skill_pc WHERE IDPCharacter=$id");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Récupère les artéfacts d'un joueur
    function MAJArtefact($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT * FROM artefact_pc WHERE IDPCharacter=$id");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Récupère les diplomes d'un joueur
    function MAJDiplome($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT * FROM diplom_pc WHERE IDPCharacter=$id");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }
            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Récupère les Resources d'un joueur
    function MAJRessources($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT * FROM association_ressource_pc WHERE IDPCharacter=$id");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Récupère les trophées d'un joueur
    function MAJTrophee($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT * FROM trophy_pc WHERE IDPCharacter=$id");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Récupère les missions d'un joueur
    function RecupPresentMissions($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT * FROM present_missions WHERE IDMission NOT IN 
                                           (SELECT IDMission from present_missions_done WHERE IDPCharacter=$id);");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            if (count($dbdata) > 0) {

                return json_encode(array(
                    "result" => true,
                    "msg" => $dbdata
                ));
            }
            else {
                return json_encode(array(
                    "result" => false,
                    "msg" => "Impossible de récupérer les missions du joueur"
                ));
            }
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Récupère les PNJ jouables
    function RecupPNJJouable($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT * FROM npc_present WHERE IDNPCharacter NOT IN 
                                            (SELECT IDNPCharacter FROM np_character WHERE IDArtefact IN 
                                            (SELECT IDArtefact FROM artefact_pc WHERE IDPCharacter = $id))");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Récupère les Examens jouables
    function RecupExamJouable($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT * from association_place_exam where IDExam NOT IN
                                            (Select IDExam from exam where IDDiplom IN 
                                            (SELECT IDDiplom from diplom_pc where IDPCharacter = $id)) 
                                            AND(IDExam IN(Select IDExam from exam where IDDiplom IN
                                            (SELECT IDDiplom from association_diploms WHERE IDDiplomRequiered IN
                                            (SELECT IDDiplom from diplom_pc WHERE IDPCharacter = $id)))OR IDExam IN
                                            (SELECT IDExam FROM exam WHERE IDDiplom NOT IN
                                            (Select IDDiplom from association_diploms) AND IDDiplom NOT IN
                                            (Select IDDiplom FROM diplom_pc WHERE IDPCharacter = $id)))");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Récupère les Divertissements jouables
    function RecupDivertJouable($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT * from association_district_entertainment WHERE IDEntertainment 
                                             NOT IN (Select IDEntertainment From entertainment_done 
                                             WHERE IDPCharacter =$id)");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Récupère les Actions Sociales (Compétences)
    function RecupActionsSocialesComp($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT * FROM action_sociale WHERE Type = 'SKILL' AND IDPCharacter=$id");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Récupère les Actions Sociales (Objets)
    function RecupActionsSocialesObjet($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT * FROM action_sociale WHERE Type = 'ITEM' AND IDPCharacter=$id");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Récupère les Artefacts jouables
    function RecupArtefactJouable($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT * from artefact WHERE IDArtefact NOT IN
                                            (SELECT IDArtefact FROM artefact_used WHERE IDPCharacter=$id) 
                                             AND IDArtefact IN(SELECT IDArtefact from artefact_pc 
                                             WHERE IDPCharacter=$id)");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Récupère la liste des joueurs
    function RecupDeLaListeDesJoueurs($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT IDPCharacter,PCName from p_character WHERE IDPCharacter!=$id");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Récupère la liste des amis du joueur
    function RecupMesAmis($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT DISTINCT IDPCharacter,PCName from p_character WHERE IDPCharacter 
                                            IN(SELECT IDPCharacter from friend WHERE IDFriend =$id) 
                                            OR IDPCharacter IN(SELECT IDFriend from friend WHERE IDPCharacter =$id) 
                                            AND IDPCharacter !=$id");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Récupère les Examens non jouables
    function RecupExamensNonJouables($id) {

        if ($id != null) {
            $dblink = new mysqli(HOST, USERNAME, PASS, DBNAME);

            if ($dblink->connect_errno) {
                printf("Impossible de se connecter à la base de données.");
                exit();
            }

            $result = $dblink->query("SELECT IDExam FROM exam WHERE IDDiplom IN 
                                           (SELECT IDDiplom FROM diplom_pc WHERE IDPCharacter = $id)");

            $dbdata = array();

            while ($row = $result->fetch_assoc())  {
                $dbdata[]=$row;
            }

            return json_encode(array(
                "result" => true,
                "msg" => $dbdata
            ));
        }
        else {
            return "Veuillez renseigner l'id du joueur";
        }
    }

    // Actualise la compétence d'un joueur
    function TransfertCompetencesEnBase($id, $idComp, $valeur) {
        if ($id != null && $idComp != null && $valeur != null) {
            // Vérification dans la base
            $bdd = $this->ConnectBDD();

            $sql = "INSERT INTO skill_pc (IDPCharacter, IDSkill, SkillLevel)
                    VALUES ($id, $idComp, $valeur)
                    ON DUPLICATE KEY UPDATE SkillLevel=$valeur";

            $result = $bdd->prepare($sql);
            $result->execute();

            return "Insertion réussie";
        }
        else {
            return "Veuillez renseigner les champs demandés";
        }
    }

    // Actualise la ressource d'un joueur
    function TransfertRessourcesEnBase($id, $idRess, $valeur) {
        if ($id != null && $idRess != null && $valeur != null) {
            // Vérification dans la base
            $bdd = $this->ConnectBDD();

            $sql = "INSERT INTO association_ressource_pc (IDPCharacter, IDRessource, Value)
                    VALUES ($id, $idRess, $valeur)
                    ON DUPLICATE KEY UPDATE Value=$valeur";

            $result = $bdd->prepare($sql);
            $result->execute();

            return "Insertion réussie";
        }
        else {
            return "Veuillez renseigner les champs demandés";
        }
    }

    // Actualise le nombre d'objets d'un joueur
    function TransfertObjetsEnBase($id, $idObjet, $valeur) {
        if ($id != null && $idObjet != null && $valeur != null) {
            // Vérification dans la base
            $bdd = $this->ConnectBDD();

            $sql = "INSERT INTO item_pc (IDPCharacter, IDItem, Quantity)
                    VALUES ($id, $idObjet, $valeur)
                    ON DUPLICATE KEY UPDATE Quantity=$valeur";

            $result = $bdd->prepare($sql);
            $result->execute();

            return "Insertion réussie";
        }
        else {
            return "Veuillez renseigner les champs demandés";
        }
    }

    // Actualise l'action sociale d'un joueur
    function TransfertActionSocialeEnBase($id, $idAmi, $type) {
        if ($id != null && $idAmi != null && $type != null) {
            // Vérification dans la base
            $bdd = $this->ConnectBDD();

            $sql = "INSERT INTO action_sociale (IDPCharacter, IDPFriend, Type)
                    VALUES ($id, $idAmi, '" . $type . "')
                    ON DUPLICATE KEY UPDATE Type= '" . $type . "'";

            $result = $bdd->prepare($sql);
            $result->execute();

            return "Insertion réussie";
        }
        else {
            return "Veuillez renseigner les champs demandés";
        }
    }
}