# Dokumentation zur Erstellung von Game Tasks

## Game Task Factory

1. Eine Factory-Klasse erstellen, die von `GameTaskFactory` erbt. Beispiel einer Factory:
```csharp 
public class ExampleTaskFactory : GameTaskFactory<TaskSpawnPoint>
{
    public ExampleTask exampleTaskPrefab;
    protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
    {
        // create a Prefab instance and get the task component
        GameObject instance = Instantiate(exampleTaskPrefab.gameObject, spawnPoint.GetSpawnPosition(), Quaternion.identity);
        ExampleTask exampleTask = instance.GetComponent<ExampleTask>();

        return exampleTask;
    }
}
```
## GameTask-Skript
2. Eine eigene GameTask-Klasse erstellen, die von `GameTask` erbt. 
3. Methode `Initialize` wirde 1x zur Initialisierung automatisch aufgerufen.
4. Methode `CheckTaskState` bestimmt den Zustand (`Ongoing`, `Success`, `Failed`) des Tasks.  
Implementiere hier die Logik, die prüft, ob die Aufgabe/der Task erledigt ist.
Beispiel-Implementierung: 
```csharp 
protected override TaskState CheckTaskState()
{
    if(inputCode.Equals(""))
    {
        return TaskState.Ongoing;
    } 
    else if (inputCode.Equals("password"))
    {
        return TaskState.Success;
    } 
    else 
    {
        return TaskState.Failure;    
    }
}
```
> Hier läuft der Task solange (über `return TaskState.Ongoing`), bis `inputCode` nicht mehr leer ist.
> Falls das Passwort übereinstimmt wird `TaskState.Success` zurückgegeben. Dies signalisiert, dass der Task erfolgreich beendet wurde.
> Ansonsten wird `TaskState.Failure` zurückgegeben. Dies bedeutet, die Aufgabe ist fehlgeschlagen.
5. Die Methode `BeforeStateCheck` wird vor dem Prüfen des aktuellen Zustands über `CheckTaskState` aufgerufen und sollte die Mechaniken 
des GameTasks enthalten, Variablen und Komponenten des GameTasks aktualisieren usw. 

In der `AfterStateCheck`-Methode kann Code implementiert werden, der explizit nach dem Überprüfen des Task-Zustands über `CheckTaskState` ausgeführt werden soll. Beispiel-Implementierung:
```csharp
protected override void AfterStateCheck()
{
    if (currentTaskState != TaskState.Ongoing)
    {
        DestroyTask();
    }
}
```
> Beispielsweise können so alle GameObjects des Tasks aus der Szene entfernt werden (Methode `DestroyTask`), falls der Task erledigt/fehlgeschlagen ist (`currentTaskState` != Ongoing).
6. Dieses GameTask-Skript kann in einem Prefab zugewiesen werden. Diese Prefab kann dann in der Factory in der Szene erstellt werden (siehe `exampleTaskPrefab` **in 1.**).
Über `instance.GetComponent<GameScriptClass>()` kann anschließend das GameTask-Skript aus dem `GameObject` erhalten werden. Dieses muss in der `CreateTask` Methode in der Factory zurückgegeben werden
## Spawn-Points
7. Für den Typparameter `T` der Oberklasse `GameTaskFactory` im neu erstellten Factory-Skript kann die Klasse 
`TaskSpawnPoint` für einfache Tasks verwendet werden, die nur ein Prefab in der Szene benötigen/spawnen wollen

_Alternativ_: Für Tasks, die mehrere Prefabs oder Objekte in der Szene benötigen (z.B. bei der Transport-Aufgabe, bei der mehrere 
Kisten in einen anderen Raum transportiert werden sollen) kann eine eigene Spawn-Point Klasse erstellt werden, die von 
`TaskSpawnPoint` erbt. Diese muss dann als Typparameter `T` angegeben werden. Diese Klasse kann spezifische Positionen und 
Daten für den Task in Form von Klassenvariablen festhalten. Beispiel einer solchen Klasse:
```csharp 
public class ExampleSpawnPoint : TaskSpawnPoint
{
   public GameObject point1;
}
```
> In diesem Beispiel kann dem Feld `point1` ein leeres `GameObject` im Unity-Editor zugewiesen werden. Die Position dieses 
> Objektes kann dann verwendet werden um in der Factory zusätzliche Objekte neben dem Task in der Szene zu erstellen.

# Zuweisung von Skripten zu GameObjects im Unity Editor
8. Leeres `GameObject` erstellen und das erstellte Spawn-Point-Skript (**aus 7. _Alternativ_**) zuweisen oder das vordefinierte Skript `TaskSpawnPoint`
verwenden
9. Leeres `GameObject` erstellen und das erstellte Factory-Skript (**aus 1.**) zuweisen
10. Spawn-Point Objekt (**aus 8.**) im Editor zu der Liste `spawnPoints` im Factory-Skript (**aus 9.**) hinzufügen
11. Factory-Objekt zu der Liste `factories` im Game-Timer Skript in der Space-Station Szene hinzufügen