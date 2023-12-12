# puzzle_template

<!---
    Bitte einen Kategorie-Ordner erstellen, falls noch nicht vorhanden.
    /docs/puzzle/templates/*hier Kategorie Ordner einfügen*
-->


# Name

Asteroid-Shooter

---

# Beschreibung

Bei dieser Aufgabe geht es darum, mit einer Kanone (dargestellt als Fadenkreuz auf einem Fenster) in Richtung der Raumstation fliegende Asteroiden abzuschießen. Hirbei dürfen die Asteroiden nicht die Raumstation berühren. Ist dies der Fall, verliert der Spieler Leben und verliert dieses, sobald dieser keine mehr hat.

HUD-Beschreibung: Zerstöre die Asteroiden, bevor sie die Raumstation treffen. Steuere das Fadenkreuz mit dem Pointer des rechten Controllers (weißer Laser) und feuere Projektile mit der Trigger-Taste (hinten am Controller) auf die Asteroiden ab.

---

# Ort

Findet in den Korridoren der Raumstation (zwischen den Räumen) statt. Hierbei wird dies durch eine Kanone unterhalb permanent angezeigt, dass dort diese Aufgabe erscheinen kann. 

---

# Mechanik

Wenn diese Aufgabe ausgewählt wird und zu den auswählbaren Aufgaben hinzugefügt wird, wird an der Stelle, wo man dies starten kann, ein Tisch mit einem Knopf gespawnt. Drückt man diesen Knopf startet das Spiel. Hierbei wird auf der Wand bei dem Tisch ein Fadenkreuz angezeigt, mit dem der Spieler mit Tastendruck ein Projektil abfeuern kann. Während dieser Aufgabe werden Asteroiden erzeugt, die sich aus einem bestimmten Bereich auf die Raumstation zubewegen.
Ziel ist es hierbei, diese Asteroiden mit den Projektilen zu zerstören. Die Steuerung findet über den Pointer des Controllers statt.

---

# Endbedingungen

Abhängig vom ausgewählten Schwierigkeitsgrad werden eine bestimmte Anzahl an Asteroiden erzeugt, die abgeschossen werden müssen. Treffen diese dabei in einen bestimmten Bereich, werden dem Spieler Leben abgezogen. Besitzt dieser Spieler keine Leben mehr, nachdem alle Asteroiden erzeugt worden sind, hat dieser verloren - anderenfalls gewonnen.

---

# Skalierungsparameter

Einfach: 20 Asteroiden, langsame Geschwindigkeit, 5 Leben
Mittel: 25 Asteroiden, mittlere Geschwindigkeit, 3 Leben
Schwer: 30 Asteroiden, schnelle Geschwindigkeit, 1 Leben