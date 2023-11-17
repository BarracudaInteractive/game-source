<?php
    //Variables to conect to database
    $servername = "localhost";
    $DbUsername = "root";
    $DbPassword = "root";
    $dbname = "userdata";

    //Create connection to database
    $conn = new mysqli($servername, $DbUsername, $DbPassword, $dbname);

    //Check connection
    if ($conn->connect_error) {
        die("1"); //1 = connection to data base failed
    }

    $UserNickname = $_POST["nickname"];
    $UserMap = $_POST["map"];
    $UserLeg = $_POST["leg"];
    $UserStage = $_POST["stage"];
    $UserTime = $_POST["time"];
    $UserDamage = $_POST["damage"];
    $UserFuel = $_POST["fuel"];

    //Querying the database to check if users nickname is already in the db
    $InsertQuery = "INSERT INTO `classiffication`(`Nickname`, `Map`, `Leg`, `Stage`, `Time`, `Damage`, `Fuel`) VALUES ('".$UserNickname."','".$UserMap."','".$UserLeg."','".$UserStage."','".$UserTime."','".$UserDamage."','".$UserFuel."')";

    try{
        $InsertResult = $conn->query($InsertQuery);

        if ($InsertResult === false){
            die("2");//2 = Query failed
                     //-- Primary key have been violated
        }
    }
    catch(Exception $e){
        die("30"); //2 = Query failed
    }

    //Success
    echo("Success At Creating User Classiffication");
    $conn->close();
?>