using System;
using System.Collections.Generic;
using System.Text;

namespace SqlDataProvider.Data
{
    public class PlayerInfoHistory : DataObject
    {
        private DateTime _lastQuestsTime;
        private DateTime _lastTreasureTime;
        private int _userID;
        private Dictionary<int, int> m_composeState;
        private object m_composeStateLocker = new object();
        private string m_composeStateString;
        private static readonly int m_exist = 15;

        private void ClearExpireComposeState()
        {
            int[] array = new int[m_composeState.Keys.Count];
            m_composeState.Keys.CopyTo(array, 0);
            Array.Sort<int>(array);
            int exist = m_exist;
            for (int i = array.Length - 1; i >= 0; i--)
            {
                exist--;
                if (exist < 0)
                {
                    m_composeState.Remove(array[i]);
                }
            }
        }

        public int ComposeStateLockIncrement(int key)
        {
            lock (m_composeStateLocker)
            {
                Dictionary<int, int> dictionary;
                int num3;
                base._isDirty = true;
                if (!m_composeState.ContainsKey(key))
                {
                    m_composeState.Add(key, 0);
                }
                (dictionary = m_composeState)[num3 = key] = dictionary[num3] + 1;
                return m_composeState[key];
            }
        }

        private string JointStateString(Dictionary<int, int> stateDict)
        {
            List<string> list = new List<string>();
            Dictionary<int, int>.Enumerator enumerator = m_composeState.GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<int, int> current = enumerator.Current;
                current = enumerator.Current;
                list.Add(string.Format("{0}-{1}", current.Key, current.Value));
            }
            return string.Join(",", list.ToArray());
        }

        private Dictionary<int, int> SplitStateString(string stateString)
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            string[] strArray = stateString.Split(new char[] { ',' });
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { '-' });
                if (strArray2.Length == 2)
                {
                    int key = int.Parse(strArray2[0]);
                    if (!dictionary.ContainsKey(key))
                    {
                        dictionary.Add(key, int.Parse(strArray2[1]));
                    }
                }
            }
            return dictionary;
        }

        public string ComposeStateString
        {
            get
            {
                m_composeStateString = JointStateString(m_composeState);
                if (m_composeStateString.Length > 200)
                {
                    ClearExpireComposeState();
                    m_composeStateString = JointStateString(m_composeState);
                    if (m_composeStateString.Length > 200)
                    {
                        throw new ArgumentOutOfRangeException("the compose state string is too long, to fix the error you should clean the column Sys_Users_History.ComposeState in DB");
                    }
                }
                return m_composeStateString;
            }
            set
            {
                m_composeStateString = value;
                m_composeState = SplitStateString(m_composeStateString);
            }
        }

        public DateTime LastQuestsTime
        {
            get
            {
                return _lastQuestsTime;
            }
            set
            {
                _lastQuestsTime = value;
            }
        }

        public DateTime LastTreasureTime
        {
            get
            {
                return _lastTreasureTime;
            }
            set
            {
                _lastTreasureTime = value;
            }
        }

        public int UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }
    }
}

