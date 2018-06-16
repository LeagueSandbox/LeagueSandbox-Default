using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class Disintegrate : GameScript
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
            spell.AddProjectileTarget("Disintegrate", target, false);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var ap = owner.Stats.TotalAbilityPower * 0.8f;
            var damage = 45 + spell.Level * 35 + ap;
            if (target != null && !ApiFunctionManager.IsDead(target))
            {
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL,
                    false);
                if (target.IsDead)
                {
                    spell.LowerCooldown(0, spell.getCooldown());
                    float manaToRecover = 55 + spell.Level * 5;
                    var newMana = owner.Stats.CurrentPar + manaToRecover;
                    var maxMana = owner.Stats.TotalPar;
                    owner.Stats.CurrentPar = Math.Min(maxMana, newMana);
                }
            }

            projectile.setToRemove();
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
