using System.Numerics;
using GameServerCore.Enums;
using GameServerCore.Domain.GameObjects;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using GameServerCore.Domain;
using LeagueSandbox.GameServer.Scripting.CSharp;
using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects;

namespace Spells
{
    public class Takedown : IGameScript
    {
        public static Particle mark;
        public static float marktime = 6.0f;
        public static float marktimeactive;
        public static float updateinterval = 0.1f;
        static IChampion owner;
        static IAttackableUnit target;

        public void OnActivate(IChampion owner)
        {
            Takedown.owner = owner;
            ApiEventManager.OnHitUnit.AddListener(this, owner, OnProc);            
            ApiEventManager.OnUpdate.AddListener(this, OnUpdate);             
        }

        public static void OnProc(IAttackableUnit target, bool isCrit)
        {
            if (mark == null)
            {
                return;
            }

            var ad = owner.Stats.AttackDamage.Total * 0.75f;
            var ap = owner.Stats.AbilityPower.Total * 0.24f;
            //TODO 1 : add MissingHealth to increase damage
            //TODO 2 : add Maximum Damage
            var damage = new[] {4,20,50,90}[owner.Spells[3].Level -1] + ad + ap;
            
            RemoveParticle(mark);
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            AddParticleTarget(owner, "Nidalee_Base_Cougar_Q_Tar.troy", target, 1);
            mark = null;
            //W CD refund
            if (target.IsDead)
            {
                AddParticleTarget(owner, "Nidalee_Base_Cougar_W_Enhanced.troy", owner);
                AddParticleTarget(owner, "Nidalee_Base_Cougar_W_Marked_Cas.troy", owner);
                var refund = (30f + owner.Spells[3].Level * 10) / 100f;
                owner.Spells[1].LowerCooldown(refund);
            };
        }

        public void OnDeactivate(IChampion owner)
        {
        }

        public void OnStartCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
            
        }

        public void OnFinishCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {            
            mark = AddParticleTarget(owner, "Nidalee_Base_Cougar_Q_Buf.troy", owner, 1, "R_HAND");            
            owner.Stats.Range.FlatBonus += 75;
            var b1 = AddBuffHudVisual("Takedown", 4f, 1, BuffType.COMBAT_ENCHANCER, owner);
            for (marktimeactive = 0.0f; marktimeactive < marktime; marktimeactive += updateinterval)
            {
                LogInfo("Current Mark Time: " + marktimeactive);
                if (mark == null)
                {
                    RemoveParticle(mark);
                    return;
                }                
                CreateTimer(marktimeactive, () => {
                    if (owner.IsAttacking && owner.AutoAttackTarget != null && mark != null)
                    {
                        LogInfo("You auto attacked a marked target.");
                        RemoveBuffHudVisual(b1);
                        RemoveParticle(mark);
                        OnProc(owner.AutoAttackTarget, false);
                        owner.Stats.Range.FlatBonus -= 75;
                        spell.SpellAnimation("Spell1", owner);
                        return;
                    }
                });
            }
            CreateTimer(4f,()=> 
            {
                if (mark != null)
                {
                RemoveBuffHudVisual(b1);
                RemoveParticle(mark);
                mark = null;
                }
            });
            
        }

        public void ApplyEffects(IChampion owner, IAttackableUnit target, ISpell spell, IProjectile projectile)
        {

        }

        public void OnUpdate(float diff)
        {

        }
    }
}
