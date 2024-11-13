using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : Singleton<NPCSpawner> {

    [SerializeField] private List<NPC> _NPCAIList = new List<NPC>();

    public NPC SpawnNPC(Vector3 _spawnPostion) {
        NPC _generatedNPC = Utils.SpawnRandomFromList<NPC>(_spawnPostion, _NPCAIList);
        return _generatedNPC;
    }

}