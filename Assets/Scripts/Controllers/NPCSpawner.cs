using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : Singleton<NPCSpawner> {

    [SerializeField] private List<NPC> _NPCAIList = new List<NPC>();
    private List<NPC> _spawnedNPCs = new List<NPC>();

    public NPC SpawnNPC(Vector3 _spawnPostion) {
        
        NPC _generatedNPC = Utils.SpawnRandomFromList<NPC>(_spawnPostion, _NPCAIList);
        _spawnedNPCs.Add(_generatedNPC);
        return _generatedNPC;
    }
    public void DeSpawnNPC(NPC _NPC) {
        _spawnedNPCs.Remove(_NPC);
    }
    public List<NPC> GetSpawnedNPCs() { 
        return _spawnedNPCs; 
    }
}