# puzzle_template

# Name

<!---
    -  Einen fancy Namen überlegen :)
-->

Knöpfe wiederholen / Simon Says

---

# Beschreibung

<!---
    - Sollte das Rätsel nur mit den nötigsten Infos beschreiben.
    - Dieser Abschnitt kann dem Spieler im HUD angezeigt werden.
-->

Die Knöpfe blinken in einer bestimmten Reihenfolge auf, wiederhole diese.

---

# Ort
<!---
    - Wo ist dieses Rätsel zu finden? (in Wand integriert, freistehend,
      über die Raumstation verteilt, ein ganzer Raum....)
-->

Das Rätsel ist an einem Wandpanel angebracht, oder ersetzt ein solches.
Alternativ wäre auch ein Gerät auf einem Tisch denkbar oder irgendeine Maschine in einem Spezialraum

---

# Mechanik

<!---
    - Exakte Beschreibung der benötigten Schritte/Aufgaben des Spielers 
-->

Es sind Knöpfe mit einer dazugehörigen Lampe(oder der Knopf leuchtet selbst auf) gegeben,
diese leuchten in einer zufälligen Reihenfolge auf,
anschließend müssen die Knöpfe in dieser Reihnfolge gedrückt werden.
Alternativ könnte die Reihnfolge auch aufeinander aufbauen, d.H. dass immer ein weiteres Licht hinzugefügt wird.
Wenn alles richtig gemacht wird, sollte ein visuelles Signal anzeigen, dass das Rätsel geschafft wurde.
Das gleiche gilt, wenn ein falscher Knopf gedrückt wurde. Dann muss das Spiel neu starten.
---

# Endbedingungen

<!---
    - Exakte Beschreibung, wann das Rätsel erfolgreich gelöst ist.
    - (optional) Exakte Beschreibung, wann es fehlschlägt.
    - (optional) Exakte beschreibung, wann Rätsel zurückgesetzt wird.
-->

Das Rätsel ist erfolgreich gelöst, wenn der Spieler die LED-Abfolge mit den Knöpfen richtig wiedergegeben hat.

Das Rätsel schlägt fehl, wenn ein falscher Knopf gedrückt wird. Anschließend wird das Rätsel neu gestartet

---

# Skalierungsparameter

<!---
    - Einstellungsvariablen/-parameter 
        - welche gibt es 
        - auswirkungen
        - was für eine Range haben sie
        - schwierigkeits Einschätzung
-->

Mögliche Einstellungen sind:
- Anzahl Knöpfe
- Anzahl an benötigten Knopfdrücken
- Geschwindigkeit der lichtanzeige
- Geschwindigkeit des erwarteten Knopfdrucks
- Anzahl erfolgreich gelöster Reihnfolgen, bevor das Rätsel gelöst ist
