using GameServerCore;
using GameServerCore.Domain;
using GameServerCore.Domain.GameObjects;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;
using System;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using GameServerCore.Enums;
using System.Numerics;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;

namespace Spells
{
    public class Bushwhack : IGameScript
    {
        public float petTimeAlive = 0.00f;

        public void OnActivate(IChampion owner)
        {
        }

        private void OnUnitHit(IAttackableUnit target, bool isCrit)
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
            //WIP
            var castrange = spell.SpellData.CastRange[0];
            var trapduration = 120.0f; 
            var fearduration = 1.5f;
            var ownerPos = new Vector2(owner.X, owner.Y);
            var spellPos = new Vector2(spell.X, spell.Y);

            if (owner.WithinRange(ownerPos, spellPos, castrange))
            {
                IMinion m = AddMinion(owner, "Nidalee_Spear", "Nidalee_Spear", spell.X, spell.Y); // no model here, don't do it
                
                m.StopMovement();
                
                m.Stats.AttackDamage.FlatBonus -= m.Stats.AttackDamage.Total;
                m.Stats.MoveSpeed.FlatBonus -= m.Stats.MoveSpeed.Total;
                AddParticle(owner, "Nidalee_Base_W_Cas.troy", spell.X, spell.Y);

                

                if (m.IsVisibleByTeam(owner.Team))
                {
                    if (!m.IsDead)
                    {
                        var units = GetUnitsInRange(m, 100, true);
                        var p = 0f;
                        // if over TrapDuration time, minion goes ded
                        //TODO:auto detect if enemy is near, then active it
                        //This version: you need to put trap under the minion, then work
                        for (p = 0f; p < trapduration; p += 0.1f)
                        {
                            foreach (var value in units)
                            {                                
                                if (owner.Team != value.Team && value is IAttackableUnit && !(value is IBaseTurret) && !(value is IObjAnimatedBuilding))
                                {                                    
                                    m.SetTargetUnit(value);
                                    
                                    AddBuffHudVisual("BushwhackDamage", 4f, 1, BuffType.COMBAT_DEHANCER, (ObjAiBase)value, 4f);
                                    var p1 = AddParticleTarget(owner, "Nidalee_Base_W_Tar.troy", value); 
                                    //about taking damage, I set it as 4 times
                                    for (petTimeAlive = 0.0f; petTimeAlive < 4f; petTimeAlive ++)
                                    {
                                        var apbonus = owner.Stats.AbilityPower.Total * 0.02f;
                                        var targethealth = value.Stats.CurrentHealth;
                                        var damage = spell.Level * 20 + (targethealth * (0.08f + spell.Level * 0.02f) + apbonus);
                                        var preDamage = damage / 4;
                                        CreateTimer(petTimeAlive, () =>
                                        {
                                            if (!value.IsDead && !m.IsDead)
                                            {
                                                value.TakeDamage(owner, preDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                                            }
                                        });
                                    }
                                    if (!m.IsDead)
                                    {
                                        m.Die(m);
                                        return;
                                    }
                                }
                            }
                        }
                        CreateTimer(trapduration, () =>
                        {
                            if (!m.IsDead)
                            {
                                AddParticle(owner, "caitlyn_yordleTrap_impact_debuf", m.X, m.Y);
                                AddParticleTarget(owner, "caitlyn_yordleTrap_trigger_sound.troy", m);
                                m.Die(m); //TODO: Fix targeting issues
                            }
                        });
                    }
                }
            }
        }

        public void ApplyEffects(IChampion owner, IAttackableUnit target, ISpell spell, IProjectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}