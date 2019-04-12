using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{

    public Team team = Team.TeamNone;
    public Transform target;
    public List<GameObject> listTarget = new List<GameObject>();
    private float timeFind;
    //private Health health;
    public void Start()
    {
        //health = this.gameObject.GetComponent<Health>();
    }
    public void FindTarget()
    {
        if (listTarget.Count > 0)
        {
            foreach (var _item in listTarget)
            {
                if (!_item) continue;
                target = _item.transform;
                break;
            }
            foreach (var _item in listTarget)
            {
                if (!_item) continue;
                if (!target)
                {
                    target = _item.transform;
                    continue;
                }
                var distanceOld = Vector3.Distance(transform.position, target.position);
                var distanceNew = Vector3.Distance(transform.position, _item.transform.position);
                if (distanceNew < distanceOld)
                {
                    target = _item.transform;
                }
            }
        }
    }
    public void SetListTarget(List<GameObject> _listTarget)
    {
        if (_listTarget != null)
            listTarget = _listTarget;
    }
    public void Update()
    {
        timeFind -= Time.deltaTime;
        if (timeFind<=0)
        {
            FindTarget();
            timeFind = 1;
        }        
    }
    public void SetTeam(Team _team)
    {
        team = _team;
        //health = this.gameObject.GetComponent<Health>();
        //if (health != null)
        //{
        //    if (team == Team.TeamRed) health.SetColor(Color.red);
        //    if (team == Team.TeamGreen) health.SetColor(Color.green);
        //}
    }

}
