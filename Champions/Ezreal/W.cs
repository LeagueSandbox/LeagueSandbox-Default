using System.Numerics;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;
using GameServerCore.Domain.GameObjects;
using GameServerCore;
using GameServerCore.Enums;

namespace Spells
{
    public class EzrealEssenceFlux : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            AddParticleTarget(owner, "ezreal_bow_yellow.troy", owner, 1, "L_HAND");
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 1000;
            var trueCoords = current + range;
            spell.AddProjectile("EzrealEssenceFluxMissile", trueCoords.X, trueCoords.Y);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            IChampion champion = target as IChampion;
            if (champion != null)
            {
                if (owner.Team != champion.Team)
                {
                    var damage = new Damage(25 + (spell.Level * 45) + (owner.Stats.AbilityPower.Total * 0.8f),
                        DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                    champion.TakeDamage(owner, damage);
                }
                else
                {
                    champion.AddBuffGameScript("EzrealEssenceFluxBuff", "EzrealEssenceFluxBuff", spell,
                        5.0f, true);
                }
            }
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
