using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class LucianQ : GameScript
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
            var range = to * 1100;
            var trueCoords = current + range;

            spell.AddLaser(trueCoords.X, trueCoords.Y);
            spell.spellAnimation("SPELL1", owner);
            ApiFunctionManager.AddParticle(owner, "Lucian_Q_laser.troy", trueCoords.X, trueCoords.Y);
            ApiFunctionManager.AddParticleTarget(owner, "Lucian_Q_cas.troy", owner);
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var damage = owner.GetStats().AttackDamage.Total * (0.45f + spell.Level * 0.15f) + (50 + spell.Level * 30);
            spell.Target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL,
                false);
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
