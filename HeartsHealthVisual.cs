using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsHealthVisual : MonoBehaviour
{
    //TODO: heartsHealthSystemStatic should be managed in the PlayerBehavior
    public static HeartsHealthSystem heartsHealthSystemStatic;
    public const int IMAGE_SIZE = 16;

    //TODO: numHearts should be managed in the PlayerBehavior
    [SerializeField] private int numHearts;
    [SerializeField] private Sprite heart0Sprite;
    [SerializeField] private Sprite heart1Sprite;
    [SerializeField] private Sprite heart2Sprite;
    [SerializeField] private Sprite heart3Sprite;
    [SerializeField] private Sprite heart4Sprite;
    [SerializeField] private AnimationClip heartbeatClip;

    private List<HeartImage> heartImagesList;
    private HeartsHealthSystem heartsHealthSystem;
    private bool isHealing;

    private void Awake()
    {
        heartImagesList = new List<HeartImage>();
    }

    private void Start()
    {
        HeartsHealthSystem heartsHealthSystem = new HeartsHealthSystem(numHearts);
        SetHeartdHealthSystem(heartsHealthSystem);
        InvokeRepeating("HealAnimatedPeriodically", .01f, .05f);
    }

    public void SetHeartdHealthSystem(HeartsHealthSystem heartsHealthSystem)
    {
        this.heartsHealthSystem = heartsHealthSystem;
        heartsHealthSystemStatic = heartsHealthSystem;

        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();
        int row = 0;
        int col = 0;
        int colMax = 10;
        for (int i = 0; i <heartList.Count; i++)
        {
            HeartsHealthSystem.Heart heart = heartList[i];
            Vector2 hearthAnchoredPosition = new Vector2(col * IMAGE_SIZE, -row * IMAGE_SIZE);
            CreateHeartImage(hearthAnchoredPosition).SetHeartFragments(heart.GetFragments());

            col++;
            if (col >= colMax)
            {
                row++;
                col = 0;
            }
        }

        heartsHealthSystem.OnDamaged += HeartsHealthSystem_OnDamaged;
        heartsHealthSystem.OnHealed += HeartsHealthSystem_OnHealed;
        heartsHealthSystem.OnDead += HeartsHealthSystem_OnDead;
    }

    private void HeartsHealthSystem_OnDead(object sender, System.EventArgs e)
    {
        //TODO
        Debug.Log("Player is DEAD!");
        throw new System.NotImplementedException();
    }

    private void HeartsHealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        isHealing = true;
        //RefreshAllHearts();
    }

    private void HeartsHealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        RefreshAllHearts();
    }

    private void RefreshAllHearts()
    {
        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();
        for (int i = 0; i < heartImagesList.Count; i++)
        {
            HeartImage heartImage = heartImagesList[i];
            HeartsHealthSystem.Heart heart = heartList[i];
            heartImage.SetHeartFragments(heart.GetFragments());
        }
    }

    private void HealAnimatedPeriodically()
    {
        if (!isHealing) return;

        bool isFullyHealed = true;
        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();
        for (int i = 0; i < heartImagesList.Count; i++)
        {
            HeartImage heartImage = heartImagesList[i];
            HeartsHealthSystem.Heart heart = heartList[i];
            if(heartImage.GetFragments() != heart.GetFragments())
            {
                //Visual is different from Logic
                heartImage.AddHeartVisualFragment();
                if(heartImage.GetFragments() == HeartsHealthSystem.MAX_FRAGMENT_AMOUNT)
                {
                    heartImage.PlayHeartbeat();
                }
                isFullyHealed = false;
                break;
            }
        }
        if (isFullyHealed)
        {
            isHealing = false;
        }
    }



    private HeartImage CreateHeartImage(Vector2 anchoredPosition)
    {
        //Create GameObject
        GameObject heartGameObject = new GameObject("Heart", typeof(Image), typeof(Animation));
        //Set a child of this transform
        heartGameObject.transform.SetParent(transform);
        heartGameObject.transform.localPosition = Vector3.zero;
        //Locate and Size Heart
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(IMAGE_SIZE, IMAGE_SIZE);
        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        //Set Animation
        heartGameObject.GetComponent<Animation>().AddClip(heartbeatClip,"Heartbeat");
        //Set heart Sprite
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = heart0Sprite;

        HeartImage heartImage = new HeartImage(this, heartImageUI, heartGameObject.GetComponent<Animation>());
        heartImagesList.Add(heartImage);

        return heartImage;
    }

    //Represents a Single Heart
    public class HeartImage
    {
        private HeartsHealthVisual heartsHealthVisual;
        private Image heartImage;
        private int fragments;
        private Animation animation;
        public HeartImage(HeartsHealthVisual heartsHealthVisual, Image heartImage, Animation animation)
        {
            this.heartsHealthVisual = heartsHealthVisual;
            this.heartImage = heartImage;
            this.animation = animation;
        }

        public void SetHeartFragments(int fragments)
        {
            this.fragments = fragments;
            switch (fragments)
            {
                case 0: heartImage.sprite = heartsHealthVisual.heart0Sprite; break;
                case 1: heartImage.sprite = heartsHealthVisual.heart1Sprite; break;
                case 2: heartImage.sprite = heartsHealthVisual.heart2Sprite; break;
                case 3: heartImage.sprite = heartsHealthVisual.heart3Sprite; break;
                case 4: heartImage.sprite = heartsHealthVisual.heart4Sprite; break;
            }
        }

        public int GetFragments()
        {
            return fragments;
        }

        public void AddHeartVisualFragment()
        {
            SetHeartFragments(fragments + 1);
        }

        public void PlayHeartbeat()
        {
            animation.Play("Heartbeat", PlayMode.StopAll);
        }
    }
}
