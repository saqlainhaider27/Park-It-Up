using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCsManager : Singleton<NPCsManager> {

    [SerializeField] private List<NPC> _NPCAIList = new List<NPC>();
    private List<NPC> _spawnedNPCs = new List<NPC>();
    private List<NPC> _NPCsWaitingForCar = new List<NPC>();

    public event EventHandler<OnListChangedEventArgs> OnNPCAddedToSpawnList;
    public event EventHandler<OnListChangedEventArgs> OnNPCRemovedFromSpawnList;
    public class OnListChangedEventArgs : EventArgs {
        public NPC _NPC;
    }

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
        OnNPCAddedToSpawnList?.Invoke(this, new OnListChangedEventArgs {
            _NPC = _NPC
        });
    }
    public void RemoveNPCFromWaitingForCarList(NPC _NPC) {
        _NPCsWaitingForCar.Remove(_NPC);
        OnNPCRemovedFromSpawnList.Invoke(this, new OnListChangedEventArgs {
            _NPC = _NPC
        });
    }

    public List<NPC> GetNPCsWaitingForCarList() {
        return _NPCsWaitingForCar;
    }
    public List<NPC> GetSpawnedNPCs() { 
        return _spawnedNPCs; 
    }
}

