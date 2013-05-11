namespace Game.Logic.Actions
{
    using Game.Logic;
    using System;

    public class PlayBackgroundSoundAction : BaseAction
    {
        private bool m_isPlay;

        public PlayBackgroundSoundAction(bool isPlay, int delay) : base(delay, 0x3e8)
        {
            this.m_isPlay = isPlay;
        }

        protected override void ExecuteImp(BaseGame game, long tick)
        {
            ((PVEGame) game).SendPlayBackgroundSound(this.m_isPlay);
            base.Finish(tick);
        }
    }
}

