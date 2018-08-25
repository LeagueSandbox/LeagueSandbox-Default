using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class SummonerExhaust : IGameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var ai = target as ObjAiBase;
            if (ai != null)
            {
                var statMod = new StatsModifier();
                statMod.MoveSpeed.PercentBonus -= 30.0f / 100.0f;
                statMod.AttackSpeed.PercentBonus -= 30.0f / 100.0f;
                statMod.Armor.BaseBonus -= 10;
                statMod.MagicResist.BaseBonus -= 10;
                ai.AddStatModifier(statMod);
                ApiFunctionManager.AddParticleTarget(owner, "Global_SS_Exhaust.troy", target);
                var visualBuff = ApiFunctionManager.AddBuffHudVisual("SummonerExhaustDebuff", 2.5f, 1, (ObjAiBase)target);
                ApiFunctionManager.CreateTimer(2.5f, () =>
                {
                    ApiFunctionManager.RemoveBuffHudVisual(visualBuff);
                    ai.RemoveStatModifier(statMod);
                });
            }
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

