using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Missiles;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Statistics;

namespace Spells
{
    public class SummonerExhaust : GameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {

        }

        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {

            ChampionStatModifier statMod = new ChampionStatModifier();
            statMod.MoveSpeed.PercentBonus -= 30.0f / 100.0f;
            statMod.AttackSpeed.PercentBonus -= 30.0f / 100.0f;
            statMod.Armor.BaseBonus -= 10;
            statMod.MagicResist.BaseBonus -= 10;
            target.AddStatModifier(statMod);
            ApiFunctionManager.AddParticleTarget(owner, "Global_SS_Exhaust.troy", target);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("SummonerExhaustDebuff", 2.5f, 1, target);
            ApiFunctionManager.CreateTimer(2.5f, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                target.RemoveStatModifier(statMod);
            });
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
