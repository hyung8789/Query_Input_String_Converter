using managers;
using System;
using System.IO;
using System.Windows;
using static Crayon.Output;

namespace 쿼리_입력_문자열_변환기
{
    internal class Program
    {
        public static readonly string selfProcessFileName =
            System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe";

        /// <summary>
        /// 대상 파일 (텍스트 형식으로 간주)로부터 문자열 반환 
        /// </summary>
        /// <param name="targetFileName">대상 파일</param>
        /// <returns>대상 파일로부터 읽어들인 문자열</returns>
        /// <exception cref="Exception">오류 : 해당 입력 파일이 존재하지 않습니다</exception>
        private static string[] ReadText(string targetFileName)
        {
            FileInfo fileInfo = new FileInfo(targetFileName);
            if (!fileInfo.Exists)
                throw new Exception("오류 : 해당 입력 파일이 존재하지 않습니다");

            //입력 구분자에 따라 분리
            string lines = File.ReadAllText(targetFileName);
            return lines.Split(SettingManager.INPUT_DELIMITER);
        }
        /// <summary>
        /// 대상 문자열을 대상 파일에 기록
        /// </summary>
        /// <param name="targetData">대상 문자열</param>
        /// <param name="outputFileName">대상 파일</param>
        /// <exception cref="Exception">오류 : 해당 출력 파일이 이미 존재합니다.</exception>
        private static void WriteText(string[] targetData, string outputFileName)
        {
            FileInfo outputFileInfo = new FileInfo(outputFileName);
            if (outputFileInfo.Exists)
                throw new Exception("오류 : 해당 출력 파일이 이미 존재합니다.");

            using (StreamWriter writer = outputFileInfo.CreateText())
            {
                foreach (var i in targetData)
                {
                    if (!string.IsNullOrEmpty(i))
                    {
                        writer.WriteLine(i);
                    }
                }
            }
        }
        /// <summary>
        /// 대상 문자열 변환
        /// </summary>
        /// <param name="targetData">대상 문자열</param>
        /// <exception cref="Exception">오류 : 입력 데이터가 존재하지 않습니다.</exception>
        private static void ConvertText(string[] targetData)
        {
            if (targetData == null || targetData.Length == 0)
                throw new Exception("오류 : 입력 데이터가 존재하지 않습니다.");

            for (int i = 0; i < targetData.Length; i++)
            {
                if (!string.IsNullOrEmpty(targetData[i].Trim()))
                {
                    targetData[i] = SettingManager.SQL_STR_SIGN_TYPE + targetData[i].Trim();

                    if (i == targetData.Length - 1) //마지막 행일 경우
                        targetData[i] += SettingManager.SQL_STR_SIGN_TYPE;
                    else
                        targetData[i] += SettingManager.SQL_STR_SIGN_TYPE + ",";
                }
            }
        }
        [STAThread]
        static void Main(string[] args)
        {
            string helpMsg =
                Bold().Underline().Text("< 쿼리 입력 문자열 변환기 >") + Environment.NewLine +
@"
엑셀, 텍스트 파일 등으로부터 받은 입력 구분자 (\n, \t, ',', ' ' 등)로 구별되는 일련의 데이터 목록을 
쿼리 조건식으로 입력을 위한 문자열 형식 (선행 및 후행에 단일 부호 혹은 이중 부호가 존재하는 문자 시퀸스)
으로 변환하기 위해, 입력 구분자 (\n, \t, ',', ' ' 등)으로 구별되는 각 데이터의 선행 및 후행에 문자열 형식에 사용되는 문자 추가
" +
Environment.NewLine + Bold().Underline().Text("< " + SettingManager.SETTING_FILE_NAME + "설정 방법 >") + Environment.NewLine +
@"
    1) INPUT_DELIMITER : 입력 파일의 각 문자열을 구분하기 위한 구분자(\n, \t, ',', ' ' 등)
        1-1) https://www.asciitable.com/ 의 10진 ASCII 코드 참조 할 것

    2) SQL_STR_SIGN_TYPE : 문자열 부호 종류
        2-1) 단일 부호 : '
        2-2) 이중 부호 : ""
" +
Environment.NewLine + Bold().Underline().Text("< 명령줄 인수 사용법 >") + Environment.NewLine +
@"
{0} (입력 파일명) (출력 파일명 (Optional), 입력하지 않을 경우 클립보드로 결과 복사)
ex) {0} input.txt result.txt

---
";
            Console.WriteLine(Green(helpMsg), selfProcessFileName);
            SettingManager.LoadSettings();

            int argsCount = args.Length;

            string inputFileName = null;
            string outputFileName = null;
            string[] inputData = null;

            switch (argsCount)
            {
            case 0:
                Console.Write("입력 파일명 >>");
                inputFileName = Console.ReadLine();
                Console.Write("출력 파일명 (Optional), 입력하지 않을 경우 클립보드로 결과 복사 >>");
                outputFileName = Console.ReadLine();
                break;

            case 1:
                inputFileName = args[0];
                break;

            case 2:
                inputFileName = args[0];
                outputFileName = args[1];
                break;

            default: //잘못 된 인자 개수
                Console.ReadKey();
                return;
            }

            try
            {
                inputData = ReadText(inputFileName);
                ConvertText(inputData);

                if (string.IsNullOrEmpty(outputFileName))
                {
                    Clipboard.Clear();
                    Clipboard.SetText(string.Join(Environment.NewLine, inputData));
                }
                else
                {
                    WriteText(inputData, outputFileName);
                }

                Console.WriteLine("done");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}