# puzzle_template

<!---
    Bitte einen Kategorie-Ordner erstellen, falls noch nicht vorhanden.
    /docs/puzzle/templates/*hier Kategorie Ordner einfügen*
-->


# Name

Passwort-Rätsel mit Büchern

---

# Beschreibung

Es soll einen abgesperrten Raum geben, in den der Spieler erst reinkommt, nachdem er ein Passwort herausgefunden hat,
bestehend aus 3 Zeichen, jedes Buch soll nummeriert sein (position des Passwortes) und eine Zahl beinhalten (Wert an Position)
Wenn man dies in ein Terminal eingibt, wird ein bestimmter Raum entsperrt. 

---

# Ort

Raum, in dem sich das Bücherregal befindet, sowie der Raum, in dem das Panel zum Eingeben der Zahl sich befindet.

---

# Mechanik

Spieler findet neben leeren Büchern auch noch Bücher, bei denen eine Seite beschrieben ist, daher könnte dieses Rätsel
etwas mehr Zeit in Anspruch nehmen. Die Bücher soll man später aufblättern können.

# Endbedingungen

Das Rätsel ist erfolgreich gelöst, wenn der Zugriff auf diesen Raum gewährt ist, man kann bei diesem Rätsel nicht versagen.

---

# Skalierungsparameter

- Anzahl Bücher, die man durchsuchen muss.
- Anzahl Zeichen, die man eingeben muss.
