# VR-Projekt-2022
Das ist die Projektarbeit von Florian Böhm, Tim Koll und Timo Nieke für das Modul "Virtual Reality und 3D Interaktion" im Sommersemester 2022 an der Universität Trier.

# Prepare
Repository ist hier verfügbar:
https://github.com/timoNieke/VR-Projekt-2022/tree/develop

Ngrok als Hosting-Service um Smartphone-Verbindung zu ermöglichen muss installiert / konfiguriert und gestartet werden

## Ngrok
Installieren Sie ngrok auf Ihren PC über folgenden Link: "https://ngrok.com/download".
Anschließend entpacken Sie die heruntergeladene ZIP-Datei und führen Sie die exe aus. 
Danach erscheint ein Konsolenbildschirm in der Sie folgenden Befehl ausführen, 
um ein AuthToken hinzuzufügen: "ngrok config add-authtoken 29sacKwZUMpp5MGzjz2KGW4QDfJ_5vr9b1dcn2aZ57zoL7XKm".
Um nun einen Server zu starten, führen Sie folgenden Befehl aus: "ngrok http 7842". 
(bzw. Der Code im Webserver-Port von airconsole) -> Menu-Szene -> Objekt "AirConsole" -> Settings

Nun läuft der ngrok-Server.

# Starten des Unity-Projekts
1. Menu-Szene muss geöffnet / gestartet werden werden
2. Es öffnet sich die Website von airconsole
3. ngrok nach folgendem Guide einbinden:
    -> https://developers.airconsole.com/#!/guides/unity-ngrok
4. Im Browser des smartphones airconsole.com aufrufen und den generierten Code eingeben  
5. Auf dem Handy öffnet sich das html-Interface
    -> Je nach Smartphone muss einmal auf "kalibrieren" drücken (wenn Spieler bei start direkt nach links oder rechts läuft)
6. In der Unity-Szene "Menu" auf "Play" drücken 
7. Schätze sammeln 


# Troubleshooting
1. Net-Socket-Exception                 -> Unity neustarten
2. Szene kann nicht gestartet werden    -> Assets - reimport all 
3. Smartphone darf nicht Stumm-geschaltet sein
