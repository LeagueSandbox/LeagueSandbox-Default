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
        private IGame game;
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
            bool game = owner.RefGame.IsRunning;
            
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
                IMinion Jack = new Minion(owner.RefGame, owner, spell.X, spell.Y, "ShacoBox", "ShacoBox", sightrange, 0);
                owner.RefGame.ObjectManager.AddObject(Jack);
                Jack.SetVisibleByTeam(owner.Team, true);
                AddParticle(owner, "JackintheboxPoof.troy", spell.X, spell.Y);

                if (Jack.IsVisibleByTeam(owner.Team))
                {
                    try
                    {
                        if (!Jack.IsDead)
                        {
                            var units = GetUnitsInRange(Jack, sightrange, true);
                            foreach (var value in units)
                            {
                                if (owner.Team != value.Team && value is IAttackableUnit && !(value is IBaseTurret) && !(value is IObjAnimatedBuilding))
                                {
                                    //TODO: Change TakeDamage to activate on Jack AutoAttackHit and not use CreateTimer
                                    Jack.SetTargetUnit(value);
                                    Jack.AutoAttackTarget = value;
                                    Jack.AutoAttackProjectileSpeed = 1450;
                                    Jack.AutoAttackHit(value);
                                    for (petTimeAlive = 0.0f; petTimeAlive < jackduration; petTimeAlive += attspeed)
                                    {
                                        CreateTimer(petTimeAlive, () => {
                                            if (!(value.IsDead))
                                            {
                                                value.TakeDamage(Jack, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                                            }
                                        });
                                    }
                                }
                            }
                            // Sorry for using 2 timers, couldn't find out how to do it more effectively.
                            CreateTimer(jackduration, () =>
                            {
                                Jack.Die(Jack);
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
