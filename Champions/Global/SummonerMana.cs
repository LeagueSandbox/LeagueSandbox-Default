using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System;

namespace Spells
{
    public class SummonerMana : IGameScript
    {
        private const float PERCENT_MAX_MANA_HEAL = 0.40f;
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
            var units = ApiFunctionManager.GetChampionsInRange(owner, 600, true);
            Champion nearbychampion = null;
            float lowestManaPercentage = 100;
            float maxMana;
            float newMana;
            for (var i = 0; i <= units.Count - 1; i++)
            {
                var value = units[i];
                if (owner.Team == value.Team && i != 0)
                {
                    var currentMana = value.GetStats().CurrentMana;
                    maxMana = value.GetStats().ManaPoints.Total;
                    if (currentMana * 100 / maxMana < lowestManaPercentage && owner != value)
                    {
                        lowestManaPercentage = currentMana * 100 / maxMana;
                        nearbychampion = value;
                    }
                }
            }
            if (nearbychampion != null)
            {
                var mp2 = nearbychampion.GetStats().CurrentMana;
                var maxMp2 = nearbychampion.GetStats().ManaPoints.Total;
                if (mp2 + maxMp2 * PERCENT_MAX_MANA_HEAL < maxMp2)
                    nearbychampion.GetStats().CurrentMana = mp2 + maxMp2 * PERCENT_MAX_MANA_HEAL;
                else
                    nearbychampion.GetStats().CurrentMana = maxMp2;
                ApiFunctionManager.AddParticleTarget(nearbychampion, "global_ss_clarity_02.troy", nearbychampion);
            }
            var mp = owner.GetStats().CurrentMana;
            var maxMp = owner.GetStats().ManaPoints.Total;
            if (mp + maxMp * PERCENT_MAX_MANA_HEAL < maxMp)
                owner.GetStats().CurrentMana = mp + maxMp * PERCENT_MAX_MANA_HEAL;
            else
                owner.GetStats().CurrentMana = maxMp;
            ApiFunctionManager.AddParticleTarget((owner as Champion), "global_ss_clarity_02.troy", owner);
        }
    }
}
