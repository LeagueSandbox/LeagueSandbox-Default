using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;

namespace Spells
{
    public class ItemSwordOfFeastAndFamine : GameScript
    {

        private Champion _owningChampion;
        private BuffGameScriptController _botrkBuff;

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
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
            _owningChampion = owner;
            _botrkBuff = owner.AddBuffGameScript("BotrkBuff", "BotrkBuff", null);
        }

        public void OnDeactivate(Champion owner)
        {
            owner.RemoveBuffGameScript(_botrkBuff);
        }
    }
}
