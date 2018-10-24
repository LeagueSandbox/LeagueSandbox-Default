using System.Numerics;
using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;
using GameServerCore;

namespace Spells
{
    public class OlafAxeThrowCast : IGameScript
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
            var current = new Vector2(owner.X, owner.Y);
            var to = new Vector2(spell.X, spell.Y) - current;
            Vector2 trueCoords;

            if (to.Length() > 1651)
            {
                to = Vector2.Normalize(to);
                var range = to * 1651;
                trueCoords = current + range;
            }
            else
            {
                trueCoords = new Vector2(spell.X, spell.Y);
            }

            spell.AddProjectile("OlafAxeThrowDamage", trueCoords.X, trueCoords.Y);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            AddParticleTarget(owner, "olaf_axeThrow_tar.troy", target, 1);
            var ad = owner.Stats.AttackDamage.Total * 1.1f;
            var ap = owner.Stats.AttackDamage.Total * 0.0f;
            var damage = new Damage(15 + spell.Level * 20 + ad + ap, DamageType.DAMAGE_TYPE_PHYSICAL, 
            DamageSource.DAMAGE_SOURCE_ATTACK, false);
            target.TakeDamage(owner, damage);
        }

        public void OnUpdate(double diff)
        {
        }
    }
}

