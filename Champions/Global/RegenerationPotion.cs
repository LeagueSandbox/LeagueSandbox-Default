using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;

namespace Spells
{
    public class RegenerationPotion : GameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            for (int i = 0; i < 30; i++)
            {
                ApiFunctionManager.CreateTimer(i * 0.5f, () =>
                {
                    owner.RestoreHealth(5.0f); // This will cause Health Potions to have half effect on champions effected by Grievous Wounds.
                });
            }
            var p = ApiFunctionManager.AddParticleTarget(owner, "GLOBAL_Item_HealthPotion.troy", owner);
            ApiFunctionManager.CreateTimer(15f, () =>
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

        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }
    }
}