using System.Numerics;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace LeagueSandbox_Default.Champions.Lucian
{
    public class LucianQ : IGameScript
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

            spell.AddLaser("LucianQ", trueCoords.X, trueCoords.Y);
            spell.SpellAnimation("SPELL1", owner);
            ApiFunctionManager.AddParticle(owner, "Lucian_Q_laser.troy", trueCoords.X, trueCoords.Y);
            ApiFunctionManager.AddParticleTarget(owner, "Lucian_Q_cas.troy", owner);
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var damage = owner.Stats.AttackDamage.Total * (0.45f + spell.Level * 0.15f) + (50 + spell.Level * 30);
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL,
                false);
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
