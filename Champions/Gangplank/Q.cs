using System;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Missiles;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class Parley : IGameScript
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
            spell.AddProjectileTarget("pirate_parley_mis", target);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var isCrit = new Random().Next(0, 100) < owner.Stats.CriticalChance.Total;
            var baseDamage = new[] {20, 45, 70, 95, 120}[spell.Level - 1] + owner.Stats.AttackDamage.Total;
            var damage = isCrit ? baseDamage * owner.Stats.CriticalDamage.Total / 100 : baseDamage;
            var goldIncome = new[] {4, 5, 6, 7, 8}[spell.Level - 1];
            if (target != null && !target.IsDead)
            {
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK,
                    false);
                if (target.IsDead)
                {
                    owner.Stats.Gold += goldIncome;
                    var manaCost = new float[] {50, 55, 60, 65, 70}[spell.Level - 1];
                    var newMana = owner.Stats.CurrentMana + manaCost / 2;
                    var maxMana = owner.Stats.ManaPoints.Total;
                    if (newMana >= maxMana)
                    {
                        owner.Stats.CurrentMana = maxMana;
                    }
                    else
                    {
                        owner.Stats.CurrentMana = newMana;
                    }
                }

                projectile.SetToRemove();
            }
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
