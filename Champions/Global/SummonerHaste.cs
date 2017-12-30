using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System;

namespace Spells
{
    public class SummonerHaste : GameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, ObjAIBase target)
        {
            ChampionStatModifier statMod = new ChampionStatModifier();
            statMod.MoveSpeed.PercentBonus = 27 / 100.0f;
            owner.AddStatModifier(statMod);
            var HasteBuff = ApiFunctionManager.AddBuffHUDVisual("SummonerHaste", 10.0f, 1, owner, 10.0f);
            ApiFunctionManager.CreateTimer(10.0f, () =>
            {
               owner.RemoveStatModifier(statMod);
            });
        }

        public void OnFinishCasting(Champion owner, Spell spell, ObjAIBase target)
        {

        }

        public void ApplyEffects(Champion owner, ObjAIBase target, Spell spell, Projectile projectile)
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
