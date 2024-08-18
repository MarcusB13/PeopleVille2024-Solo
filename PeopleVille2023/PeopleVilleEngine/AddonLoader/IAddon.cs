using System.Diagnostics.Contracts;

namespace PeopleVilleEngine;

public interface IAddOn {
    public string ServiceName { get; }
    public void Execute(Village village);

}
