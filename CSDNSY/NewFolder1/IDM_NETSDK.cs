using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


    public class IDM_NETSDK
    {

        #region 基础常量
        public const int IDM_SUCCESS = 0;
        public const int IDM_ERROR_DEFINED = 1012120000;
        public const int IDM_ERROR_UNINITIALIZED = (IDM_ERROR_DEFINED + 1);         /* SDK未初始化 */
        public const int IDM_ERROR_PASSWORD = (IDM_ERROR_DEFINED + 2);              /* 用户名密码错误 */
        public const int IDM_ERROR_INVALID_HANDLE = (IDM_ERROR_DEFINED + 3);        /* 无效的句柄 */
        public const int IDM_ERROR_CHANNEL_NUMBER = (IDM_ERROR_DEFINED + 4);        /* 通道号错误 */
        public const int IDM_ERROR_MAX_NUM = (IDM_ERROR_DEFINED + 5);               /* 超过最大数量 */
        public const int IDM_ERROR_CONNECT_FAILED = (IDM_ERROR_DEFINED + 6);        /* 连接设备失败 */
        public const int IDM_ERROR_SEND_FAILED = (IDM_ERROR_DEFINED + 7);           /* 发送失败 */
        public const int IDM_ERROR_RECEIVE_FAILED = (IDM_ERROR_DEFINED + 8);        /* 接收失败 */
        public const int IDM_ERROR_RECEIVE_TIMEOUT = (IDM_ERROR_DEFINED + 9);       /* 接收超时 */
        public const int IDM_ERROR_DATA = (IDM_ERROR_DEFINED + 10);                 /* 发送或者接收的数据错误 */
        public const int IDM_ERROR_PARAMETER = (IDM_ERROR_DEFINED + 11);            /* 参数错误 */
        public const int IDM_ERROR_OPERATE = (IDM_ERROR_DEFINED + 12);              /* 操作错误 */
        public const int IDM_ERROR_BUFFER_NOT_ENOUGH = (IDM_ERROR_DEFINED + 13);    /* 数据缓冲区不足 */
        public const int IDM_ERROR_CREATE_SOCKET = (IDM_ERROR_DEFINED + 14);        /* 创建SOCKET失败 */
        public const int IDM_ERROR_SET_SOCKET = (IDM_ERROR_DEFINED + 15);           /* 主动注册模式下 找不到设备ID */
        public const int IDM_ERROR_BIND_SOCKET = (IDM_ERROR_DEFINED + 16);          /* 绑定SOCKET失败 */
        public const int IDM_ERROR_LISTEN_SOCKET = (IDM_ERROR_DEFINED + 17);        /* 监听SOCKET失败 */
        public const int IDM_ERROR_CREATE_FILE = (IDM_ERROR_DEFINED + 18);          /* 创建文件失败 */
        public const int IDM_ERROR_OPEN_FILE = (IDM_ERROR_DEFINED + 19);            /* 打开文件失败 */
        public const int IDM_ERROR_WRITE_FILE = (IDM_ERROR_DEFINED + 20);           /* 写文件失败 */
        public const int IDM_ERROR_LOAD_LIVE = (IDM_ERROR_DEFINED + 21);            /* 预览组件加载失败 */
        public const int IDM_ERROR_LOAD_VOD = (IDM_ERROR_DEFINED + 22);             /* 回放组件加载失败 */
        public const int IDM_ERROR_LOAD_ALARM = (IDM_ERROR_DEFINED + 23);           /* 报警组件加载失败 */
        public const int IDM_ERROR_LOAD_VOICETALK = (IDM_ERROR_DEFINED + 24);       /* 语音对讲组件加载失败 */
        public const int IDM_ERROR_NOT_SUPPORT = (IDM_ERROR_DEFINED + 25);          /* 设备不支持 */
        public const int IDM_ERROR_ALLOC_FAILED = (IDM_ERROR_DEFINED + 26);         /* 分配内存失败 */
        public const int IDM_ERROR_NULLPTR = (IDM_ERROR_DEFINED + 27);              /* 空指针 */
        public const int IDM_ERROR_LOAD_PDSP = (IDM_ERROR_DEFINED + 28);            /* 设备搜索组件加载失败 */
        public const int IDM_ERROR_KEEPALIVE_TIMEOUT = (IDM_ERROR_DEFINED + 29);    /* 接收心跳超时 */
        public const int IDM_ERROR_INVALID_SOCKET = (IDM_ERROR_DEFINED + 30);       /* 网络套接字失效 */
        public const int IDM_ERROR_NO_NETCARD = (IDM_ERROR_DEFINED + 31);           /* 网卡列表加载失败 */
        public const int IDM_ERROR_SN_EXIST = (IDM_ERROR_DEFINED + 32);             /* SN已存在 */
        public const int IDM_ERROR_TASK_RUNNING = (IDM_ERROR_DEFINED + 33);         /* 异步任务运行中 */

        public const int IDM_IPV4_ADDRESS_LEN = 16;
        public const int IDM_IPV6_ADDRESS_LEN = 64;
        public const int IDM_DOMAIN_NAME_LEN = 64;
        public const int IDM_SERIAL_NUMBER_LEN = 64;
        public const int IDM_MAC_ADDRESS_LEN = 64;
        public const int IDM_DEVICE_ID_LEN = 64;
        public const int IDM_DEVICE_NAME_LEN = 64;
        public const int IDM_DEVICE_MODEL_LEN = 64;
        public const int IDM_DEVICE_IP_MAX_LEN = 16;
        public const int IDM_USERNAME_MAX_LEN = 64;
        public const int IDM_PASSWORD_MAX_LEN = 64;
        public const int IDM_TIMESTR_MAX_LEN = 64;
        public const int IDM_RTSPURL_MAX_LEN = 256;

        public const int IDM_UPNP_PORT_NUM = 32;
        public const int IDM_ALARM_OUT_MAX_NUM = 16;
        public const int IDM_CHANNEL_MAX_NUM = 256;
        public const int IDM_HARDDISK_MAX_NUM = 64;

        /*下列异常事件类型是IDM_DEV_Exception_Callback_PF回调函数中的ulType*/
        public const int EXCEPTION_KEEPALIVE = 0;  /*设备连接异常(设置了断线重连,将会恢复连接)*/
        public const int EXCEPTION_REALPLAY = 1;
        public const int EXCEPTION_PLAYBACK = 2;
        public const int EXCEPTION_ALARM = 3;    /*告警连接异常(设置了断线重连,将会恢复告警)*/
        public const int EXCEPTION_VOICE = 4;
        public const int EXCEPTION_RECONNECT = 5;   /*设备重连成功*/
        public const int EXCEPTION_CHN_OFFLINE = 6;
        public const int EXCEPTION_RECONNECT_FAILED = 7;   /*设备重连失败(设置了断线重连, 恢复连接时设备报错)*/
        public const int EXCEPTION_ALARM_RECONNECT = 8;   /*告警恢复成功*/
        public const int EXCEPTION_LIVE_CREATE_FILE_FAILED = 9;   /*实时码流本地录制，创建失败(lHandle是预览句柄(plRealPlayHandle))*/
        public const int EXCEPTION_LIVE_WRITE_FILE_FAILED = 10;  /*实时码流本地录制，写文件失败(lHandle是预览句柄(plRealPlayHandle))*/
        public const int EXCEPTION_VOD_CREATE_FILE_FAILED = 11;   /*回放码流本地录制，创建失败(Handle是回放句柄(lPlayBackHandle))*/
        public const int EXCEPTION_VOD_WRITE_FILE_FAILED = 12; /*回放码流本地录制，写文件失败(Handle是回放句柄(lPlayBackHandle))*/

        public const int STREAM_VIDEO = 0;
        public const int STREAM_AUDIO = 1;
        public const int STREAM_PRIVATE = 2;

        //录像回放控制参数=
        public const int PLAYBACK_START = 0;
        public const int PLAYBACK_PAUSE = 1;      /*录像暂停*/
        public const int PLAYBACK_RESUME = 2;      /*录像恢复*/
        public const int PLAYBACK_SPEED = 3;      /*设置倍速*/
        public const int PLAYBACK_FRAME = 4;      /*单帧播放*/
        public const int PLAYBACK_FORWARD = 5;
        public const int PLAYBACK_BACKWARD = 6;
        public const int PLAYBACK_SET_SEEK = 7;
        public const int PLAYBACK_GET_SEEK = 8;
        public const int PLAYBACK_SET_TIME = 9;
        public const int PLAYBACK_GET_TIME = 10;
        public const int PLAYBACK_AUDIO_ON = 11;
        public const int PLAYBACK_AUDIO_OFF = 12;
        public const int PLAYBACK_SET_VOLUME = 13;

        //云台控制命令宏 IDM_DEV_PTZControl 方法的luCommond参数
        public const int PTZ_LIGHT = 1;
        public const int PTZ_WIPER = 2;
        public const int PTZ_FAN = 3;
        public const int PTZ_HEATER = 4;
        public const int PTZ_INFRARED = 5;
        public const int PTZ_FOCUS_NEAR = 11;
        public const int PTZ_FOCUS_FAR = 12;
        public const int PTZ_ZOOM_IN = 13;
        public const int PTZ_ZOOM_OUT = 14;
        public const int PTZ_IRIS_OPEN = 15;
        public const int PTZ_IRIS_CLOSE = 16;
        public const int PTZ_UP = 21;
        public const int PTZ_DOWN = 22;
        public const int PTZ_LEFT = 23;
        public const int PTZ_RIGHT = 24;
        public const int PTZ_LEFT_UP = 25;
        public const int PTZ_RIGHT_UP = 26;
        public const int PTZ_LEFT_DOWN = 27;
        public const int PTZ_RIGHT_DOWN = 28;
        public const int PTZ_AUTO_SCAN = 29;
        public const int PTZ_AUTO_FOCUS = 30;
        public const int PTZ_RESET_LENS = 31;
        public const int PTZ_3D_ZOOM = 32;//3D定位 IDM_DEV_PTZ_3D_ZOOM_INFO_S
        public const int PTZ_UP_ZOOM_IN = 33;
        public const int PTZ_UP_ZOOM_OUT = 34;
        public const int PTZ_DOWN_ZOOM_IN = 35;
        public const int PTZ_DOWN_ZOOM_OUT = 36;
        public const int PTZ_LEFT_ZOOM_IN = 37;
        public const int PTZ_LEFT_ZOOM_OUT = 38;
        public const int PTZ_RIGHT_ZOOM_IN = 39;
        public const int PTZ_RIGHT_ZOOM_OUT = 40;
        public const int PTZ_LEFT_UP_ZOOM_IN = 41;
        public const int PTZ_LEFT_UP_ZOOM_OUT = 42;
        public const int PTZ_RIGHT_UP_ZOOM_IN = 43;
        public const int PTZ_RIGHT_UP_ZOOM_OUT = 44;
        public const int PTZ_LEFT_DOWN_ZOOM_IN = 45;
        public const int PTZ_LEFT_DOWN_ZOOM_OUT = 46;
        public const int PTZ_RIGHT_DOWN_ZOOM_IN = 47;
        public const int PTZ_RIGHT_DOWN_ZOOM_OUT = 48;
        public const int PTZ_SETUP_PRESET = 51;
        public const int PTZ_CLEAR_PRESET = 52;
        public const int PTZ_GOTO_PRESET = 53;
        public const int PTZ_SETUP_ZERO = 54;
        public const int PTZ_CLEAR_ZERO = 55;
        public const int PTZ_GOTO_ZERO = 56;
        public const int PTZ_START_RECORD_TRAIL = 57;
        public const int PTZ_STOP_RECORD_TRAIL = 58;
        public const int PTZ_START_RUN_TRAIL = 59;
        public const int PTZ_STOP_RUN_TRAIL = 60;
        public const int PTZ_CLEAR_TRAIL = 61;
        public const int PTZ_CLEAR_ALL_TRAIL = 62;
        public const int PTZ_START_RUN_CRUISE = 63;
        public const int PTZ_STOP_RUN_CRUISE = 64;
        public const int PTZ_CLEAR_CRUISE = 65;
        public const int PTZ_LOCK = 67;
        public const int PTZ_UNLOCK = 68;
        public const int PTZ_IRCUT = 69;
        public const int PTZ_ONE_TOUCH_PARK = 70; //设置并开启一键守望				
        public const int PTZ_ONE_TOUCH_CRUISE = 71; //调用一键巡航				
        public const int PTZ_SET_INIT_POS = 72; //设置枪球联动球机云台初始位置					
        public const int PTZ_MANUAL_TRACK = 73; //手动跟踪（枪球联动） IDM_DEV_PTZ_3D_ZOOM_INFO_S
        public const int PTZ_ASSIST_FOCUS = 74; //辅助聚焦             IDM_DEV_ASSIST_FOCUS_INFO_S

        public const int SYSTEM_RESTORE = 0;
        public const int SYSTEM_REBOOT = 1;

        public const int ONE_TOUCH_PARK = 70;
        public const int ONE_TOUCH_CRUISE = 71;
        public const int SET_PTZ_INIT_POS = 72;
        public const int MANUAL_TRACK = 73;

        public const int CONFIG_VIDEO_CFG = 0x00000402;
        #endregion

        #region 委托回调

        /* 异步登录回调函数 */

        public delegate void IDM_DEV_Login_Callback_PF(
            int lUserID,
            int lResult,
            IDM_DEV_DEVICE_INFO_S pstDeviceInfo,
            IntPtr pUserData
        );

        public delegate void IDM_DEV_RealPlay_Callback_PF(
        int lRealPlayHandle,
        uint ulDataType, //0视频帧,1音频帧,2其它数据
        IntPtr pucBuffer,//私有封装格式数据
        int ulBufferSize,
        IntPtr pUserData
        );

        /* 实时预览回调函数 */
        public delegate void IDM_DEV_RealPlayES_Callback_PF(
            int lRealPlayHandle,
            IDM_DEV_PACKET_INFO_S pstPacketInfo,
            IntPtr pUserData
            );

        #endregion


        #region 结构体

        [StructLayout(LayoutKind.Sequential)]
        public struct IDM_DEV_VIDEO_ENCODE_PARAM_S
        {
            public byte ucStreamType;
            public byte ucVideoType;
            public byte ucEncodeType;
            public byte ucEncodeLevel;
            public byte ucSmartEncode;
            public byte ucQuality;
            public byte ucBitrateType;
            public byte ucSmoothing;
            public ushort usIFrameInterval;
            public ushort usResolution;
            public ushort usFrameRate;
            public ushort usBitrate;
            public ushort usImageWidth;
            public ushort usImageHeight;
            public byte ucThirdStreamEnable;
            public byte ucBncOutputEnable;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] aucRes;
        }


        /* 设备参数信息 */
        [StructLayout(LayoutKind.Sequential)]
        public struct IDM_DEV_DEVICE_INFO_S
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = IDM_DEVICE_ID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szDeviceID;               /* 设备ID */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = IDM_DEVICE_NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szDeviceName;             /* 设备名称 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = IDM_SERIAL_NUMBER_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szSerialNum;              /* 设备序列号 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = IDM_MAC_ADDRESS_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szMacAddress;             /* 设备Mac地址 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = IDM_DEVICE_IP_MAX_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szIP;                     /* 设备IP地址 */
            public int ulChannel;                              /* 通道号 */
            public int usPort;                                 /* 端口号 */
            public byte ucRemainLoginTimes;                     /* 剩余可登录次数：用户名密码错误时有效 */
            public byte ucPasswordLevel;                        /* 密码安全等级：0-无效 1-默认密码 2-弱密码 3-中密码 4-强密码 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = IDM_DEVICE_IP_MAX_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szLocalIP;       /* 本地IP地址 */
            public int ulRemainLockTime;                       /* 剩余锁定时间：单位：秒，用户锁定时有效 */

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] szAuthType;                          /* 认证类型 当前固定为摘要认证 取Digest*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] szRealm;                             /* 领域参数 根据设备唯一序号生成 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public byte[] szNonce;                             /* 现时参数 根据事件生成 有生命周期 */

            public uint ulUserLoginID;                          /* 用户登录ID */
            public uint ulLinkSessionID;                        /* 登录主链路Session ID*/
            public uint ulKeepaliveIntervel;                    /* 单次心跳时间 秒*/
            public uint ulKeepaliveFailedTimes;                 /* 连续心跳失败次数 达到次数认为离线需要重新登录 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] szUserAuth;                       /* 用户权限 多元素英文逗号组合而成 每个元素最大8字节 最多128个元素*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] szPlayBackAuthChannels;                 /* 用户回放通道级权限 0-1组成 0无权限 1有权限*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] szPreviewAuthChannels;                  /* 用户实况通道级权限 0-1组成 0无权限 1有权限*/
            public int ucUserLockStatus;                       /* 用户锁定状态 失败时有效 0-未锁定 1-锁定*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3000, ArraySubType = UnmanagedType.I1)]
            public byte[] aucRes;
        }

        /* 设备重连 */
        [StructLayout(LayoutKind.Sequential)]
        public struct IDM_DEV_RECONNECT_INFO_S
        {
            public uint uiInterval;  // 重连时间间隔，单位 : 毫秒(最小值3000)
            public int ucEnable;   // 是否重连，0 - 不重连，1 - 重连，默认值为0

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = IDM_DEVICE_IP_MAX_LEN)]
            public byte[] ucRes;

        }



        /* 登录结构体 */
        [StructLayout(LayoutKind.Sequential)]
        public struct IDM_DEV_USER_LOGIN_INFO_S
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = IDM_DEVICE_IP_MAX_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szDeviceIP;           /* 设备IP地址 */
            public ushort usPort;                  /* 设备端口号 */
            public byte ucRes1;                  /* 内部使用  请置0*/
            public byte ucRes2;                 // 内部使用，请置0  
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = IDM_USERNAME_MAX_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szUsername;           /* 登录用户名 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = IDM_PASSWORD_MAX_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szPassword;           /* 登录密码 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = IDM_DEVICE_ID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szDeviceID;           /* 设备ID，主动注册模式下使用ID进行登录*/
            public long lLoginMode;             /* 登录模式, 0-SDK私有协议登录 ,1-主动注册 */
            public IDM_DEV_Login_Callback_PF pfLoginCallBack;      /* 异步登录回调函数，暂不启用 */
            public IntPtr pUserData;            /* 用户数据 */
            public byte ucCertLoginMode;        /* 证书登录模式 0为普通登录 1为证书登录 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 43, ArraySubType = UnmanagedType.I1)]
            public byte[] szTargetIP;           /* 目标IP(兼容IPv4,IPv6),优先使用szDeviceIP字段*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] aucRes;

        }


        /* 预览参数结构体 */
        [StructLayout(LayoutKind.Sequential)]
        public struct IDM_DEV_PREVIEW_INFO_S
        {
            public long ulChannel;                              /* 通道号 */
            public long ulStreamType;                           /* 码流类型：0-主码流 1-子码流 2-三码流 */
            public long ulLinkMode;                             /* 连接方式：0-TCP 1-UDP 2-多播 3-RTP/RTSP，暂时只支持TCP */
            public byte ulStreamTimeout;                      /*收流超时时间(秒)[5-120] 不在范围内,默认30秒*/
            public byte ucStreamMode;                         /* 流模式, 0:音频复合流, 1:纯视频流 2:纯音频流*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 254, ArraySubType = UnmanagedType.I1)]
            public byte[] aucRes;                               //254
        }

        /* 时间参数 CONFIG_SYSTEM_TIME */
        [StructLayout(LayoutKind.Sequential)]
        public struct IDM_DEV_TIME_PARAM_S
        {
            public int usYear;                                 /* 年 */
            public int usMonth;                                /* 月 */
            public int usDay;                                  /* 日 */
            public int usHour;                                 /* 时 */
            public int usMinute;                               /* 分 */
            public int usSecond;                               /* 秒 */
        }


        /* 手动抓拍参数 */
        [StructLayout(LayoutKind.Sequential)]
        public struct IDM_DEV_MANUALSNAP_S
        {
            public uint ulChanID;
            public uint ulStreamType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] aucRes;
        }


        /* 手动抓拍结果 */
        [StructLayout(LayoutKind.Sequential)]
        public struct IDM_DEV_MANUALSNAP_RESULT_S
        {
            public IntPtr pBuffer;                          /* 数据缓冲区指针 */
            public uint ulBufferSize;                        /* 数据缓冲区长度 */
            public uint ulPictureSize;                       /* 图片大小 */
            public IDM_DEV_TIME_PARAM_S stTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] aucRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IDM_DEV_PACKET_INFO_S
        {
            public uint ulFrameNum;                             /* 帧序号 */
            public uint ulPacketType;                           /* 数据类型: 帧类型，0xF1-视频I帧，0xF2-视频P帧，0xF3-MJPEG图片帧，0xF4-辅助帧，0xF5-音频帧，0xF6-视频B帧 */
            public uint ulEncodeType;                           /* 编码类型: 视频帧时: (只在I帧时才有编码格式) 1: MJPEG  2: H.264  3: H.265  4:  MPEG4		音频帧时: 1: ADPCM 2:G.722 3:G.711U 4:G.711A 5:G.726 6:AAC 7:MP2L2 8:PCM 9:G.722.1*/
            public uint ulPacketMode;                           /* 打包方式  0: 默认  */
            public uint ulTimeStamp;                            /* 时间戳低位  时间戳的低位4个字节  */
            public uint ulTimeStampHight;                       /* 时间戳高位  时间戳的高位4个字节  可以定义 8 字节的 时间戳 取  高位左移32位和低位相加*/
            public uint ulFrameRate;                            /* 帧率 */
            public ushort usWidth;                                /* 宽度 */
            public ushort usHeight;                               /* 高度 */
            public uint ulBufferSize;                           /* 数据大小 */
            public IntPtr pucBuffer;                            /* 数据缓冲指针 数据是H264/H265裸流帧数据 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] aucRes;//64
        }

        #endregion

        #region 函数导入声明
        /// <summary>
        /// 获取SDK版本
        /// </summary>
        /// <returns></returns>
        [DllImport(@"NetSDK/idm_netsdk.dll")]
        public static extern int IDM_DEV_GetSDKVersion();


        /// <summary>
        /// 初始化SDK
        /// </summary>
        /// <returns></returns>
        [DllImport(@"NetSDK/idm_netsdk.dll")]
        public static extern int IDM_DEV_Init();


        /// <summary>
        /// 登录设备
        /// </summary>
        /// <param name="stLoginInfo"></param>
        /// <param name="pstDeviceInfo"></param>
        /// <param name="plUserID"></param>
        /// <returns></returns>
        [DllImport(@"NetSDK/idm_netsdk.dll")]
        public static extern int IDM_DEV_Login(IDM_DEV_USER_LOGIN_INFO_S stLoginInfo, ref IDM_DEV_DEVICE_INFO_S pstDeviceInfo, ref int plUserID);


        /// <summary>
        /// 启动预览
        /// </summary>
        /// <param name="lUserID"></param>
        /// <param name="stPreviewInfo"></param>
        /// <param name="pfRealPlayCallBack"></param>
        /// <param name="pUserData"></param>
        /// <param name="plRealPlayHandle"></param>
        /// <returns></returns>
        [DllImport(@"NetSDK/idm_netsdk.dll")]
        public static extern int IDM_DEV_RealPlay(int lUserID, IDM_DEV_PREVIEW_INFO_S stPreviewInfo, IDM_DEV_RealPlay_Callback_PF pfRealPlayCallBack, IntPtr pUserData, ref int plRealPlayHandle);


        [DllImport(@"NetSDK/idm_netsdk.dll")]
        public static extern int IDM_DEV_RealPlayES(int lUserID, IDM_DEV_PREVIEW_INFO_S sPreviewInfo, IDM_DEV_RealPlayES_Callback_PF pfRealPlayCallBack, IntPtr pUserData, ref int plRealPlayHandle);

        /*
    *@brief: 保存实时流到文件
    *@param: IN lRealPlayHandle 预览句柄
    *@param: IN pcFileName 保存的文件路径
    *@return: 成功返回IDM_SUCCESS，失败返回错误码
    */
        [DllImport(@"NetSDK/idm_netsdk.dll")]
        public static extern int IDM_DEV_SaveRealPlayData(int lRealPlayHandle, string pcFileName);

        /*
        *@brief: 停止保存实时流到文件
        *@param: IN lRealPlayHandle 预览句柄
        *@return: 成功返回IDM_SUCCESS，失败返回错误码
        */
        [DllImport(@"NetSDK/idm_netsdk.dll")]
        public static extern int IDM_DEV_StopSaveRealPlayData(int lRealPlayHandle);

        /*
        *@brief: 手动抓图并保存在用户申请的内存中
        *@param: IN lUserID 设备句柄
        *@param: IN pInter 抓拍参数
        *@param: OUT pOuter 响应参数
        *@return: 成功返回IDM_SUCCESS，失败返回错误码
        */
        [DllImport(@"NetSDK/idm_netsdk.dll")]
        public static extern int IDM_DEV_ManualSnap(int lUserID, ref IDM_DEV_MANUALSNAP_S pInter, ref IDM_DEV_MANUALSNAP_RESULT_S pOuter);

        /// <summary>
        /// 停止预览
        /// </summary>
        /// <param name="lRealPlayHandle"></param>
        /// <returns></returns>
        [DllImport(@"NetSDK/idm_netsdk.dll")]
        public static extern int IDM_DEV_StopRealPlay(int lRealPlayHandle);
        /// <summary>
        /// 断线重连
        /// </summary>
        /// <param name="stReconnectInfo"></param>
        /// <returns></returns>
        [DllImport(@"NetSDK/idm_netsdk.dll")]
        public static extern int IDM_DEV_SetReconnect(IDM_DEV_RECONNECT_INFO_S stReconnectInfo);

        [DllImport(@"NetSDK/idm_netsdk.dll")]
        public static extern int IDM_DEV_SetConfig(int lUserID, uint ulCommand, uint ulChannel, IntPtr pBuffer, uint ulBufferSize);

        [DllImport(@"NetSDK/idm_netsdk.dll")]
        public static extern int IDM_DEV_SetRealPlayESCallback(int lRealPlayHandle, IDM_DEV_RealPlayES_Callback_PF pfRealPlayCallback, IntPtr pUserData);
        /// <summary>
        /// 设备登出
        /// </summary>
        /// <param name="lUserID">设备句柄</param>
        /// <returns></returns>
        [DllImport(@"NetSDK/idm_netsdk.dll")]
        public static extern int IDM_DEV_Logout(int lUserID);

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <returns></returns>
        [DllImport(@"NetSDK/idm_netsdk.dll")]
        public static extern int IDM_DEV_Cleanup();

        #endregion

    }


