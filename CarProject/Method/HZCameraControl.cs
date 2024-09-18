using Messages.custom_msgs;
using OpenCvSharp;
using Sunny.UI.Win32;
using System;
using System.Text;
using static IDM_NETSDK;

namespace CarProject.Method
{
    public class HZCameraControl
    {
        long mUserID = 0;
       long mRealPlayHandle = 0;
        int mlPort = -1;
        uint mChannel ;

        bool IsInit = false;
        bool IsLogin = false;
        bool IsPlaying = false;
        public bool isUpdate = true;
        //IDM_NETSDK.IDM_DEV_RealPlay_Callback_PF mRealPlayCallBack;
        IDM_NETSDK.IDM_DEV_RealPlayES_Callback_PF mRealPlayESCallBack;
        public HZ_PLAY.fDecCBFun mDecCBFunc;
        //public HZ_PLAY.fDisplayCBFun mDisplayCBFunc;//fCBDecode

        public HZCameraControl() { }

        public bool Init()
        {
            IsInit = IDM_NETSDK.IDM_DEV_Init() == IDM_NETSDK.IDM_SUCCESS;
            return IsInit;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="_ip">监控IP</param>
        /// <param name="_port">端口</param>
        /// <param name="_userName">用户名</param>
        /// <param name="_password">密码</param>
        /// <returns></returns>
        public bool Login(string _ip, int _port, string _userName, string _password,ref long code)
        {
            IDM_NETSDK.IDM_DEV_USER_LOGIN_INFO_S user_info = new IDM_NETSDK.IDM_DEV_USER_LOGIN_INFO_S
            {
                lLoginMode = 0,
                usPort = (ushort)_port,
                aucRes = new byte[64]
            };
            //IP
          user_info.szDeviceIP = _ip;


            user_info.szUsername = _userName;
            //密码 
            user_info.szPassword = _password;
            //登录
            IDM_NETSDK.IDM_DEV_DEVICE_INFO_S device_info = new IDM_NETSDK.IDM_DEV_DEVICE_INFO_S();
             //  IsLogin = IDM_NETSDK.IDM_DEV_Login(user_info, ref device_info, ref mUserID) == IDM_NETSDK.IDM_SUCCESS;
             code = IDM_NETSDK.IDM_DEV_Login(user_info, ref device_info, ref mUserID);
            if(code == IDM_NETSDK.IDM_SUCCESS)IsLogin = true;
            if (IsLogin)
            {
                mChannel =  device_info.ulChannel;
            }
            return IsLogin;
        }
       
        /// <summary>
        /// 开启实时流
        /// </summary>
        /// <returns></returns>
        public bool OpenPlay(ref long code)
        {
            IDM_DEV_PREVIEW_INFO_S stPreviewInfo = new IDM_DEV_PREVIEW_INFO_S();
            stPreviewInfo.ulChannel =mChannel;   //通道号
            stPreviewInfo.ulStreamType = 0; // 码流类型：0-主码流 1-子码流 2-三码流          
            stPreviewInfo.ulLinkMode = 0; // 连接方式：0-TCP 1-UDP 2-多播，暂时只支持TCP
            stPreviewInfo.ucStreamMode = 0; //  流模式, 0:音频复合流, 1:纯视频流 2:纯音频流
            IDM_DEV_RealPlayES_Callback_PF pfRealPlayCallback = new IDM_DEV_RealPlayES_Callback_PF(OnRealPlayCallBackES);
            mRealPlayESCallBack = OnRealPlayCallBackES;
            long magicNumber = 989796554321;
           // IntPtr pUserData = new IntPtr(0x000000e67478d651);
           IntPtr pUserData =new IntPtr(magicNumber);
           
            code = IDM_NETSDK.IDM_DEV_RealPlayES(mUserID, stPreviewInfo, mRealPlayESCallBack, pUserData, ref mRealPlayHandle);
            if (code == 0) IsPlaying = true;



            //IntPtr lPlayID = IntPtr.Zero; //CK.Handle ; // 初始化句柄
            //mRealPlayESCallBack = OnRealPlayCallBackES;

            //long ret = IDM_NETSDK.IDM_DEV_RealPlayES(mUserID, stPreviewInfo, pfRealPlayCallback, new IntPtr(magicNumber), ref mRealPlayHandle);
            //if (ret == 0)
            //{
               
            //     IsPlaying=true;
            //}
         
            return IsPlaying;
        }

        /// <summary>
        /// 解码回调
        /// </summary>
        /// <param name="lRealPlayHandle"></param>
        /// <param name="infos"></param>
        /// <param name="pUserData"></param>
        void OnRealPlayCallBackES(long lRealPlayHandle, IDM_NETSDK.IDM_DEV_PACKET_INFO_S infos, IntPtr pUserData)
        {
            bool B = false;
            try
            {
                Console.WriteLine("11" + infos.ulPacketType);
                switch (infos.ulPacketType)
                {
                    case 0xF1:
                        if (mlPort == -1)
                        {
                            if (!HZ_PLAY.PLAY_GetFreePort(ref mlPort))
                            {
                                throw new Exception("get free port failed");
                            }
                            if (!HZ_PLAY.PLAY_SetStreamOpenMode(mlPort, 0))
                            {
                                throw new Exception("set StreamOpenMode failed");
                            }
                            if (!HZ_PLAY.PLAY_OpenStream(mlPort, IntPtr.Zero, 0, 1024 * 1024))
                            {
                                throw new Exception("open Stream failed");
                            }
                            if (!HZ_PLAY.PLAY_SetDecCallBack(mlPort, mDecCBFunc))
                            {
                                throw new Exception("set DecCallBack failed");
                            }
                            if (!HZ_PLAY.PLAY_SetDecCBStream(mlPort, 1))
                            {
                                throw new Exception("set DecCBStream failed");
                            }
                            if (!HZ_PLAY.PLAY_Play(mlPort, IntPtr.Zero))
                            {
                                throw new Exception("play failed");
                            }
                        }
                        break;
                    case 0xF2: break;
                    case 0xF3: break;
                    case 0xF4: break;
                    case 0xF5: break;
                    default:
                        if (isUpdate)
                        {
                            if (mlPort != -1)
                                B= HZ_PLAY.PLAY_InputData(mlPort, infos.pucBuffer, (int)infos.ulBufferSize);
                        }
                        break;
                }
                //if(mlPort!=-1)
                B= HZ_PLAY.PLAY_InputData(mlPort, infos.pucBuffer, (int)infos.ulBufferSize);
            }
            catch (Exception e)
            {
                Console.WriteLine("RealPlay:" + e.ToString());
            }
        }
        /// <summary>
        /// 停止检测，并关闭实时流
        /// </summary>
        public void Stop()
        {
            mRealPlayESCallBack = null;
            mDecCBFunc = null;
            IDM_NETSDK.IDM_DEV_SetRealPlayESCallback(mRealPlayHandle, null, IntPtr.Zero);
            IDM_NETSDK.IDM_DEV_StopRealPlay(mRealPlayHandle);
            IDM_NETSDK.IDM_DEV_Logout(mUserID);
            IDM_NETSDK.IDM_DEV_Cleanup();
            int port = mlPort;
            mlPort = -1;
            HZ_PLAY.PLAY_Stop(port);
            HZ_PLAY.PLAY_ReleasePort(port);
        }
    }
} 