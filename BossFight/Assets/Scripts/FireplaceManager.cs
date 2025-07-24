using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireplaceManager : MonoBehaviour
{
    public static FireplaceManager Instance;

    private List<Fireplace> fireplaces;

    void Awake() => Instance = this;

    public void Register(Fireplace f) => fireplaces.Add(f);

    public void ExtinguishAll()
    {
        foreach (var f in fireplaces)
            f.Extinguish();
    }

    public bool AllLit() => fireplaces.All(f => f.IsLit());

    public static implicit operator FireplaceManager(Fireplace v)
    {
        throw new NotImplementedException();
    }
}

