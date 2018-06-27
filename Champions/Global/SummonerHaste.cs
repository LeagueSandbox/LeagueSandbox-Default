using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class SummonerHaste : IGameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            StatsModifier statMod = new StatsModifier();
            statMod.MoveSpeed.PercentBonus = 27 / 100.0f;
            owner.AddStatModifier(statMod);
            var hasteBuff = ApiFunctionManager.AddBuffHudVisual("SummonerHaste", 10.0f, 1, owner, 10.0f);
            ApiFunctionManager.CreateTimer(10.0f, () => { owner.RemoveStatModifier(statMod); });
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
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

