using System;
using System.Collections.Generic;

namespace FirstResponseMP.Shared.Objects;

public class PedData(string firstName, string lastName)
{
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public string FullName => FirstName + " " + LastName;
    public List<string> Warrants { get; } = []; // TODO?: Refactor into object
    // TODO?: Gender



    public void Regenerate() => throw new NotImplementedException();
    public void RegenerateWarrants() => throw new NotImplementedException();
}