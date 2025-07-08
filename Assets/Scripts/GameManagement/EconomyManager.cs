using System.Collections.Generic;
using UnityEngine;
using Mirror;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/guides/networkbehaviour
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

public class EconomyManager : NetworkBehaviour
{
    public static EconomyManager singleton;

    private List<Player> players = new List<Player>();

    protected override void OnValidate()
    {
        base.OnValidate();
    }

    private void Awake()
    {
        if (singleton != null && singleton != this) { Destroy(this); } else { singleton = this; }
    }

    public void RegisterPlayer(Player newPlayer)
    {
        players.Add(newPlayer);
    }

    void Start()
    {

    }
}
