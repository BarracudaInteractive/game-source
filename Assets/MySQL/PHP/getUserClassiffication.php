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

    //Querying the database to check if users nickname is already in the db
    $GetQuery = "SELECT * FROM `classiffication` WHERE `Nickname` = '".$UserNickname."' AND `Map` = '".$UserMap."' AND `Leg` = '".$UserLeg."' AND `Stage` = '".$UserStage."';";

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
        for ($i = 0; $i < $GetResult->num_rows; $i++){
            $row = $GetResult->fetch_assoc();
            $data = $row["Nickname"] . " " . $row["Map"] . " " . $row["Leg"] . " " . $row["Stage"] . " " . $row["Time"] . " " . $row["Damage"] . " " . $row["Fuel"] . "\t";
        }
        echo($data);
    }
    else{
        die("55"); //55 = No user found
    }

    $conn->close();
?>