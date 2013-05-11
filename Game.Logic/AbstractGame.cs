using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base.Packets;
using Game.Logic.Phy.Object;
using System.Threading;

namespace Game.Logic
{
    public enum eRoomType
    {
        Match = 0,
        Freedom = 1,
        FightLib = 5,//FIGHT_LIB_ROOM
        Dungeon = 4,//DUNGEON_ROOM
        matchNpc = 9,
        Freshman = 10,
        AcademyDungeon = 11,//ACADEMY_DUNGEON_ROOM:int = 11;
        ScoreLeage = 12,//LEAGE_ROOM:int = 12;SCORE_ROOM:int = 12;        
        GuildLeageRank = 13,//GUILD_LEAGE_MODE:int = 13; RANK_ROOM:int = 13;        
        WordBoss = 14,//WORLD_BOSS_FIGHT:int = 14;
    }

    public enum eGameType
    {
        Free = 0,               //自由站
        Guild = 1,              //工会战
        Dungeon = 7,
        FightLib = 8,
        unknow1 = 2,
        ScoreLeage = 12,
        unknow3 = 17,
        WordBoss = 14,
        Freshman = 10,
        GuildLeageRank = 13,
        unknow7 = 15,
        unknow8 = 16,
        ALL = 4,                //不分类型
       
    }

    public enum eHardLevel
    {
        Simple = 0,
        Normal = 1,
        Hard = 2,
        Terror = 3,
    }

    public enum eGameState
    {
        Inited,
        Prepared,
        Loading,
        GameStartMovie,
        GameStart,
        Playing,
        GameOverMovie,
        GameOver,
        Stopped,
        SessionPrepared,
        ALLSessionStopped
    }

    public enum eLevelLimits
    {
        Other = 0,
        ZeroToTen = 1,
        ElevenToTwenty = 2,
        TwentyOneToThirty = 3,

    }

    public delegate void GameEventHandle(AbstractGame game);

    public class AbstractGame
    {
        private int m_id;

        protected eRoomType m_roomType;

        protected eGameType m_gameType;

        protected eMapType m_mapType;

        protected int m_timeType;

        public int Id
        {
            get { return m_id; }
        }

        public eRoomType RoomType
        {
            get { return m_roomType; }
        }

        //public eTeamType TeamType
        //{
        //    get { return m_teamType; }
        //}

        public eGameType GameType
        {
            get { return m_gameType; }
        }

        public int TimeType
        {
            get { return m_timeType; }
        }

        public AbstractGame(int id, eRoomType roomType, eGameType gameType, int timeType)
        {
            m_id = id;
            m_roomType = roomType;
            m_gameType = gameType;
            m_timeType = timeType;
            switch (m_roomType)
            {
                case eRoomType.Freedom:
                    m_mapType = eMapType.Normal;
                    break;
                case eRoomType.Match:
                    m_mapType = eMapType.PairUp;
                    break;
                default:
                    m_mapType = eMapType.Normal;
                    break;
            }
        }

        public virtual void Start()
        {
            OnGameStarted();
        }
        public virtual void Stop()
        {
            OnGameStopped();
        }

        public virtual bool CanAddPlayer() { return false; }
        public virtual void Pause(int time) { }
        public virtual void Resume() { }
        public virtual void ProcessData(GSPacketIn pkg) { }
        public virtual Player AddPlayer(IGamePlayer player) { return null; }
        public virtual Player RemovePlayer(IGamePlayer player, bool IsKick) { return null; }
        private int m_disposed = 0;
        public void Dispose() 
        {
            int disposed = Interlocked.Exchange(ref m_disposed, 1);
            if (disposed == 0)
            {
                Dispose(true);
                //GC.SuppressFinalize(this);
            }
        }
        protected virtual void Dispose(bool disposing) { }

        #region Events

        public event GameEventHandle GameStarted;
        public event GameEventHandle GameStopped;

        protected void OnGameStarted()
        {
            if (GameStarted != null) GameStarted(this);
        }

        protected void OnGameStopped()
        {
            if (GameStopped != null) GameStopped(this);
        }

        #endregion
    }
}
