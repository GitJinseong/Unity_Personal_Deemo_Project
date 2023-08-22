
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;

public class Park_CSVReader : MonoBehaviour
{
    #region 싱글톤 선언
    private static Park_CSVReader m_instance; // 싱글톤이 할당될 static 변수
    public static Park_CSVReader instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 CSVReader 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<Park_CSVReader>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }
    #endregion
    
    public const char DELIMITER = ','; // CSV 파일에서 사용하는 구분자 (기본값은 콤마)

    // csv 파일의 정보를 행과 열로 구분하여 저장할 딕셔너리
    // 행은 키 값이 되고, 열은 키 값 내부의 값이 된다.
    public Dictionary<string, List<string>> dataDictionary = default;

    // CSV 파일을 읽는 함수
    // csvFileName에는 "MusicList"와 같이 파일의 이름을 입력한다. (확장자 제외)
    public Dictionary<string, List<string>> ReadCSVFile(string csvFileName)
    {
        dataDictionary = new Dictionary<string, List<string>>();

        TextAsset csvTextAsset = Resources.Load<TextAsset>(csvFileName);

        if (csvTextAsset != null)
        {

            string[] lines = csvTextAsset.text.Split('\n');

            if (lines.Length > 0)
            {
                string[] headers = lines[0].Split(DELIMITER);

                foreach (string header in headers)
                {
                    dataDictionary.Add(header, new List<string>());
                }

                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] values = line.Split(DELIMITER);

                    for (int j = 0; j < values.Length; j++)
                    {
                        dataDictionary[headers[j]].Add(values[j]);
                    }
                }
            }
            else
            {
                Debug.LogError("CSV file is empty.");
            }
        }
        else
        {
            Debug.LogError("CSV file not found: " + csvFileName);
        }

        return dataDictionary;
    }

    // 변환된 csv 파일의 정보가 저장된 딕셔너리 내부의 값을 출력하는 함수.
    // "행:열1,열2,열3"과 같이 출력된다.
    // 매개 변수로 받을 딕셔너리의 구조는 <string, List<string>> 이어야 한다.
    public void PrintData(Dictionary<string, List<string>> dictionary)
    {
        // 딕셔너리의 각 항목을 출력
        foreach (KeyValuePair<string, List<string>> entry in dictionary)
        {
            string category = entry.Key;
            List<string> values = entry.Value;

            Debug.Log(category + ": " + string.Join(", ", values));
        }
    }

    public void WriteCSVFile(string csvFileName)
    {
        string filePath = Application.dataPath + "/" + csvFileName;

        StringBuilder csvContent = new StringBuilder();

        // 헤더 라인 작성
        csvContent.AppendLine(string.Join(DELIMITER.ToString(), dataDictionary.Keys));

        // 각 데이터 행 작성
        int rowCount = dataDictionary.Values.First().Count;
        for (int i = 0; i < rowCount; i++)
        {
            List<string> row = new List<string>();
            foreach (var key in dataDictionary.Keys)
            {
                row.Add(dataDictionary[key][i]);
            }
            csvContent.AppendLine(string.Join(DELIMITER.ToString(), row));
        }

        // 파일에 내용 쓰기
        File.WriteAllText(filePath, csvContent.ToString());

        Debug.Log("CSV file saved to: " + filePath);
    }
}
////using System.Collections.Generic;
////using UnityEngine;
////using System.IO;

////public class CSVReader : MonoBehaviour
////{
////    #region 싱글톤 선언
////    private static CSVReader m_instance; // 싱글톤이 할당될 static 변수
////    public static CSVReader instance
////    {
////        get
////        {
////            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
////            if (m_instance == null)
////            {
////                // 씬에서 CSVReader 오브젝트를 찾아 할당
////                m_instance = FindObjectOfType<CSVReader>();
////            }

////            // 싱글톤 오브젝트를 반환
////            return m_instance;
////        }
////    }
////    #endregion

////    public const char DELIMITER = ','; // CSV 파일에서 사용하는 구분자 (기본값은 콤마)

////    // csv 파일의 정보를 행과 열로 구분하여 저장할 딕셔너리
////    // 행은 키 값이 되고, 열은 키 값 내부의 값이 된다.
////    public Dictionary<string, List<string>> dataDictionary = default;

