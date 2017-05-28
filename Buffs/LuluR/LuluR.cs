using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace LuluR
{
    class LuluR : IGameScript
    {
        GameScriptInformation info;
        Spell spell;
        Unit owner;
        Unit target;
        ChampionStatModifier statMod;
        float healthBefore;
        float meantimeDamage;
        float healthNow;
        float healthBonus;
        public void OnActivate(GameScriptInformation scriptInfo)
        {
            info = scriptInfo;
            spell = info.OwnerSpell;
            owner = info.OwnerUnit;
            target = info.TargetUnit;

            statMod = new ChampionStatModifier();
            statMod.Size.PercentBonus = statMod.Size.PercentBonus + 1;
            healthBefore = target.GetStats().CurrentHealth;
            healthBonus = 150 + 150 * owner.GetStats().Level;
            statMod.HealthPoints.BaseBonus = statMod.HealthPoints.BaseBonus + 150 + 150 * owner.GetStats().Level;
            target.GetStats().CurrentHealth = target.GetStats().CurrentHealth + 150 + 150 * owner.GetStats().Level;
            target.AddStatModifier(statMod);
        }
        public void OnDeactivate()
        {
            healthNow = target.GetStats().CurrentHealth - healthBonus;
            meantimeDamage = healthBefore - healthNow;
            float bonusDamage = healthBonus - meantimeDamage;
            target.RemoveStatModifier(statMod);
            if (target.GetStats().CurrentHealth > target.GetStats().HealthPoints.Total)
            {
                target.GetStats().CurrentHealth = target.GetStats().CurrentHealth - bonusDamage;
            }
        }
    }
}
