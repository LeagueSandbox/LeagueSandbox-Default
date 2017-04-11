using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Gangplank
{
    public class Q : IGameScript
    {
        GameScriptInformation info;
        Spell spell;
        Unit owner;
        public void OnActivate(GameScriptInformation scriptInfo)
        {
            info = scriptInfo;
            spell = info.OwnerSpell;
            owner = info.OwnerUnit;
            //Setup event listeners
            ApiEventManager.OnSpellFinishCast.AddListener(this, spell, OnFinishCasting);
            ApiEventManager.OnSpellApplyEffects.AddListener(this, spell, ApplyEffects);
        }
        public void OnDeactivate() { }
        public void OnFinishCasting(Unit target) {
            spell.AddProjectileTarget("pirate_parley_mis", target);
        }
        public void ApplyEffects(Unit target, Projectile projectile) {
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
     }
}