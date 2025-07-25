using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireplaceManager : MonoBehaviour
{
    public static FireplaceManager Instance;

    private List<Fireplace> fireplaces = new();

    void Awake() => Instance = this;

    void Start()
    {
        Debug.Log("Total fireplaces: " + fireplaces.Count);
    }

    public void Register(Fireplace f)
    {
        fireplaces.Add(f);
        Debug.Log("Registered fireplace: " + f.name);
    }

    //method for dracula
    public void ExtinguishAll()
    {
        foreach (var f in fireplaces)
            f.Extinguish();
    }

    public bool AllLit() => fireplaces.All(f => f.IsLit());

}

