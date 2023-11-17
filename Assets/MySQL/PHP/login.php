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
    $UserPassword = MD5($_POST["password"]);

    //Querying the database to check if users nickname is already in the db
    $LoginQuery = "SELECT `Nickname` FROM `users` WHERE `Nickname` = '".$UserNickname."' AND `Password` = '".$UserPassword."';";

    try{
        $LoginResult = $conn->query($LoginQuery);

        if ($LoginResult === false){
            die("2");//2 = Query failed
                     //-- Primary key have been violated
        }
    }
    catch(Exception $e){
        die("30"); //2 = Query failed
    }

    if ($LoginResult->num_rows > 0){
        //Echo org name
        echo("Success");
    }
    else{
        die("55"); //55 = No user found
    }

    $conn->close();
?>