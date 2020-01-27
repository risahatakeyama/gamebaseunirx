using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UniRx;
using UnityEngine;

namespace FictionProject.Master
{
    public class MasterLoader
    {
        private string _linkListUrl = "";//linkMbのurl
        private string _directoryPath;
        private Dictionary<string, string> _dicLink;
        private Dictionary<long, PuzzlePieceMB> _puzzlePieceMB;
        private Dictionary<long, PuzzleSetMB> _puzzleSetMB;


        public MasterLoader()
        {
            _directoryPath = Application.persistentDataPath + "/Master";
        }

        /// <summary>
        /// MBのURLが入ったMBを取得（LinkMBの取得）
        /// </summary>
        public IEnumerator LoadLinkList()
        {
            WWW linkList = new WWW(_linkListUrl);

            while (!linkList.isDone)
            {
                //ダウンロードの進捗を表示
                Debug.Log("マスターリストのダウンロード中");
                yield return null;
            }

            if (!string.IsNullOrEmpty(linkList.error))
            {
                Debug.Log("ダウンロード途中でエラー");
            }
            else
            {
                //ローカルのディレクトリ取得
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }
                //ローカルに保存
                var filePath = _directoryPath + "/LinkMB";
                if (File.Exists(filePath))
                {
                    var reader = new StreamReader(filePath);
                    string fileContent = reader.ReadToEnd();
                    reader.Close();
                    if (fileContent != linkList.text.TrimStart())
                    {
                        Debug.Log("内容が違うのでダウンロード１");
                        File.WriteAllBytes(filePath, linkList.bytes);
                    }
                    else
                    {
                        Debug.Log("内容が同じなのでローカルに保存なし");
                    }
                }
                else
                {
                    Debug.Log("内容が違うのでダウンロード２");
                    File.WriteAllBytes(filePath, linkList.bytes);
                }
                _dicLink = new Dictionary<string, string>();
                _dicLink = JsonConvert.DeserializeObject<Dictionary<string, string>>(linkList.text.TrimStart());

                Debug.Log("LinkMBJson変換終了");

                #region 個々のマスターブックをダウンロードする
                DownLoadMasterBook<PuzzlePieceMB>().ToObservable().Subscribe(type =>
                {
                    _puzzlePieceMB = type;
                });
                DownLoadMasterBook<PuzzleSetMB>().ToObservable().Subscribe(type =>
                {
                    _puzzleSetMB = type;
                });
                #endregion 個々のマスターブックをダウンロードする

                Debug.Log("MasterBook化完了");
            }
        }

        /// <summary>
        /// MBをダウンロードする(個々)
        /// </summary>
        private IEnumerable<Dictionary<long, T>> DownLoadMasterBook<T>()
        {
            var url = _dicLink[typeof(T).Name];
            WWW wwwMaster = new WWW(url);
            while (!wwwMaster.isDone)
            {
                Debug.Log("マスターのダウンロード中" + url);
                yield return null;
            }

            if (!string.IsNullOrEmpty(wwwMaster.error))
            {
                Debug.Log("ダウンロード途中でエラー");
            }
            else
            {
                var filePath = _directoryPath + "/" + typeof(T).Name;
                if (File.Exists(filePath))
                {
                    var reader = new StreamReader(filePath);
                    string fileContent = reader.ReadToEnd();
                    reader.Close();
                    if (fileContent != wwwMaster.text.TrimStart())
                    {
                        File.WriteAllBytes(filePath, wwwMaster.bytes);
                    }
                    else
                    {
                        Debug.Log("内容が同じなのでローカルに保存なし");
                    }
                }
                else
                {
                    File.WriteAllBytes(filePath, wwwMaster.bytes);
                }
                Debug.Log("jsonダウンロード");
                var master = JsonConvert.DeserializeObject<Dictionary<long, T>>(wwwMaster.text.TrimStart());
                Debug.Log("jsonダウンロード成功");
                yield return master;
            }
        }

        /// <summary>
        /// MBを参照する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>(long id) where T : MBBase
        {
            if (typeof(T) == typeof(PuzzlePieceMB))
            {
                return (T)(object)_puzzlePieceMB[id];
            }
            if (typeof(T) == typeof(PuzzleSetMB))
            {
                return (T)(object)_puzzleSetMB[id];
            }
            return default(T);
        }
    }
}
