using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class VayneTumble : GameScript
    {

        private bool _nextAutoBonusDamage = false;
        private bool _listenerAdded = false;
        private Champion _owningChampion;
        private Spell _owningSpell;
        private Buff _tumbleBuff;

        public void OnActivate(Champion owner)
        {

        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {

            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 300;
            var trueCoords = current + range;

            ApiFunctionManager.DashToLocation(owner, trueCoords.X, trueCoords.Y, 1200, false,"Spell1");

            if (!_listenerAdded)
            {
                ApiEventManager.OnHitUnit.AddListener(this, owner, OnAutoAttack);
                _listenerAdded = true;
            }
            if (_owningChampion != owner)
            {
                _owningChampion = owner;
            }
            if (_owningSpell != spell)
            {
                _owningSpell = spell;
            }
            _nextAutoBonusDamage = true;
            _tumbleBuff = ApiFunctionManager.AddBuffHUDVisual("VayneTumble", 6.0f, 1, owner);
            ApiFunctionManager.CreateTimer(6.0f, () =>
            {
                // If auto has not yet been consumed
                if (_nextAutoBonusDamage == true)
                {
                    ApiFunctionManager.RemoveBuffHUDVisual(_tumbleBuff);
                    _nextAutoBonusDamage = false;
                }
            });
        }

        void OnAutoAttack(AttackableUnit target, bool isCrit)
        {
            if (_nextAutoBonusDamage)
            {
                _nextAutoBonusDamage = false;
                ApiFunctionManager.RemoveBuffHUDVisual(_tumbleBuff);
                var ad = (new float[] { 0.3f, 0.35f, 0.4f, 0.45f, 0.5f }[_owningSpell.Level - 1]) * _owningChampion.GetStats().AttackDamage.Total;
                target.TakeDamage(_owningChampion, ad, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PASSIVE, false);
            }
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
