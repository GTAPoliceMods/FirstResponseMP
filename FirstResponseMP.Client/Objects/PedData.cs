using System;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using FirstResponseMP.Shared.Extensions;
using FirstResponseMP.Shared.Interfaces;
using RandomNameGeneratorLibrary;

namespace FirstResponseMP.Client.Objects;
// TODO?: Replicate for server

public class PedData : IPedData
{
    private const int WarrantChance = 20;

    private static List<string> PossibleWarrants = [
        "Bench Warrant"
    ];
    
    public PedData(Gender gender)
    {
        Regenerate(gender);
        RegenerateWarrants();
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName => FirstName + " " + LastName;
    public List<string> Warrants { get; private set; } = []; // TODO?: Refactor into object
    // TODO?: Gender


    public void Regenerate(Gender gender)
    {
        FirstName = gender == Gender.Male ? new Random().GenerateRandomMaleFirstName() : new Random().GenerateRandomFemaleFirstName();
        LastName = new Random().GenerateRandomLastName();
    }

    public async void RegenerateWarrants()
    {
        Warrants.Clear();
        var rnd = new Random();
        while (rnd.Next(1, 101) < WarrantChance)
        {
            var selectWarrant = PossibleWarrants.SelectRandom();
            if (!Warrants.Contains(selectWarrant))
                Warrants.Add(selectWarrant);
            await BaseScript.Delay(100);
        }
    }

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