using UnityEngine;
using System.Collections;

public class TrafficCar : MonoBehaviour {

	private int 		m_WayPointNo;
	private Transform	m_Transform;
	//private bool		m_IsForward = true;
	public float 		Speed;

	public bool			IsCharacterInCrossing = false;
	private bool 		m_CharacterIsNearCrossing = false;

	private Quaternion  m_PreviousRot;
	private Quaternion  m_Rotation;

	public Transform 	NextWayPoint;
	private Vector3[]	m_WayPointPositions;
	private Vector3		m_NextPos;

	// Use this for initialization
	void Start () {

		if (name == "TrafficCar_1(Clone)") {
			m_WayPointPositions =  new Vector3[10];
			m_WayPointPositions [0] = new Vector3 (116.6f, 1f, 186.8f);
			m_WayPointPositions [1] = new Vector3 (-63.9f, 0f, 186.8f);
			m_WayPointPositions [2] = new Vector3 (-319.8f, 0f, 192.9f);
			m_WayPointPositions [3] = new Vector3 (-597f, 0f, 189.7f);
			m_WayPointPositions [4] = new Vector3 (-720f, 1f, 182.1f);
			m_WayPointPositions [5] = new Vector3 (-775.5f, 1f, 176.4f);
			m_WayPointPositions [6] = new Vector3 (-821.2f, 1f, 205.8f);
			m_WayPointPositions [7] = new Vector3 (-920f, 0f, 214f);
			m_WayPointPositions [8] = new Vector3 (-1055.3f, 1f, 220.1f);
			m_WayPointPositions [9] = new Vector3 (-1170.2f, 1f, 212.9f);
		}
		else if(name == "TrafficCar_2(Clone)") {

			m_WayPointPositions =  new Vector3[11];
			m_WayPointPositions [0] = new Vector3 (-1170.2f, 1f, 226.6f);
			m_WayPointPositions [1] = new Vector3 (-1111.4f, 1f, 233.1f);
			m_WayPointPositions [2] = new Vector3 (-1059.2f, 1f, 235.2f);
			m_WayPointPositions [3] = new Vector3 (-979f, 0f, 232.9f);
			m_WayPointPositions [4] = new Vector3 (-866.1f, 1f, 220.7f);
			m_WayPointPositions [5] = new Vector3 (-798.3f, 1f, 231f);
			m_WayPointPositions [6] = new Vector3 (-733.2f, 1f, 221.6f);
			m_WayPointPositions [7] = new Vector3 (-663.2f, 0f, 213.9f);
			m_WayPointPositions [8] = new Vector3 (-387f, 0f, 216.2f);
			m_WayPointPositions [9] = new Vector3 (-128.5f, 0f, 211.4f);
			m_WayPointPositions [10] = new Vector3 (112.9f, 1f, 210.8f);
		}

		NextWayPoint = (Transform)Instantiate (NextWayPoint);
			
		NextWayPoint.position = m_WayPointPositions [m_WayPointNo];

		m_Transform = transform;
	}
	
	// Update is called once per frame
	void Update () {

		if(IsCharacterInCrossing && m_CharacterIsNearCrossing)
		{
			return;
		}
		else
		{
			m_CharacterIsNearCrossing = false;
		}

		m_NextPos = new Vector3 (m_WayPointPositions [m_WayPointNo].x, m_Transform.position.y, m_WayPointPositions [m_WayPointNo].z);

		m_Transform.position = Vector3.MoveTowards(m_Transform.position, m_NextPos, Time.deltaTime * Speed);

		if(Vector3.Distance(m_Transform.position, m_NextPos) < 0.5f)
		{
			if(IsCharacterInCrossing && m_WayPointPositions[m_WayPointNo].y == 0)
			{
				m_CharacterIsNearCrossing = true;
				return;
			}

			m_WayPointNo++;

			if(m_WayPointNo >= m_WayPointPositions.Length)
			{
				m_WayPointNo = 1;
				NextWayPoint.position = m_WayPointPositions [m_WayPointNo];
				m_Transform.position = new Vector3(m_WayPointPositions[0].x, m_Transform.position.y, m_WayPointPositions[0].z);
			}
			else
			{
				NextWayPoint.position = m_WayPointPositions [m_WayPointNo];
				m_Transform.LookAt(NextWayPoint);
				m_Rotation = Quaternion.Euler(new Vector3(0, m_Transform.rotation.eulerAngles.y, 0)); 
				m_Transform.rotation = m_Rotation;
			}

		}
	}
}
