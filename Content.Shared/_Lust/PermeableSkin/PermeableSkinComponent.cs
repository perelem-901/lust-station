using Robust.Shared.GameStates;

namespace Content.Shared._Lust.PermeableSkin;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class PermeableSkinComponent : Component
{
    [DataField, AutoNetworkedField]
    public double Permeability = 0.2;
}
