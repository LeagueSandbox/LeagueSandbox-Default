using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class SummonerExhaust : IGameScript
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

            ChampionStatModifier statMod = new ChampionStatModifier();
            statMod.MoveSpeed.PercentBonus -= 30.0f / 100.0f;
            statMod.AttackSpeed.PercentBonus -= 30.0f / 100.0f;
            statMod.Armor.BaseBonus -= 10;
            statMod.MagicResist.BaseBonus -= 10;
            target.AddStatModifier(statMod);
            ApiFunctionManager.AddParticleTarget((owner as Champion), "Global_SS_Exhaust.troy", target);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("SummonerExhaustDebuff", 2.5f, 1, target);
            ApiFunctionManager.CreateTimer(2.5f, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                target.RemoveStatModifier(statMod);
            });
        }
        
    }
}