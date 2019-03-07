using UnityEngine;
using System.Collections;
using UnityEngine.UI;


//NOTE! You should hava a Camera with "MainCamera" tag and a canvas with a Screen Space - Overlay mode to script works properly;

public class HealthBar : MonoBehaviour {
	
	public GameDesigner.PlayerSystem player;				//A path to the Health filed;			
	public RectTransform HealthbarPrefab;			//Our health bar prefab;
	public float yOffset;							//Horizontal position offset;
	public bool keepSize = true;	                //keep distance independed size;
	public float scale = 1;							//Scale of the healthbar;
	public Vector2 sizeOffsets;						//Use this to overwright healthbar width and height values;
	public bool DrawOFFDistance;					//Disable health bar if it out of drawDistance;
	public float drawDistance = 10;
	public bool showHealthInfo;						//Show the health info on top of the health bar or not;
	public enum HealthInfoAlignment {top, center, bottom};
	public HealthInfoAlignment healthInfoAlignment = HealthInfoAlignment.center;
	public float healthInfoSize = 10;
    public AlphaStateMachineSetting alphaStateMachineSetting;
	private Image healthVolume, backGround;			//Health bar images, should be named as "Health" and "Background";
	private Text healthInfo;						//Health info, a healthbar's child Text object(should be named as HealthInfo);
	private CanvasGroup canvasGroup;
	private Vector2 healthbarSize, healthInfoPosition;
	private Transform thisT;
	private float lastHealth, camDistance, dist, rate;
	private Camera cam;
    private GameObject healthbarRoot;
	public Canvas canvas;

