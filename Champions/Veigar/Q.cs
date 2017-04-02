using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Veigar
{
    public class Q : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target){
            if (owner.Skin == 8)
            {
                ApiFunctionManager.AddParticleTarget(owner, "Veigar_Skin08_Q_cas.troy", owner);
            }
            else
            {
                ApiFunctionManager.AddParticleTarget(owner, "Veigar_Base_Q_cas.troy", owner);
            }
        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            spell.AddProjectileTarget("VeigarBalefulStrike", target);
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            // 60% of your own AP
            var ap = owner.GetStats().AbilityPower.Total * 0.6f;
            // 80/125/170/215/260 Damage + Your AP
            var damage = 35 + spell.Level * 45 + ap;
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            if (owner.Skin == 8)
            {
                ApiFunctionManager.AddParticleTarget(owner, "Veigar_Skin08_Q_tar.troy", target);
            }
            else
            {
                ApiFunctionManager.AddParticleTarget(owner, "Veigar_Base_Q_tar.troy", target);
            }
            projectile.setToRemove();
            //if Q kills a minion --> Add 1/2/3/4/5 permanent AP
            if (ApiFunctionManager.IsDead(target) && ApiFunctionManager.UnitIsMinion(target))
            {
                //Buffs/VeigarQ/VeigarQ.cs
                owner.AddBuffGameScript("VeigarQ","VeigarQ", spell);
                if (owner.Skin == 8)
                {
                    ApiFunctionManager.AddParticleTarget(owner, "Veigar_Skin08_Q_powerup.troy", owner);
                    ApiFunctionManager.AddParticleTarget(owner, "Veigar_Skin08_Q_minionKill.troy", owner);
                }
                else
                {
                    ApiFunctionManager.AddParticleTarget(owner, "Veigar_Base_Q_powerup.troy", owner);
                }
            }
            //if Q kills a Champion --> Add 2/4/6/8/10 permanent AP
            if (ApiFunctionManager.IsDead(target) && ApiFunctionManager.UnitIsChampion(target))
            {
                //Buffs/VeigarQ/VeigarQ.cs
                owner.AddBuffGameScript("VeigarQ", "VeigarQ", spell);
                owner.AddBuffGameScript("VeigarQ", "VeigarQ", spell);
                if (owner.Skin == 8)
                {
                    ApiFunctionManager.AddParticleTarget(owner, "Veigar_Skin08_Q_powerup.troy", owner);
                }
                else
                {
                    ApiFunctionManager.AddParticleTarget(owner, "Veigar_Base_Q_powerup.troy", owner);
                }
            }
            //if Q kills a Monster --> Add 2/4/6/8/10 permanent AP
            if (ApiFunctionManager.IsDead(target) && ApiFunctionManager.UnitIsMonster(target))
            {
                //Buffs/VeigarQ/VeigarQ.cs
                owner.AddBuffGameScript("VeigarQ", "VeigarQ", spell);
                owner.AddBuffGameScript("VeigarQ", "VeigarQ", spell);
                if (owner.Skin == 8)
                {
                    ApiFunctionManager.AddParticleTarget(owner, "Veigar_Skin08_Q_powerup.troy", owner);
                }
                else
                {
                    ApiFunctionManager.AddParticleTarget(owner, "Veigar_Base_Q_powerup.troy", owner);
                }
            }
        }
        public void OnUpdate(double diff) { }
     }
}