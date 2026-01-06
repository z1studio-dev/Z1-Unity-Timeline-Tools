using UnityEngine;
using System.ComponentModel;
using UnityEngine.Timeline;
public class NotesMarker : Marker
{
    public string title = "empty";
    public Color color = Color.white;
    public bool showLineOverlay = false;
    public bool ignoreInPlayMode = false;
    [TextArea(10, 15)] public string note;
}
