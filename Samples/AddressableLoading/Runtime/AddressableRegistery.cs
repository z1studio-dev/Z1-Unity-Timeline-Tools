
using System.Collections.Generic;
using UnityEngine;

public class AddressableRegistery : MonoBehaviour
{
    public class Status
    {
        public GameObject instance;
        public bool isLoading;
    }

    private readonly Dictionary<int, Status> map = new Dictionary<int, Status>(8);

    public Status Get(int key)
    {
        if (!map.TryGetValue(key, out var s))
        {
            s = new Status();
            map.Add(key, s);
        }
        return s;
    }

    public void Clear(int key)
    {
        if (map.TryGetValue(key, out var s))
        {
            s.instance = null;
            s.isLoading = false;
        }
    }
}