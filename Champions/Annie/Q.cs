using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class Disintegrate : IGameScript
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
            spell.AddProjectileTarget("Disintegrate", target, false);
        }
        public void ApplyEffects(Unit target, Projectile projectile) {
            var ap = owner.GetStats().AbilityPower.Total * 0.8f;
            var damage = 45 + spell.Level * 35 + ap;

            if (target != null && !ApiFunctionManager.IsDead(target))
            {
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                if (target.IsDead)
                {
                    spell.LowerCooldown(0, spell.getCooldown());
                    float manaToRecover = 55 + spell.Level * 5;
                    var newMana = owner.GetStats().CurrentMana + manaToRecover;
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
            }
            projectile.setToRemove();
        }
     }
}