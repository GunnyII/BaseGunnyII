using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;

namespace Game.Logic.Actions
{
    public class CheckPVEGameStateAction : IAction
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private long m_time;
        private bool m_isFinished;

        public CheckPVEGameStateAction(int delay)
        {
            m_time = TickHelper.GetTickCount() + delay;
            m_isFinished = false;
        }

        public void Execute(BaseGame game, long tick)
        {
            if (m_time <= tick && game.GetWaitTimer() < tick)
            {
                PVEGame pve = game as PVEGame;
                if (pve != null)
                {
                    switch (pve.GameState)
                    {
                        case eGameState.Inited:
                            pve.Prepare();
                            break;
                        case eGameState.Prepared:
                            pve.PrepareNewSession();
                            break;
                        case eGameState.SessionPrepared:
                            if (pve.CanStartNewSession())
                            {
                                pve.StartLoading();
                                break;

                            }
                            else
                            {
                                game.WaitTime(1000);
                            }
                            break;
                        case eGameState.Loading:
                            if (pve.IsAllComplete())
                            {
                                pve.StartGame();
                                break;

                            }
                            else
                            {
                                game.WaitTime(1000);
                            }
                            break;
                        //TODO
                        case eGameState.GameStartMovie:
                            if (game.CurrentActionCount <= 1)
                            {
                                pve.StartGame();
                                break;

                            }
                            else
                            {
                                pve.StartGameMovie();
                            }
                            break;
                        case eGameState.GameStart:
                            pve.PrepareNewGame();
                            break;
                        case eGameState.Playing:
                            
                            if ((pve.CurrentLiving == null || pve.CurrentLiving.IsAttacking == false) && game.CurrentActionCount <= 1)
                            {
                                if (pve.CanGameOver())
                                {
                                    //log.Error(string.Format("stage : {0}", -1));
                                    pve.GameOver();
                                    break;

                                }
                                else
                                {
                                    //log.Error(string.Format("stage : {0}", 0));
                                    pve.NextTurn();
                                }
                            }
                            break;
                        case eGameState.GameOver:
                            if (pve.HasNextSession())
                            {
                                //log.Error(string.Format("stage : {0}", 1));
                                pve.PrepareNewSession();
                                break;
                            }
                            else
                            {
                                //log.Error(string.Format("stage : {0}", 2));
                                pve.GameOverAllSession();
                            }
                            break;
                        case eGameState.ALLSessionStopped:
                                                       
                            if ((pve.PlayerCount != 0) && (pve.WantTryAgain != 0))
                            {
                                if (pve.WantTryAgain == 1)
                                {
                                    pve.SessionId--;
                                    pve.PrepareNewSession();
                                    //log.Error(string.Format("stage : {0}", 3)); 
                                }
                                else
                                {
                                    game.WaitTime(1000);
                                    //log.Error(string.Format("stage : {0}", 4)); 
                                }
                                break;
                            }
                        pve.Stop();
                        break;

                    }
                }
                m_isFinished = true;
            }
        }

        public bool IsFinished(long tick)
        {
            return m_isFinished;
        }
    }
}
