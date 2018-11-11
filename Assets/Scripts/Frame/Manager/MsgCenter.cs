﻿using System.Collections.Generic;
using UnityEngine;

namespace U3DEventFrame
{
    public enum ManagerID
    {
        LuaManager          = 0,
        
        LNetManager         = FrameTools.MsgSpan * 1,
        LUIManager          = FrameTools.MsgSpan * 2,
        LNPCManager         = FrameTools.MsgSpan * 3,
        LCharatorManager    = FrameTools.MsgSpan * 4,
        LAssetManager       = FrameTools.MsgSpan * 5,
        LGameManager        = FrameTools.MsgSpan * 6,
        LDataManager        = FrameTools.MsgSpan * 7,
        LAudioManager       = FrameTools.MsgSpan * 8,
        LDBManager          = FrameTools.MsgSpan * 9,
        
        NetManager          = FrameTools.MsgSpan * 12,
        UIManager           = FrameTools.MsgSpan * 13,
        NPCManager          = FrameTools.MsgSpan * 14,
        CharatorManager     = FrameTools.MsgSpan * 15,
        AssetManager        = FrameTools.MsgSpan * 16,
        GameManager         = FrameTools.MsgSpan * 17,
        DataManager         = FrameTools.MsgSpan * 18,
        AudioManager        = FrameTools.MsgSpan * 19,
        DBManager           = FrameTools.MsgSpan * 20
    }

    public class MsgCenter : MonoBehaviour
    {
        public static MsgCenter Instance;

        private void Awake()
        {
            Instance = this;
            
#if USE_MutiMSGQueue
            _msgQueue = new Queue<MsgBase>();
#endif

            gameObject.AddComponent<AssetManager>();
        }

        private void Update()
        {
#if USE_MutiMSGQueue
            while (_msgQueue.Count > 0)
            {
                MsgBase tmpBody = _msgQueue.Dequeue();
                AnasysisMsg(tmpBody);
            }
#endif
        }

        public void SendMsg(MsgBase tmpBody)
        {
#if USE_MutiMSGQueue
            if (tmpBody != null)
            {
                lock (this)
                {
                    _msgQueue.Enqueue(tmpBody);
                }
            }
#endif
            
            AnasysisMsg(tmpBody);
        }

        private void AnasysisMsg(MsgBase tmpBody)
        {
            if (tmpBody == null)
                return;

            int tmpid = tmpBody.GetManager();

            if (tmpid < (int)ManagerID.NetManager)
            {
                // lua
            }
            else
            {
                switch (tmpid)
                {
                    case (ushort)ManagerID.NetManager:
                        break;
                    case (ushort)ManagerID.UIManager:
                        break;
                    case (ushort)ManagerID.NPCManager:
                        break;
                    case (ushort)ManagerID.CharatorManager:
                        break;
                    case (ushort)ManagerID.AssetManager:
                        AssetManager.Instance.ProcessEvent(tmpBody);
                        break;
                    case (ushort)ManagerID.GameManager:
                        break;
                    case (ushort)ManagerID.DataManager:
                        break;
                    case (ushort)ManagerID.AudioManager:
                        break;
                    case (ushort)ManagerID.DBManager:
                        break;
                    default:
                        break;
                }
            }
        }

#if USE_MutiMSGQueue
        private Queue<MsgBase> _msgQueue;
#endif
    }
}
