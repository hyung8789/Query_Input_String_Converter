﻿using System;
using System.IO;
using 쿼리_입력_문자열_변환기;
using static utils.IniUtil;

namespace managers
{
    public class SettingManager
    {
        public static readonly string SETTING_FILE_NAME = "settings.ini";
        public static readonly string INPUT_DELIMITER_KEY = "INPUT_DELIMITER";
        public static readonly string SQL_STR_SIGN_TYPE_KEY = "SQL_STR_SIGN_TYPE";

        public static char INPUT_DELIMITER = '\n'; //입력 구분자
        public static string SQL_STR_SIGN_TYPE = "'"; //문자열 부호 종류
        /// <summary>
        /// 설정 저장
        /// </summary>
        public static void SaveSettings()
        {
            var iniFile = new IniFile();
            iniFile[Program.selfProcessFileName][INPUT_DELIMITER_KEY] = INPUT_DELIMITER;
            iniFile[Program.selfProcessFileName][SQL_STR_SIGN_TYPE_KEY] = SQL_STR_SIGN_TYPE;
            iniFile.Save(SETTING_FILE_NAME);
        }
        /// <summary>
        /// 설정 불러오기
        /// </summary>
        public static void LoadSettings()
        {
            FileInfo settingFileInfo = new FileInfo(SETTING_FILE_NAME);

            if (settingFileInfo.Exists)
            {
                var iniFile = new IniFile();
                iniFile.Load(SETTING_FILE_NAME);

                if (!iniFile.ContainsSection(Program.selfProcessFileName)) //현재 실행 프로세스 파일 명에 해당하는 섹션이 존재하지 않을 경우
                    goto USE_DEFAULT_SETTINGS;

                INPUT_DELIMITER = Convert.ToChar(iniFile[Program.selfProcessFileName][INPUT_DELIMITER_KEY].ToInt());
                SQL_STR_SIGN_TYPE = iniFile[Program.selfProcessFileName][SQL_STR_SIGN_TYPE_KEY].ToString();
                return;
            }

USE_DEFAULT_SETTINGS: //기본 설정 사용
            SaveSettings();
        }
    }
}