	// Use this for initialization
	void Start () 
	{
		if( !canvas )
		{
			Canvas[] canvases = FindObjectsOfType<Canvas>();

			for (int i = 0; i < canvases.Length; i++)
			{
				if(canvases[i].enabled && canvases[i].gameObject.activeSelf && canvases[i].renderMode == RenderMode.ScreenSpaceOverlay)
					canvas = canvases[i];
			}

			if( !canvas )
			{
				var canva = new GameObject("_Canvas").AddComponent<Canvas>();
				canva.renderMode = RenderMode.ScreenSpaceOverlay;
				var scaler = canva.gameObject.AddComponent<CanvasScaler>();
				scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
				scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
				canvas = canva;
			}
		}

		if( !player )
		{
			player = GetComponent<GameDesigner.PlayerSystem>();

			if( !player )
			{
				player = gameObject.AddComponent<GameDesigner.PlayerSystem>();
			}
		}
		lastHealth = player.Hp;

		if(!HealthbarPrefab)
		{
			return;
		}
		
		thisT = this.transform;
        if (canvas.transform.Find("HealthbarRoot") != null)
            healthbarRoot = canvas.transform.Find("HealthbarRoot").gameObject;
        else
            healthbarRoot = new GameObject("HealthbarRoot", typeof(RectTransform));
        healthbarRoot.transform.SetParent(canvas.transform, false);
		healthbarRoot.transform.SetAsFirstSibling();
		HealthbarPrefab = (RectTransform)Instantiate(HealthbarPrefab, new Vector2 (-1000, -1000), Quaternion.identity);
        HealthbarPrefab.name = "HealthBar";
        HealthbarPrefab.SetParent(healthbarRoot.transform, false);
		canvasGroup = HealthbarPrefab.GetComponent<CanvasGroup> ();
		
		healthVolume = HealthbarPrefab.transform.Find ("Health").GetComponent<Image>();
		backGround = HealthbarPrefab.transform.Find ("Background").GetComponent<Image>();
		healthInfo = HealthbarPrefab.transform.Find ("HealthInfo").GetComponent<Text> ();
		healthInfo.resizeTextForBestFit = true;
		healthInfo.rectTransform.anchoredPosition = Vector2.zero;
		healthInfoPosition = healthInfo.rectTransform.anchoredPosition;
		healthInfo.resizeTextMinSize = 1;
		healthInfo.resizeTextMaxSize = 500;
        HealthbarPrefab.transform.Find("PlayerName").GetComponent<Text>().text = player.PlayerName;

        healthbarSize = HealthbarPrefab.sizeDelta;
        canvasGroup.alpha = alphaStateMachineSetting.fullAplpha;
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update(){
		if(!HealthbarPrefab)
			return;

		HealthbarPrefab.transform.position = cam.WorldToScreenPoint(new Vector3(thisT.position.x, thisT.position.y + yOffset, thisT.position.z));
		healthVolume.fillAmount =  player.Hp/player.HpMax;

		if(backGround.fillAmount > healthVolume.fillAmount + 0.1f)
			backGround.fillAmount = healthVolume.fillAmount + 0.1f;
        if (backGround.fillAmount > healthVolume.fillAmount)
			backGround.fillAmount -= 0.2f * Time.deltaTime;
        else
            backGround.fillAmount = healthVolume.fillAmount;
	}
	
	
	void LateUpdate()
	{
		if(!HealthbarPrefab)
			return;
		
		camDistance = Vector3.Dot(thisT.position - cam.transform.position, cam.transform.forward);

		if(lastHealth != player.Hp)
        {
            rate = Time.time + alphaStateMachineSetting.onHit.duration;
			lastHealth = player.Hp;

			if(showHealthInfo)
				healthInfo.text = player.Hp +" / "+player.HpMax;
			else
				healthInfo.text = "";
        }

        if (!OutDistance() && IsVisible())
        {
			if (player.Hp <= 0)
            {
                if (alphaStateMachineSetting.nullFadeSpeed > 0)
                {
                    if (backGround.fillAmount <= 0)
                        canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, alphaStateMachineSetting.nullAlpha, alphaStateMachineSetting.nullFadeSpeed);
                }
                else
                    canvasGroup.alpha = alphaStateMachineSetting.nullAlpha;
            }
			else if (player.Hp == player.HpMax)
                canvasGroup.alpha = alphaStateMachineSetting.fullFadeSpeed > 0 ? Mathf.MoveTowards(canvasGroup.alpha, alphaStateMachineSetting.fullAplpha, alphaStateMachineSetting.fullFadeSpeed) : alphaStateMachineSetting.fullAplpha;
            else
            {
                if (rate > Time.time)
                    canvasGroup.alpha = alphaStateMachineSetting.onHit.onHitAlpha;
                else
                    canvasGroup.alpha = alphaStateMachineSetting.onHit.fadeSpeed > 0 ? Mathf.MoveTowards(canvasGroup.alpha, alphaStateMachineSetting.defaultAlpha, alphaStateMachineSetting.onHit.fadeSpeed) : alphaStateMachineSetting.defaultAlpha;
            }
        }
        else
            canvasGroup.alpha = alphaStateMachineSetting.defaultFadeSpeed > 0 ? Mathf.MoveTowards(canvasGroup.alpha, 0, alphaStateMachineSetting.defaultFadeSpeed) : 0;

		
		if(player.Hp <= 0)
		{
			player.Hp = 0;
			canvasGroup.alpha = 0;
		}

		dist = keepSize ? camDistance / scale :  scale;

		HealthbarPrefab.sizeDelta = new Vector2 (healthbarSize.x/(dist-sizeOffsets.x/100), healthbarSize.y/(dist-sizeOffsets.y/100));
		
		healthInfo.rectTransform.sizeDelta = new Vector2 (HealthbarPrefab.sizeDelta.x * healthInfoSize/10, 
		                                                  HealthbarPrefab.sizeDelta.y * healthInfoSize/10);
		
		healthInfoPosition.y = HealthbarPrefab.sizeDelta.y + (healthInfo.rectTransform.sizeDelta.y - HealthbarPrefab.sizeDelta.y) / 2;
		
		if(healthInfoAlignment == HealthInfoAlignment.top)
			healthInfo.rectTransform.anchoredPosition = healthInfoPosition;
		else if (healthInfoAlignment == HealthInfoAlignment.center)
			healthInfo.rectTransform.anchoredPosition = Vector2.zero;
		else
			healthInfo.rectTransform.anchoredPosition = -healthInfoPosition;

		if(player.Hp > player.HpMax)
			player.Hp = player.HpMax;
	}

	void OnDestroy()
	{
		if( HealthbarPrefab )
		{
			Destroy( HealthbarPrefab.gameObject );
		}
	}

	bool IsVisible()
	{
		return canvas.pixelRect.Contains (HealthbarPrefab.position);
	}

    bool OutDistance()
	{
        return DrawOFFDistance == true && camDistance > drawDistance;
    }
}

[System.Serializable]
public class AlphaStateMachineSetting
{
    
    public float defaultAlpha = 0.7F;           //Default healthbar alpha (health is bigger then zero and not full);
    public float defaultFadeSpeed = 0.1F;
    public float fullAplpha = 1.0F;             //Healthbar alpha when health is full;
    public float fullFadeSpeed = 0.1F;
    public float nullAlpha = 0.0F;              //Healthbar alpha when health is zero or less;
    public float nullFadeSpeed = 0.1F;
    public OnHit onHit;                         //On hit StateMachineSetting
}

[System.Serializable]
public class OnHit
{
    public float fadeSpeed = 0.1F;              //Alpha state fade speed;
    public float onHitAlpha = 1.0F;             //On hit alpha;
    public float duration = 1.0F;               //Duration of alpha state;
}
