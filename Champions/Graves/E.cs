using System.Numerics;
using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class GravesMove : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - owner.Position);
            var range = to * 425;
            var trueCoords = owner.Position + range;

            ApiFunctionManager.DashToLocation(owner, trueCoords.X, trueCoords.Y, 1200, false, "Spell3");
            var p = ApiFunctionManager.AddParticleTarget(owner, "Graves_Move_OnBuffActivate.troy", owner);
            owner.AddBuffGameScript("Quickdraw", "Quickdraw", spell, 4.0f, true);
            ApiFunctionManager.CreateTimer(4.0f, () =>
            {
                ApiFunctionManager.RemoveParticle(p);
            });
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
