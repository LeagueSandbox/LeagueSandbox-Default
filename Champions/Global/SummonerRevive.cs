using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System;

namespace Spells
{
    public class SummonerRevive : IGameScript
    {
        Unit owner;
        public void OnActivate(GameScriptInformation gameScriptInformation)
        {
            owner = gameScriptInformation.OwnerUnit;
            ApiEventManager.OnSpellFinishCast.AddListener(this, gameScriptInformation.OwnerSpell, OnFinishCasting);
        }

        public void OnDeactivate()
        {
        }

        public void OnFinishCasting(Unit target)
        {
            if (!owner.IsDead)
            {
                return;
            }
            (owner as Champion).Respawn();

            ChampionStatModifier statMod = new ChampionStatModifier();
            statMod.MoveSpeed.PercentBonus = 125.0f / 100.0f;
            owner.AddStatModifier(statMod);
            ApiFunctionManager.AddParticleTarget((owner as Champion), "Global_SS_Revive.troy", owner);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("SummonerReviveSpeedBoost", 12.0f, 1, owner);
            ApiFunctionManager.CreateTimer(12.0f, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                owner.RemoveStatModifier(statMod);
            });
        }
        
    }
}
