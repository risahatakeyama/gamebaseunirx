using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FictionProject.Expantion;
using FictionProject.Data;
using System;
using System.Linq;
using FictionProject.Master;
using UnityEngine.AddressableAssets;
namespace FictionProject.UI
{
    //複数存在する
    public class PazzleSetUIScript : MonoBehaviour
    {
        public PazzleGridLayoutGroup _layoutGroup;
        //private List<List<PazzlePiecePartsUI>> _pazzlePieceBoard;
        private List<PazzlePiecePartsUI> pazzlePieceList;


        public void Init(PuzzleSetMB puzzleSetMB)
        {
            //_pazzlePieceBoard = new List<List<PazzlePiecePartsUI>>();

            pazzlePieceList = new List<PazzlePiecePartsUI>();
            //pieceを生成する
            var currentHorizontal = 0;
            var currentVartical = 0;
            foreach (var parentRectTransform in _layoutGroup.GetChildRectTransforms)
            {
                var pazzlePiece = CreateResource.Instance.CreateGameObject<PazzlePiecePartsUI>();

                var parentLayoutGroup =
                    parentRectTransform.gameObject.GetComponent<HorizontalLayoutGroup>();
                var param = new Dictionary<string, object>()
                {
                    {"onCallBack",(Action)UpdatePazzle },
                    {"parentLayoutGroup",parentLayoutGroup }
                };
                pazzlePiece.InitPiece(param);
                pazzlePiece.horizontal = currentHorizontal;
                pazzlePiece.vartical = currentVartical;
                currentHorizontal++;

                pazzlePiece.gameObject.transform.SetParent(parentRectTransform);
                pazzlePieceList.Add(pazzlePiece);

                if (pazzlePieceList.Count == _layoutGroup.HorizontalPazzleCount)
                {
                    //_pazzlePieceBoard.Add(pazzlePieceList);
                    //pazzlePieceList = new List<PazzlePiecePartsUI>();
                    currentVartical++;
                    currentHorizontal = 0;
                }
            }


        }
        private void UpdatePazzle()
        {
            //パズルピースを消したり

            //移動したりする場所
            foreach (var pazzlePiece in pazzlePieceList)
            {
                //pazzlePieceList.
            }
        }
        private void SetPuzzle(PuzzleSetMB puzzleSetMB)
        {
            var pieceIdList = puzzleSetMB.puzzlePieceIdList;
            var puzzlePieceInfoList = new List<PuzzlePieceInfo>();

            pieceIdList.ForEach(id =>
            {
                var puzzlePieceMB = ApplicationContext.MasterLoader.Get<PuzzlePieceMB>(id);

                //いずれアセットバンドル化
                Addressables.LoadAsset<Sprite>(puzzlePieceMB.imagePath)
                .Completed += sprite => { puzzlePieceInfoList.Add(new PuzzlePieceInfo(id, sprite.Result)); };

            });

            //ランダムで最初作って、
            if (pazzlePieceList == null || pazzlePieceList.Count == 0)
                return;
            pazzlePieceList.ForEach(parts =>
            {
                int index = UnityEngine.Random.Range(0, puzzlePieceInfoList.Count - 1);
                var info = puzzlePieceInfoList[index];
                parts.image.sprite = info.sprite;
                parts.pieceId = info.id;

            });
            //組み合わせがない場合は作り直す。（繰り返し
            Check();
        }
        private void Check()
        {
             
        }
    }
}

