using System;
using System.Numerics;
using System.Linq;
using GameServerCore;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using GameServerCore.Domain.GameObjects;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using GameServerCore.Domain;
using LeagueSandbox.GameServer.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;

namespace Spells
{
    public class Pounce : IGameScript
    {
        IChampion _championRef;
        public void OnActivate(IChampion owner)
        {
        }

        public void OnDeactivate(IChampion owner)
        {
        }

        public void OnStartCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
            
        }

        public void OnFinishCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {                                    
            var damage = owner.Spells[3].Level * 50f + owner.Stats.AbilityPower.Total * 0.3f;
            var trueCoords = this.getTrueCoords(owner, spell, target);
            //To have damage in location, so add a minion to do AOE
            IMinion m = AddMinion(owner, "TestCube", "TestCube", trueCoords.X, trueCoords.Y);
            var distanTime = owner.GetDistanceTo(m) * 0.0007f;

            //wait for Dash API complete
            DashToLocation(owner, trueCoords.X, trueCoords.Y, 1200, false, "Spell2", 30, 0, 0, 0);
            owner.Stats.MoveSpeed.FlatBonus += 1250;
            CreateTimer(distanTime, () => { owner.Stats.MoveSpeed.FlatBonus -= 1250; });

            var p1 = AddParticle(owner, "Nidalee_Base_Cougar_W.troy", owner.X, owner.Y, 1);             
            CancelDash(owner);
            
            CreateTimer(distanTime, () =>
            {                
                foreach (var enemyTarget in GetUnitsInRange(m, 200, true)
                .Where(x => x.Team == CustomConvert.GetEnemyTeam(owner.Team)))
                {
                   
                    if (enemyTarget is IChampion || enemyTarget is IMinion || enemyTarget is IMonster && enemyTarget.Team != owner.Team)
                    {
                        AddParticleTarget(owner, "Nidalee_Base_Cougar_W_Tar.troy", enemyTarget);
                        enemyTarget.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                    }
                    var refund = (30f + owner.Spells[3].Level * 10f) / 100f;
                    if (enemyTarget.IsDead)
                    {
                        AddParticleTarget(owner, "Nidalee_Base_Cougar_W_Enhanced.troy", owner);
                        AddParticleTarget(owner, "Nidalee_Base_Cougar_W_Marked_Cas.troy", owner);
                        owner.Spells[1].LowerCooldown(refund);
                    }
                }
            });
            
            m.Die(m);
        }

        private Vector2 getTrueCoords(IChampion owner, ISpell spell, IAttackableUnit target)
        {
            var current = owner.GetPosition();
            var to = new Vector2(spell.X, spell.Y) - current;
            var trueCoords = new Vector2();

            if (to.Length() > 375)
            {
                to = Vector2.Normalize(to);
                var range = to * 375;
                trueCoords = new Vector2(current.X, current.Y) + range;
            }
            else
                trueCoords = new Vector2(spell.X, spell.Y);
            return trueCoords;
        }

        public void ApplyEffects(IChampion owner, IAttackableUnit target, ISpell spell, IProjectile projectile)
        {            
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
