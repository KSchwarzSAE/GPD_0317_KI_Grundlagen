using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AStar
{

    public static List<Vector3> CalculatePath(PathPoint _start, PathPoint _end)
    {
        // OpenList => PathPoints welche noch geprüft werden müssen
        List<PathPoint> openList = new List<PathPoint>();
        openList.Add(_end);

        // ClosedList => PathPoints, welche bereits geprüft wurden
        List<PathPoint> closedList = new List<PathPoint>();

        PathPoint current = null;
        while(openList.Count > 0 && (current = openList[0]) && current != _start)
        {
            // Element aus der OpenList in die Closed List schieben
            openList.Remove(current);
            closedList.Add(current);

            // alle nachbarn durchgehen
            foreach (PathPoint neighbour in current.Reachable)
            {
                // Ist der Nachbar in einer der beiden Listen => nächstes Element prüfen
                if (openList.Contains(neighbour) || closedList.Contains(neighbour))
                    continue;

                // Aktuelle Node als Vorgänger setzten
                neighbour.prior = current;

                // Kosten um den Nachbar zu erreichen, sind die Kosten um den aktuellen Punkt zu erreichen
                neighbour.g = current.g
                    // plus die Kosten vom aktuellen Punkt zum Zielpunkt
                    + (neighbour.transform.position - current.transform.position).sqrMagnitude;

                // Die Heuristik berechnen => Kosten sind (wahrscheinlich) relativ zum Abstand zum Ziel
                neighbour.h = (neighbour.transform.position - _end.transform.position).sqrMagnitude;

                // Die finalen Kosten sind die Kosten den Punkt zu erreichen, plus die geschätzten Kosten zum Ziel
                neighbour.c = neighbour.g + neighbour.h;

				// Nachbar in die Open List setzten
				openList.Add (neighbour);
            }

            // Open list sortieren
            openList.Sort();
        }

        // es wurde kein weg gefunden
        if (current != _start)
            return null;

        // Den Weg (rückwärts) in eine Liste schreiben
        List<Vector3> path = new List<Vector3>();

        while(current != null)
        {
            path.Add(current.transform.position);
            current = current.prior;
        }

        return path;
    }

}
