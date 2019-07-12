// 5/31 玉那覇臣　BGM切り替え
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{

    private int OldPage;
    private int nowPage;
    private int nowFlow;

    private bool[] DrawFlg = new bool[4];
    private List<GameObject> PageList = new List<GameObject>();

    public Filter_Fade Framefade;
    public UIManager uimanager;
    public TextManager textmanager;

    [SerializeField] GameObject[] pages; // カット1～4

    [SerializeField] GameObject StoryBGMManager;

    [SerializeField] private AudioClip _BGM_Enemyinvasion;

    private AudioSource _audioSource;

    private void Start()
    {
        OldPage = 0;
        nowPage = 0;
        nowFlow = 0;
        for (int i = 0; i < DrawFlg.Length; i++)
        {
            DrawFlg[i] = false;
        }

        _audioSource = StoryBGMManager.GetComponent<AudioSource>();
    }
    private void Update()
    {
        switch (nowFlow)
        {
            case 0:
                if (!DrawFlg[nowPage])
                {
                    PageOn();
                }
                Framefade.SetFadeFlg(false);
                textmanager.FadeOff(OldPage);
                break;
            case 1:
                Framefade.SetFadeFlg(true);
                break;
            case 2:
                // テキスト描画
                textmanager.FadeOn(nowPage);
                break;
            case 3:
                uimanager.AllTransparentOff();
                break;
            default:
                break;
        }
    }

    public void PageOn()
    {


        for (int i = 0; i < PageList.Count; i++)
        {
            Destroy(PageList[i]);
        }

        PageList.Clear();
        DrawFlg[OldPage] = false;

        if (!DrawFlg[nowPage])
        {
            GameObject NowPage = Instantiate(pages[nowPage]) as GameObject;
            PageList.Add(NowPage);
            DrawFlg[nowPage] = true;
        }
        if (nowPage == 1)
        {
            _audioSource.clip = _BGM_Enemyinvasion;
            _audioSource.Play();
        }
        if(nowPage == 3)
            _audioSource.Stop();

    }

    public void PageNext()
    {
        if (nowPage < pages.Length - 1)
        {
            nowPage++;
            OldPage = nowPage - 1;
        }
    }

    public void PageBack()
    {
        if (nowPage > 0)
        {
            nowPage--;
            OldPage = nowPage + 1;
        }
    }

    public void FlowNext()
    {
        nowFlow++;
    }

    public void FlowReset()
    {
        nowFlow = 0;
    }

    public int GetFlow()
    {
        return nowFlow;
    }

    public int GetNowPage()
    {
        return nowPage;
    }
}