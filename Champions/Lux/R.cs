using System.Numerics;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class LuxMaliceCannon : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 3340;
            var trueCoords = current + range;

            spell.AddLaser("LuxMaliceCannon", trueCoords.X, trueCoords.Y);
            ApiFunctionManager.AddParticle(owner, "LuxMaliceCannon_beam.troy", trueCoords.X, trueCoords.Y);
            ApiFunctionManager.FaceDirection(owner, trueCoords, false);
            spell.SpellAnimation("SPELL4", owner);
            ApiFunctionManager.AddParticleTarget(owner, "LuxMaliceCannon_cas.troy", owner);
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            target.TakeDamage(owner, 200f + spell.Level * 100f + owner.Stats.AbilityPower.Total * 0.75f,
                DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
