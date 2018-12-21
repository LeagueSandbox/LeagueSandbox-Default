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
    public class JackInTheBox : IGameScript
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
            var fearduration = 0.5f + (0.25 * (spell.Level - 1));
            var jackduration = 5.0f;
            var apbonus = owner.Stats.AbilityPower.Total * 0.2f;
            var damage = 35 + ((15 * (spell.Level - 1)) + apbonus);
            var attspeed = 1 / 1.8f; // 1.8 attacks a second = .55... seconds per attack
            var castrange = spell.SpellData.CastRange[0];
            //TODO: Implement Fear buff
            //var fearrange = 300;
            var sightrange = 600;
            var ownerPos = new Vector2(owner.X, owner.Y);
            var spellPos = new Vector2(spell.X, spell.Y);

            if (owner.WithinRange(ownerPos, spellPos, castrange))
            {
                IMinion m = AddMinion(owner, "ShacoBox", "ShacoBox", spell.X, spell.Y, sightrange);
                AddParticle(owner, "JackintheboxPoof.troy", spell.X, spell.Y);

                if (m.IsVisibleByTeam(owner.Team))
                {
                    try
                    {
                        if (!m.IsDead)
                        {
                            var units = GetUnitsInRange(m, sightrange, true);
                            foreach (var value in units)
                            {
                                if (owner.Team != value.Team && value is IAttackableUnit && !(value is IBaseTurret) && !(value is IObjAnimatedBuilding))
                                {
                                    //TODO: Change TakeDamage to activate on Jack AutoAttackHit, not use CreateTimer, and make Pets use owner stats
                                    m.SetTargetUnit(value);
                                    m.AutoAttackTarget = value;
                                    m.AutoAttackProjectileSpeed = 1450;
                                    m.AutoAttackHit(value);
                                    for (petTimeAlive = 0.0f; petTimeAlive < jackduration; petTimeAlive += attspeed)
                                    {
                                        CreateTimer(petTimeAlive, () => {
                                            if (!value.IsDead && !m.IsDead)
                                            {
                                                value.TakeDamage(m, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                                            }
                                        });
                                    }
                                }
                            }
                            CreateTimer(jackduration, () =>
                            {
                                if (!m.IsDead)
                                {
                                    m.Die(m);
                                    RemoveMinion(m, m);
                                }
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
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

        public void OnData()
        {
        }
    }
}
