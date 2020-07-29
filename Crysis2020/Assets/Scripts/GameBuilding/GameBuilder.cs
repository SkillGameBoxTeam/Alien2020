using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBuilder : Singleton<GameBuilder>
{
    [SerializeField] private float radiusCreate;

    [SerializeField] private GameObject money;
    [SerializeField] private int maxCountOfMoneyPoint;
    [SerializeField] private int startHealth = 10;
    [SerializeField] private int maxHealth = 20;
    private int maxEnemyHealth = 20;

    //[SerializeField] private int maxCountOfVirus = 30;

    //[SerializeField] private GameObject bat;
    //[SerializeField] private float timeOutForBat = 30f;
    //[SerializeField] private int maxCountOfBat = 1;
    private bool boolControlLevel = true;

    /// <summary>
    /// список уровней с сеттами
    /// </summary>
    [SerializeField] private List<Level> levels;
    private int currLevelNumber;
    private Level currLevel;

    private HashSet<GameObject> bases;
    private HashSet<GameObject> portables;
    private HashSet<GameObject> constObj;
    private HashSet<GameObject> otherObj;

    [SerializeField] private float timerToSlow = 0.1f;
    [SerializeField] private float timerToSlowKoeff = 0.1f;
    [SerializeField] private bool timerToSlowIsLerp = false;

    private string errorLog;

    [SerializeField] private GameObject hat;
    [SerializeField] private int currToHat = 2;

    //[SerializeField] private bool isStoryMode = true;


    /// <summary>
    /// Тип хеш-сета игры, для работы с созданием и уничтожением объектов.
    /// </summary>
    public enum TypeOfHashSet
    {
        bases,
        portables,
        nope,
        other
    }

    [SerializeField] private List<GameObject> cameras;

    private Dictionary<GameObject, GameObjectAutoMoveClass> moveableObjects;

    //private List<KeyValuePair<GameObject, GameObjectAutoMove>> moveableObjectsList;

    private Transform playerTransform;
    private UI_Control uI_Control;


    //gravity
    private HashSet<Rigidbody> affectedBodies = new HashSet<Rigidbody>();
    [SerializeField] private float gravityForce = 10f;
    //gravity-

    private SoundControl soundControl;

    public bool isFreeRun = false;

    private void Awake()
    {
        soundControl = SoundControl.Instance;
        uI_Control = UI_Control.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        SetCamera();
        //
        bases = new HashSet<GameObject>();
        portables = new HashSet<GameObject>();
        constObj = new HashSet<GameObject>();
        otherObj = new HashSet<GameObject>();

        moveableObjects = new Dictionary<GameObject, GameObjectAutoMoveClass>();

        playerTransform = PlayerControl.Instance.transform;
        

        //Работа с уровнями
        currLevel = GetStartLevel();

        BuildTheLevel();

        //работа с постоянными активностями
        for (int i = 0; i < maxCountOfMoneyPoint; i++)
        {
            CreateObjRndPOint(money,false,true);
        }
        
        GameStats.SetInGameMoneyPoint(maxCountOfMoneyPoint);

        //for (int i = 0; i < maxCountOfBat; i++)
        //{
        //    AddBat();
        //}
        uI_Control.ShowBoostButton(false);
        GameStats.freeRunPts = 0;
    }
    private void Update()
    {
        //Работа с уровнями
        ControlCurrentLevel();
        ControlBases();

        //работа с постоянными активностями
        ControlMoneyPoint();
        //ControlBat();

        //
        ControlMovableGameObjs();
        

    }

    private void FixedUpdate()
    {
        DoGravity();
        MoveObjects();
    }


    private Level GetStartLevel()
    {
        if (GameStats.StartLevel < levels.Count)
        {
            currLevelNumber = GameStats.StartLevel - 1;
        }
        else
        {
            currLevelNumber = levels.Count - 1;
        }

        return levels[currLevelNumber];
    }

    private void ControlMoneyPoint()
    {
        if (GameStats.GetInGameMoneyPoint() < maxCountOfMoneyPoint)
        {
            CreateObjRndPOint(money, true, true);
            GameStats.IncreaseInGameMoneyPoint();
        }
    }

    //private void ControlBat()
    //{
    //    if (GameStats.GetBatsInGame() < maxCountOfBat)
    //    {
    //        AddBat(timeOutForBat);
    //    }
    //}

    private void ControlCurrentLevel()
    {
        if (boolControlLevel && (GameStats.GetHealth() >= currLevel.healthPointsToWin)
            && (GameStats.GetEnemyHealth()>= currLevel.healthEnemyPointsToWin))
        {
            if (currLevelNumber + 1 >= levels.Count)
            {
                if (!isFreeRun)
                {
                    GameStats.acceptFreeRun = true;
                    DataControl.SaveData();
                    uI_Control.ShowWinMenu();
                }
                else
                {
                    boolControlLevel = false;
                }
                
            }
            else
            {
                currLevelNumber++;
                currLevel = levels[currLevelNumber];
                if (GameStats.openLevels< currLevelNumber)
                {
                    GameStats.openLevels = currLevelNumber;
                    DataControl.SaveData();
                }
                
                
                BuildTheLevel();
            }
        }
    }

    private void ClearObjHashSets()
    {
        foreach (GameObject item in bases)
        {
            Destroy(item);
        }
        bases.Clear();

        foreach (GameObject item in portables)
        {
            Destroy(item);
        }
        portables.Clear();

        foreach (GameObject item in constObj)
        {
            Destroy(item);
        }
        foreach (GameObject item in otherObj)
        {
            if (item)
            {
                Destroy(item);
            }
            
        }
        

        constObj.Clear();

        moveableObjects.Clear();

    }

    private void BuildTheLevel()
    {
        PlayerControl.Instance.MoveForceKoef = 1f;
        SetHpFromLevel();
        SetEnemyHpFromLevel();
        ClearObjHashSets();
        ControlConstantGameObjs();

        maxCountOfMoneyPoint = currLevel.maxMoneyInGame;

        if (!isFreeRun)
        {
            GameStats.SetСurrency();
            uI_Control.ShowDescribePanel(currLevel.description);
        } 
        
        uI_Control.RenewMoneyUI();

        uI_Control.RenewtHealthUI();

        InputParams.hitButton = true;
        soundControl.Win();
    }


    /// <summary>
    /// Создание движущихся объектов
    /// </summary>
    private void ControlMovableGameObjs()
    {

        if (moveableObjects.Count<currLevel.maxCountOfMovableObjs && currLevel.moveableObjs.Count > 0)
        {
            GameObjectAutoMove gobAM = currLevel.moveableObjs[Random.Range(0, currLevel.moveableObjs.Count)];

            GameObjectAutoMoveClass gobAMClass = new GameObjectAutoMoveClass(gobAM.gameObjectAutoMoveClass);

            GameObject gobj = CreateObjRndPOint(gobAMClass.gameObj, true);

            gobAMClass.rb = gobj.GetComponent<Rigidbody>();



            moveableObjects.Add(gobj, gobAMClass);
        }
        

    }

    /// <summary>
    /// Создание постоянных некотроллируемых в количестве объектов
    /// </summary>
    private void ControlConstantGameObjs()
    {
        foreach (ObjsToSpawnConstruct gset in currLevel.constantGameObjs)
        {
            if (gset.objToSpawn && gset.counts>0)
            {
                for (int i = 0; i < gset.counts; i++)
                {
                    CreateObjRndPOint(gset.objToSpawn, true, true);
                }
                
            }
        }
    }

    /// <summary>
    /// устанавливает максимальное здоровье уровня и текущее его знаечение
    /// </summary>
    private void SetHpFromLevel()
    {
        maxHealth = currLevel.healthPointsToWin;
        GameStats.SetHearts();
        uI_Control.RenewHearts();
        SetHealth(currLevel.startHealthPoints);
    }
    private void SetEnemyHpFromLevel()
    {
        maxEnemyHealth = currLevel.healthEnemyPointsToWin;
        SetEnemyHealth(currLevel.startEnemyHealthPoints);
    }


    private void ControlBases()
    {
        if (bases.Count < currLevel.maxBasesInGame)
        {
            //createBase
            GameActivitySet currGS = GetRndGameSet(currLevel.ActivitySets);
            
            GameObject curGO = currGS.GetBaseGameObj();

            if (curGO)
            {
                CreateObjRndPOint(curGO, false, true);
            }


            List<GameObject> curActis = currGS.GetActivities();
            if (curActis.Count>0)
            {
                for (int i = 0; i < currGS.countOfActivityToCreate; i++)
                {
                    GameObject currActs = GetRndListGO(curActis);
                    if (currActs)
                    {
                        CreateObjRndPOint(currActs, false, true);
                    }
                   
                }
            }
            
            
        }
    }

    /// <summary>
    /// СОздает указанный объект в случайной точке планеты
    /// </summary>
    /// <param name="curObj">префаб создания</param>
    /// <param name="count">количество точек по планете</param>
    public GameObject CreateObjRndPOint(GameObject curObj, bool rndRotate = false, bool erthIsParent = false)
    {
        Vector3 rndSurfPoint = transform.position + Random.onUnitSphere * radiusCreate;
        Quaternion rot = Quaternion.FromToRotation(curObj.transform.up, rndSurfPoint.normalized);

        GameObject gobj;
        if (erthIsParent)
        {
            gobj = Instantiate(curObj, rndSurfPoint, rot, transform);
        }
        else
        {
            gobj = Instantiate(curObj, rndSurfPoint, rot);
        }
        
            
        if (rndRotate)
        {
            if (Random.Range(0, 2) == 0)
            {
                gobj.transform.Rotate(0f, 90f, 0f, Space.Self);
            }

        }
        return gobj;
    }

    /// <summary>
    /// Создает объект под игроком
    /// </summary>
    /// <param name="curObj"></param>
    public void CreateObjUnderPlayer(GameObject curObj)
    {
        Quaternion rot = Quaternion.FromToRotation(curObj.transform.up, playerTransform.position.normalized);
        Instantiate(curObj, playerTransform.position, rot, transform);
    }

    //public void AddBat(float timer = 0)
    //{
    //    GameStats.IncreaseBat();
    //    if (timer > 0)
    //    {
    //        StartCoroutine(AddBatCoroutine(timer));
    //    }
    //    else
    //    {
    //        CreateObjRndPOint(bat);
    //    }
    //}
    //public void AddBatWithDefaultTimer()
    //{
    //    StartCoroutine(AddBatCoroutine(timeOutForBat));
    //}

    //IEnumerator AddBatCoroutine(float timer)
    //{
    //    yield return new WaitForSeconds(timer);
    //    CreateObjRndPOint(bat);

    //}

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetMaxEnemyHealth()
    {
        return maxEnemyHealth;
    }

    public void SetHealth(int hp = 1)
    {
        GameStats.SetHealth(hp);
        uI_Control.RenewtHealthUI();
    }
    public void IncreaseHealth(int hp=1)
    {
        GameStats.IncreaseHealth(hp);
        uI_Control.RenewtHealthUI();

        GameStats.freeRunPts += hp;
    }
    public void DecreaseHealth(int hp=1)
    {
        GameStats.DecreaseHearts();
        uI_Control.RenewHearts();

        //GameStats.DecreaseHealth(hp);
        uI_Control.RenewtHealthUI();
    }
    
    ///
    public void SetEnemyHealth(int hp = 1)
    {
        GameStats.SetEnemyHealth(hp);
        uI_Control.RenewEnemyUI();
    }
    public void IncreaseEnemyHealth(int hp = 1)
    {
        GameStats.IncreaseEnemyHealth(hp);
        uI_Control.RenewEnemyUI();

        GameStats.freeRunPts += hp;
    }
    public void DecreaseEnemyHealth(int hp = 1)
    {
        GameStats.DecreaseEnemyHealth(hp);
        uI_Control.RenewEnemyUI();
    }

    //


    public void IncreaseCurr(int curr=1)
    {
        GameStats.IncreaseСurr(curr);
        uI_Control.RenewMoneyUI();
        soundControl.Coin();

        //if (GameStats.GetСurrency()>currToHat)
        //{
        //    hat.SetActive(true);
        //}

        

    }
    public void DecreaseCurr(int curr=1)
    {
        GameStats.DecreaseСurr(curr);
        uI_Control.RenewMoneyUI();
        //if (GameStats.GetСurrency() < currToHat)
        //{
        //    hat.SetActive(false);
        //}
        
    }

    public void DecreaseInGameMoneyPoint()
    {
        GameStats.DecreaseInGameMoneyPoint();
    }

    //public bool AcceptSpawnVirus()
    //{
    //    if (GameStats.GetVirusCount() >= maxCountOfVirus)
    //    {
    //        return false;
    //    }
    //    return true;

    //}

    ///работа с уровнями
    public void AddBaseToHashSet(GameObject gObj)
    {
        bases.Add(gObj);
        GameStats.IncreaseBasesInGame();
    }
    public void DelBaseFromHashSet(GameObject gObj)
    {
        moveableObjects.Remove(gObj);
        bases.Remove(gObj);
        GameStats.DecreaseBasesInGame();
    }
    
    //
    public void AddConstObjToHashSet(GameObject gObj)
    {
        constObj.Add(gObj);

    }
    public void DelConstObjHashSet(GameObject gObj)
    {
        constObj.Remove(gObj);
        moveableObjects.Remove(gObj);

    }


    public void AddPortableToHashSet(GameObject gObj)
    {
        portables.Add(gObj);
        
    }
    public void DelPortableFromHashSet(GameObject gObj)
    {
        portables.Remove(gObj);
        moveableObjects.Remove(gObj);

    }
    public void AddOtherObjToHashSet(GameObject gObj)
    {
        otherObj.Add(gObj);

    }
    public void DelOtherObjFromHashSet(GameObject gObj)
        {
            otherObj.Remove(gObj);
            moveableObjects.Remove(gObj);

        }


    public void DelMoveableFromDict(GameObject gObj)
    {
        moveableObjects.Remove(gObj);

    }


    /// <summary>
    /// Возращает случайный объект из списка
    /// </summary>
    /// <param name="goList"></param>
    /// <returns></returns>
    private GameObject GetRndListGO(List<GameObject> goList) 
    {
        if (goList.Count == 1)
        {
            return goList[0];
        }
        else if (goList.Count > 1)
        {
            return goList[Random.Range(0, goList.Count)];
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// Возращает случайный GameActivitySet из списка
    /// </summary>
    /// <param name="goList"></param>
    /// <returns></returns>
    private GameActivitySet GetRndGameSet(List<GameActivitySet> goList)
    {
        if (goList.Count == 1)
        {
            return goList[0];
        }
        else if (goList.Count > 1)
        {
            return goList[Random.Range(0, goList.Count)];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// уничтожает объект и контролирует его нахождение в хеш-сете
    /// </summary>
    /// <param name="objToDestroy"></param>
    /// <param name="timerToDestroy"></param>
    /// <param name="hsType"></param>
    public void SetDestroyObj(GameObject objToDestroy, TypeOfHashSet hsType, float timerToDestroy = 0)
    {
        if (timerToDestroy>0)
        {
            StartCoroutine(SetDestroyCorut(objToDestroy, timerToDestroy, hsType));
        }
        else
        {
            SetDestroyLocal(objToDestroy, hsType);
        }
    }
    private IEnumerator SetDestroyCorut(GameObject objToDestroy, float timerToDestroy, TypeOfHashSet hsType)
    {
        yield return new WaitForSeconds(timerToDestroy);
        SetDestroyLocal(objToDestroy, hsType);

    }

    private void SetDestroyLocal(GameObject objToDestroy, TypeOfHashSet hsType)
    {
        if (objToDestroy)
        {
            if (hsType == TypeOfHashSet.bases)
            {
                DelBaseFromHashSet (objToDestroy);
            }
            else if (hsType == TypeOfHashSet.portables)
            {
                DelPortableFromHashSet(objToDestroy);
            }
            else if (hsType == TypeOfHashSet.other)
            {
                DelOtherObjFromHashSet(objToDestroy); 
            }
        
            DelMoveableFromDict(objToDestroy);

            Rigidbody rbToDel = objToDestroy.GetComponent<Rigidbody>();
            affectedBodies.Remove(rbToDel);
            GameStats.freeRunPts++; 
            Destroy(objToDestroy); 
        }
    }

    public void GameSlowDown(float timeToSlow = 1f, float koef = 0.5f, bool isLerp = false)
    {
        StartCoroutine(GameSlowDownCorut(timeToSlow, koef, isLerp));
    }
    
    private IEnumerator GameSlowDownCorut(float timeToSlow = 1f, float koef = 0.5f, bool isLerp = false)
    {
        if (isLerp)
        {

        }
        else
        {
            Time.timeScale = koef;
            yield return new WaitForSecondsRealtime(timeToSlow);
            Time.timeScale = 1f;
        }
        

    }

    public void GamePause()
    {
        Time.timeScale = 0f;
    }
    public void GameResume()
    {
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void SetCamera(int numCam = -1)
    {
        
        if (numCam>=0 && numCam < cameras.Count)
        {
            GameStats.SetCurrentCamera(numCam);
        }

        for (int i = 0; i < cameras.Count; i++)
        {
            if (i == GameStats.GetCurrentCamera())
            {
                cameras[i].SetActive(true);
            }
            else
            {
                cameras[i].SetActive(false);
            }
        }
    }

    private void MoveObjects()
    {
        foreach (KeyValuePair<GameObject, GameObjectAutoMoveClass> item in moveableObjects)
        {
            Moveobject(item.Key, item.Value);
        }
    }

    private void Moveobject(GameObject gobj, GameObjectAutoMoveClass gameObjectAutoMove)
    {
        if (gameObjectAutoMove.NoAutoMove == false)
        {
            Vector3 playerPosition = playerTransform.position;

            if (gameObjectAutoMove.typeAutoMove == GameObjectAutoMoveClass.TypeAutoMove.follow)
            {
                if (Vector3.Distance(gobj.transform.position, playerPosition) > gameObjectAutoMove.StopDistance)
                {
                    if (gameObjectAutoMove.generalToStep.GetTimeOut(gameObjectAutoMove.timeToStep, Time.fixedDeltaTime))
                    {
                        gobj.transform.LookAt(playerPosition, gobj.transform.up);
                    }
                    //Quaternion rotation = Quaternion.FromToRotation(-gobj.transform.up, transform.position - gobj.transform.position);
                    Quaternion rotation = Quaternion.FromToRotation(gobj.transform.up, gobj.transform.position.normalized);
                    gobj.transform.rotation = rotation * gobj.transform.rotation;



                    Vector3 down = Vector3.Project(gameObjectAutoMove.rb.velocity, gobj.transform.up);
                    Vector3 forward = gobj.transform.forward * gameObjectAutoMove.Speed * Time.fixedDeltaTime;
                    if (gameObjectAutoMove.rb)
                    {
                        gameObjectAutoMove.rb.velocity = down + forward;
                    }
                    

                }
                else
                {
                    gameObjectAutoMove.rb.angularVelocity -= gameObjectAutoMove.rb.angularVelocity;
                    gameObjectAutoMove.rb.velocity -= gameObjectAutoMove.rb.velocity;
                }
            }
            else if (gameObjectAutoMove.typeAutoMove == GameObjectAutoMoveClass.TypeAutoMove.rnddir)
            {
                //Quaternion rotation = Quaternion.FromToRotation(-gobj.transform.up, transform.position - gobj.transform.position);
                Quaternion rotation = Quaternion.FromToRotation(gobj.transform.up, gobj.transform.position.normalized);
                gobj.transform.rotation = rotation * gobj.transform.rotation;

                if (gameObjectAutoMove.generalToStep.GetTimeOut(gameObjectAutoMove.timeToStep, Time.fixedDeltaTime))
                {
                    
                    int rndQ = Random.Range(-1, 1);
                    if (rndQ == 0)
                    {
                        rndQ = 1;
                    }

                    Quaternion rotationAdd = Quaternion.FromToRotation(gobj.transform.forward, rndQ * gobj.transform.right);
                    gobj.transform.rotation = rotationAdd * gobj.transform.rotation;
                    //gobj.transform.Rotate(gobj.transform.up, rndQ * 90);
                }

                Vector3 down = Vector3.Project(gameObjectAutoMove.rb.velocity, gobj.transform.up);
                Vector3 forward = gobj.transform.forward * gameObjectAutoMove.Speed * Time.fixedDeltaTime;
                if (gameObjectAutoMove.rb)
                {
                    gameObjectAutoMove.rb.velocity = down + forward;
                }
                
            }
            else if (gameObjectAutoMove.typeAutoMove == GameObjectAutoMoveClass.TypeAutoMove.nope)
            {
                //Quaternion rotation = Quaternion.FromToRotation(-gobj.transform.up, transform.position - gobj.transform.position);
                Quaternion rotation = Quaternion.FromToRotation(gobj.transform.up, gobj.transform.position.normalized);
                gobj.transform.rotation = rotation * gobj.transform.rotation;
            }

            if (gameObjectAutoMove.jump)
            {
                if (gameObjectAutoMove.generalToJump.GetTimeOut(gameObjectAutoMove.timeToJump, Time.fixedDeltaTime))
                {
                    if (gameObjectAutoMove.rb)
                    {
                        gameObjectAutoMove.rb.AddForce(gobj.transform.up * gameObjectAutoMove.jumpForce, ForceMode.VelocityChange);
                    }
                    
                }
            }
        }
    }

    //gravity
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            affectedBodies.Add(other.attachedRigidbody);
        }
    }

    private void DoGravity()
    {
        foreach (Rigidbody body in affectedBodies)
        {
            if (body != null)
            {
                Vector3 forceDirection = (transform.position - body.position).normalized;
                body.AddForce(forceDirection * gravityForce* Time.fixedDeltaTime* body.mass, ForceMode.VelocityChange);
            }

        }
    }


    public void RetryLevel(bool fromStart = false)
    {
        if (fromStart)
        {
            currLevel = GetStartLevel();
        }
        BuildTheLevel();
    }
   

}
