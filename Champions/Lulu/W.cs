using GameServerCore.Domain.GameObjects;
using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class LuluW : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            spell.SpellAnimation("SPELL2", owner);
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var champion = (Champion) target;
            if (champion.Team != owner.Team)
            {
                spell.AddProjectileTarget("LuluWTwo", target);
            }
            else
            {
                var p1 = AddParticleTarget(owner, "Lulu_W_buf_02.troy", target, 1);
                var p2 = AddParticleTarget(owner, "Lulu_W_buf_01.troy", target, 1);
                var time = 2.5f + 0.5f * spell.Level;
                ((ObjAiBase) target).AddBuffGameScript("LuluWBuff", "LuluWBuff", spell, time, true);
                CreateTimer(time, () =>
                {
                    RemoveParticle(p1);
                    RemoveParticle(p2);
                });
            }
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            // TODO: problematic code, if the target is only AttackableUnit crash will occure
            var champion = (Champion) target;
            var time = 1 + 0.25f * spell.Level;
            champion.AddBuffGameScript("LuluWDebuff", "LuluWDebuff", spell, time, true);
            var model = champion.Model;
            ChangeModel(owner.Skin, target);

            var p = AddParticleTarget(owner, "Lulu_W_polymorph_01.troy", target, 1);
            CreateTimer(time, () =>
            {
                RemoveParticle(p);
                champion.Model = model;
            });
            projectile.SetToRemove();
        }

        public void OnUpdate(double diff)
        {
        }

        private void ChangeModel(int skinId, AttackableUnit target)
        {
            switch (skinId)
            {
                case 0:
                    ((Champion) target).Model = "LuluSquill";
                    break;
                case 1:
                    ((Champion) target).Model = "LuluCupcake";
                    break;
                case 2:
                    ((Champion) target).Model = "LuluKitty";
                    break;
                case 3:
                    ((Champion) target).Model = "LuluDragon";
                    break;
                case 4:
                    ((Champion) target).Model = "LuluSnowman";
                    break;
            }
        }
    }
}
