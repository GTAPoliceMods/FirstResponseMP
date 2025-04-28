using System;
using System.Collections.Generic;
using System.Linq;
using FirstResponseMP.Shared.Interfaces;

namespace FirstResponseMP.Client.Objects;
// TODO?: Replicate for server

public class PedData(string firstName, string lastName) : IPedData
{
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public string FullName => FirstName + " " + LastName;
    public List<string> Warrants { get; } = []; // TODO?: Refactor into object
    // TODO?: Gender


    public void Regenerate() => throw new NotImplementedException();
    public void RegenerateWarrants() => throw new NotImplementedException();

    public override string ToString()
    {
        return FullName;
    }

    public string ToDataString() => @$"
Name: {FullName}\n
Wanted: {(Warrants.Any() ? "~r~Yes" : "~s~No")}\n
".Trim();
    
    public object ToObject() => throw new NotImplementedException();
    public static PedData FromObject(object obj) => throw new NotImplementedException();
}