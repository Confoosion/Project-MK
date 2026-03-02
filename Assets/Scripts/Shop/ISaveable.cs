public interface ISaveable
{
    string GetSaveID();       // Unique key for this item
    string SerializeState();  // Convert state to JSON string
    void LoadState(string json); // Restore state from JSON string
}