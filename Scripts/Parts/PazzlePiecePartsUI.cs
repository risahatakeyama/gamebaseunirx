using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using FictionProject.Expansion;
using DG.Tweening;
namespace FictionProject.UI
{
    [PrefabPath("UI/Parts/Parts-PazzlePiece")]
    public class PazzlePiecePartsUI : MonoBehaviour
    {

        public Button button;
        public Image image;
        public RectTransform rectTransform;

        /// <summary>座標情報(縦) </summary>
        public int vartical { get; set; }
        /// <summary>座標情報(横) </summary>
        public int horizontal { get; set; }

        public long pieceId { get; set; }

        private RectTransform currentParentRectTransform;

        private HorizontalLayoutGroup currentparentLayoutGroup;

        public void InitPiece(Dictionary<string,object> param)
        {
            var sprite = param.Get<Sprite>("sprite");
            image.sprite = sprite;

            vartical = param.GetInt("vartical");
            horizontal = param.GetInt("horizontal");

            currentParentRectTransform = param.Get<RectTransform>("currentParentRectTransform");

            currentparentLayoutGroup = param.Get<HorizontalLayoutGroup>("currentparentLayoutGroup");

            var onCallBack = param.Get<Action>("onCallBack");
            
            button.OnClickIntentAsObservable()
                .Subscribe(_=> {
                    onCallBack();
                });
        }
        /// <summary>
        /// 場所変更
        /// </summary>
        public void ChangePlace(RectTransform afterParentRectTransform,HorizontalLayoutGroup afterParentLayoutGroup)
        {
            if (currentparentLayoutGroup == null)
                return;
            currentparentLayoutGroup.childControlHeight = false;
            currentparentLayoutGroup.childControlWidth = false;

            var tempLayoutGroup = currentparentLayoutGroup;
            //一旦動いたように見せる
            rectTransform
                .DOMove(afterParentRectTransform.anchoredPosition,0.5f)
                .OnComplete(()=> {
                    currentparentLayoutGroup = afterParentLayoutGroup;
                    rectTransform.SetParent(afterParentRectTransform);
                    tempLayoutGroup.childControlHeight = true;
                    tempLayoutGroup.childControlWidth = true;
            });
        }



    }

}


