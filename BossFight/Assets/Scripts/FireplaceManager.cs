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
    }

    public void Register(Fireplace f)
    {
        fireplaces.Add(f);
    }

    //method for dracula
    public void ExtinguishAll()
    {
        foreach (var f in fireplaces)
            f.Extinguish();
    }

    public bool AllLit() => fireplaces.All(f => f.IsLit());

}

