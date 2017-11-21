using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	


    public List<CanvasManager.Action> ReadSaveData()
    {
        int counter = 0;
        string line;
        List<CanvasManager.Action> actionType = new List<CanvasManager.Action>();
        // Read the file and display it line by line.  
        System.IO.StreamReader file =
            new System.IO.StreamReader(@"Assets\Streaming Assets\SaveData.txt");
        while ((line = file.ReadLine()) != null)
        {
            if(line == "ATTACK")
            {
                actionType.Add(CanvasManager.Action.ATTACK);
            }

            if (line == "DODGE")
            {
                actionType.Add(CanvasManager.Action.DODGE);
            }
            if (line == "THINK")
            {
                actionType.Add(CanvasManager.Action.THINK);
            }
            if (line == "WATCH")
            {
                actionType.Add(CanvasManager.Action.WATCH);
            }

        }

        file.Close();
        System.Console.WriteLine("There were {0} lines.", counter);
        // Suspend the screen.  
        System.Console.ReadLine();
        return actionType;


    }


    public void WriteToFile()
    {
        string[] lines =  new string[CanvasManager.Instance.Action_List.Count];

        for(int i=0;i<CanvasManager.Instance.Action_List.Count;i++)
        {
            lines[i] = CanvasManager.Instance.Action_List[i].ToString();
        }

        System.IO.File.WriteAllLines(@"Assets\Streaming Assets\SaveData.txt", lines);

    }


	// Update is called once per frame
	void Update () {
      


    }
}
