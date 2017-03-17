using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System;

namespace Global
{
    public class SummonerRevive : GameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {

        }

        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            if (!owner.IsDead)
            {
                return;
            }
            owner.Respawn();
            ApiFunctionManager.AddParticleTarget(owner, "Global_SS_Revive.troy", owner);
        }

        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
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
