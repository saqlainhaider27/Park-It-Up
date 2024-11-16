using System.Collections.Generic;
using UnityEngine;

public class NPCsManager : Singleton<NPCsManager> {

    [SerializeField] private List<NPC> _NPCAIList = new List<NPC>();
    private List<NPC> _spawnedNPCs = new List<NPC>();
    private List<NPC> _NPCsWaitingForCar = new List<NPC>();
    public NPC SpawnNPC(Vector3 _spawnPostion) {
        
        NPC _generatedNPC = Utils.SpawnRandomFromList<NPC>(_spawnPostion, _NPCAIList);
        _spawnedNPCs.Add(_generatedNPC);
        return _generatedNPC;
    }
    public void DeSpawnNPC(NPC _NPC) {
        _spawnedNPCs.Remove(_NPC);
    }
    public void AddNPCToWaitingForCarList(NPC _NPC) {
        _NPCsWaitingForCar.Add(_NPC);
    }
    public void RemoveNPCFromWaitingForCarList(NPC _NPC) {
        _NPCsWaitingForCar.Remove(_NPC);
    }

    public List<NPC> GetNPCsWaitingForCarList() {
        return _spawnedNPCs;
    }
    public List<NPC> GetSpawnedNPCs() { 
        return _spawnedNPCs; 
    }
}