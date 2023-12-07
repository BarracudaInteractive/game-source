using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAssets
{
    //Buscar todos los textos con 
    //t: TextMeshProUGUI si es TMP en la escena,
    //t: Text si es Text en la escena
    
    //Menu Texts

    private string _MenuAgeE = "Age";
    private string _MenuAgeS = "Edad";

    private string _MenuAgeSelectionE = " y/o";
    private string _MenuAgeSelectionS = " años";

    private string _MenuApplayE = "APPLY";
    private string _MenuApplayS = "APLICAR";

    private string _MenuContinueE = "CLICK TO CONTINUE";
    private string _MenuContinueS = "HAGA CLICK PARA CONTINUAR";

    private string _MenuContinueLIE = "CONTINUE";
    private string _MenuContinueLIS = "CONTINUAR";

    private string _MenuContinueSIE = "CONTINUE";
    private string _MenuContinueSIS = "CONTINUAR";

    private string _MenuCreditsE = "CREDITS";
    private string _MenuCreditsS = "CRÉDITOS";

    private string _MenuCredits1E = "King Juan Carlos University's video game development team ";
    private string _MenuCredits1S = "Equipo de desarrollo de videojuegos de la Universidad Rey Juan Carlos";

    private string _MenuCredits2E = "King Juan Carlos University's video game development team ";
    private string _MenuCredits2S = "Equipo de desarrollo de videojuegos de la Universidad Rey Juan Carlos";

    private string _MenuCredits_1E = "Credits";
    private string _MenuCredits_1S = "Créditos";

    private string _MenuCreditsMembers1E = "Rubio \r\nGarrido,\r\nAdrian";
    private string _MenuCreditsMembers1S = "Rubio \r\nGarrido,\r\nAdrián";

    private string _MenuCreditsMembers2E = "Rubio \r\nGarrido,\r\nAdrian";
    private string _MenuCreditsMembers2S = "Rubio \r\nGarrido,\r\nAdrián";

    private string _MenuCreditsMembers3E = "Martín\r\nHita,\r\nÁlvaro";
    private string _MenuCreditsMembers3S = "Martín\r\nHita,\r\nÁlvaro";

    private string _MenuCreditsMembers4E = "Hernandez\r\nTamayo,\r\nDaniel";
    private string _MenuCreditsMembers4S = "Hernández\r\nTamayo,\r\nDaniel";

    private string _MenuCreditsMembers5E = "Martinez\r\nGamero,\r\nEric";
    private string _MenuCreditsMembers5S = "Martínez\r\nGamero,\r\nEric";

    private string _MenuCreditsMembers6E = "Montes\r\nVeredas,\r\nSergio";
    private string _MenuCreditsMembers6S = "Montes\r\nVeredas,\r\nSergio";

    private string _MenuEffectsE = "Effects";
    private string _MenuEffectsS = "Efectos";

    private string _MenuErrorLogLIE = "Err: incorrect user or password";
    private string _MenuErrorLogLIS = "Err: usuario o contraseña incorrecta";

    private string _MenuErrorLogSIE = "Err: empty user or password";
    private string _MenuErrorLogSIS = "Err: usuario o contraseña vacio";

    private string _MenuExitGameE = "Do you wish to leave the game?";
    private string _MenuExitGameS = "¿Desea abandonar el juego?";

    private string _MenuExitLanguageE = "Do you wish to leave the game?";
    private string _MenuExitLanguageS = "¿Desea abandonar el juego?";

    private string _MenuExitLIE = "Do you wish to leave the game?";
    private string _MenuExitLIS = "¿Desea abandonar el juego?";

    private string _MenuExitNoGameE = "NO!";
    private string _MenuExitNoGameS = "¡NO!";

    private string _MenuExitNoLanguageE = "NO!";
    private string _MenuExitNoLanguageS = "¡NO!";

    private string _MenuExitNoLIE = "NO!";
    private string _MenuExitNoLIS = "¡NO!";

    private string _MenuExitNoSelectionE = "NO!";
    private string _MenuExitNoSelectionS = "¡NO!";

    private string _MenuExitNoSIE = "NO!";
    private string _MenuExitNoSIS = "¡NO!";

    private string _MenuExitSelectionE = "Do you wish to leave the game?";
    private string _MenuExitSelectionS = "¿Desea abandonar el juego?";

    private string _MenuExitSIE = "Do you wish to leave the game?";
    private string _MenuExitSIS = "¿Desea abandonar el juego?";

    private string _MenuExitYesGameE = "SURE";
    private string _MenuExitYesGameS = "SI";

    private string _MenuExitYesLanguageE = "SURE";
    private string _MenuExitYesLanguageS = "SI";

    private string _MenuExitYesLIE = "SURE";
    private string _MenuExitYesLIS = "SI";

    private string _MenuExitYesSelectionE = "SURE";
    private string _MenuExitYesSelectionS = "SI";

    private string _MenuExitYesSIE = "SURE";
    private string _MenuExitYesSIS = "SI";

    private string _MenuItemLabelSettingsE = "Option A";
    private string _MenuItemLabelSettingsS = "Opción A";

    private string _MenuItemLabelSIE = "Option A";
    private string _MenuItemLabelSIS = "Opción A";

    private string _MenuLabelSettingsE = "Male";
    private string _MenuLabelSettingsS = "Masculino";

    private string _MenuLabelSIE = "Male";
    private string _MenuLabelSIS = "Masculino";

    private string _MenuLanguageE = "Language";
    private string _MenuLanguageS = "Idioma";

    private string _MenuLanguageTextE = "Select language";
    private string _MenuLanguageTextS = "Seleccione idioma";

    private string _MenuLogInE = "LOG IN";
    private string _MenuLogInS = "INICIAR SESION";

    private string _MenuMusicE = "Music";
    private string _MenuMusicS = "Música";

    private string _MenuNameLIE = "Enter your username";
    private string _MenuNameLIS = "Introduzca su usuario";

    private string _MenuNameSIE = "Please, enter a name";
    private string _MenuNameSIS = "Por favor, introduzca un nombre";

    private string _MenuPasswordLIE = "Enter your password";
    private string _MenuPasswordLIS = "Introduzca su contraseña";

    private string _MenuPasswordSIE = "Please, create a password";
    private string _MenuPasswordSIS = "Por favor, cree una cintraseña";

    private string _MenuSelectionE = "Is this your first time playing Rally Team Tactics, or do you already have an account?";
    private string _MenuSelectionS = "¿Es tu primera vez jugando Rally Team Tactics, o ya tienes una cuenta?";

    private string _MenuSelection1TE = "FIRST TIME";
    private string _MenuSelection1TS = "PRIMERA VEZ";

    private string _MenuSettingsE = "Settings";
    private string _MenuSettingsS = "Ajustes";

    private string _MenuSettingsFullScreenE = "Full Screen";
    private string _MenuSettingsFullScreenS = "Pantalla Completa";

    private string _MenuSexE = "Sex";
    private string _MenuSexS = "Sexo";

    private string _MenuSoundE = "Sound";
    private string _MenuSoundS = "Sonido";

    //Prefs Texts

    private string _PrefsApplayE = "APPLAY";
    private string _PrefsApplayS = "APLICAR";

    private string _PrefsBeginStageE = "BEGIN";
    private string _PrefsBeginStageS = "COMENZAR";

    private string _PrefsCreditsE = "CREDITS";
    private string _PrefsCreditsS = "CRÉDITOS";

    private string _PrefsCredits1E = "King Juan Carlos University's video game development team ";
    private string _PrefsCredits1S = "Equipo de desarrollo de videojuegos de la Universidad Rey Juan Carlos";

    private string _PrefsCredits_1E = "Credits";
    private string _PrefsCredits_1S = "Créditos";

    private string _PrefsFullScreenE = "Full Screen";
    private string _PrefsFullScreenS = "Pantalla Completa";

    private string _PrefsCreditsMembers1E = "Rubio \r\nGarrido,\r\nAdrian";
    private string _PrefsCreditsMembers1S = "Rubio \r\nGarrido,\r\nAdrián";

    private string _PrefsCreditsMembers2E = "Rubio \r\nGarrido,\r\nAdrian";
    private string _PrefsCreditsMembers2S = "Rubio \r\nGarrido,\r\nAdrián";

    private string _PrefsCreditsMembers3E = "Martín\r\nHita,\r\nÁlvaro";
    private string _PrefsCreditsMembers3S = "Martín\r\nHita,\r\nÁlvaro";

    private string _PrefsCreditsMembers4E = "Hernandez\r\nTamayo,\r\nDaniel";
    private string _PrefsCreditsMembers4S = "Hernández\r\nTamayo,\r\nDaniel";

    private string _PrefsCreditsMembers5E = "Martinez\r\nGamero,\r\nEric";
    private string _PrefsCreditsMembers5S = "Martínez\r\nGamero,\r\nEric";

    private string _PrefsCreditsMembers6E = "Montes\r\nVeredas,\r\nSergio";
    private string _PrefsCreditsMembers6S = "Montes\r\nVeredas,\r\nSergio";

    private string _PrefsDescriptionE = "SUNBURST RACER\r\nEric Strom's first rally car, a classic that despite its heavy weight, maintains an excellent balance between handling and speed";
    private string _PrefsDescriptionS = "SUNBURST RACER\r\nEl primer coche de rally de Eric Storm, un clasico que, a pesar de su alto peso, mantiene un equilibrio excelente entre manejo y velocidad";

    private string _PrefsEffectsE = "Effects";
    private string _PrefsEffectsS = "Efectos";

    private string _PrefsEventNameE = "Wildfire Rally\r\nExpedition";
    private string _PrefsEventNameS = "Wildfire Rally\r\nExpedición";

    private string _PrefsEventName1E = "Wildfire Rally\r\nExpedition";
    private string _PrefsEventName1S = "Wildfire Rally\r\nExpedición";

    private string _PrefsExitE = "Do you wish to leave the game?";
    private string _PrefsExitS = "¿Desea abandonar el juego?";

    private string _PrefsExitLegE = "Do you wish to leave the game?";
    private string _PrefsExitLegS = "¿Desea abandonar el juego?";

    private string _PrefsExitNoE = "NO!";
    private string _PrefsExitNoS = "¡NO!";

    private string _PrefsExitNoLegE = "NO!";
    private string _PrefsExitNoLegS = "¡NO!";

    private string _PrefsExitNoStageE = "NO!";
    private string _PrefsExitNoStageS = "¡NO!";

    private string _PrefsExitNoVehicleE = "NO!";
    private string _PrefsExitNoVehicleS = "¡NO!";

    private string _PrefsExitStageE = "Do you wish to leave the game?";
    private string _PrefsExitStageS = "¿Desea abandonar el juego?";

    private string _PrefsExitVehicleE = "Do you wish to leave the game?";
    private string _PrefsExitVehicleS = "¿Desea abandonar el juego?";

    private string _PrefsExitYesE = "SURE";
    private string _PrefsExitYesS = "SI";

    private string _PrefsExitYesLegE = "SURE";
    private string _PrefsExitYesLegS = "SI";

    private string _PrefsExitYesStageE = "SURE";
    private string _PrefsExitYesStageS = "SI";

    private string _PrefsExitYesVehicleE = "SURE";
    private string _PrefsExitYesVehicleS = "SI";

    private string _PrefsItemLabelE = "Option A";
    private string _PrefsItemLabelS = "Opción A";

    private string _PrefsLabelE = "English";
    private string _PrefsLabelS = "Español";

    private string _PrefsLanguageE = "Language";
    private string _PrefsLanguageS = "Idioma";

    private string _PrefsMusicE = "Music";
    private string _PrefsMusicS = "Música";

    private string _PrefsPlayLegE = "PLAY";
    private string _PrefsPlayLegS = "JUGAR";

    private string _PrefsProfileE = "PROFILE";
    private string _PrefsProfileS = "PERFIL";

    private string _PrefsSelectE = "SELECT";
    private string _PrefsSelectS = "SELECCIONE";

    private string _PrefsSoundE = "Sound";
    private string _PrefsSpundS = "Sonido";

    private string _PrefsStageName1E = "Day 1 - Morning\r\nAurora Ridge Stage";
    private string _PrefsStageName1S = "Día 1 - Mañana\r\nEtapa de la Puesta Boreal";

    private string _PrefsStageName2E = "Day 1 - Night\r\nWhispering Pine Stage";
    private string _PrefsStageName2S = "Día 1 - Noche\r\nEtapa del Pino Susurrante";

    private string _PrefsStageName3E = "Day 2 - Morning\r\nSeraph's Summit Stage";
    private string _PrefsStageName3S = "Día 2 - Mañana\r\nEtapa de la Cumbre de Seraph";

    private string _PrefsStageWorking1E = "CURRENTLY IN \r\nDEVELOPMENT\r\nRedirects to: \r\nDay 1 - Morning";
    private string _PrefsStageWorking1S = "ACTUALMENTE EN \r\nDESARROLLO\r\nRedirige a: \r\nDia 1 - Mañana";

    private string _PrefsStageWorking2E = "CURRENTLY IN \r\nDEVELOPMENT\r\nRedirects to: \r\nDay 1 - Morning";
    private string _PrefsStageWorking2S = "ACTUALMENTE EN \r\nDESARROLLO\r\nRedirige a: \r\nDia 1 - Mañana";

    private string _PrefsStartE = "START";
    private string _PrefsStartS = "EMPEZAR";

    private string _PrefsVelTextE = "Velocity Tread Leg";
    private string _PrefsVelTextS = "Tramo de Rodadura de Velocidad";

    //Day1M Texts

    private string _Day1M1E = "I";
    private string _Day1M1S = "I";

    private string _Day1M2E = "II";
    private string _Day1M2S = "II";

    private string _Day1M3E = "III";
    private string _Day1M3S = "III";

    private string _Day1M4E = "IV";
    private string _Day1M4S = "IV";

    private string _Day1M5E = "V";
    private string _Day1M5S = "V";

    private string _Day1M6E = "VI";
    private string _Day1M6S = "VI";

    private string _Day1MBackE = "BACK";
    private string _Day1MBackS = "ATRÁS";

    private string _Day1MBack1E = "BACK";
    private string _Day1MBack1S = "ATRÁS";

    private string _Day1MBack2E = "BACK";
    private string _Day1MBack2S = "ATRÁS";

    private string _Day1MBeforeE = "BEFORE PRESSING PLAY";
    private string _Day1MBeforeS = "ANTES DE PULSAR JUGAR";

    private string _Day1MChangeE = "CHANGE CAMERA";
    private string _Day1MChangeS = "CAMBIAR CÁMARA";

    private string _Day1MCheckpointE = "CHECKPOINT";
    private string _Day1MCheckpointS = "PUNTO DE CONTROL";

    private string _Day1MCongratsE = "CONGRATULATIONS!";
    private string _Day1MCongratsS = "FELICIDADES";

    private string _Day1MContinueE = "CONTINUE";
    private string _Day1MContinueS = "Continuar";

    private string _Day1MContinue1E = "CONTINUE";
    private string _Day1MContinue1S = "Continuar";

    private string _Day1MContinue2E = "CONTINUE";
    private string _Day1MContinue2S = "Continuar";

    private string _Day1MContinue3E = "CONTINUE";
    private string _Day1MContinue3S = "Continuar";

    private string _Day1MCreditsE = "CREDITS";
    private string _Day1MCreditsS = "CRÉDITOS";

    private string _Day1MCredits1E = "King Juan Carlos University's video game development team ";
    private string _Day1MCredits1S = "Equipo de desarrollo de videojuegos de la Universidad Rey Juan Carlos";

    private string _Day1MCredits3E = "Credits";
    private string _Day1MCredits3S = "Créditos";

    private string _Day1MCreditsMembers1E = "Rubio \r\nGarrido,\r\nAdrian";
    private string _Day1MCreditsMembers1S = "Rubio \r\nGarrido,\r\nAdrián";

    private string _Day1MCreditsMembers2E = "Rubio \r\nGarrido,\r\nAdrian";
    private string _Day1MCreditsMembers2S = "Rubio \r\nGarrido,\r\nAdrián";

    private string _Day1MCreditsMembers3E = "Martín\r\nHita,\r\nÁlvaro";
    private string _Day1MCreditsMembers3S = "Martín\r\nHita,\r\nÁlvaro";

    private string _Day1MCreditsMembers4E = "Hernandez\r\nTamayo,\r\nDaniel";
    private string _Day1MCreditsMembers4S = "Hernández\r\nTamayo,\r\nDaniel";

    private string _Day1MCreditsMembers5E = "Martinez\r\nGamero,\r\nEric";
    private string _Day1MCreditsMembers5S = "Martínez\r\nGamero,\r\nEric";

    private string _Day1MCreditsMembers6E = "Montes\r\nVeredas,\r\nSergio";
    private string _Day1MCreditsMembers6S = "Montes\r\nVeredas,\r\nSergio";

    private string _Day1MDamageE = "D\r\nA\r\nM\r\nA\r\nG\r\nE";
    private string _Day1MDamageS = "D\r\nA\r\nÑ\r\nO";

    private string _Day1MDefeatTextE = "";
    private string _Day1MDefeatTextS = "";

    private string _Day1MDesc1E = "Click a SECTION to choose TEMPER and PACENOTE for its CHECKPOINTS";
    private string _Day1MDesc1S = "Seleccione una SECCIÓN para elegir el TEMPERAMENTO y TIPO DE CURVA para cada PUNTO DE CONTROL";

    private string _Day1MDesc2E = "You can PAUSE and SPEED UP. Losing all FUEL/reaching max DAMAGE is a loss";
    private string _Day1MDesc2S = "Puedes PAUSAR y ACELERAR. Gastar todo el COMBUSTIBLE/alcanzar el maximo DAÑO siginifica perder";

    private string _Day1MDesc3E = "Welcome! In RTT, you play as a co-pilot. You can move freely through the stage";
    private string _Day1MDesc3S = "¡Bienvenido! En RTT juegas como un copiloto. Puedes moverte libremente por el escenario";

    private string _Day1MDesc4E = "Select checkpoints marked in RED before starting. They will then turn GREEN";
    private string _Day1MDesc4S = "Seleccione los puntos de control marcados en ROJO antes de empezar. Se pondrán VERDE";

    private string _Day1MDesc5E = "SOME CHECKPOINTS ARE NOT SELECTED!\r\nLOCATE THE RED AURA ABOVE THEM";
    private string _Day1MDesc5S = "ALGUNOS PUNTOS DE CONTROL NO ESTAN SEECCIONADOS\r\nLOCALICE EL AURA ROJA ENCIMA DE ELLOS";

    private string _Day1MEffectsE = "Effects";
    private string _Day1MEffectsS = "Efectos";

    private string _Day1MExitE = "EXIT\t";
    private string _Day1MExitS = "SALIR\t";

    private string _Day1MExit1E = "EXIT\t";
    private string _Day1MExit1S = "SALIR\t";

    private string _Day1MExPacenoteE = "CHECKPOINT 4\r\nEXCELLENT PACENOTE, BUT POOR ATTITUDE";
    private string _Day1MExPacenoteS = "PUNTO DE CONTROL 4\r\nEXCELENTE RUTA, PERO ACTITUD POBRE";

    private string _Day1MFuelE = "F\r\nU\r\nE\r\nL";
    private string _Day1MFuelS = "C\r\nO\r\nM\r\nB\r\nU\r\nS\r\nT\r\nI\r\nB\r\nL\r\nE";

    private string _Day1MFullScreenE = "Full Screen";
    private string _Day1MFullScreenS = "Pantalla Completa";

    private string _Day1MGearE = "GEAR: 0";
    private string _Day1MGearS = "Marcha: 0";

    private string _Day1MGOTextE = "GAME OVER";
    private string _Day1MGOTextS = "HAS PERDIDO";

    private string _Day1MHUDE = "INGAME HUD";
    private string _Day1MHUDS = "HUD IN GAME";

    private string _Day1MIndicatorsE = "INDICATORS";
    private string _Day1MIndicatorsS = "INDICADORES";

    private string _Day1MItemLabelE = "Option A";
    private string _Day1MItemLabelS = "Opción A";

    private string _Day1MKPHE = "KPH: 00";
    private string _Day1MKPHS = "KPH: 00";

    private string _Day1MLabelE = "English";
    private string _Day1MLabelS = "Español";

    private string _Day1MLanguageE = "Language";
    private string _Day1MLanguageS = "Idioma";

    private string _Day1MLoadE = "Load last \r\ngame data?";
    private string _Day1MLoadS = "¿Cargar última \r\npartida guardada?";

    private string _Day1MMoveE = "MOVE";
    private string _Day1MMoveS = "MOVER";

    private string _Day1MMusicE = "Music";
    private string _Day1MMusicS = "Música";

    private string _Day1MNextE = "Next: ";
    private string _Day1MNextS = "Siguiente: ";

    private string _Day1MNextStageE = "Day 1 - Night\r\nWhispering Pine Stage";
    private string _Day1MNextStageS = "Día 1 - Night\r\nEstapa del Pino Susurrante";

    private string _Day1MNum1E = "1/4";
    private string _Day1MNum1S = "1/4";

    private string _Day1MNum2E = "2/4";
    private string _Day1MNum2S = "2/4";

    private string _Day1MNum3E = "3/4";
    private string _Day1MNum3S = "3/4";

    private string _Day1MNum4E = "4/4";
    private string _Day1MNum4S = "4/4";

    private string _Day1MPlayE = "PLAY!";
    private string _Day1MPlayS = "¡JUGAR!";

    private string _Day1MR0E = "Name\t\tTime \tFuel \tDamage";
    private string _Day1MR0S = "Nombre\t\tTiempo \tCombustible \tDaño";

    private string _Day1MR1DE = "";
    private string _Day1MR1DS = "";

    private string _Day1MR1FE = "";
    private string _Day1MR1FS = "";

    private string _Day1MR1NE = "";
    private string _Day1MR1NS = "";

    private string _Day1MR1TE = "";
    private string _Day1MR1TS = "";

    private string _Day1MR2DE = "";
    private string _Day1MR2DS = "";

    private string _Day1MR2FE = "";
    private string _Day1MR2FS = "";

    private string _Day1MR2NE = "";
    private string _Day1MR2NS = "";

    private string _Day1MR2TE = "";
    private string _Day1MR2TS = "";

    private string _Day1MR3DE = "";
    private string _Day1MR3DS = "";

    private string _Day1MR3FE = "";
    private string _Day1MR3FS = "";

    private string _Day1MR3NE = "";
    private string _Day1MR3NS = "";

    private string _Day1MR3TE = "";
    private string _Day1MR3TS = "";

    private string _Day1MR4DE = "";
    private string _Day1MR4DS = "";

    private string _Day1MR4FE = "";
    private string _Day1MR4FS = "";

    private string _Day1MR4NE = "";
    private string _Day1MR4NS = "";

    private string _Day1MR4TE = "";
    private string _Day1MR4TS = "";

    private string _Day1MR5DE = "";
    private string _Day1MR5DS = "";

    private string _Day1MR5FE = "";
    private string _Day1MR5FS = "";

    private string _Day1MR5NE = "";
    private string _Day1MR5NS = "";

    private string _Day1MR5TE = "";
    private string _Day1MR5TS = "";

    private string _Day1MRecordNoE = "NAH";
    private string _Day1MRecordNoS = "NAH";

    private string _Day1MRecordYesE = "yes!";
    private string _Day1MRecordYesS = "¡si!";

    private string _Day1MResultsE = "RESULTS";
    private string _Day1MResultsS = "RESULTADOS";

    private string _Day1MReturnE = "RETURN TO MENU";
    private string _Day1MReturnS = "VOLVER AL MENU";

    private string _Day1MRotateE = "ROTATE";
    private string _Day1MRotateS = "ROTAR";

    private string _Day1MSecondsE = "Seconds";
    private string _Day1MSecondsS = "Segundos";

    private string _Day1MSectionE = "SECTION HAS CHECKPOINTS";
    private string _Day1MSectionS = "LA SECCIÓN TIENE PUNTOS DE CONTROL";

    private string _Day1MSection1E = "SECTION";
    private string _Day1MSection1S = "SECCIÓN";

    private string _Day1MSelectionNameE = "CHECKPOINT X";
    private string _Day1MSelectionNameS = "PUNTO DE CONTROL X";

    private string _Day1MSettingsE = "Settings";
    private string _Day1MSettingsS = "Ajustes";

    private string _Day1MSNameE = "T\r\nE\r\nM\r\nP\r\nE\r\nR";
    private string _Day1MSNameS = "T\r\nE\r\nM\r\nP\r\nE\r\nR\r\nA\r\nM\r\nE\r\nN\r\nT\r\nO";

    private string _Day1MSoundE = "Sound";
    private string _Day1MSoundS = "Sonido";

    private string _Day1MStatsE = "STATS";
    private string _Day1MStatsS = "ESTADÍSTICAS";

    private string _Day1MSubmitE = "SUBMIT";
    private string _Day1MSumbitS = "ENTREGAR";

    private string _Day1MTimeTextE = "GAME OVER";
    private string _Day1MTimeTextS = "HAS PERDIDO";

    private string _Day1MTutorialE = "TUTORIAL";
   
    //Day1N Texts

    private string _Day1NBackE = "BACK";
    private string _Day1NBackS = "ATRÁS";

    private string _Day1NBack1E = "BACK";
    private string _Day1NBack1S = "ATRÁS";

    private string _Day1NBack2E = "BACK";
    private string _Day1NBack2S = "ATRÁS";

    private string _Day1NBeforeE = "BEFORE PRESSING PLAY";
    private string _Day1NBeforeS = "ANTES DE PULSAR JUGAR";

    private string _Day1NChangeE = "CHANGE CAMERA";
    private string _Day1NChangeS = "CAMBIAR CÁMARA";

    private string _Day1NCheckpointE = "CHECKPOINT";
    private string _Day1NCheckpointS = "PUNTO DE CONTROL";

    private string _Day1NCongratsE = "CONGRATULATIONS!";
    private string _Day1NCongratsS = "FELICIDADES";

    private string _Day1NContinueE = "CONTINUE";
    private string _Day1NContinueS = "Continuar";

    private string _Day1NContinue1E = "CONTINUE";
    private string _Day1NContinue1S = "Continuar";

    private string _Day1NContinue2E = "CONTINUE";
    private string _Day1NContinue2S = "Continuar";

    private string _Day1NContinue3E = "CONTINUE";
    private string _Day1NContinue3S = "Continuar";

    private string _Day1NDamageE = "D\r\nA\r\nM\r\nA\r\nG\r\nE";
    private string _Day1NDamageS = "D\r\nA\r\nÑ\r\nO";

    private string _Day1NDefeatTextE = "";
    private string _Day1NDefeatTextS = "";

    private string _Day1NDesc1E = "Click a SECTION to choose TEMPER and PACENOTE for its CHECKPOINTS";
    private string _Day1NDesc1S = "Seleccione una SECCIÓN para elegir el TEMPERAMENTO y TIPO DE CURVA para cada PUNTO DE CONTROL";
    
    private string _Day1NDesc2E = "You can PAUSE and SPEED UP. Losing all FUEL/reaching max DAMAGE is a loss";
    private string _Day1NDesc2S = "Puedes PAUSAR y ACELERAR. Gastar todo el COMBUSTIBLE/alcanzar el maximo DAÑO siginifica perder";
    
    private string _Day1NDesc3E = "Welcome! In RTT, you play as a co-pilot. You can move freely through the stage";
    private string _Day1NDesc3S = "¡Bienvenido! En RTT juegas como un copiloto. Puedes moverte libremente por el escenario";
    
    private string _Day1NDesc4E = "Select checkpoints marked in RED before starting. They will then turn GREEN";
    private string _Day1NDesc4S = "Seleccione los puntos de control marcados en ROJO antes de empezar. Se pondrán VERDE";
    
    private string _Day1NDesc5E = "SOME CHECKPOINTS ARE NOT SELECTED!\r\nLOCATE THE RED AURA ABOVE THEM";
    private string _Day1NDesc5S = "ALGUNOS PUNTOS DE CONTROL NO ESTAN SEECCIONADOS\r\nLOCALICE EL AURA ROJA ENCIMA DE ELLOS";
    
    private string _Day1NEffectsE = "Effects";
    private string _Day1NEffectsS = "Efectos";

    private string _Day1NExitE = "EXIT\t";
    private string _Day1NExitS = "SALIR\t";

    private string _Day1NExit1E = "EXIT\t";
    private string _Day1NExit1S = "SALIR\t";

    private string _Day1NExPacenoteE = "CHECKPOINT 4\r\nEXCELLENT PACENOTE, BUT POOR ATTITUDE";
    private string _Day1NExPacenoteS = "PUNTO DE CONTROL 4\r\nEXCELENTE RUTA, PERO ACTITUD POBRE";

    private string _Day1NFuelE = "F\r\nU\r\nE\r\nL";
    private string _Day1NMFuelS = "C\r\nO\r\nM\r\nB\r\nU\r\nS\r\nT\r\nI\r\nB\r\nL\r\nE";

    private string _Day1NGearE = "GEAR: 0";
    private string _Day1NGearS = "Marcha: 0";

    private string _Day1NGOTextE = "GAME OVER";
    private string _Day1NGOTextS = "HAS PERDIDO";

    private string _Day1NHeaderE = "TIME";
    private string _Day1NHeaderS = "TIEMPO";

    private string _Day1NHUDE = "INGAME HUD";
    private string _Day1NHUDS = "HUD IN GAME";

    private string _Day1NIndicatorsE = "INDICATORS";
    private string _Day1NIndicatorsS = "INDICADORES";

    private string _Day1NItemLabelE = "Option A";
    private string _Day1NItemLabelS = "Opción A";

    private string _Day1NKPHE = "KPH: 00";
    private string _Day1NKPHS = "KPH: 00";

    private string _Day1NLabelE = "English";
    private string _Day1NLabelS = "Español";

    private string _Day1NLanguageE = "Language";
    private string _Day1NLanguageS = "Idioma";

    private string _Day1NLoadE = "Load last \r\ngame data?";
    private string _Day1NLoadS = "¿Cargar última \r\npartida guardada?";

    private string _Day1NMoveE = "MOVE";
    private string _Day1NMoveS = "MOVER";

    private string _Day1NMusicE = "Music";
    private string _Day1NMusicS = "Música";

    private string _Day1NNextE = "Next: ";
    private string _Day1NNextS = "Siguiente: ";

    private string _Day1NNextStageE = "Day 1 - Night\r\nWhispering Pine Stage";
    private string _Day1NNextStageS = "Día 1 - Night\r\nEstapa del Pino Susurrante";

    private string _Day1MNullE = "";
    private string _Day1MNullS = "";

    private string _Day1NNum1E = "1/4";
    private string _Day1NNum1S = "1/4";

    private string _Day1NNum2E = "2/4";
    private string _Day1NNum2S = "2/4";

    private string _Day1NNum3E = "3/4";
    private string _Day1NNum3S = "3/4";

    private string _Day1NNum4E = "4/4";
    private string _Day1NNum4S = "4/4";

    private string _Day1NPlayE = "PLAY!";
    private string _Day1NPlayS = "¡JUGAR!";

    private string _Day1NR0E = "Name\t\tTime \tFuel \tDamage";
    private string _Day1NR0S = "Nombre\t\tTiempo \tCombustible \tDaño";

    private string _Day1NR1DE = "";
    private string _Day1NR1DS = "";

    private string _Day1NR1FE = "";
    private string _Day1NR1FS = "";

    private string _Day1NR1NE = "";
    private string _Day1NR1NS = "";

    private string _Day1NR1TE = "";
    private string _Day1NR1TS = "";

    private string _Day1NR2DE = "";
    private string _Day1NR2DS = "";

    private string _Day1NR2FE = "";
    private string _Day1NR2FS = "";

    private string _Day1NR2NE = "";
    private string _Day1NR2NS = "";

    private string _Day1NR2TE = "";
    private string _Day1NR2TS = "";

    private string _Day1NR3DE = "";
    private string _Day1NR3DS = "";

    private string _Day1NR3FE = "";
    private string _Day1NR3FS = "";

    private string _Day1NR3NE = "";
    private string _Day1NR3NS = "";

    private string _Day1NR3TE = "";
    private string _Day1NR3TS = "";

    private string _Day1NR4DE = "";
    private string _Day1NR4DS = "";

    private string _Day1NR4FE = "";
    private string _Day1NR4FS = "";

    private string _Day1NR4NE = "";
    private string _Day1NR4NS = "";

    private string _Day1NR4TE = "";
    private string _Day1NR4TS = "";

    private string _Day1NR5DE = "";
    private string _Day1NR5DS = "";

    private string _Day1NR5FE = "";
    private string _Day1NR5FS = "";

    private string _Day1NR5NE = "";
    private string _Day1NR5NS = "";

    private string _Day1NR5TE = "";
    private string _Day1NR5TS = "";

    private string _Day1NRecordNoE = "NAH";
    private string _Day1NRecordNoS = "NAH";

    private string _Day1NRecordYesE = "yes!";
    private string _Day1NRecordYesS = "¡si!";

    private string _Day1NResultsE = "RESULTS";
    private string _Day1NResultsS = "RESULTADOS";

    private string _Day1NReturnE = "RETURN TO MENU";
    private string _Day1NReturnS = "VOLVER AL MENU";

    private string _Day1NRotateE = "ROTATE";
    private string _Day1NRotateS = "ROTAR";

    private string _Day1NSecondsE = "Seconds";
    private string _Day1NSecondsS = "Segundos";

    private string _Day1NSectionE = "SECTION HAS CHECKPOINTS";
    private string _Day1NSectionS = "LA SECCIÓN TIENE PUNTOS DE CONTROL";

    private string _Day1NSection1E = "SECTION";
    private string _Day1NSection1S = "SECCIÓN";

    private string _Day1NSelectionNameE = "CHECKPOINT X";
    private string _Day1NSelectionNameS = "PUNTO DE CONTROL X";

    private string _Day1NSettingsE = "Settings";
    private string _Day1NSettingsS = "Ajustes";

    private string _Day1NSNameE = "T\r\nE\r\nM\r\nP\r\nE\r\nR";
    private string _Day1NSNameS = "T\r\nE\r\nM\r\nP\r\nE\r\nR\r\nA\r\nM\r\nE\r\nN\r\nT\r\nO";

    private string _Day1NSoundE = "Sound";
    private string _Day1NSoundS = "Sonido";

    private string _Day1NStatsE = "STATS";
    private string _Day1NStatsS = "ESTADÍSTICAS";

    private string _Day1NSubmitE = "SUBMIT";
    private string _Day1NSumbitS = "ENTREGAR";

    private string _Day1NTotalTimeE = "00.00";
    private string _Day1NTotalTimeS = "00.00";

    private string _Day1NTutorialE = "TUTORIAL";
    private string _Day1NTutorialS = "TUTORIAL";

    //Day2A Texts

    private string _Day2ABackE = "BACK";
    private string _Day2ABackS = "ATRÁS";

    private string _Day2ABack1E = "BACK";
    private string _Day2ABack1S = "ATRÁS";

    private string _Day2ABack2E = "BACK";
    private string _Day2ABack2S = "ATRÁS";

    private string _Day2ABeforeE = "BEFORE PRESSING PLAY";
    private string _Day2ABeforeS = "ANTES DE PULSAR JUGAR";

    private string _Day2AChangeE = "CHANGE CAMERA";
    private string _Day2AChangeS = "CAMBIAR CÁMARA";

    private string _Day2ACheckpointE = "CHECKPOINT";
    private string _Day2ACheckpointS = "PUNTO DE CONTROL";

    private string _Day2ACongratsE = "CONGRATULATIONS!";
    private string _Day2ACongratsS = "FELICIDADES";

    private string _Day2AContinueE = "CONTINUE";
    private string _Day2AContinueS = "Continuar";

    private string _Day2AContinue1E = "CONTINUE";
    private string _Day2AContinue1S = "Continuar";

    private string _Day2AContinue2E = "CONTINUE";
    private string _Day2AContinue2S = "Continuar";

    private string _Day2AContinue3E = "CONTINUE";
    private string _Day2AContinue3S = "Continuar";

    private string _Day2ADamageE = "D\r\nA\r\nM\r\nA\r\nG\r\nE";
    private string _Day2ADamageS = "D\r\nA\r\nÑ\r\nO";

    private string _Day2ADefeatTextE = "";
    private string _Day2ADefeatTextS = "";

    private string _Day2ADesc1E = "Click a SECTION to choose TEMPER and PACENOTE for its CHECKPOINTS";
    private string _Day2ADesc1S = "Seleccione una SECCIÓN para elegir el TEMPERAMENTO y TIPO DE CURVA para cada PUNTO DE CONTROL";

    private string _Day2ADesc2E = "You can PAUSE and SPEED UP. Losing all FUEL/reaching max DAMAGE is a loss";
    private string _Day2ADesc2S = "Puedes PAUSAR y ACELERAR. Gastar todo el COMBUSTIBLE/alcanzar el maximo DAÑO siginifica perder";

    private string _Day2ADesc3E = "Welcome! In RTT, you play as a co-pilot. You can move freely through the stage";
    private string _Day2ADesc3S = "¡Bienvenido! En RTT juegas como un copiloto. Puedes moverte libremente por el escenario";

    private string _Day2ADesc4E = "Select checkpoints marked in RED before starting. They will then turn GREEN";
    private string _Day2ADesc4S = "Seleccione los puntos de control marcados en ROJO antes de empezar. Se pondrán VERDE";

    private string _Day2ADesc5E = "SOME CHECKPOINTS ARE NOT SELECTED!\r\nLOCATE THE RED AURA ABOVE THEM";
    private string _Day2ADesc5S = "ALGUNOS PUNTOS DE CONTROL NO ESTAN SEECCIONADOS\r\nLOCALICE EL AURA ROJA ENCIMA DE ELLOS";

    private string _Day2AEffectsE = "Effects";
    private string _Day2AEffectsS = "Efectos";

    private string _Day2AExitE = "EXIT\t";
    private string _Day2AExitS = "SALIR\t";

    private string _Day2AExit1E = "EXIT\t";
    private string _Day2AExit1S = "SALIR\t";

    private string _Day2AExPacenoteE = "CHECKPOINT 4\r\nEXCELLENT PACENOTE, BUT POOR ATTITUDE";
    private string _Day2AExPacenoteS = "PUNTO DE CONTROL 4\r\nEXCELENTE RUTA, PERO ACTITUD POBRE";

    private string _Day2AFuelE = "F\r\nU\r\nE\r\nL";
    private string _Day2AMFuelS = "C\r\nO\r\nM\r\nB\r\nU\r\nS\r\nT\r\nI\r\nB\r\nL\r\nE";

    private string _Day2AGearE = "GEAR: 0";
    private string _Day2AGearS = "Marcha: 0";

    private string _Day2AGOTextE = "GAME OVER";
    private string _Day2AGOTextS = "HAS PERDIDO";

    private string _Day2AHeaderE = "TIME";
    private string _Day2AHeaderS = "TIEMPO";

    private string _Day2AHUDE = "INGAME HUD";
    private string _Day2AHUDS = "HUD IN GAME";

    private string _Day2AIndicatorsE = "INDICATORS";
    private string _Day2AIndicatorsS = "INDICADORES";

    private string _Day2AItemLabelE = "Option A";
    private string _Day2AItemLabelS = "Opción A";

    private string _Day2AKPHE = "KPH: 00";
    private string _Day2AKPHS = "KPH: 00";

    private string _Day2ALabelE = "English";
    private string _Day2ALabelS = "Español";

    private string _Day2ALanguageE = "Language";
    private string _Day2ALanguageS = "Idioma";

    private string _Day2ALoadE = "Load last \r\ngame data?";
    private string _Day2ALoadS = "¿Cargar última \r\npartida guardada?";

    private string _Day2AMoveE = "MOVE";
    private string _Day2AMoveS = "MOVER";

    private string _Day2AMusicE = "Music";
    private string _Day2AMusicS = "Música";

    private string _Day2ANextE = "Next: ";
    private string _Day2ANextS = "Siguiente: ";

    private string _Day2ANextStageE = "Day 1 - Night\r\nWhispering Pine Stage";
    private string _Day2ANextStageS = "Día 1 - Night\r\nEstapa del Pino Susurrante";

    private string _Day2ANullE = "";
    private string _Day2ANullS = "";

    private string _Day2ANum1E = "1/4";
    private string _Day2ANum1S = "1/4";

    private string _Day2ANum2E = "2/4";
    private string _Day2ANum2S = "2/4";

    private string _Day2ANum3E = "3/4";
    private string _Day2ANum3S = "3/4";

    private string _Day2ANum4E = "4/4";
    private string _Day2ANum4S = "4/4";

    private string _Day2APlayE = "PLAY!";
    private string _Day2APlayS = "¡JUGAR!";

    private string _Day2AR0E = "Name\t\tTime \tFuel \tDamage";
    private string _Day2AR0S = "Nombre\t\tTiempo \tCombustible \tDaño";

    private string _Day2AR1DE = "";
    private string _Day2AR1DS = "";

    private string _Day2AR1FE = "";
    private string _Day2AR1FS = "";

    private string _Day2AR1NE = "";
    private string _Day2AR1NS = "";

    private string _Day2AR1TE = "";
    private string _Day2AR1TS = "";

    private string _Day2AR2DE = "";
    private string _Day2AR2DS = "";

    private string _Day2AR2FE = "";
    private string _Day2AR2FS = "";

    private string _Day2AR2NE = "";
    private string _Day2AR2NS = "";

    private string _Day2AR2TE = "";
    private string _Day2AR2TS = "";

    private string _Day2AR3DE = "";
    private string _Day2AR3DS = "";

    private string _Day2AR3FE = "";
    private string _Day2AR3FS = "";

    private string _Day2AR3NE = "";
    private string _Day2AR3NS = "";

    private string _Day2AR3TE = "";
    private string _Day2AR3TS = "";

    private string _Day2AR4DE = "";
    private string _Day2AR4DS = "";

    private string _Day2AR4FE = "";
    private string _Day2AR4FS = "";

    private string _Day2AR4NE = "";
    private string _Day2AR4NS = "";

    private string _Day2AR4TE = "";
    private string _Day2AR4TS = "";

    private string _Day2AR5DE = "";
    private string _Day2AR5DS = "";

    private string _Day2AR5FE = "";
    private string _Day2AR5FS = "";

    private string _Day2AR5NE = "";
    private string _Day2AR5NS = "";

    private string _Day2AR5TE = "";
    private string _Day2AR5TS = "";

    private string _Day2ARecordNoE = "NAH";
    private string _Day2ARecordNoS = "NAH";

    private string _Day2ARecordYesE = "yes!";
    private string _Day2ARecordYesS = "¡si!";

    private string _Day2AResultsE = "RESULTS";
    private string _Day2AResultsS = "RESULTADOS";

    private string _Day2AReturnE = "RETURN TO MENU";
    private string _Day2AReturnS = "VOLVER AL MENU";

    private string _Day2ARotateE = "ROTATE";
    private string _Day2ARotateS = "ROTAR";

    private string _Day2ASecondsE = "Seconds";
    private string _Day2ASecondsS = "Segundos";

    private string _Day2ASectionE = "SECTION HAS CHECKPOINTS";
    private string _Day2ASectionS = "LA SECCIÓN TIENE PUNTOS DE CONTROL";

    private string _Day2ASection1E = "SECTION";
    private string _Day2ASection1S = "SECCIÓN";

    private string _Day2ASelectionNameE = "CHECKPOINT X";
    private string _Day2ASelectionNameS = "PUNTO DE CONTROL X";

    private string _Day2ASettingsE = "Settings";
    private string _Day2ASettingsS = "Ajustes";

    private string _Day2ASNameE = "T\r\nE\r\nM\r\nP\r\nE\r\nR";
    private string _Day2ASNameS = "T\r\nE\r\nM\r\nP\r\nE\r\nR\r\nA\r\nM\r\nE\r\nN\r\nT\r\nO";

    private string _Day2ASoundE = "Sound";
    private string _Day2ASoundS = "Sonido";

    private string _Day2AStatsE = "STATS";
    private string _Day2AStatsS = "ESTADÍSTICAS";

    private string _Day2ASubmitE = "SUBMIT";
    private string _Day2ASumbitS = "ENTREGAR";

    private string _Day2ATotalTimeE = "00.00";
    private string _Day2ATotalTimeS = "00.00";

    private string _Day2ATutorialE = "TUTORIAL";
    private string _Day2ATutorialS = "TUTORIAL";

    private string _Day2AWorkingStageE = "CURRENTLY IN \r\nDEVELOPMENT\r\nRedirects to: \r\nDay 1 - Morning";
    private string _Day2AWorkingStageS = "ACTUALMENTE EN \r\nDESARROLLO\r\nRedirige a: \r\nDía 1 - Mañana";
}