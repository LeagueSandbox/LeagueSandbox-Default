using System;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class EzrealTrueshotBarrage : GameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            ApiFunctionManager.AddParticleTarget(owner, "Ezreal_bow_huge.troy", owner, 1, "L_HAND");
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 20000;
            var trueCoords = current + range;

            spell.AddProjectile("EzrealTrueshotBarrage", trueCoords.X, trueCoords.Y, true);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var reduc = Math.Min(projectile.ObjectsHit.Count, 7);
            var bonusAd = owner.GetStats().AttackDamage.Total - owner.GetStats().AttackDamage.BaseValue;
            var ap = owner.GetStats().AbilityPower.Total * 0.9f;
            var damage = 200 + spell.Level * 150 + bonusAd + ap;
            target.TakeDamage(owner, damage * (1 - reduc / 10), DamageType.DAMAGE_TYPE_MAGICAL,
                DamageSource.DAMAGE_SOURCE_SPELL, false);
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
