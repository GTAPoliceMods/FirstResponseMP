using System;
using System.Collections.Generic;
using CitizenFX.Core;
using FirstResponseMP.Shared.Objects;
using PedData = FirstResponseMP.Client.Objects.PedData;

namespace FirstResponseMP.Client.Functions;

public class MDTFunctions
{
    static List<string> FirstNames =
    [
        "James", "John", "Robert", "Michael", "William",
        "David", "Richard", "Joseph", "Thomas", "Charles",
        "Christopher", "Daniel", "Matthew", "Anthony", "Donald",
        "Mark", "Paul", "Steven", "Andrew", "Kenneth",
        "Joshua", "Kevin", "Brian", "George", "Edward",
        "Mary", "Patricia", "Jennifer", "Linda", "Elizabeth",
        "Barbara", "Susan", "Jessica", "Sarah", "Karen",
        "Nancy", "Lisa", "Betty", "Margaret", "Sandra",
        "Ashley", "Kimberly", "Emily", "Donna", "Michelle",
        "Dorothy", "Carol", "Amanda", "Melissa", "Deborah"
    ];

    private static List<string> LastNames =
    [
        "Smith", "Johnson", "Williams", "Brown", "Jones",
        "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
        "Hernandez", "Lopez", "Wilson", "Anderson", "Taylor",
        "Moore", "Jackson", "Martin", "Lee", "Thompson",
        "White", "Harris", "Clark", "Lewis", "Robinson",
        "Walker", "Young", "Allen", "King", "Wright",
        "Scott", "Green", "Baker", "Adams", "Nelson",
        "Hill", "Campbell", "Mitchell", "Roberts", "Carter",
        "Phillips", "Evans", "Turner", "Torres", "Parker",
        "Collins", "Edwards", "Stewart", "Flores", "Morris"
    ];

    public static PedData? GetPedData(Ped ped) => throw new NotImplementedException();

    public static void SetPedData(Ped ped, PedData pedData) => throw new NotImplementedException();

    public static PedData EnsurePedData(Ped ped) => throw new NotImplementedException();
}