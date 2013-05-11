using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Logic.Phy.Object;
using Game.Logic.Effects;
namespace Game.Logic.Actions
{

    public class LivingOffSealAction:BaseAction
    {
        private Living m_Living;
        private Living m_Target;

        public LivingOffSealAction(Living Living, Living target, int delay)
            : base(delay, 0x3e8)
        {
            m_Living = Living;
            m_Target = target;
          
        }

        protected override void ExecuteImp(BaseGame game, long tick)
        {
            SealEffect effect = (SealEffect)m_Target.EffectList.GetOfType(eEffectType.SealEffect);
            if (effect != null)
            {
                effect.Stop();
            }
            base.Finish(tick);
        }

    }
}
