using System.Linq;
using GameServerCore;
using GameServerCore.Enums;
using GameServerCore.Domain.GameObjects;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using GameServerCore.Domain;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class KarthusLayWasteA1 : IGameScript
    {
        public void OnActivate(IChampion owner)
        {
        }

        public void OnDeactivate(IChampion owner)
        {
        }

        public void OnStartCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
            AddParticleTarget(owner, "Karthus_Base_Q_Hand_Glow.troy", owner, 1, "R_Hand");
            AddParticle(owner, "Karthus_Base_Q_Point.troy", spell.X, spell.Y);
            AddParticle(owner, "Karthus_Base_Q_Ring.troy", spell.X, spell.Y);
            AddParticle(owner, "Karthus_Base_Q_Skull_Child.troy", spell.X, spell.Y);
            CreateTimer(0.5f, () =>
            {
                IGameObject m = AddMinion(owner, "TestCube", "TestCube", spell.X, spell.Y);
                var range = GetUnitsInRange(m, 100, true);
                var ap = owner.Stats.AbilityPower.Total;
                var damage = 20f + spell.Level * 20f + ap * 0.3f;
                
                foreach (var units in range)
                {
                    if (units.Team != owner.Team && units is IChampion || units is IMinion)
                    {
                        if (range.Count == 1)
                        {
                            AddParticle(owner, "Karthus_Base_Q_Hit_Miss.troy", spell.X, spell.Y);
                        }
                        if (range.Count == 2)
                        {
                            damage *= 2;
                            AddParticle(owner, "Karthus_Base_Q_Hit_Single.troy", spell.X, spell.Y);
                            units.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, true);
                        }
                        if (range.Count > 2)
                        {
                            AddParticle(owner, "Karthus_Base_Q_Hit_Many.troy", spell.X, spell.Y);
                            units.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                        }
                    }
                }
                m.SetToRemove();
                AddParticle(owner, "Karthus_Base_Q_Explosion.troy", spell.X, spell.Y);
                AddParticle(owner, "Karthus_Base_Q_Explosion_Sound.troy", spell.X, spell.Y);
            });
        }   

        public void OnFinishCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
            
        }

        public void ApplyEffects(IChampion owner, IAttackableUnit target, ISpell spell, IProjectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
