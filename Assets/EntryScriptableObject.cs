using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EntryScriptableObject", order = 1)]
public class EntryScriptableObject : ScriptableObject
{
    public string prefabName;
    
    // all entries
    public string entryTitle;
    public string entryDescription;
    public int entryType; // 1, 2, 3 (task, calendar, notes)
    
    // task (priority level, completion, deadline)
    public int priorityLevel;
    public int completionStatus;
    public string deadline;
    
    // calendar (start, end, repeat)
    public string start;
    public string end;
    public int repeatStatus; 
    
}
