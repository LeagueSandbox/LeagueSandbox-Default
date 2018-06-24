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
            ObjAIBase aiTarget = target as ObjAIBase;
            if(aiTarget == null)
            {
                return;
            }
            ChampionStatModifier statMod = new ChampionStatModifier();
            statMod.MoveSpeed.PercentBonus -= 30.0f / 100.0f;
            statMod.AttackSpeed.PercentBonus -= 30.0f / 100.0f;
            statMod.Armor.BaseBonus -= 10;
            statMod.MagicResist.BaseBonus -= 10;
            aiTarget.AddStatModifier(statMod);
            ApiFunctionManager.AddParticleTarget(owner, "Global_SS_Exhaust.troy", target);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("SummonerExhaustDebuff", 2.5f, 1, (ObjAIBase) target);
            ApiFunctionManager.CreateTimer(2.5f, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                aiTarget.RemoveStatModifier(statMod);
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

