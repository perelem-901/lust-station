using Content.Shared._Lust.PermeableSkin;
using Content.Shared.Body.Systems;
using Content.Shared.Chemistry;
using Content.Shared.Chemistry.Components;
using Content.Shared.FixedPoint;

namespace Content.Server._Lust.PermeableSkin;

public sealed class PermeableSkinSystem : EntitySystem
{
    [Dependency] private readonly SharedBloodstreamSystem _bloodstream = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<PermeableSkinComponent, ReactionEntityEvent>(OnReaction);
    }

    private void OnReaction(Entity<PermeableSkinComponent> ent, ref ReactionEntityEvent args)
    {
        if (args.Method != ReactionMethod.Touch)
            return;

        if (args.ReagentQuantity.Quantity == FixedPoint2.Zero)
            return;

        var transferAmount = FixedPoint2.New(args.ReagentQuantity.Quantity.Double() * ent.Comp.Permeability);
        if (transferAmount == FixedPoint2.Zero)
            return;

        var solution = new Solution();
        solution.AddReagent(args.ReagentQuantity.Reagent, transferAmount);

        _bloodstream.TryAddToBloodstream(ent.Owner, solution);
    }
}
