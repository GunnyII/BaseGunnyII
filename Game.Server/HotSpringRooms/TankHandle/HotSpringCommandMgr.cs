using Game.Server;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Game.Server.HotSpringRooms.TankHandle
{
   public class HotSpringCommandMgr
    {
        private Dictionary<int, IHotSpringCommandHandler> handles = new Dictionary<int, IHotSpringCommandHandler>();

        public HotSpringCommandMgr()
        {
            handles.Clear();
            SearchCommandHandlers(Assembly.GetAssembly(typeof(GameServer)));
        }

        public IHotSpringCommandHandler LoadCommandHandler(int code)
        {
            return handles[code];
        }

        protected void RegisterCommandHandler(int code, IHotSpringCommandHandler handle)
        {
            handles.Add(code, handle);
        }

        protected int SearchCommandHandlers(Assembly assembly)
        {
            int num = 0;
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsClass != true)
                    continue;
                if (type.GetInterface("Game.Server.HotSpringRooms.TankHandle.IHotSpringCommandHandler") == null)
                    continue;

                HotSpringCommandAttribute[] Attr = (HotSpringCommandAttribute[])type.GetCustomAttributes(typeof(HotSpringCommandAttribute), true);
                if (Attr.Length > 0)
                {
                    try
                    {
                    num++;
                    RegisterCommandHandler(Attr[0].Code, Activator.CreateInstance(type) as IHotSpringCommandHandler);
                    }
                    catch
                    {
                      Console.WriteLine("Error :" + Attr[0].ToString());
                    }
                }

            }
            return num;
        }
    }
}

