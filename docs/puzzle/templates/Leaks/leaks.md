# puzzle_template

<!---
    Bitte einen Kategorie-Ordner erstellen, falls noch nicht vorhanden.
    /docs/puzzle/templates/*hier Kategorie Ordner einfügen*
-->


# Name

<!---
    -  Einen fancy Namen überlegen :)
-->

der Luftdruck sinkt...

---

# Beschreibung

<!---
    - Sollte das Rätsel nur mit den nötigsten Infos beschreiben.
    - Dieser Abschnitt kann dem Spieler im HUD angezeigt werden.
-->

Einige Teile der Raumstation sind von Luftlecks betroffen. 
Repariere diese Lecks.

---

# Ort
<!---
    - Wo ist dieses Rätsel zu finden? (in Wand integriert, freistehend,
      über die Raumstation verteilt, ein ganzer Raum....)
-->

Auf der Karte verteilt (Wände oder Böden mit kleinen Löchern)

---

# Mechanik

<!---
    - Exakte Beschreibung der benötigten Schritte/Aufgaben des Spielers 
-->

User findet ein Leck -> User stopft das Leck (Interaktion mit dem Leck per Werkzeug, welches
noch definiert werden muss) -> User setzt den Barometer in dem Raum zurück.

---

# Endbedingungen

<!---
    - Exakte Beschreibung, wann das Rätsel erfolgreich gelöst ist.
    - (optional) Exakte Beschreibung, wann es fehlschlägt.
    - (optional) Exakte beschreibung, wann Rätsel zurückgesetzt wird.
-->

Rätsel ist gelöst, wenn der Benutzer sämtliche Lecks gestopft hat.
Ein Leck gilt als gestopft, wenn der User damit interagiert hat und
anschließend den Barometer zurückgesetzt hat.

---

# Skalierungsparameter

<!---
    - Einstellungsvariablen/-parameter 
        - welche gibt es 
        - auswirkungen
        - was für eine Range haben sie
        - schwierigkeits Einschätzung
-->

Anzahl Lecks: 5-9
Schwierigkeitsgrad: Fortgeschritten.
