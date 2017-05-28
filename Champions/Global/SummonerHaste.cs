using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System;

namespace Spells
{
    public class SummonerHaste : IGameScript
    {
        Unit owner;
        public void OnActivate(GameScriptInformation gameScriptInformation)
        {
            owner = gameScriptInformation.OwnerUnit;
            ApiEventManager.OnSpellCast.AddListener(this, gameScriptInformation.OwnerSpell, OnStartCasting);
        }

        public void OnDeactivate()
        {
        }

        public void OnStartCasting(Unit target)
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
    }
}
