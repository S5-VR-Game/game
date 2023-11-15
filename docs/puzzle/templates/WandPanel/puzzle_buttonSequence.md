# puzzle_template

# Name

<!---
    -  Einen fancy Namen überlegen :)
-->

Knöpfe in reihenfolge drücken

---

# Beschreibung

<!---
    - Sollte das Rätsel nur mit den nötigsten Infos beschreiben.
    - Dieser Abschnitt kann dem Spieler im HUD angezeigt werden.
-->

Knöpfe anhand einer Anleitung in einer bestimmten Reihenfolge drücken.

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

Es sind Knöpfe gegeben,
diese müssen in einer Reihenfolge gedrückt werden,
welche man von einer Anleitung(die woanders in der Station "versteckt" ist, oder einem anderen Rätsel als Lösung bekommt)
ablesen muss.
Wenn alles richtig gemacht wird, sollte ein visuelles Signal anzeigen, dass das Rätsel geschafft wurde.
Das gleiche gilt, wenn ein falscher Knopf gedrückt wurde. Dann muss das Spiel neu starten.
---

# Endbedingungen

<!---
    - Exakte Beschreibung, wann das Rätsel erfolgreich gelöst ist.
    - (optional) Exakte Beschreibung, wann es fehlschlägt.
    - (optional) Exakte beschreibung, wann Rätsel zurückgesetzt wird.
-->

Das Rätsel ist erfolgreich gelöst, wenn der Spieler den Knopf-Code richtig eingegeben hat.

Das Rätsel schlägt fehl, wenn ein falscher Knopf gedrückt wird.
Anschließend wird der Speicher der gedrückten Knöpfe geleert um den Code erneut eingeben zu können.

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
- Geschwindigkeit des erwarteten Knopfdrucks
- Anzahl an benötigten Knopfdrücken
- Anzahl erfolgreich gelöster Reihnfolgen, bevor das Rätsel gelöst ist
