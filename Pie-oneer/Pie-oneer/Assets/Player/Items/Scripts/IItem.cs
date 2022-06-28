using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    string Name { get; }
    Sprite Sprite { get; }

    void Collect();
    void Use();
}