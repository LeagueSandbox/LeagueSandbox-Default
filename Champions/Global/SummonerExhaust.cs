using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;

namespace Spells
{
    public class SummonerExhaust : GameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var ai = target as ObjAIBase;
            if (ai == null)
            {
                return;
            }

            target.Stats.SlowsApplied.Add(0.3f);
            target.Stats.PercentAttackSpeedDebuff.Add(-0.3f);
            target.Stats.FlatArmorReduction += 10;
            target.Stats.FlatMagicReduction += 10;
            ApiFunctionManager.AddParticleTarget(owner, "Global_SS_Exhaust.troy", target);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("SummonerExhaustDebuff", 2.5f, 1, BuffType.CombatDehancer, (ObjAIBase)target);
            ApiFunctionManager.CreateTimer(2.5f, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                target.Stats.SlowsApplied.Remove(0.3f);
                target.Stats.PercentAttackSpeedDebuff.Remove(-0.3f);
                target.Stats.FlatArmorReduction -= 10;
                target.Stats.FlatMagicReduction -= 10;
            });
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

