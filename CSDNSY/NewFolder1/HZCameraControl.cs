using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static IDM_NETSDK;

namespace CSDNSY
{
    public class HZCameraControl
    {
        int mUserID = -1;
        int mRealPlayHandle = -1;
        int mlPort = -1;
        int mChannel = -1;
        byte[] szDeviceID;

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
        public bool Login(string _ip, int _port, string _userName, string _password)
        {
            IDM_NETSDK.IDM_DEV_USER_LOGIN_INFO_S user_info = new IDM_NETSDK.IDM_DEV_USER_LOGIN_INFO_S
            {
                lLoginMode = 0,
                usPort = (ushort)_port,
                aucRes = new byte[64]
            };
            //IP
            byte[] ip = Encoding.Default.GetBytes(_ip);
            user_info.szTargetIP = new byte[43];
            user_info.szDeviceIP = new byte[IDM_NETSDK.IDM_DEVICE_IP_MAX_LEN];
            ip.CopyTo(user_info.szTargetIP, 0);
            ip.CopyTo(user_info.szDeviceIP, 0);

            //用户名
            byte[] username = Encoding.Default.GetBytes(_userName);
            user_info.szUsername = new byte[IDM_NETSDK.IDM_USERNAME_MAX_LEN];
            username.CopyTo(user_info.szUsername, 0);
            //密码
            byte[] password = Encoding.Default.GetBytes(_password);
            user_info.szPassword = new byte[IDM_NETSDK.IDM_PASSWORD_MAX_LEN];
            password.CopyTo(user_info.szPassword, 0);
            //登录
            IDM_NETSDK.IDM_DEV_DEVICE_INFO_S device_info = new IDM_NETSDK.IDM_DEV_DEVICE_INFO_S();
            IsLogin = IDM_NETSDK.IDM_DEV_Login(user_info, ref device_info, ref mUserID) == IDM_NETSDK.IDM_SUCCESS;
            if (IsLogin)
            {
                mChannel = device_info.ulChannel;
                szDeviceID = device_info.szDeviceID;
               
            }
            return IsLogin;
        }

        /// <summary>
        /// 开启实时流
        /// </summary>
        /// <returns></returns>
        public bool OpenPlay()
        {
            IntPtr pUserData = IntPtr.Zero;
            IDM_NETSDK.IDM_DEV_PREVIEW_INFO_S private_info = new IDM_NETSDK.IDM_DEV_PREVIEW_INFO_S()
            {
                ulChannel = mChannel,
                ulLinkMode = 0,
                ulStreamTimeout = 50,
                ulStreamType = 0,
                ucStreamMode = 1,
                aucRes = new byte[254]
            };
            //mRealPlayCallBack = OnRealPlayCallBack;
            //IsPlaying = IDM_NETSDK.IDM_DEV_RealPlay(mUserID, private_info, mRealPlayCallBack, pUserData, ref mRealPlayHandle) == IDM_NETSDK.IDM_SUCCESS;

            mRealPlayESCallBack = OnRealPlayCallBackES;
            IsPlaying = IDM_NETSDK.IDM_DEV_RealPlayES(mUserID, private_info, mRealPlayESCallBack, pUserData, ref mRealPlayHandle) == IDM_NETSDK.IDM_SUCCESS;
            return IsPlaying;
        }

        /// <summary>
        /// 解码回调
        /// </summary>
        /// <param name="lRealPlayHandle"></param>
        /// <param name="infos"></param>
        /// <param name="pUserData"></param>
        void OnRealPlayCallBackES(int lRealPlayHandle, IDM_NETSDK.IDM_DEV_PACKET_INFO_S infos, IntPtr pUserData)
        {
            try
            {
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
                                HZ_PLAY.PLAY_InputData(mlPort, infos.pucBuffer, (int)infos.ulBufferSize);
                        }
                        break;
                }
                //if(mlPort!=-1)
                HZ_PLAY.PLAY_InputData(mlPort, infos.pucBuffer, (int)infos.ulBufferSize);
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
        /// <summary>
        /// 是否修改成功
        /// </summary>
        bool IsSetCofig = false;
        /// <summary>
        /// 修改配置
        /// </summary>
        /// <returns></returns>
        public bool SetConfig(string _ip,int _port,string _userName,string _password)
        {
           

            GetConfig();
           return IsSetCofig;
        }
        public IntPtr video_info;
        /// <summary>
        /// 获取视频参数
        /// </summary>
        /// <returns></returns>
        public bool GetConfig()
        {
            video_info = IntPtr.Zero; 
            IsSetCofig= IDM_NETSDK.IDM_DEV_GetConfig(mUserID, 0x00000402, (uint)mChannel, ref video_info, (uint)2000) == IDM_NETSDK.IDM_SUCCESS;
           
            return IsSetCofig;
        }
    }

}