////    // CSV 파일을 읽는 함수
////    // dirPath에는 "Assets/Resources/ZombieDatas"과 같이 디렉토리 경로를 입력한다.
////    // csvFileName에는 "ZombieSurvivalDatas.csv"과 같이 csv 파일의 이름을 입력한다.
////    public Dictionary<string, List<string>> ReadCSVFile()
////    {
////        dataDictionary = new Dictionary<string, List<string>>();
////        string filePath = "Assets/Resources/" + "MusicList.csv";
////        try
////        {
////            using (StreamReader reader = new StreamReader(filePath))
////            {
////                string[] headers = reader.ReadLine().Split(DELIMITER);

////                foreach (string header in headers)
////                {
////                    dataDictionary.Add(header, new List<string>());
////                }

////                while (!reader.EndOfStream)
////                {
////                    string line = reader.ReadLine();
////                    string[] values = line.Split(DELIMITER);

////                    for (int i = 0; i < values.Length; i++)
////                    {
////                        dataDictionary[headers[i]].Add(values[i]);
////                    }
////                }
////            }
////            return dataDictionary;
////        }

////        // csv 파일에 문제가 있을 경우 오류 메시지 출력
////        catch (IOException e)
////        {
////            Debug.LogError("Error reading the CSV file: " + e.Message);
////            return default;
////        }
////    }

////    // 변환된 csv 파일의 정보가 저장된 딕셔너리 내부의 값을 출력하는 함수.
////    // "행:열1,열2,열3"과 같이 출력된다.
////    // 매개 변수로 받을 딕셔너리의 구조는 <string, List<string>> 이어야 한다.
////    public void PrintData(Dictionary<string, List<string>> dictionary)
////    {
////        // 딕셔너리의 각 항목을 출력
////        foreach (KeyValuePair<string, List<string>> entry in dictionary)
////        {
////            string category = entry.Key;
////            List<string> values = entry.Value;

////            Debug.Log(category + ": " + string.Join(", ", values));
////        }
////    }
////}

//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;

//public class CSVReader : MonoBehaviour
//{
//    #region 싱글톤 선언
//    private static CSVReader m_instance; // 싱글톤이 할당될 static 변수
//    public static CSVReader instance
//    {
//        get
//        {
//            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
//            if (m_instance == null)
//            {
//                // 씬에서 CSVReader 오브젝트를 찾아 할당
//                m_instance = FindObjectOfType<CSVReader>();
//            }

//            // 싱글톤 오브젝트를 반환
//            return m_instance;
//        }
//    }
//    #endregion

//    public const char DELIMITER = ','; // CSV 파일에서 사용하는 구분자 (기본값은 콤마)

//    // csv 파일의 정보를 행과 열로 구분하여 저장할 딕셔너리
//    // 행은 키 값이 되고, 열은 키 값 내부의 값이 된다.
//    public Dictionary<string, List<string>> dataDictionary = default;

//    // CSV 파일을 읽는 함수
//    // dirPath에는 "Assets/Resources/ZombieDatas"과 같이 디렉토리 경로를 입력한다.
//    // csvFileName에는 "ZombieSurvivalDatas.csv"과 같이 csv 파일의 이름을 입력한다.
//    public Dictionary<string, List<string>> ReadCSVFile()
//    {
//        dataDictionary = new Dictionary<string, List<string>>();
//        string filePath = "Assets/Resources/" + "MusicList.csv";
//        try
//        {
//            using (StreamReader reader = new StreamReader(filePath))
//            {
//                string[] headers = reader.ReadLine().Split(DELIMITER);

//                foreach (string header in headers)
//                {
//                    dataDictionary.Add(header, new List<string>());
//                }

//                while (!reader.EndOfStream)
//                {
//                    string line = reader.ReadLine();
//                    string[] values = line.Split(DELIMITER);

//                    for (int i = 0; i < values.Length; i++)
//                    {
//                        dataDictionary[headers[i]].Add(values[i]);
//                    }
//                }
//            }
//            return dataDictionary;
//        }

//        // csv 파일에 문제가 있을 경우 오류 메시지 출력
//        catch (IOException e)
//        {
//            Debug.LogError("Error reading the CSV file: " + e.Message);
//            return default;
//        }
//    }

//    // 변환된 csv 파일의 정보가 저장된 딕셔너리 내부의 값을 출력하는 함수.
//    // "행:열1,열2,열3"과 같이 출력된다.
//    // 매개 변수로 받을 딕셔너리의 구조는 <string, List<string>> 이어야 한다.
//    public void PrintData(Dictionary<string, List<string>> dictionary)
//    {
//        // 딕셔너리의 각 항목을 출력
//        foreach (KeyValuePair<string, List<string>> entry in dictionary)
//        {
//            string category = entry.Key;
//            List<string> values = entry.Value;

//            Debug.Log(category + ": " + string.Join(", ", values));
//        }
//    }
//}