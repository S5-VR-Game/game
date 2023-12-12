# puzzle_template

<!---
    Bitte einen Kategorie-Ordner erstellen, falls noch nicht vorhanden.
    /docs/puzzle/templates/*hier Kategorie Ordner einfügen*
-->


# Name

<!---
    -  Einen fancy Namen überlegen :)
-->

Basketball

---

# Beschreibung


<!---
    - Sollte das Rätsel nur mit den nötigsten Infos beschreiben.
    - Dieser Abschnitt kann dem Spieler im HUD angezeigt werden.
-->

Befördere diesen Gegenstand in den markierten Container (wefen geht auch :) ), verwende den Gravity Knopf um den Gegenstand schwerelos zu machen.

---

# Ort
<!---
    - Wo ist dieses Rätsel zu finden? (in Wand integriert, freistehend,
      über die Raumstation verteilt, ein ganzer Raum....)
-->

??

---

# Mechanik

<!---
    - Exakte Beschreibung der benötigten Schritte/Aufgaben des Spielers 
-->

Der Spieler bekommt z.B. einen Ball, dieser soll in einen Container (Eimer/Loch in der Wand Kiste) befördert werden,
der gerade aufläuchtet/seine Farbe ändert, ..
Beim Ball-Spawn befindet sich ein Knopf, der bei dem Ball die Schwerelosigkeit triggert (toggelt zwischen mit/ohne Erdanziehungskraft)
Wurde ein Container getroffen, despawnt der Ball der Spieler erhält einen Punkt und ein neuer erscheint. 


---

# Endbedingungen

<!---
    - Exakte Beschreibung, wann das Rätsel erfolgreich gelöst ist.
    - (optional) Exakte Beschreibung, wann es fehlschlägt.
    - (optional) Exakte beschreibung, wann Rätsel zurückgesetzt wird.
-->

Das Spiel ist "gelöst" wenn die Zielpunktzahl erreicht ist.


---

# Skalierungsparameter

<!---
    - Einstellungsvariablen/-parameter 
        - welche gibt es 
        - auswirkungen
        - was für eine Range haben sie
        - schwierigkeits Einschätzung
-->

Schwierigkeit kann eingestellt werden:
    - indem die erforderliche Punktzahl erreicht wurde
    - die Würfe für die Container komplizierter ist (andere Position)
    
    