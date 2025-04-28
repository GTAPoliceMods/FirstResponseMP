using System.Collections.Generic;

namespace FirstResponseMP.Shared.Interfaces;

public interface IPedData
{
    public string FirstName { get; }
    public string LastName { get; }
    public string FullName { get; }

    public List<string> Warrants { get; }

    public void Regenerate();
 
    public void RegenerateWarrants();
    
    public string ToDataString();
    
    public object ToObject();
}