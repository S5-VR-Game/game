# puzzle_template

<!---
    Bitte einen Kategorie-Ordner erstellen, falls noch nicht vorhanden.
    /docs/puzzle/templates/*hier Kategorie Ordner einfügen*
-->


# Name

<!---
    -  Einen fancy Namen überlegen :)
-->

Eine suspekte Mischung

---

# Beschreibung

<!---
    - Sollte das Rätsel nur mit den nötigsten Infos beschreiben.
    - Dieser Abschnitt kann dem Spieler im HUD angezeigt werden.
-->

Mische die Richtige Substanz zusammen und gieße damit die Pflanze.

---

# Ort
<!---
    - Wo ist dieses Rätsel zu finden? (in Wand integriert, freistehend,
      über die Raumstation verteilt, ein ganzer Raum....)
-->

Das Rätsel erscheint im Labor der Raumstation

---

# Mechanik

<!---
    - Exakte Beschreibung der benötigten Schritte/Aufgaben des Spielers 
-->

Der Spieler muss zuerst im Labor ein Rezept finden, auf dem die genaue Zusammensetzung
der Flüssigkeit steht. Der Spieler muss drei an Flüssigkeiten, welche sich in
Reagenzgläsern befinden, in einem anderen großen Reagenzglas zusammenmischen und damit
dann eine Pflanze gießen (lediglich eine Interaktion mit dem Reagenzglas). Auf dem Tisch
befinden sich allerdings mehr solcher Flüssigkeiten.

---

# Endbedingungen

<!---
    - Exakte Beschreibung, wann das Rätsel erfolgreich gelöst ist.
    - (optional) Exakte Beschreibung, wann es fehlschlägt.
    - (optional) Exakte beschreibung, wann Rätsel zurückgesetzt wird.
-->

Rätsel erfolgreich gelöst, wenn der Spieler das gemisch richtig zusammengemischt hat
und die Pflanze damit gegossen hat. Das Rätsel schlägt fehl, wenn das gemisch Falsch gemischt
worden ist. (das Reagenzglas schmilzt)

---

# Skalierungsparameter

<!---
    - Einstellungsvariablen/-parameter 
        - welche gibt es 
        - auswirkungen
        - was für eine Range haben sie
        - schwierigkeits Einschätzung
-->

Anzahl Flüssigkeiten auf dem Tisch: 5-9
Schwierigkeitsgrad: Mittel
