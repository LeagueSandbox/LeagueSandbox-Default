using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace MeditateBuff
{
    internal class MeditateBuff: BuffGameScript
    {
        private ChampionStatModifier _statMod;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            //in order to add the damage reduction I'll use armor & magic resist so here is how it works :
            //damage percentage that'll hit yi = 100 / 100 + armor |or| damage percentage = 100  / 100+ magic resist
            //this means that armor = (100 / damage percentage ) - 100 
            // yi reduces damage by 50/55/60/65/70%
            //that means damage that will hit him is 50/45/40/35/30%
            // & by having that we can know how much armor is needed in order to reduce the damage
            // during the operations 50% will become 0.5
            // example : (100 / 0.5 ) - 100 = 100armor / magic resist
            // yeh this is really broken but don't forget that it's yi
            _statMod = new ChampionStatModifier();
            _statMod.Armor.FlatBonus = (new float[] { 100f, 122.22f, 150.0f, 185.71f, 233.33f })[ownerSpell.Level - 1];
            _statMod.MagicResist.FlatBonus = (new float[] { 100f, 122.22f, 150.0f, 185.71f, 233.33f })[ownerSpell.Level - 1];
            unit.AddStatModifier(_statMod);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            unit.RemoveStatModifier(_statMod);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}