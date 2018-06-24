using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class GravesMove : GameScript
    {

        private Champion _owningChampion;
        private bool _listenerActivated = false;
        private Spell _owningSpell;


        public void OnActivate(Champion owner)
        {
        }

        private void ReduceCooldown(AttackableUnit unit, bool isCrit)
        {
            _owningSpell.LowerCooldown(2, 1);
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {

            _owningChampion = owner;
            _owningSpell = spell;
            if (!_listenerActivated)
            {
                _listenerActivated = true;
                ApiEventManager.OnHitUnit.AddListener(this, owner, ReduceCooldown);
            }
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 425;
            var trueCoords = current + range;

            ApiFunctionManager.DashToLocation(owner, trueCoords.X, trueCoords.Y, 1200, false, "Spell3");
            Particle p = ApiFunctionManager.AddParticleTarget(owner, "Graves_Move_OnBuffActivate.troy", owner);
            var visualBuff1 = ApiFunctionManager.AddBuffHUDVisual("GravesMoveSteroid", 5.0f, 1, owner, 4.0f);
            var buff = owner.AddBuffGameScript("Quickdraw", "Quickdraw", spell, 4.0f, true);
            ApiFunctionManager.CreateTimer(4.0f, () =>
            {
                ApiFunctionManager.RemoveParticle(p);
            });
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
