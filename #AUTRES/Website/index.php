<!doctype html>
<html lang="fr">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="Mark Otto, Jacob Thornton, and Bootstrap contributors">
    <meta name="generator" content="Jekyll v3.8.5">
    <title>Genius</title>

    <link rel="canonical" href="https://getbootstrap.com/docs/4.3/examples/jumbotron/">
    <link rel="icon" href="media/Logo_Genius_icononly.png"/>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css"
          integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">

    <!-- Bootstrap core CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"
          integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
</head>
<body>
<nav class="navbar navbar-expand-md navbar-dark bg-dark">
    <a title="Retourner à l'accueil" class="navbar-brand" href="#"><img src="media/Logo_Genius_icononly.png"
                                                                        class="logo mr-2">Genius</a>
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarsExampleDefault"
            aria-controls="navbarsExampleDefault" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
    </button>

    <div class="collapse navbar-collapse" id="navbarsExampleDefault">
        <ul class="navbar-nav mr-auto">
            <li class="nav-item active">
                <a class="nav-link" href="#">Accueil <span class="sr-only">(current)</span></a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="#scenario">Scénario</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="#liensUtiles">Liens utiles</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="#images">Images</a>
            </li>
        </ul>
    </div>
</nav>

<main role="main">

    <!-- Main jumbotron for a primary marketing message or call to action -->
    <div class="jumbotron rounded-0">
        <div class="container">
            <h1 class="display-3">Bienvenue sur Genius !</h1>
            <p>Genius est un jeu mobile développé par l'IUT d'Aix-Marseille afin d'aider les lycées et étudiants à
                trouver leur voie à l'IUT.</p>
            <p><a class="btn btn-primary btn-lg" href="media/Genius.apk" download="Genius.apk">Télécharger le jeu (APK) &raquo;</a></p>
        </div>
    </div>

    <a href="#"><img title="Remonter en haut de la page" class="fleche"
                     src="http://www.le-passe-nuages.com/fleche_haut.png"></a>

    <div class="container" id="scenario">
        <div class="row">
            <h2>Scénario</h2>
            <p>Bienvenue à Daedalus, une ville moderne où de nombreuses grandes entreprises se sont implantées. <br>
                Mais depuis peu, des perturbations au niveau de l'IA ont été détectées. Vous êtes donc un agent envoyé
                par Orca, une grande agence, pour investiguer ces perturbations à l'aide d'un téléphone doté d'une IA
                avancée. Les anciens agents envoyés ayant été repérés, l'agence a décidé de faire rentrer ses agents
                dans des entreprises "par le bas", en faisant des études et en obtenant les diplômes pour être
                embauchés. <br>Votre but, effectuer des missions afin d'assimiler des compétences, indispensables pour
                passer des examens et obtenir ainsi des diplômes qui vous permettront d'être embauché.</p>
        </div>
        <hr>
    </div>

    <div class="container" id="liensUtiles">
        <div class="row">
            <h2>Liens utiles</h2>
            <div class="col-md-12 mt-2">
                <div class="list-group">
                    <a target="_blank" href="https://github.com/DavidL4roche/Genius"
                       class="list-group-item list-group-item-action">
                        <img class="linkLogo mr-2" src="https://image.flaticon.com/icons/svg/25/25231.svg">GitHub
                    </a>
                    <a target="_blank" href="https://appetize.io/app/y5aegy4ecx3y73m5y3gftppmer"
                       class="list-group-item list-group-item-action">
                        <img class="linkLogo mr-2" src="https://appetize.io/images/logo1_colored_tight.png">Appetize.io
                    </a>
                    <a target="_blank" href="https://xd.adobe.com/view/93503f1d-517c-4a44-7d43-d2b8701185c4-e561/"
                       class="list-group-item list-group-item-action">
                        <img class="linkLogo mr-2"
                             src="https://upload.wikimedia.org/wikipedia/commons/thumb/c/c2/Adobe_XD_CC_icon.svg/2000px-Adobe_XD_CC_icon.svg.png">Prototype
                    </a>
                </div>
            </div>
        </div>
        <hr>
    </div>

    <div class="container" id="images">
        <h2>Images du jeu</h2>
        <div class="row">
            <div class="col-md-4 mt-2">
                <div class="card mb-4 shadow-sm">
                    <img class="w-100 m-0 rounded" src="media/menu.PNG">
                    <div class="card-body">
                        <p class="card-text">Voici l'écran principal, votre téléphone. Vous pouvez avoir accès à la
                            carte (<span class="font-italic">Daedalus</span>), à votre profil et à d'autres menus utiles
                            du jeu.</p>
                        <div class="d-flex justify-content-between align-items-center">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4 mt-2">
                <div class="card mb-4 shadow-sm">
                    <img class="w-100 m-0 rounded" src="media/daedalus.PNG">
                    <div class="card-body">
                        <p class="card-text">Voici <span class="font-italic">Daedalus</span>, la ville principale du
                            jeu. Vous pouvez accéder à tout les
                            quartiers présents pour effectuer des missions ou passer des examens.</p>
                        <div class="d-flex justify-content-between align-items-center">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4 mt-2">
                <div class="card mb-4 shadow-sm">
                    <img class="w-100 m-0 rounded" src="media/quartier.PNG">
                    <div class="card-body">
                        <p class="card-text">Voici le quartier Administration. Plusieurs missions sont présentes. Leur
                            rang indique que ces missions demandent plus ou moins de ressources et de compétences.</p>
                        <div class="d-flex justify-content-between align-items-center">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <p class="text-center">
            <a href="https://xd.adobe.com/view/93503f1d-517c-4a44-7d43-d2b8701185c4-e561/"
               class="btn btn-outline-primary mx-auto" target="_blank">Voir plus</a>
        </p>
    </div>
    <hr>

    <div class="container" id="infosUtiles">
        <div class="row">
            <h2>Informations utiles</h2>
            <p>
                <span class="font-italic">Genius</span> a été developpé par David LAROCHE et Nathan BERNARD en 2019 dans
                le cadre d'un projet d'alternance en Licence Professionnelle (Web Dev).
            </p>
        </div>
        <hr>
    </div>
</main>

<footer class="footer mt-auto py-3">
    <div class="container text-center">
        <span class="text-muted">&copy; Genius - 2019</span>
    </div>
</footer>

<style>
    .jumbotron {
        background: linear-gradient(to left, transparent, black), url("media/daedalus2.png") no-repeat;
        background-size: 100%;
        color: white;
        text-shadow: 0 0 7px rgba(0, 0, 0, 0.5);
    }

    .logo {
        width: 50px;
    }

    .linkLogo {
        width: 20px;
    }

    .fleche {
        position: fixed;
        right: 10px;
        bottom: 17px;
        width: 40px;
    }
</style>
<script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"
        integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo"
        crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"
        integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1"
        crossorigin="anonymous"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"
        integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM"
        crossorigin="anonymous"></script>
</html>
