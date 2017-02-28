using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Gangplank
{
    public class Q : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target){

        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target) {
            spell.AddProjectileTarget("pirate_parley_mis", target);
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) {
            var isCrit = new Random().Next(0, 100) < owner.GetStats().CriticalChance.Total;
            var baseDamage = new[] {20, 45, 70, 95, 120}[spell.Level - 1] + owner.GetStats().AttackDamage.Total;
            var damage = isCrit ? baseDamage * owner.GetStats().getCritDamagePct() / 100 : baseDamage;
            var goldIncome = new[] {4, 5, 6, 7, 8}[spell.Level - 1];
            if (target != null && !target.IsDead)
            {
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);
                if (target.IsDead)
                {
                    owner.GetStats().Gold += goldIncome;
                    var manaCost = new float[] {50, 55, 60, 65, 70}[spell.Level - 1];
                    var newMana = owner.GetStats().CurrentMana + manaCost / 2;
                    var maxMana = owner.GetStats().ManaPoints.Total;
                    if (newMana >= maxMana)
                    {
                        owner.GetStats().CurrentMana = maxMana;
                    }
                    else
                    {
                        owner.GetStats().CurrentMana = newMana;
                    }
                }
                projectile.setToRemove();
            }
        }
        public void OnUpdate(double diff) {

        }
     }
}