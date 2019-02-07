using System;
using System.Numerics;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain;
using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;

namespace Spells
{
    public class PrimalSurge : IGameScript
    {
        public void OnStartCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
            spell.SpellAnimation("Spell3", owner);
        }

        public void OnFinishCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
            var ap = owner.Stats.AbilityPower.Total * 0.7f;
            float healthGain = new[] { 50, 85, 120, 155, 190 }[spell.Level - 1] + ap;

            var newHealth = target.Stats.CurrentHealth + healthGain;
            PerformHeal(owner, spell, target);            
        }

        public void ApplyEffects(IChampion owner, IAttackableUnit target, ISpell spell, IProjectile projectile)
        {
        }

        public void PerformHeal(IChampion owner, ISpell spell, IAttackableUnit target)
        {
            if (owner.Model == "Nidalee" && target != null)
            {
                var ap = owner.Stats.AbilityPower.Total * 0.5f;
                float healthGain = 5f + spell.Level * 40 + ap;

                var newHealth = target.Stats.CurrentHealth + healthGain;
                
                if (target != null)
                {
                    target.Stats.CurrentHealth = Math.Min(newHealth, target.Stats.HealthPoints.Total);
                    AddParticleTarget(owner, "nidalee_primalSurge_tar_flash.troy", target, 1);

                    ((ObjAiBase)target).AddBuffGameScript("NidaleeE", "NidaleeE", spell, 7f, true);
                }                
            }
            if (owner.Model == "Nidalee_Cougar")
            {
                return;
            }
        }
        
        public void OnUpdate(double diff)
        {
        }

        public void OnActivate(IChampion owner)
        {
        }

        public void OnDeactivate(IChampion owner)
        {
        }
    }
}

