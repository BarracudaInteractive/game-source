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

    //Querying the database to check if users nickname is already in the db
    $DeleteQuery = "DELETE FROM `classiffication` WHERE `Nickname` = '".$UserNickname."'";

    try{
        $DeleteResult = $conn->query($DeleteQuery);

        if ($DeleteResult === false){
            die("2");//2 = Query failed
                     //-- Primary key have been violated
        }
    }
    catch(Exception $e){
        die("30"); //2 = Query failed
    }

    //Success
    echo("Success At Deleting User Classiffication");
    $conn->close();
?>