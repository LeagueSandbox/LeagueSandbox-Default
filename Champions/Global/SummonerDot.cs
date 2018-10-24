using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;
using GameServerCore;

namespace Spells
{
    public class SummonerDot : IGameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var visualBuff = AddBuffHudVisual("SummonerDot", 4.0f, 1, BuffType.COMBAT_DEHANCER, (ObjAiBase) target, 4.0f);
            var p = AddParticleTarget(owner, "Global_SS_Ignite.troy", target, 1);
            var damage = new Damage(10 + owner.Stats.Level * 4, DamageType.DAMAGE_TYPE_TRUE, 
                DamageSource.DAMAGE_SOURCE_SUMMONER_SPELL, false);
            target.TakeDamage(owner, damage);
            for(float i = 1.0f; i < 5; ++i)
            {
                CreateTimer(1.0f * i, () =>
                {
                    target.TakeDamage(owner, damage);
                });
            }
            CreateTimer(4.0f, () =>
            {
                RemoveParticle(p);
            });
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }

        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }
    }
}

