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

    //Querying the database to check if users nickname is already in the db
    $GetQuery = "SELECT `Nickname` FROM `users` WHERE 1;";

    try{
        $GetResult = $conn->query($GetQuery);

        if ($GetResult === false){
            die("2");//2 = Query failed
                     //-- Primary key have been violated
        }
    }
    catch(Exception $e){
        die("30"); //2 = Query failed
    }

    if ($GetResult->num_rows > 0){
        $data = "";
        for ($i = 0; $i < $GetResult->num_rows; $i = $i + 1){
            $row = $GetResult->fetch_array();
            $data = $data . $row["Nickname"] . "\t";
        }

        echo($data);
    }
    else{
        die("55"); //55 = No user found
    }

    $conn->close();
?>