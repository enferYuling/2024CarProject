using System;
using System.Runtime.InteropServices;

namespace CarProject.Method
{
    public class HZ_PLAY
    {
        #region 结构体
        /* 帧信息 */
        public struct FRAME_INFO
        {
            public int nWidth;              // 画面宽，单位像素。如果是音频数据则为0
            public int nHeight;             // 画面高，如果是音频数据则为0
            public int nStamp;              // 时标信息，单位毫秒
            public int nType;               // 视频帧类型，T_AUDIO16，T_RGB32，T_IYUV
            public int nFrameRate;          // 视频表示帧率，音频表示采样率
        }

        public struct FRAME_DECODE_INFO
        {
            public int nFrameType;
            public IntPtr pAudioData;
            public int nAudioDataLen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public IntPtr[] pVideoData;
            [MarshalAs(UnmanagedType.SysInt, SizeConst = 3)]
            public int[] nStride;//3
            [MarshalAs(UnmanagedType.SysInt, SizeConst = 3)]
            public int[] nWidth;//3
            [MarshalAs(UnmanagedType.SysInt, SizeConst = 3)]
            public int[] nHeight;//3
            [MarshalAs(UnmanagedType.SysInt, SizeConst = 64)]
            public int[] nReserved;//64
        }

        public struct FRAME_INFO_EX
        {

            public int nFrameType;
            public int nFrameSeq;
            public int nStamp;
            public int nWidth;
            public int nHeight;
            public int nFrameRate;
            public int nChannels;
            public int nBitPerSample;
            public int nSamplesPerSec;
            [MarshalAs(UnmanagedType.SysInt, SizeConst = 64)]
            public int[] nReserved;//64
        }



        #endregion

        #region 委托回调
        /**
		* 解码回调函数。
		*
		* @param[in] nPort 通道号
		* @param[in] pBuf 解码后的音视频数据
		* @param[in] nSize 解码后的音视频数据pBuf的长度
		* @param[in] pFrameInfo 图像和声音信息，请参见FRAME_INFO结构体
		* @param[in] pUserData 用户自定义参数
		* @param[in] nReserved2 保留参数
*/
        public delegate void fDecCBFun(int nPort, IntPtr pBuf, int nSize, FRAME_INFO pFrameInfo, IntPtr pUserData, int nReserved2);

        public delegate void fCBDecode(int nPort, FRAME_DECODE_INFO pFrameDecodeInfo, FRAME_INFO_EX pFrameInfo, IntPtr pUser);

        public delegate void fDishz_playplayCBFun(int nPort, IntPtr pBuf, int nSize, int nWidth, int nHeight, int nStamp, int nType, IntPtr nReserved);
        #endregion

        #region C++函数
        // @bref        获取版本号
        // $retvalue    返回值为版本号字符串，失败返回NULL
        [DllImport(@"hz_playplay.dll")]
        public static extern int PLAY_GetSdkVersion();

        [DllImport(@"hz_playplay.dll")]
        public static extern bool PLAY_GetFreePort(ref int plPort);

        /// <summary>
        /// 设置流模式
        /// </summary>
        /// <param name="nPort">播放端口</param>
        /// <param name="nMode">0 实时流，1文件流</param>
        /// <returns></returns>
        [DllImport(@"hz_playplay.dll")]
        public static extern bool PLAY_SetStreamOpenMode(int nPort, int nMode);

        [DllImport(@"hz_playplay.dll")]
        public static extern bool PLAY_OpenStream(int nPort, IntPtr pFileHeadBuf, int nSize, int nBufPoolSize);

        [DllImport(@"hz_playplay.dll")]
        public static extern bool PLAY_InputData(int nPort, IntPtr pBuf, int nSize);


        /**
	 * 设置解码回调流类型，在PLAY_Play之前调用。
	 *
	 * @param[in] nPort 通道号
	 * @param[in] nStream 1 视频流；2 音频流；3 复合流
	 * @return BOOL，成功返回TRUE，失败返回FALSE
	 * @note 如果返回失败，可以调用PLAY_GetLastErrorEx接口获取错误码。
	 */
        [DllImport(@"hz_playplay.dll")]
        public static extern bool PLAY_SetDecCBStream(int nPort, int nStream);

        /**
	 * 设置解码回调，替换播放器中的显示部分，由用户自己控制显示，该函数在
	 * PLAY_Play之前调用，在PLAY_Stop时自动失效，下次调用PLAY_Play之前
	 * 需要重新设置。解码部分不控制速度，只要用户从回调函数中返回，解码器
	 * 就会解码下一部分数据。适用于只解码不显示的情形。
	 *
	 * @param[in] nPort 通道号
	 * @param[out] DecCBFun 解码回调函数指针，不能为NULL
	 * @return BOOL，成功返回TRUE，失败返回FALSE
	 * @note 如果返回失败，可以调用PLAY_GetLastErrorEx接口获取错误码。
	 */
        [DllImport(@"hz_playplay.dll")]
        public static extern bool PLAY_SetDecCallBack(int nPort, fDecCBFun DecCBFun);

        [DllImport(@"hz_playplay.dll")]
        public static extern bool PLAY_SetVisibleDecodeCallBack(int nPort, fCBDecode cbDec, IntPtr pUser);

        [DllImport(@"hz_playplay.dll")]
        public static extern bool PLAY_SetDishz_playplayCallBack(int nPort, fDishz_playplayCBFun Dishz_playplayCBFun, int nUser);
        /**
	 * 开启播放。播放开始，播放视频画面大小将根据hWnd窗口调整，要全屏显示，只要把hWnd窗口放大到全屏。开始解码线程，若送入的显示窗   
	 * 口句柄为NULL，则不显示，但是不影响解码。
	*
	* @param[in] nPort 通道号
	* @param[in] hWnd 播放视频的窗口句柄
	* @return BOOL，成功返回TURE，失败返回FALSE
	* @note 如果返回失败，可以调用PLAY_GetLastErrorEx接口获取错误码。
	 */
        [DllImport(@"hz_playplay.dll")]
        public static extern bool PLAY_Play(int nPort, IntPtr hWnd);

        /**
		 * 关闭播放。
		 *
		 * @param[in] nPort 通道号
		 * @return BOOL，成功返回TURE，失败返回FALSE
		 * @note 如果返回失败，可以调用PLAY_GetLastErrorEx接口获取错误码。
		 */
        [DllImport(@"hz_playplay.dll")]
        public static extern bool PLAY_Stop(int nPort);

        /**
		 * 暂停/恢复播放。
		 *
		 * @param[in] nPort 通道号
		 * @param[in] nPause 1：暂停；0：恢复
		 * @return 成功返回TURE，失败返回FALSE
		 * @note 如果返回失败，可以调用PLAY_GetLastErrorEx接口获取错误码。
		 */
        [DllImport(@"hz_playplay.dll")]
        public static extern bool PLAY_Pause(int nPort, int nPause);



        [DllImport(@"hz_playplay.dll")]
        public static extern bool PLAY_CloseStream(int nPort);



        [DllImport(@"hz_playplay.dll")]
        public static extern bool PLAY_ReleasePort(int lPort);

        #endregion

        //
    }
} 